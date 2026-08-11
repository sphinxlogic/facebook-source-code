using System;
using System.Collections.Generic;
using System.Web;

namespace Facebook
{
    /// <summary>Defines a contract that specifies how implementations of <see cref="FacebookContextBase{TCache,TSession}" /> store cached items.</summary>    
    public interface ICacheWrapper
    {
        void Init(HttpContext httpContext);

        /// <summary>Adds the <paramref name="key"/>/<paramref name="value"/> pair to the cache object.</summary>
        /// <param name="key">The cache key used to reference the item.</param>
        /// <param name="value">The item to be added to the cache.</param>
        /// <param name="scope">A <see cref="CacheScope" /> value representing the scope for which the item should be cached.</param>
        void Add(String key, Object value, CacheScope scope);

        /// <summary>Removes the specified item from the cache object.</summary>
        /// <param name="key">A <see cref="String" /> identifier for the cache item to remove.</param>
        /// <returns>The item removed from the cache object. If the value in the key parameter is not found, returns <c>null</c>.</returns>
        Object Remove(String key);

        /// <summary>Determines whether the cache object contains an item with the specified key.</summary>
        /// <param name="key">The key to locate in the cache object.</param>
        /// <returns><c>true</c> if the cache object contains an item with the key; otherwise, <c>false</c>.</returns>
        Boolean ContainsKey(String key);

        /// <summary>Gets or sets the cache item at the specified key.</summary>
        /// <param name="key">A <see cref="String" /> identifier that represents the key for the cache item.</param>
        /// <returns>The specfied cache item.</returns>
        Object this[String key] { get; set; }
    }
}