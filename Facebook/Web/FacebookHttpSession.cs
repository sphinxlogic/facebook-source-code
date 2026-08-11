using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using Facebook.Api;
using System.Xml.Serialization;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Facebook.Web
{
    /// <summary>Provides access to session-state values as well as session-level data about the HTTP request.</summary>
    /// <remarks>The <see cref="FacebookHttpSession" /> serves the same purpose for web applications as <see cref="SessionInfo" /> does for
    /// desktop applications, and it can also be used to manage session state for FBML web applications. For FBML web applications,
    /// using the customary <see cref="System.Web.SessionState.HttpSessionState" /> object is not an option since the ASP.NET session will not be persisted between
    /// requests.</remarks>
    [Serializable]
    public class FacebookHttpSession : SessionInfo, ISessionInfo, IDictionary<String, Object>, IDictionary, IList<KeyValuePair<String, Object>>, IList, ICollection
    {
        public FacebookHttpSession() { }

        /// <summary>Initializes an instance of <see cref="FacebookHttpRequest" /> using data from the specified <see cref="HttpContext" /> object.</summary>
        /// <param name="httpContext">A reference to the <see cref="HttpContext" /> object for the current HTTP request.</param>
        public FacebookHttpSession(HttpContext httpContext)
        {
            this.HttpContext = httpContext;
            this.Merge(httpContext);
        }

		internal FacebookHttpSession(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

        private HttpContext HttpContext { get; set; }

        /// <summary>Initializes an instance of <see cref="FacebookHttpRequest" /> using data from the specified <see cref="IDictionary{String, Object}" /> object.</summary>
        /// <remarks><para>This constructor supports <see cref="IFacebookObject" />.</para></remarks>
        protected internal FacebookHttpSession(IDictionary<String, Object> dictionary)
            : base(dictionary) { }

        /// <summary>Gets a user-specific key used to store and retrieve <see cref="FacebookHttpSession" /> instances.</summary>
        internal String SessionCacheKey { get; private set; }

        /// <summary>Gets a user-specific key used to store and retrieve <see cref="FacebookHttpSession" /> instances.</summary>
        internal static String GetSessionCacheKey(HttpContext httpContext)
        {
            return String.Format("__FBAPI__SESSION__{0}", httpContext.Session.SessionID);
        }

        /// <summary>A static initializer for <see cref="FacebookHttpSession" />.</summary>
        /// <param name="context">A reference to the <see cref="FacebookHttpContext" /> object for the current HTTP request.</param>
        public static FacebookHttpSession Init(FacebookHttpContext context)
        {
            var sessionCacheKey = FacebookHttpSession.GetSessionCacheKey(context.HttpContext);
            FacebookHttpSession session = null;
            if (context.HasPersistedFacebookSession())
            {
                session = (FacebookHttpSession)context.SessionCache[sessionCacheKey];
                session.Merge(context.HttpContext);
            }
            else
            {
                session = new FacebookHttpSession(context.HttpContext);
                session.SessionCacheKey = sessionCacheKey;
            }
            session.HttpContext = context.HttpContext;

            return session;
        }

        public void Persist()
        {
            var context = FacebookHttpContext.GetCurrent(this.HttpContext);
            var sessionCacheKey = FacebookHttpSession.GetSessionCacheKey(this.HttpContext);
            context.SessionCache[sessionCacheKey] = this;
        }

        /// <summary>Copies data from the specified <see cref="HttpContext" /> object into the <see cref="FacebookHttpSession" /> object.</summary>
        /// <param name="context">A reference to the <see cref="HttpContext" /> object for the current HTTP request.</param>
        /// <remarks><para>This method allows new data to be updated in the <see cref="FacebookHttpSession" /> object without overwriting existing
        /// values that do not exist in <paramref name="context" />.</para></remarks>
        public void Merge(HttpContext context)
        {
            Byte added;
            if (Byte.TryParse(context.Request.Params["fb_sig_added"], out added)) this.Added = added == 1;

            UnixDateTime expires;
            if (context.Request.Params["fb_sig_expires"] == "0") this.Expires = DateTime.MaxValue;
            else if (UnixDateTime.TryParse(context.Request.Params["fb_sig_expires"], out expires)) this.Expires = expires.ToDateTime();

            String apiKey = context.Request.Params["fb_sig_api_key"];
            if (!String.IsNullOrEmpty(apiKey)) this.ApiKey = apiKey;

            String sessionKey = context.Request.Params["fb_sig_session_key"];
            if (!String.IsNullOrEmpty(sessionKey)) this.SessionKey = sessionKey;

            String strFriends = context.Request.Params["fb_sig_friends"];
            if (!String.IsNullOrEmpty(strFriends)) this.Friends = new List<Int64>(
                 from strFriend in strFriends.Split(',')
                 select Int64.Parse(strFriend));

            String locale = context.Request.Params["fb_sig_locale"];
            if (!String.IsNullOrEmpty(locale)) this.Locale = locale;

            Int64 uid;
            if (!Int64.TryParse(context.Request.Params["fb_sig_user"], out uid))
                if (!Int64.TryParse(context.Request.Params["fb_sig_canvas_user"], out uid))
                    Int64.TryParse(context.Request.Params["fb_sig_profile_user"], out uid);
            if (uid > 0) this.Uid = uid;

            String strLinkedAccounts = context.Request.Params["fg_sig_linked_account_ids"];
            if (!String.IsNullOrEmpty(strLinkedAccounts)) this.Friends = new List<Int64>(
                 from strLinkedAccount in strLinkedAccounts.Split(',')
                 select Int64.Parse(strLinkedAccounts));

            UnixDateTime lastProfileUpdate;
            if (UnixDateTime.TryParse(context.Request.Params["fb_sig_profile_update_time"], out lastProfileUpdate)) this.LastProfileUpdate = lastProfileUpdate.ToDateTime();

            String extPerms = context.Request.Params["fb_sig_ext_perms"];
            if (!String.IsNullOrEmpty(extPerms)) this.ExtendedPermissions = FieldName.FieldsToValues<ExtendedPermission>(extPerms);
        }

        public void MergeConnect(HttpContext context, String apiKey)
        {
            var requestParams = context.Request.Params;

            String user = null;
            String ss = null;
            String sessionKey = null;
            String expires = null;
            String baseDomain = null;

            if (requestParams[apiKey] != null)
            {
                // grab values
                user = requestParams[apiKey + "_user"];
                ss = requestParams[apiKey + "_ss"];
                sessionKey = requestParams[apiKey + "_session_key"];
                expires = requestParams[apiKey + "_expires"];
                baseDomain = requestParams["base_domain_" + apiKey];
            }
            else if (requestParams["session"] != null)
            {
                
            }

            // do all the necessary value exist? some may have expired, requiring login by user again
            if (user != null
                && ss != null
                && sessionKey != null
                && expires != null)
            {
                // store these values 
                this.ApiKey = apiKey;
                this.Uid = Int64.Parse(user);
                this.SessionKey = sessionKey;
                this.Secret = ss;
                this.Expires = expires == "0" ? DateTime.MaxValue : UnixDateTime.ToDateTime(Double.Parse(expires));
            }
        }

        /// <summary>Gets or sets a value representing whether the user for the current HTTP request has added the application.</summary>
        [XmlElement("added")]
        public Boolean Added
        {
            get { return this.GetValueType<Boolean>("added"); }
            set { this.InnerDictionary["added"] = value; }
        }

        /// <summary>Gets or sets the list of friend <c>UID</c>s of the visiting user included in the current HTTP request.</summary>
        [XmlElement("friends")]
        public List<Int64> Friends
        {
            get { return this.GetValueTypeCollection<Int64>("friends", "friend"); }
            set { this.InnerDictionary["friends"] = value; }
        }

        /// <summary>Gets or sets the user's locale.</summary>
        [XmlElement("locale")]
        public String Locale
        {
            get { return this.GetString("locale"); }
            set { this.InnerDictionary["locale"] = value; }
        }

        /// <summary>Gets or sets a list of <c>account_id</c> values that match the user's email hash that were previously sent to Facebook using <see cref="Facebook.Api.Controllers.ConnectController.RegisterUsers" />.</summary>
        [XmlElement("linkedAccountIds")]
        public List<Int64> LinkedAccountIds
        {
            get { return this.GetValueTypeCollection<Int64>("linked_account_ids", "linked_account_id"); }
            set { this.InnerDictionary["linked_account_ids"] = value; }
        }

        /// <summary>Gets or sets the date/time that the user's profile was last updated.</summary>
        [XmlElement("last_profile_update")]
        public DateTime LastProfileUpdate
        {
            get { return this.GetValueType<DateTime>("last_profile_update"); }
            set { this.InnerDictionary["last_profile_update"] = value; }
        }

        /// <summary>Gets or sets a list of any extended permissions that the user has granted the application.</summary>
        [XmlElement("extended_permissions")]
        public ExtendedPermission ExtendedPermissions
        {
            get { return this.GetValueType<ExtendedPermission>("extended_permissions"); }
            set { this.InnerDictionary["extended_permissions"] = value; }
        }

        private const String SESSION_PREFIX = "__SESSION__";
        private String GetInternalKey(String key)
        {
            return SESSION_PREFIX + key;
        }

        #region [ IDictionary<String, Object> Members ]
        /// <summary>Adds the specified <paramref name="value" /> to the session state using the specified <paramref name="key" />.</summary>
        /// <param name="key">The key used to reference the session state item.</param>
        /// <param name="value">The object to be added to the session state.</param>
        public void Add(String key, Object value)
        {
            this.InnerDictionary.Add(this.GetInternalKey(key), value);
        }

        /// <summary>Gets a <see cref="Boolean" /> value representing whether an item with the specified <paramref name="key" /> is contained in the session state.</summary>
        /// <param name="key">The key used to reference the session state item.</param>
        /// <returns><c>true</c> if the session state contains an item identified by the specified <paramref name="key"/>; otherwise, <c>false</c>.</returns>
        public Boolean ContainsKey(String key)
        {
            return this.InnerDictionary.ContainsKey(this.GetInternalKey(key));
        }

        /// <summary>Gets a collection of keys contained in the session state.</summary>
        public ICollection<String> Keys
        {
            get { return this.InnerDictionary.Keys.Where(key => key.StartsWith(SESSION_PREFIX)).ToList(); }
        }

        /// <summary>Removes the specified item from the session state.</summary>
        /// <param name="key">The key used to reference the session state item.</param>
        /// <returns><c>true</c> if an item was removed; otheriwse, <c>false</c>.</returns>
        public Boolean Remove(String key)
        {
            return this.InnerDictionary.Remove(this.GetInternalKey(key));
        }

        /// <summary>Gets the session state item associated with the specified <paramref name="key" />.</summary>
        /// <param name="key">The key used to reference the session state item.</param>
        /// <param name="value">When this method returns, the session state item associated with the specified <paramref name="key" /> if the key is found; otherwise, <c>null</c>.</param>
        /// <returns><c>true</c> if a session state item associated with the specified <paramref name="key" /> is found; otherwise, <c>false</c>.</returns>
        public Boolean TryGetValue(String key, out Object value)
        {
            return this.InnerDictionary.TryGetValue(this.GetInternalKey(key), out value);
        }

        /// <summary>Gets a collection of session state items contained in the session state.</summary>
        public ICollection<Object> Values
        {
            get { return this.InnerDictionary.Where(kvp => kvp.Key.StartsWith(SESSION_PREFIX)).Select(kvp => kvp.Value).ToList(); }
        }

        /// <summary>Gets or sets the session state item at the specified key.</summary>
        /// <param name="key">The key used to reference the session state item.</param>
        /// <returns>The specified session state item.</returns>
        public Object this[String key]
        {
            get
            {
                Object value;
                this.TryGetValue(key, out value);
                return value;
            }
            set { this.InnerDictionary[this.GetInternalKey(key)] = value; }
        }

        #endregion

        #region [ ICollection<KeyValuePair<String, Object>> Members ]

        void ICollection<KeyValuePair<String, Object>>.Add(KeyValuePair<String, Object> item)
        {
            this.Add(item.Key, item.Value);
        }

        /// <summary>Removes all items from the session state.</summary>
        public override void Clear()
        {
            DateTime expired = DateTime.UtcNow.AddYears(-1);
            var cookieKeys = (from key in this.HttpContext.Request.Cookies.AllKeys
                              where key.Contains(this.ApiKey)
                              select key).ToList();
            foreach (String key in cookieKeys)
            {
                HttpContext.Current.Response.Cookies.Remove(key);
                HttpContext.Current.Request.Cookies.Remove(key);
            }            
        }

        Boolean ICollection<KeyValuePair<String, Object>>.Contains(KeyValuePair<String, Object> item)
        {
            return this.InnerDictionary.Contains(new KeyValuePair<String, Object>(this.GetInternalKey(item.Key), item.Value));
        }

        void ICollection<KeyValuePair<String, Object>>.CopyTo(KeyValuePair<String, Object>[] array, Int32 arrayIndex)
        {
            this.InnerDictionary.Where(kvp => kvp.Key.StartsWith(SESSION_PREFIX)).ToArray().CopyTo(array, arrayIndex);
        }

        /// <summary>Gets the number of items stored in the session state.</summary>
        public Int32 Count
        {
            get { return this.InnerDictionary.Keys.Where(key => key.StartsWith(SESSION_PREFIX)).Count(); }
        }

        Boolean ICollection<KeyValuePair<String, Object>>.IsReadOnly
        {
            get { return false; }
        }

        Boolean ICollection<KeyValuePair<String, Object>>.Remove(KeyValuePair<String, Object> item)
        {
            return this.InnerDictionary.Remove(item);
        }

        #endregion

        #region [ IEnumerable<KeyValuePair<string,object>> Members ]

        IEnumerator<KeyValuePair<String, Object>> IEnumerable<KeyValuePair<String, Object>>.GetEnumerator()
        {
            return this.InnerDictionary.Where(kvp => kvp.Key.StartsWith(SESSION_PREFIX)).GetEnumerator();
        }

        #endregion

        #region [ IEnumerable Members ]

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.InnerDictionary.Where(kvp => kvp.Key.StartsWith(SESSION_PREFIX)).GetEnumerator();
        }

        #endregion

        #region [ IList<KeyValuePair<String,Object>> Members ]

        Int32 IList<KeyValuePair<String, Object>>.IndexOf(KeyValuePair<String, Object> item)
        {
            return ((IList<KeyValuePair<String, Object>>)this.InnerDictionary).IndexOf(item);
        }

        void IList<KeyValuePair<String, Object>>.Insert(Int32 index, KeyValuePair<String, Object> item)
        {
            ((IList<KeyValuePair<String, Object>>)this.InnerDictionary).Insert(index, item);
        }

        void IList<KeyValuePair<String, Object>>.RemoveAt(Int32 index)
        {
            ((IList<KeyValuePair<String, Object>>)this.InnerDictionary).RemoveAt(index);
        }

        KeyValuePair<String, Object> IList<KeyValuePair<String, Object>>.this[Int32 index]
        {
            get { return ((IList<KeyValuePair<String, Object>>)this.InnerDictionary)[index]; }
            set { ((IList<KeyValuePair<String, Object>>)this.InnerDictionary)[index] = value; }
        }

        #endregion

        #region [ ICollection<KeyValuePair<String,Object>> Members ]


        void ICollection<KeyValuePair<String, Object>>.Clear()
        {
            this.Clear();
        }

        Int32 ICollection<KeyValuePair<String, Object>>.Count
        {
            get { return this.Count; }
        }

        #endregion

        #region [ IList Members ]

        Int32 IList.Add(Object value)
        {
            var kvp = (KeyValuePair<String, Object>)value;
            this.Add(kvp.Key, kvp.Value);
            return this.Count;
        }

        Boolean IList.Contains(Object value)
        {
            var kvp = (KeyValuePair<String, Object>)value;
            return this.Contains(kvp);
        }

        Int32 IList.IndexOf(Object value)
        {
            return ((IList)this.InnerDictionary).IndexOf(value);
        }

        void IList.Insert(Int32 index, Object value)
        {
            ((IList)this.InnerDictionary).Insert(index, value);
        }

        Boolean IList.IsFixedSize
        {
            get { return false; }
        }

        Boolean IList.IsReadOnly
        {
            get { return false; }
        }

        void IList.Remove(Object value)
        {
            ((IList)this.InnerDictionary).Remove(value);
        }

        void IList.RemoveAt(Int32 index)
        {
            ((IList)this.InnerDictionary).RemoveAt(index);
        }

        Object IList.this[Int32 index]
        {
            get { return ((IList)this.InnerDictionary)[index]; }
            set { ((IList)this.InnerDictionary)[index] = value; }
        }

        #endregion

        #region [ ICollection Members ]

        void ICollection.CopyTo(Array array, Int32 index)
        {
            ((ICollection)this.InnerDictionary).CopyTo(array, index);
        }

        Int32 ICollection.Count
        {
            get { return this.Count; }
        }

        Boolean ICollection.IsSynchronized
        {
            get { return ((ICollection)this.InnerDictionary).IsSynchronized; }
        }

        Object ICollection.SyncRoot
        {
            get { return ((ICollection)this.InnerDictionary).SyncRoot; }
        }

        #endregion

        #region [ IDictionary Members ]

        void IDictionary.Add(Object key, Object value)
        {
            this.Add((String)key, value);
        }

        void IDictionary.Clear()
        {
            this.Clear();
        }

        Boolean IDictionary.Contains(Object key)
        {
            return this.InnerDictionary.ContainsKey((String)key);
        }

        IDictionaryEnumerator IDictionary.GetEnumerator()
        {
            return ((IDictionary)this.InnerDictionary).GetEnumerator();
        }

        Boolean IDictionary.IsFixedSize
        {
            get { return false; }
        }

        Boolean IDictionary.IsReadOnly
        {
            get { return false; }
        }

        ICollection IDictionary.Keys
        {
            get { return (ICollection)this.InnerDictionary.Keys; }
        }

        void IDictionary.Remove(Object key)
        {
            this.Remove((String)key);
        }

        ICollection IDictionary.Values
        {
            get { return (ICollection)this.InnerDictionary.Values; }
        }

        Object IDictionary.this[Object key]
        {
            get { return this[(String)key]; }
            set { this[(String)key] = value; }
        }

        #endregion
    }
}