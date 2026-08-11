using System;

namespace Facebook
{
    [AttributeUsage(AttributeTargets.Field)]
    public class PermissionAttribute : Attribute
    {
        public PermissionAttribute()
        {
            this.CanBeRequested = true;
        }

        /// <summary>Gets or sets a <see cref="Boolean" /> representing whether the permission can be requested from a user using the showPermissionDialog API.</summary>
        /// <remarks>The default value is <c>true</c>.</remarks>
        public Boolean CanBeRequested { get; set; }
    }
}