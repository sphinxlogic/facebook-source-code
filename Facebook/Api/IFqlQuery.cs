using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Facebook.Api
{
    public interface IFqlQuery
    {
        String Name { get; }
        String Query { get; }
        FqlQueryReturnType ReturnType { get; }
    }
}