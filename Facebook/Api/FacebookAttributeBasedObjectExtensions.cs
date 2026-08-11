using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.XPath;
using System.Globalization;

namespace Facebook.Api
{
    /// <summary>Provides extension methods for implementations of <see cref="IFacebookAttributeBasedObject" />.</summary>
    public static class FacebookAttributeBasedObjectExtensions
    {
        private static String GetXPath(String attributeName)
        {
            return String.Format("//*[@key='{0}']", attributeName);
        }

        public static T GetValueTypeFromSelf<T>(this IFacebookAttributeBasedObject obj)
            where T : struct
        {
            var storageKey = "Root__Self";
            if (obj.InnerDictionary.ContainsKey(storageKey)) return (T)obj.InnerDictionary[storageKey];
            else if (obj.XmlContent == null) return default(T);
            else
            {
                var result = ConvertStruct<T>(obj.XmlContent.Root.Value);
                obj.InnerDictionary.Add(storageKey, result);
                return result;
            }
        }

        public static T GetValueTypeFromRoot<T>(this IFacebookAttributeBasedObject obj, String attributeName)
            where T: struct
        {
            var storageKey = String.Format("Root__{0}", attributeName);
            if (obj.InnerDictionary.ContainsKey(storageKey)) return (T)obj.InnerDictionary[storageKey];
            else if (obj.XmlContent == null) return default(T);
            else
            {
                var attribute = obj.XmlContent.Root.Attribute(attributeName);
                T result = default(T);
                if (attributeName != null)
                {
                    result = ConvertStruct<T>(attribute.Value);
                }
                obj.InnerDictionary.Add(storageKey, result);
                return result;
            }
        }

        /// <summary>Gets the value of the element specified by <paramref name="xpath" />.</summary>
        /// <typeparam name="T">The type of value to return.</typeparam>
        /// <param name="obj">A reference to the current <see cref="IFacebookAttributeBasedObject" /> instance.</param>
        /// <returns>A <typeparamref name="T" /> value containing the value of the specified element.</returns>
        public static T GetValueType<T>(this IFacebookAttributeBasedObject obj, String attributeName)
            where T : struct
        {
            var xpath = GetXPath(attributeName);
            if (obj.InnerDictionary.ContainsKey(attributeName)) return (T)obj.InnerDictionary[attributeName];
            else if (obj.XmlContent == null) return default(T);
            else
            {
                var element = obj.XmlContent.XPathSelectElement(xpath);
                T result = default(T);
                if (element != null)
                {
                    result = ConvertStruct<T>(element.Value);
                }
                obj.InnerDictionary.Add(attributeName, result);
                return result;
            }
        }

        /// <summary>Gets the value of the element specified by <paramref name="xpath" />.</summary>
        /// <param name="obj">A reference to the current <see cref="IFacebookAttributeBasedObject" /> instance.</param>
        /// <returns>A <see cref="String" /> value containing the value of the specified element.</returns>
        public static String GetString(this IFacebookAttributeBasedObject obj, String attributeName)
        {
            var xpath = GetXPath(attributeName);
            if (obj.InnerDictionary.ContainsKey(attributeName)) return (String)obj.InnerDictionary[attributeName];
            else if (obj.XmlContent == null) return null;
            else
            {
                var element = obj.XmlContent.XPathSelectElement(xpath);
                String result = null;
                if (element != null) result = element.Value;
                obj.InnerDictionary.Add(xpath, result);
                return result;
            }
        }

        /// <summary>Gets the value of the element specified by <paramref name="xpath" />.</summary>
        /// <typeparam name="T">The type of value to return.</typeparam>
        /// <param name="obj">A reference to the current <see cref="IFacebookAttributeBasedObject" /> instance.</param>
        /// <returns>A <typeparamref name="T" /> object representation of the value of the specified element and its children.</returns>
        public static T GetFacebookObject<T>(this IFacebookAttributeBasedObject obj, String attributeName)
            where T : class, IFacebookAttributeBasedObject, new()
        {
            var xpath = GetXPath(attributeName);
            if (obj.InnerDictionary.ContainsKey(attributeName)) return (T)obj.InnerDictionary[attributeName];
            else if (obj.XmlContent == null) return default(T);
            else
            {
                var element = obj.XmlContent.XPathSelectElement( xpath);
                T result = null;
                if (element != null)
                {
                    result = new T();
                    result.Init(element);
                }
                obj.InnerDictionary.Add(attributeName, result);
                return result;
            }
        }

        public static T GetFacebookObjectFromSelf<T>(this IFacebookAttributeBasedObject obj)
            where T : class, IFacebookAttributeBasedObject, new()
        {
            var storageKey = "Root__Self";
            if (obj.InnerDictionary.ContainsKey(storageKey)) return (T)obj.InnerDictionary[storageKey];
            else if (obj.XmlContent == null) return default(T);
            else
            {
                var result = new T();
                result.Init(obj.XmlContent.Root);
                obj.InnerDictionary.Add(storageKey, result);
                return result;
            }
        }

        public static FacebookAttributeBasedList<T> GetFacebookObjectCollectionFromSelf<T>(this IFacebookAttributeBasedObject obj, String itemXPath)
            where T : class, IFacebookAttributeBasedObject, new()
        {
            var storageKey = "Root__Self";
            if (obj.InnerDictionary.ContainsKey(storageKey)) return (FacebookAttributeBasedList<T>)obj.InnerDictionary[storageKey];
            else if (obj.XmlContent == null) return null;
            else
            {
                FacebookAttributeBasedList<T> result = new FacebookAttributeBasedList<T>(obj.XmlContent.Root, itemXPath);
                obj.InnerDictionary.Add(storageKey, result);
                return result;
            }
        }

        /// <summary>Gets a collection of <typeparamref name="T"/> objects that are the children of the element specified by <paramref name="xpath" />.</summary>
        /// <typeparam name="T">The type of value to return.</typeparam>
        /// <param name="obj">A reference to the current <see cref="IFacebookAttributeBasedObject" /> instance.</param
        /// <returns>A list of <typeparamref name="T" /> objects representing the children of the specified element.</returns>
        public static FacebookAttributeBasedList<T> GetFacebookObjectCollection<T>(this IFacebookAttributeBasedObject obj, String attributeName, String itemXPath)
            where T : class, IFacebookAttributeBasedObject, new()
        {
            var xpath = GetXPath(attributeName);
            if (obj.InnerDictionary.ContainsKey(xpath)) return (FacebookAttributeBasedList<T>)obj.InnerDictionary[attributeName];
            else if (obj.XmlContent == null) return null;
            else
            {
                var element = obj.XmlContent.XPathSelectElement(xpath);
                FacebookAttributeBasedList<T> result = null;
                if (element != null) result = new FacebookAttributeBasedList<T>(element, itemXPath);
                obj.InnerDictionary.Add(attributeName, result);
                return result;
            }
        }

        /// <summary>Gets a collection of <typeparamref name="T"/> values that are the children of the element specified by <paramref name="xpath" />.</summary>
        /// <typeparam name="T">The type of value to return.</typeparam>
        /// <param name="obj">A reference to the current <see cref="IFacebookAttributeBasedObject" /> instance.</param>
        /// <param name="xpath">A relative <c>XPath</c> query to the desired element.</param>
        /// <param name="itemXPath">A relative <c>XPath</c> query to the child items within the element specified by <paramref name="xpath" />.</param>
        /// <returns>A list of <typeparamref name="T" /> values representing the children of the specified element.</returns>
        /*public static List<T> GetValueTypeCollection<T>(this IFacebookAttributeBasedObject obj, String attributeName, String itemXPath)
            where T : struct
        {
            var xpath = GetXPath(attributeName);
            if (obj.InnerDictionary.ContainsKey(xpath)) return (List<T>)obj.InnerDictionary[xpath];
            else if (obj.XmlContent == null) return null;
            else
            {
                var elements = obj.XmlContent.XPathSelectElements(itemXPath);
                var values = elements.Select(element => ConvertStruct<T>(element.Value));
                List<T> result = null;
                if (elements != null) result = new List<T>(values);
                obj.InnerDictionary.Add(xpath, result);
                return result;
            }
        }*/

        /// <summary>Gets a collection of <see cref="String" /> values that are the children of the element specified by <paramref name="xpath" />.</summary>
        /// <param name="obj">A reference to the current <see cref="IFacebookAttributeBasedObject" /> instance.</param>
        /// <param name="xpath">A relative <c>XPath</c> query to the desired element.</param>
        /// <param name="itemXPath">A relative <c>XPath</c> query to the child items within the element specified by <paramref name="xpath" />.</param>
        /// <returns>A list of <see cref="String" /> values representing the children of the specified element.</returns>
        /*public static List<String> GetStringCollection(this IFacebookAttributeBasedObject obj, String xpath, String itemXPath)
        {
            if (obj.InnerDictionary.ContainsKey(xpath)) return (List<String>)obj.InnerDictionary[xpath];
            else if (obj.XmlContent == null) return null;
            else
            {
                var elements = obj.XmlContent.XPathSelectElements(itemXPath);
                List<String> result = null;
                if (elements != null)
                {
                    var values = elements.Select(element => element.Value);
                    result = new List<String>(values);
                }
                obj.InnerDictionary.Add(xpath, result);
                return result;
            }
        }*/

        private static T ConvertStruct<T>(String input)
        {
            if (String.IsNullOrEmpty(input)) return default(T);
            else
            {
                var type = typeof(T);
                if (type == typeof(DateTime))
                {
                    DateTime dtVal;
                    if (DateTime.TryParseExact(input, "MM/dd/yyyy", CultureInfo.GetCultureInfo("en-US"), DateTimeStyles.AssumeUniversal | DateTimeStyles.AllowWhiteSpaces, out dtVal))
                    {
                        return (T)(Object)dtVal;
                    }
                    else
                    {
                        UnixDateTime unixTime;
                        UnixDateTime.TryParse(input, out unixTime);
                        return (T)(Object)unixTime.ToDateTime();
                    }
                }
                else if (type == typeof(Boolean))
                {
                    Byte value;
                    if (Byte.TryParse(input, out value) && (value == 0 || value == 1)) return (T)(Object)Convert.ToBoolean(value);
                    else return default(T);
                }
                else if (type.IsEnum)
                {
                    try{ return (T)Enum.Parse(type, input); }
                    catch { return default(T); }
                }
                else return (T)Convert.ChangeType(input, typeof(T));
            }
        }
    }
}