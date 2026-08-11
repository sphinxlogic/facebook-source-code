using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Facebook.Api
{
    [Serializable]
    public class FqlMultiquery : FacebookObjectBase, IFqlMultiquery
    {
        public FqlMultiquery()
            : base()
        {
            this.Queries = new List<IFqlQuery>();
        }

        /// <summary>Initializes an instance of <see cref="Batch" />.</summary>
        /// <param name="context">A reference to the <see cref="IFacebookContext" /> object that will own the batch.</param>
        public FqlMultiquery(IFacebookContext context)
            : this()
        {
            this.Context = context;
        }

        public FqlMultiquery(XElement content)
            : base(content)
        {
            this.Queries = new List<IFqlQuery>();
        }

        /// <summary>Adds a new query to the multiquery.</summary>
        /// <typeparam name="TReturnType">The return type of the query.</typeparam>
        public String Add<TReturnType>(String name, String query)
            where TReturnType : class, IFacebookObject
        {
            this.Queries.Add(new FqlQuery<TReturnType>(FqlQueryReturnType.FacebookObject, name, query));
            return name;
        }

        public String Add<TReturnType>(String query)
            where TReturnType : class, IFacebookObject
        {
            var name = Guid.NewGuid().ToString("N");
            return this.Add<TReturnType>(name, query);
        }

        /// <summary>Gets the internal list of queries.</summary>
        internal List<IFqlQuery> Queries { get; private set; }

        List<IFqlQuery> IFqlMultiquery.Queries
        {
            get { return this.Queries; }
        }

        /// <summary>Gets a reference to the <see cref="IFacebookContext" /> object that owns the batch.</summary>
        internal IFacebookContext Context { get; private set; }

        public FacebookList<TReturnType> GetResult<TReturnType>(String name)
            where TReturnType : class, IFacebookObject, new()
        {
            if (this.Responses.ContainsKey(name)) return new FacebookResponse<FacebookList<TReturnType>>(new XDocument(this.Responses[name]));
            else return null;
        }

        internal Dictionary<String, XElement> Responses
        {
            get
            {
                if (this.InnerDictionary.ContainsKey("__responses")) return (Dictionary<String, XElement>)this.InnerDictionary["__responses"];
                else if (this.XmlContent == null) return null;
                else
                {
                    var elements = this.XmlContent.Descendants("fql_result");
                    var result = (
                        from element in elements
                        select new
                        {
                            Name = element.Element("name").Value,
                            Response = element.Element("fql_result_set")
                        }).ToDictionary(r => r.Name, r => r.Response);
                    this.InnerDictionary["__responses"] = result;
                    return result;
                }
            }            
        }
    }
}