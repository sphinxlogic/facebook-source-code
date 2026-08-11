using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Facebook
{
    public interface IFacebookAttributeBasedObject
    {
        /// <summary>Gets a reference to a dictionary of properties and values for the object.</summary>
        IDictionary<String, Object> InnerDictionary { get; }

        /// <summary>Gets a reference the <see cref="XDocument" /> data source specified for this instance.</summary>
        XDocument XmlContent { get; }

        /// <summary>Initializes the <see cref="FacebookAttributeBasedObjectBase" /> instance using the specified <see cref="XElement" /> object as the data source.</summary>
        /// <param name="dataSource">A <see cref="XElement" /> object containing XML that will be queried by the object's properties.</param>
        void Init(XElement dataSource);
    }
}