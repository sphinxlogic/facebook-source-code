using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace Facebook.Api.Controllers
{
    public class FqlController : FacebookApiController
    {
        public FqlController(IFacebookContext facebookContext) : 
                base(facebookContext) {
        }

        public FacebookResponse<XElement> QueryXml(String fql)
        {
            Dictionary<string, object> args = new Dictionary<string, object>();
            args.Add("query", fql);
            var response = this.ExecuteRequest<XElement>("Fql.query", args);
            return response;
        }

        public FacebookJsonResponse<XElement> QueryJson(String fql)
        {
            Dictionary<string, object> args = new Dictionary<string, object>();
            args.Add("query", fql);
            var response = this.ExecuteRequest<XElement>("Fql.query", args);
            return new FacebookJsonResponse<XElement>(response);
        }

        public FacebookResponse<FacebookList<TValue>> Query<TValue>(String fql)
            where TValue : class, IFacebookObject, new()
        {
            Dictionary<string, object> args = new Dictionary<string, object>();
            args.Add("query", fql);
            var response = this.ExecuteRequest<FacebookList<TValue>>("Fql.query", args);
            return response;
        }

        public FacebookResponse<FacebookList<TValue>> Query<TValue>(String fqlFormat, params Object[] args)
            where TValue : class, IFacebookObject, new()
        {
            return Query<TValue>(String.Format(fqlFormat, args));
        }

        public FacebookResponse<TMultiquery> Multiquery<TMultiquery>(TMultiquery multiquery)
            where TMultiquery : class, IFqlMultiquery
        {
            Dictionary<string, object> args = new Dictionary<string, object>();
            var queries = (
                from query in multiquery.Queries
                select new { query.Name, query.Query }).ToDictionary(q => q.Name, q => q.Query);
            args.Add("queries", JsonConvert.SerializeObject(queries));
            var response = this.ExecuteRequest<TMultiquery>("Fql.multiquery", args);
            return response;
        }

        public FacebookResponse<FqlMultiquery> Multiquery(IDictionary<String, String> queries)
        {
            Dictionary<string, object> args = new Dictionary<string, object>();
            args.Add("queries", JsonConvert.SerializeObject(queries));
            var response = this.ExecuteRequest<FqlMultiquery>("Fql.multiquery", args);
            return response;
        }
    }
}
