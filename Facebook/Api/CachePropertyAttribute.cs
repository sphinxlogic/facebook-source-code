using System;

namespace Facebook.Api
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class CachePropertyAttribute : Attribute
    {
        public CachePropertyAttribute()
        {
            this.Scope = CacheScope.MaxDuration;
        }

        public CachePropertyAttribute(String bucketName, CacheScope scope)
        {
            this.BucketName = bucketName;
            this.Scope = scope;
        }

        public CachePropertyAttribute(CacheScope scope)
        {
            this.Scope = scope;
        }

        public CachePropertyAttribute(String bucketName)
            : this(bucketName, CacheScope.MaxDuration) { }

        public String BucketName { get; set; }

        public CacheScope Scope { get; set; }
    }
}