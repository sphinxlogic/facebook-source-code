using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;

namespace Facebook.Api.Generator
{
    public class FacebookTypeDefinition
    {
        private FacebookTypeDefinition (XmlSchemaType schemaType, XmlQualifiedName schemaTypeName, IDictionary<XmlQualifiedName, XmlSchemaType> structTypes)
        {
            XmlTypeCode typeCode = XmlTypeCode.None;

            // Attempt to use the TypeCode, or parse the typecode from the schema type name
            if (schemaType != null) typeCode = schemaType.TypeCode;
            else try { typeCode = (XmlTypeCode)Enum.Parse(typeof(XmlTypeCode), schemaTypeName.Name.ToPascalCase()); }
                catch { }
            if (typeCode != XmlTypeCode.None) this.Type = typeCode.ToType();

            // Look at the parent type of the element
            if (this.Type == null && structTypes.ContainsKey(schemaTypeName) && structTypes[schemaTypeName] is XmlSchemaSimpleType)
            {
                var simpleType = (XmlSchemaSimpleType)structTypes[schemaTypeName];
                this.Type = simpleType.TypeCode.ToType();
            }

            // Check for child sequences - this could indicate a collection.
            if (this.Type == null && schemaType != null && schemaType is XmlSchemaComplexType)
            {
                var complexType = (XmlSchemaComplexType)schemaType;
                if (complexType.ContentTypeParticle is XmlSchemaGroupBase)
                {
                    this.Group = (XmlSchemaGroupBase)complexType.ContentTypeParticle;
                    if (this.Group.Items.Count == 1)
                    {
                        this.ChildType = FacebookTypeDefinition.Get((XmlSchemaElement)this.Group.Items[0], structTypes);
                        var listType = "List";
                        if (this.ChildType.Type == null) listType = "FacebookList";
                        this.TypeName = listType + "<" + this.ChildType.TypeName + ">";
                        this.IsCollection = true;
                        if (schemaType.Name == null) this.FieldName = ((XmlSchemaElement)complexType.Parent).Name;
                    }
                    else
                    {
                        this.TypeName = schemaType.Name.ToPascalCase();
                        this.FieldName = schemaType.Name;
                        this.IsCustomType = true;
                    }
                }
            }
            // If the type still hasn't been found, use the type name as it is
            else
            {
                this.TypeName = this.Type == null ? schemaTypeName.Name.ToPascalCase() : this.Type.Name;
                this.FieldName = schemaTypeName.Name;
            }
        }

        public static FacebookTypeDefinition Get (XmlSchemaElement element, IDictionary<XmlQualifiedName, XmlSchemaType> structTypes)
        {
            return new FacebookTypeDefinition(element.SchemaType, element.SchemaTypeName, structTypes) { ApiElementName = element.Name };
        }

        public static FacebookTypeDefinition Get (XmlSchemaComplexType complexType, IDictionary<XmlQualifiedName, XmlSchemaType> structTypes)
        {
            return new FacebookTypeDefinition(complexType, complexType.QualifiedName, structTypes) { ApiElementName = complexType.QualifiedName.Name };
        }

        public static FacebookTypeDefinition Get(XmlSchemaObject schemaObject, IDictionary<XmlQualifiedName, XmlSchemaType> structTypes)
        {
            if (schemaObject is XmlSchemaElement) return FacebookTypeDefinition.Get((XmlSchemaElement)schemaObject, structTypes);
            else return FacebookTypeDefinition.Get((XmlSchemaComplexType)schemaObject, structTypes);
        }

        public String TypeName { get; set; }
        public String FieldName { get; set; }
        public Boolean IsCustomType { get; set; }
        public Boolean IsCollection { get; set; }
        public XmlSchemaGroupBase Group { get; set; }
        public FacebookTypeDefinition ChildType { get; set; }
        public String ApiElementName { get; set; }
        public Type Type { get; set; }
    }
}