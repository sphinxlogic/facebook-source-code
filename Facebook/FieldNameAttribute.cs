using System;
using System.Collections.Generic;
using System.Linq;
using Facebook.Text;

namespace Facebook
{
    [AttributeUsage(AttributeTargets.Field)]
    public class FieldNameAttribute : Attribute
    {
        public FieldNameAttribute(String fieldName)
        {
            this.FieldName = fieldName;
        }

        public String FieldName { get; set; }
    }

    public static class FieldName
    {
        private static class D<T>
            where T : struct
        {
            public static readonly Type Type = typeof(T);

            public static Dictionary<T, String> ValueToField = new Dictionary<T, String>();
            public static Dictionary<T, String[]> ValuesToFields = new Dictionary<T, String[]>();
            public static Dictionary<String, T> FieldsToValues = new Dictionary<String, T>();
            public static Dictionary<String, String> FieldsToNames = new Dictionary<String, String>();

            public static readonly T[] Values = Enum.GetValues(Type).OfType<T>().ToArray();
            public static readonly Dictionary<Int64, T> IntToValue = (
                from val in Enum.GetValues(typeof(T)).OfType<T>()
                select new { Val = Convert.ToInt64(val), Enum = val }).ToDictionary(v => v.Val, v => v.Enum);

            public static readonly Object ValueToFieldLockObject = new Object();
            public static readonly Object ValuesToFieldsLockObject = new Object();
            public static readonly Object FieldsToValuesLockObject = new Object();
            public static readonly Object FieldToNameLockObject = new Object();
        }

        public static T FieldsToValues<T>(String fields)
            where T : struct
        {
            return FieldsToValues<T>(fields.Split(','));
        }

        public static T FieldsToValues<T>(params String[] fields)
            where T : struct
        {
            var fieldsList = fields.ToList();
            fieldsList.Sort();
            var str = fieldsList.Join(",");

            if (D<T>.FieldsToValues.ContainsKey(str)) return D<T>.FieldsToValues[str];
            else
            {
                lock (D<T>.FieldsToValuesLockObject)
                {
                    if (D<T>.FieldsToValues.ContainsKey(str)) return D<T>.FieldsToValues[str];
                    else
                    {
                        var names =
                            from field in fieldsList
                            select FieldToName<T>(field);

                        var strNames = names.Join(",");
                        var result = (T)Enum.Parse(D<T>.Type, strNames);
                        D<T>.FieldsToValues.Add(str, result);
                        return result;
                    }
                }
            }
        }

        private static String FieldToName<T>(String field)
            where T: struct
        {
            if (D<T>.FieldsToNames.ContainsKey(field)) return D<T>.FieldsToNames[field];
            else if (D<T>.FieldsToNames.Count == 0)
            {
                lock (D<T>.FieldToNameLockObject)
                {
                    if (D<T>.FieldsToNames.ContainsKey(field)) return D<T>.FieldsToNames[field];
                    else
                    {
                        D<T>.FieldsToNames = (
                            from val in D<T>.Values
                            select new { Field = ValueToField<T>(val), Name = val.ToString() }).ToDictionary(f => f.Field, f => f.Name);

                        if (D<T>.FieldsToNames.ContainsKey(field)) return D<T>.FieldsToNames[field];
                        else return field;
                    }
                }
            }
            else return field;
        }

        public static String[] ToFieldNames<T>(this T flagSet)
            where T : struct
        {
            if (D<T>.ValuesToFields.ContainsKey(flagSet)) return D<T>.ValuesToFields[flagSet];
            else
            {
                lock (D<T>.ValuesToFieldsLockObject)
                {
                    if (D<T>.ValuesToFields.ContainsKey(flagSet)) return D<T>.ValuesToFields[flagSet];
                    else
                    {
                        var flagsVal = Convert.ToInt64(flagSet);

                        var fields = (
                            from prop in D<T>.IntToValue
                            where (flagsVal & prop.Key) == prop.Key
                            select ValueToField<T>(prop.Value)).ToArray();

                        fields = fields.Where(f => f != null).ToArray();

                        D<T>.ValuesToFields.Add(flagSet, fields);
                        return fields;
                    }
                }
            }
        }

        private static String ValueToField<T>(T flag)
            where T : struct
        {
            if (D<T>.ValueToField.ContainsKey(flag)) return D<T>.ValueToField[flag];
            else
            {
                lock (D<T>.ValueToFieldLockObject)
                {
                    if (D<T>.ValueToField.ContainsKey(flag)) return D<T>.ValueToField[flag];
                    else
                    {
                        var field = D<T>.Type.GetField(flag.ToString());
                        var attrs = field.GetCustomAttributes(typeof(FieldNameAttribute), false);
                        if (attrs.Length == 0)
                        {
                            D<T>.ValueToField.Add(flag, null);
                            return flag.ToString();
                        }
                        else
                        {
                            var attr = (FieldNameAttribute)attrs[0];
                            D<T>.ValueToField.Add(flag, attr.FieldName);
                            return attr.FieldName;
                        }
                    }
                }
            }
        }
    }
}