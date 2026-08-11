using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Runtime.Serialization;

namespace Facebook.Api
{
    /// <summary>Represents a strongly typed list of <see cref="FacebookObjectBase" /> objects.</summary>
    /// <typeparam name="T"></typeparam>
    public class FacebookAttributeBasedList<T> : List<T>, IFacebookAttributeBasedList, ISerializable
        where T : IFacebookAttributeBasedObject, new()
    {        
        internal FacebookAttributeBasedList(SerializationInfo info, StreamingContext context)            
        {
            var count = info.GetInt32("__Count");
            for(var index = 0; index < count; index++)
            {
                this.Add((T)info.GetValue(index.ToString(), typeof(T)));
            }
        }

        /// <summary>Initializes an instance of <see cref="FacebookList{T}" /> with the specified content.</summary>
        /// <param name="content">An <see cref="XElement" /> object containing the list serialized into XML.</param>
        /// <param name="itemXPath">The XPath identifier of a single <typeparamref name="T"/> object within the list.</param>
        public FacebookAttributeBasedList(XElement content, String itemXPath)
            : base(FacebookAttributeBasedList<T>.GetObjects(content, itemXPath)) { }

        public FacebookAttributeBasedList() { }

        public FacebookAttributeBasedList(IEnumerable<T> collection)
            : base(collection) { }

        /// <summary>Deserializes a list of <typeparamref name="T" /> objects from the specified XML content.</summary>
        /// <param name="content">An <see cref="XElement" /> object containing the list serialized into XML.</param>
        /// <param name="itemXPath">The XPath identifier of a single <typeparamref name="T"/> object within the list.</param>
        /// <returns>An <see cref="IEnumerable{T}" /> containing the objects queried from the specified content.</returns>
        protected static IEnumerable<T> GetObjects(XElement content, String itemXPath)
        {
            return
                from element in content.XPathSelectElements("//" + itemXPath)
                select FacebookAttributeBasedList<T>.GetFacebookObject(element);
        }

        /// <summary>Deserializes a single instance of <typeparamref name="T" /> from the specified xml content.</summary>
        /// <param name="content">An <see cref="XElement" /> object containing the an XML serialized representation of a <typeparamref name="T" /> object.</param>
        /// <returns></returns>
        private static T GetFacebookObject(XElement content)
        {
            var obj = new T();
            obj.Init(content);
            return obj;
        }

        #region [ ISerializable Members ]

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("__Count", this.Count, typeof(Int32));
            for(var index = 0; index < this.Count ; index++)
            {
                var item = this[index];
                info.AddValue(index.ToString(), item, typeof(T));
            }
        }

        #endregion
    }
}