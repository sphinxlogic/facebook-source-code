using System;
using System.Collections.Generic;

namespace Facebook.Api
{
    public interface IFqlMultiquery : IFacebookObject
    {
        List<IFqlQuery> Queries { get; }
    }
}