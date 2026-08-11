using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Newtonsoft.Json;
using Facebook.Api;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Xml;

namespace Facebook
{
    /// <summary>Makes a request to a Facebook API method.</summary>
    [Serializable]
    [XmlRoot("facebookRequest")]    
    public class FacebookRequest
    {
        /// <summary>The URL of the Facebook API REST application.</summary>
        internal const String FACEBOOK_REST_URL = "https://api.facebook.com/restserver.php";

        /// <summary>Intializes an instance of <see cref="FacebookRequest" /> for the specified <paramref name="methodName"/>.</summary>
        /// <param name="facebookContext">A reference to the <see cref="IFacebookContext" /> object the request is being made for.</param>
        /// <param name="methodName">The name of the API method.</param>
        /// <param name="args">A <see cref="IDictionary{String,Object}" /> object containing arguments to be passed with the method call.</param>
        /// <param name="excludedArgs">An array of <see cref="String" /> values specifying arguments that should be left out of the API method call.</param>
        public FacebookRequest(IFacebookContext facebookContext, String methodName, IDictionary<String, Object> args, params String[] excludedArgs)
            : this(methodName, facebookContext.Session, args, excludedArgs)
        {
            this.Context = facebookContext;
        }

        internal FacebookRequest(String methodName, ISessionInfo session, IDictionary<String, Object> args, String[] excludedArgs)
        {
            this.MethodName = methodName;
            this.Args = new XmlSerializableDictionary<String, Object>(args);
            this.ExcludedArgs = excludedArgs;
            this.Session = session;
        }

        /// <summary>Gets a reference to the <see cref="IFacebookContext" /> the request if being made for.</summary>
        [XmlIgnore]
        public IFacebookContext Context { get; internal set; }

        /// <summary>Gets the name of the method being called.</summary>
        [XmlAttribute("method")]
        public String MethodName { get; internal set; }

        /// <summary>Gets a dictionary of arguments that will be inluded in the request.</summary>
        [XmlElement("args")]
        public IXmlSerializableDictionary<String, Object> Args { get; internal set; }

        /// <summary>Gets or sets the array of <see cref="String" /> values specifying arguments that should be left out of the API method call.</summary>
        [XmlArray("excludedArgs")]
        public String[] ExcludedArgs { get; set; }

        [XmlElement("session")]
        public ISessionInfo Session { get; set; }

        /// <summary>Executes the request.</summary>
        /// <typeparam name="TValue">The expected return type of the method call.</typeparam>
        /// <param name="timeout">The amount of time, in milliseconds, the request will be active before timing out.</param>
        /// <returns>A reference to a <see cref="FacebookResponse{TValue}" /> object that will contain the value of the response.</returns>
        internal FacebookResponse<TValue> Execute<TValue>(Int32 timeout)
        {
            try
            {
                var request = this.CreateWebRequest(timeout);
                var response = (HttpWebResponse)request.GetResponse();

                return FacebookRequest.ProcessResponse<TValue>(response);
            }
            catch (Exception ex) { return new FacebookResponse<TValue>(ex); }
        }

        /// <summary>Creates an <see cref="HttpWebRequest" /> object for the request.</summary>
        /// <param name="timeout">The amount of time, in milliseconds, the request will be active before timing out.</param>
        /// <returns>An <see cref="HttpWebRequest" /> object for the request.</returns>
        internal HttpWebRequest CreateWebRequest(Int32 timeout)
        {
            var content = this.GenerateRequestContent();
            var request = (HttpWebRequest)WebRequest.Create(this.GetRestUrl());
            request.Timeout = timeout;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = content.Length;

            using (var stream = request.GetRequestStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(content.ToString());
                    writer.Close();
                }
            }

            return request;
        }

        /// <summary>Generates a <see cref="String" /> containing the content stream for the request.</summary>
        /// <returns>A <see cref="String" /> containing the content stream for the request.</returns>
        internal String GenerateRequestContent()
        {
            var args = new XmlSerializableDictionary<String, Object>(this.Args ?? new XmlSerializableDictionary<String, Object>());

            // values in FacebookArgs need to be merged into the args we'll be using the generate the request
            var fbArgs = args.Where(arg => arg.Value.GetType().IsSubclassOf(typeof(FacebookArgs))).ToArray();
            foreach (var fbarg in fbArgs)
            {
                args.Remove(fbarg.Key);
                var fbargResults = ((FacebookArgs)fbarg.Value).GetArgs();
                foreach (var fbargResult in fbargResults)
                {
                    args.Add(fbargResult.Key, fbargResult.Value);
                }
            }

            // remove anything that has been defined as excluded
            foreach (var excluded in this.ExcludedArgs) args.Remove(excluded);

            // add all the standard required parameters
            args.Add("method", this.MethodName);
            args.Add("api_key", this.Session == null ? this.Context.ApiKey : this.Session.ApiKey);
            args.Add("v", this.Context.Version);

            if (this.Context.HasSession && !this.ExcludedArgs.Contains("session_key"))
            {
                //args.Add("ss", "1");
                args.Add("session_key", this.Session.SessionKey);
                args.Add("call_id", DateTime.Now.Ticks);
            }
            else
            {
                args.Add("ss", "0");
                args.Add("session_key", String.Empty);
                args.Add("call_id", String.Empty);
            }

            // perform any processing or serialization on non-primitive parameter types
            var processedArgs = new Dictionary<String, Object>();
            foreach (var arg in args)
            {
                Type type = null;
                Object value = null;
                if (arg.Value != null)
                {
                    type = arg.Value.GetType();

                    // strings and primitive types don't need special handling
                    if (type == typeof(String) || type.IsPrimitive) value = arg.Value;

                    else if (type.IsGenericType && type.GetInterfaces().Contains(typeof(IEnumerable)))
                    {
                        var typeParams = type.GetGenericArguments();

                        // enumerables of primitive types are serialized to comma-separated lists (note: not strings this time, they should be json serialized)
                        if (typeParams.Length == 1 && typeParams[0].IsPrimitive) value = ((IEnumerable)arg.Value).ToDelimitedString(',');
                    }
                }

                // everything else gets JSON serialized
                if (value == null) value = JsonConvert.SerializeObject(arg.Value);

                processedArgs.Add(arg.Key, value);
            }

            var sig = FacebookRequest.ComputeSignature(this.GetSecret(), processedArgs);
            processedArgs.Add("sig", sig);

            // generate and return the form POST content
            var content = new StringBuilder();
            foreach (var processedArg in processedArgs)
            {
                content.AppendFormat("{0}{1}={2}", content.Length > 0 ? "&" : String.Empty, processedArg.Key, HttpUtility.UrlEncode(processedArg.Value.ToString()));
            }
            return content.ToString();
        }

        /// <summary>Computes the signature of a request passing the specified <paramref name="arguments"/>.</summary>
        /// <param name="secret">The application secret.</param>
        /// <param name="arguments">The list of arguments being passed into the method request.</param>
        /// <returns>A <see cref="String" /> containing a request signature generated according to the documentation provided by Facebook in the article <a href="http://wiki.developers.facebook.com/index.php/How_Facebook_Authenticates_Your_Application">How Facebook Authenticates Your Application</a>.</returns>
        internal static String ComputeSignature(String secret, IDictionary<String, Object> arguments)
        {
            var sortedArgs = arguments.OrderBy(arg => arg.Key);

            var seedBuilder = new StringBuilder();
            foreach (var arg in sortedArgs)
            {
                Object value = null;
                IEnumerable col = null;
                if (arg.Value.TryMakeEnumerable(out col)) value = col.ToDelimitedString(',');
                else value = arg.Value;

                seedBuilder.AppendFormat("{0}={1}", arg.Key, value);
            }
            seedBuilder.Append(secret);

            var seed = seedBuilder.ToString();
            var seedBytes = Encoding.ASCII.GetBytes(seed);
            var sig = MD5.Create().ComputeHashString(seed);

            return sig.ToLower();
        }

        /// <summary>Processes the <see cref="HttpWebResponse" /> object generated by the request and returns a <see cref="FacebookResponse{TValue}" /> object containing the result.</summary>
        /// <typeparam name="TValue">The expected return type of the method call.</typeparam>
        /// <param name="response">A <see cref="HttpWebResponse" /> object.</param>
        /// <returns>A <see cref="FacebookResponse{TValue}" /> object containing the result of the API method call.</returns>
        internal static FacebookResponse<TValue> ProcessResponse<TValue>(HttpWebResponse response)
        {
            String responseContent = null;

            using (var stream = response.GetResponseStream())
            {
                using (var reader = new StreamReader(stream))
                {
                    responseContent = reader.ReadToEnd();
                    reader.Close();
                }
            }

            return new FacebookResponse<TValue>(responseContent);
        }

        internal String GetRestUrl()
        {
            if (this.Context.ApplicationType == ApplicationType.Website) return FACEBOOK_REST_URL;
            else if (Configuration.FacebookSectionGroup.HasDesktopSection)
            {
                var authConfig = Configuration.FacebookSectionGroup.Desktop.Authentication.GetByApiKey(this.Context.ApiKey);
                if (authConfig == null) return FACEBOOK_REST_URL;
                else if (this.MethodName == "Auth.createToken")
                {
                    Uri uri;
                    if (Uri.TryCreate(authConfig.AuthUrl, authConfig.CreateTokenUrl, out uri)) return uri.ToString();
                    else return FACEBOOK_REST_URL;
                }
                else if (this.MethodName == "Auth.getSession")
                {
                    Uri uri;
                    if (Uri.TryCreate(authConfig.AuthUrl, authConfig.GetSessionUrl, out uri)) return uri.ToString();
                    else return FACEBOOK_REST_URL;
                }
                else return FACEBOOK_REST_URL;
            }
            else return FACEBOOK_REST_URL;
        }

        /// <summary>Returns the appropriate secret depending on the type of application, the state of the user's session and the method being called.</summary>
        internal String GetSecret()
        {
            if (this.Context != null && this.Context.ApplicationType == ApplicationType.Website) return this.Context.Secret;
            else if (this.Context != null && (!this.Context.HasSession || this.MethodName == "Auth.createToken" || this.MethodName == "Auth.getSession"))
            {
                return this.Context.Secret;
            }
            else return this.Session.Secret;
        }
    }
}