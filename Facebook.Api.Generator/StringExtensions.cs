using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Facebook.Api.Generator
{
    public static class StringExtensions
    {
        private static String ToCase(this String value, Boolean capitalizeFirst)
        {
            if (value.IndexOfAny("0123456789".ToCharArray()) >= 0)
            {
                return Regex.Replace(value, @"\b(?:(?<split>[-_0-9])*(?<cap>[A-Za-z])(?<rest>[A-Za-z]*))*", new MatchEvaluator(match =>
                {
                    var b = new StringBuilder();
                    for (var i = 0; i < match.Groups["cap"].Captures.Count; i++)
                    {
                        var first = match.Groups["cap"].Captures[i].Value;
                        if (i > 0 || capitalizeFirst) b.Append(first.ToUpper());
                        else b.Append(first);
                        if (match.Groups["rest"].Captures.Count > i) b.Append(match.Groups["rest"].Captures[i].Value);
                        if (match.Groups["split"].Captures.Count > i && Regex.IsMatch(match.Groups["split"].Captures[i].Value, @"[0-9]")) b.Append(match.Groups["split"].Captures[i].Value);
                    }
                    return b.ToString();
                }));
            }
            else
            {                                
                var result = String.Join(String.Empty, value.Split("-_".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(s => s.ToCharArray()[0].ToString().ToUpper() + s.Substring(1)).ToArray());
                if (capitalizeFirst) return result;
                else return result.ToCharArray()[0].ToString().ToLower() + result.Substring(1);
            }
        }

        public static String ToPascalCase(this String value)
        {
            return value.ToCase(true);
        }

        public static String ToCamelCase(this String value)
        {
            return value.ToCase(false);
        }

        public static String CapitalizeFirst(this String value)
        {
            return value.Substring(0, 1).ToUpper() + value.Substring(1);
        }
    }
}