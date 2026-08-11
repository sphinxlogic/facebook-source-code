using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Facebook.Api;

namespace Facebook.Serialization
{
    public static class SerializationExtensions
    {
        public static Dictionary<Type, XmlSerializer> Serializers = new Dictionary<Type, XmlSerializer>();

        public static void DeserializeQueue(this IFacebookContext context, XElement serializedQueue)
        {
            var queueItems = new List<IQueueItem>();

            var serializedItems = 
                from container in serializedQueue.Descendants("queueItemContainer")
                select new {
                    SerializedItem = container.FirstNode,
                    Serializer = GetSerializer(Type.GetType(container.Attribute("type").Value))
                };

            foreach (var item in serializedItems)
            {
                using (var reader = item.SerializedItem.CreateReader())
                {
                    var queueItem = (IQueueItem)item.Serializer.Deserialize(reader);
                    queueItems.Add(queueItem);
                }
            }                
        }

        public static void SerializeQueue(this IFacebookContext context, XmlWriter writer)
        {
            writer.WriteStartDocument();

            writer.WriteStartElement("facebookQueue");

            foreach (var queueItem in context.QueueItems)
            {
                var queueItemType = queueItem.GetType();

                writer.WriteStartElement("queueItemContainer");

                writer.WriteAttributeString("type", queueItemType.FullName);

                var serializer = GetSerializer(queueItemType);
                serializer.Serialize(writer, queueItem);

                writer.WriteEndElement();
            }

            writer.WriteEndElement();

            writer.WriteEndDocument();
        }

        internal static XmlSerializer GetSerializer(Type type)
        {
            if (!Serializers.ContainsKey(type))
            {
                Serializers.Add(type, new XmlSerializer(type));
            }
            return Serializers[type];
        }

        #region [ Overloads ]
        public static void DeserializeQueue(this IFacebookContext context, XmlReader reader)
        {
            XDocument doc = XDocument.Load(reader);
            context.DeserializeQueue(doc.Root);            
        }

        public static void DeserializeQueue(this IFacebookContext context, Stream input)
        {
            using (var reader = XmlReader.Create(input))
            {
                context.DeserializeQueue(reader);
            }
        }

        public static XElement SerializeQueue(this IFacebookContext context)
        {
            var output = new XDocument();
            using (var writer = output.CreateWriter())
            {
                context.SerializeQueue(writer);
            }
            return output.Root;
        }

        public static void SerializeQueue(this IFacebookContext context, Stream output)
        {
            using (var writer = XmlWriter.Create(output))
            {
                context.SerializeQueue(writer);
            }
        }
        #endregion
    }
}