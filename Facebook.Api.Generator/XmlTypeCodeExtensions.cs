using System;
using System.Xml.Schema;

namespace Facebook.Api.Generator
{
    public static class XmlTypeCodeExtensions
    {
        public static Type ToType(this XmlTypeCode typeCode)
        {
            switch (typeCode)
            {
                case XmlTypeCode.AnyUri: return typeof(Uri);
                case XmlTypeCode.Base64Binary: return typeof(Byte[]);
                case XmlTypeCode.Boolean: return typeof(Boolean);
                case XmlTypeCode.Byte: return typeof(Byte);
                case XmlTypeCode.DayTimeDuration: return typeof(TimeSpan);
                case XmlTypeCode.Decimal: return typeof(Decimal);
                case XmlTypeCode.Double: return typeof(Double);
                case XmlTypeCode.Float: return typeof(Single);

                case XmlTypeCode.YearMonthDuration:
                case XmlTypeCode.Time:
                case XmlTypeCode.Date:
                case XmlTypeCode.DateTime: return typeof(DateTime);


                case XmlTypeCode.Short: return typeof(Int16);

                case XmlTypeCode.NonNegativeInteger:
                case XmlTypeCode.NonPositiveInteger:
                case XmlTypeCode.PositiveInteger:
                case XmlTypeCode.NegativeInteger:
                case XmlTypeCode.Int:
                case XmlTypeCode.Integer: return typeof(Int64);


                case XmlTypeCode.Long: return typeof(Int64);

                case XmlTypeCode.NormalizedString:
                case XmlTypeCode.Text:
                case XmlTypeCode.String: return typeof(String);

                case XmlTypeCode.UnsignedInt: return typeof(UInt32);
                case XmlTypeCode.UnsignedLong: return typeof(UInt64);

                case XmlTypeCode.UnsignedByte:
                case XmlTypeCode.UnsignedShort: return typeof(UInt16);

                case XmlTypeCode.AnyAtomicType:
                case XmlTypeCode.Attribute:
                case XmlTypeCode.Comment:
                case XmlTypeCode.Document:
                case XmlTypeCode.Duration:
                case XmlTypeCode.Element:
                case XmlTypeCode.Entity:
                case XmlTypeCode.GDay:
                case XmlTypeCode.GMonth:
                case XmlTypeCode.GMonthDay:
                case XmlTypeCode.GYear:
                case XmlTypeCode.GYearMonth:
                case XmlTypeCode.HexBinary:
                case XmlTypeCode.Id:
                case XmlTypeCode.Idref:
                case XmlTypeCode.Item:
                case XmlTypeCode.Language:
                case XmlTypeCode.NCName:
                case XmlTypeCode.Name:
                case XmlTypeCode.Namespace:
                case XmlTypeCode.NmToken:
                case XmlTypeCode.Node:
                case XmlTypeCode.None:
                case XmlTypeCode.Notation:
                case XmlTypeCode.ProcessingInstruction:
                case XmlTypeCode.QName:
                case XmlTypeCode.Token:
                case XmlTypeCode.UntypedAtomic:
                default: return null;
            }
        }
    }
}