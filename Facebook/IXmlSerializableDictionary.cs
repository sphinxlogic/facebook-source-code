using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Facebook
{
    public interface IXmlSerializableDictionary<TKey, TValue> : IDictionary<TKey, TValue>, IXmlSerializable
    {
    }
}