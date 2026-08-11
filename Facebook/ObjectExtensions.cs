using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace Facebook
{
    public static class ObjectExtensions
    {
        public static String ToJson(this Object value)
        {
            var jsonBuilder = new StringBuilder();
            var serializer = new JsonSerializer();
            var writer = new JsonTextWriter(new StringWriter(jsonBuilder));
            serializer.Serialize(writer, value);
            return jsonBuilder.ToString();
        }

        public static String GetDelimitedString(this Object[] collection, String delimiter)
        {
            if(collection == null) return null;
            var s = new StringBuilder();
            if (collection.Length > 0)
            {
                s.AppendFormat("{0}", collection[0]);
                for (var index = 1; index < collection.Length; index++) s.AppendFormat("{0}{1}", delimiter, collection[index]);
            }
            return s.ToString();
        }
    }
}
