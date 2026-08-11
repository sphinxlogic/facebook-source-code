using System;
using System.Collections.Generic;
using System.Linq;
using Facebook.Api;
using System.Reflection;
using System.Text;

namespace Facebook
{
    public class ObjectCache
    {
        public ObjectCache(IFacebookContext context)
        {
            this.Context = context;
        }

        private IFacebookContext Context { get; set; }

        public void Add(CacheableAttribute attribute, IFacebookObject value)
        {
            this.InitAllProperties(ref value);

            var bucketKey = this.GetKey(attribute.BucketName, this.GetKeyValue(attribute.KeyProperties, value));
            foreach (var property in value.InnerDictionary)
            {
                var propKey = String.Format("{0}__{1}", bucketKey, property.Key);
                this.Context.Cache.Add(propKey, property.Value, CacheScope.MaxDuration);
            }
        }

        private void InitAllProperties(ref IFacebookObject value)
        {
            var type = value.GetType();
            var properties = type.GetCachedMembers().Where(m => m.MemberType == MemberTypes.Property);
            foreach (var property in properties.OfType<PropertyInfo>())
            {
                property.GetValue(value, null);
            }
        }

        private String GetKeyValue(String[] keyProperties, IFacebookObject value)
        {
            var key = new StringBuilder(value.InnerDictionary[keyProperties[0]].ToString());
            for (var index = 1; index < keyProperties.Length; index++) key.AppendFormat("___{0}", value.InnerDictionary[keyProperties[index]]);
            return key.ToString();
        }

        public Boolean TryGet(String bucketName, String key, String[] properties, out IDictionary<String, Object> result)
        {
            var bucketKey = this.GetKey(bucketName, key);
            result = new Dictionary<String, Object>();
            foreach (var property in properties)
            {
                var propKey = String.Format("{0}__{1}", property);
                if (this.Context.Cache.ContainsKey(propKey)) result.Add(property, this.Context.Cache[propKey]);
            }

            return properties.Length == result.Count;
        }

        private String GetKey(String bucketName, String key)
        {
            return String.Format("__ObjectCache__{0}__{1}", bucketName, key);
        }
    }
}