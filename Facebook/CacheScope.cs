using System;

namespace Facebook
{
    public enum CacheScope
    {
        NotCached,
        Request,
        Session,
        MaxDuration,
        Permanent
    }
}