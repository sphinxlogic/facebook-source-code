using System;

namespace Facebook.Api
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class CacheableAttribute : Attribute
    {
        public CacheableAttribute(String bucketName, params String[] keyProperties)
        {
            this.BucketName = bucketName;
            this.KeyProperties = keyProperties;            
        }

        public String BucketName { get; set; }

        public String[] KeyProperties { get; set; }

        public String GetKey()
        {
            return this.KeyProperties.GetDelimitedString("___");
        }
    }
}