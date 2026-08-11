using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace Facebook.Api
{
    public class FacebookAttributeBasedObjectBase : IFacebookAttributeBasedObject, ISerializable
    {
        internal FacebookAttributeBasedObjectBase(SerializationInfo info, StreamingContext context)            
        {
            this.InnerDictionary = (IDictionary<String, Object>)info.GetValue("InnerDictionary", typeof(IDictionary<String, Object>));
            this.XmlContent = (XDocument)info.GetValue("XmlContent", typeof(XDocument));
        }

        /// <summary>Initializes an instance of <see cref="FacebookObjectBase" /> using the specified <see cref="XElement" /> object as the data source.</summary>
        /// <param name="content">A <see cref="XElement" /> object containing XML that will be queried by the object's properties.</param>
        public FacebookAttributeBasedObjectBase(XElement content)
        {
            this.Init(content);
        }

        /// <summary>Initializes an instance of <see cref="FacebookObjectBase" /> that will be prepopulated using data from the specified <see cref="IDictionary{String,Object}" /> object.</summary>
        /// <param name="dict">A <see cref="IDictionary{String,Object}" /> containing data that will be used to prepopulate the <see cref="FacebookObjectBase" /> instance.</param>
        public FacebookAttributeBasedObjectBase(IDictionary<String, Object> dict)
        {
            this.InnerDictionary = new Dictionary<String, Object>(dict);
        }

        /// <summary>Initializes an instance of <see cref="FacebookObjectBase" /> with no prepopulated data or XML data source.</summary>
        public FacebookAttributeBasedObjectBase()
        {
            this.InnerDictionary = new Dictionary<String, Object>();
        }

        /// <summary>Initializes the <see cref="FacebookObjectBase" /> instance using the specified <see cref="XElement" /> object as the data source.</summary>
        /// <param name="dataSource">A <see cref="XElement" /> object containing XML that will be queried by the object's properties.</param>
        public void Init(XElement dataSource)
        {
            this.XmlContent = new XDocument(dataSource);
            this.InnerDictionary = new Dictionary<String, Object>();
        }

        /// <summary>Gets a reference the <see cref="XDocument" /> data source specified for this instance.</summary>
        protected internal XDocument XmlContent { get; set; }

        /// <summary>Gets a reference to the dictionary object used to store the results of XPath property queries.</summary>
        protected IDictionary<String, Object> InnerDictionary { get; private set; }

        /// <summary>Gets a reference the <see cref="XDocument" /> data source specified for this instance.</summary>
        XDocument IFacebookAttributeBasedObject.XmlContent { get { return this.XmlContent; } }

        /// <summary>Gets a reference to the dictionary object used to store the results of XPath property queries.</summary>
        IDictionary<String, Object> IFacebookAttributeBasedObject.InnerDictionary
        {
            get { return this.InnerDictionary; }
        }

        #region [ ISerializable Members ]

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("InnerDictionary", this.InnerDictionary, typeof(IDictionary<String, Object>));
            info.AddValue("XmlContent", this.XmlContent, typeof(XDocument));
        }

        #endregion
    }
}
