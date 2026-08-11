using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Facebook
{
    public static class Reflection
    {
        private static Dictionary<Type, Dictionary<Type, Object>> TypeAttributes = new Dictionary<Type, Dictionary<Type, Object>>();
        private static Dictionary<MemberInfo, Dictionary<Type, Object>> MemberAttributes = new Dictionary<MemberInfo, Dictionary<Type, Object>>();
        private static Dictionary<Type, List<MemberInfo>> Members = new Dictionary<Type, List<MemberInfo>>();

        private static void ReflectAttributes(Type type)
        {
            var attributes = type.GetCustomAttributes(true);
            if (attributes.Length > 0)
            {
                var attributeDict = attributes.ToDictionary(attr => attr.GetType(), attr => attr);
                Reflection.TypeAttributes.Add(type, attributeDict);
            }
            else Reflection.TypeAttributes.Add(type, new Dictionary<Type, Object>());
        }

        private static void ReflectAttributes(MemberInfo memberInfo)
        {
            var attributes = memberInfo.GetCustomAttributes(true);
            if (attributes.Length > 0)
            {
                var attributeDict = attributes.ToDictionary(attr => attr.GetType(), attr => attr);
                Reflection.MemberAttributes.Add(memberInfo, attributeDict);
            }
            else Reflection.MemberAttributes.Add(memberInfo, new Dictionary<Type, Object>());
        }

        private static void ReflectMembers(Type type)
        {
            var members = type.GetMembers();
            Reflection.Members.Add(type, members.ToList());
        }

        public static Boolean HasAttribute<TAttribute>(this Type type)
            where TAttribute : Attribute
        {
            var attributeType = typeof(TAttribute);
            return type.HasAttribute(attributeType);
        }

        public static Boolean HasAttribute<TAttribute>(this MemberInfo memberInfo)
            where TAttribute : Attribute
        {
            var attributeType = typeof(TAttribute);
            return memberInfo.HasAttribute(attributeType);
        }

        public static Boolean HasAttribute(this Type type, Type attributeType)
        {
            if (!Reflection.TypeAttributes.ContainsKey(type))
            {
                Reflection.ReflectAttributes(type);
            }
            return Reflection.TypeAttributes[type].ContainsKey(attributeType);
        }

        public static Boolean HasAttribute(this MemberInfo memberInfo, Type attributeType)
        {
            if (!Reflection.MemberAttributes.ContainsKey(memberInfo))
            {
                Reflection.ReflectAttributes(memberInfo);
            }
            return Reflection.MemberAttributes[memberInfo].ContainsKey(attributeType);
        }

        public static TAttribute GetAttribute<TAttribute>(this Type type)
            where TAttribute : Attribute
        {
            var attributeType = typeof(TAttribute);
            return (TAttribute)type.GetAttribute(attributeType);
        }

        public static Attribute GetAttribute(this Type type, Type attributeType)
        {
            if (type.HasAttribute(attributeType)) return (Attribute)Reflection.MemberAttributes[type][attributeType];
            else return null;
        }

        public static TAttribute GetAttribute<TAttribute>(this MemberInfo memberInfo)
            where TAttribute : Attribute
        {
            var attributeType = typeof(TAttribute);
            return (TAttribute)memberInfo.GetAttribute(attributeType);
        }

        public static Attribute GetAttribute(this MemberInfo memberInfo, Type attributeType)
        {
            if (memberInfo.HasAttribute(attributeType)) return (Attribute)Reflection.MemberAttributes[memberInfo][attributeType];
            else return null;
        }

        public static List<MemberInfo> GetCachedMembers(this Type type)
        {
            if (!Reflection.Members.ContainsKey(type)) Reflection.ReflectMembers(type);
            return Reflection.Members[type];
        }
    }
}