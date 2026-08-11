using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Facebook.Text
{
    /// <summary>
    /// A set of extension methods that provide various string delimitation functionality.
    /// </summary>
    public static class DelimitationExtensions
    {
        /// <summary>
        /// Returns <paramref name="original"/> with <paramref name="value"/> and <paramref name="delimiter"/> appended to the end. If <paramref name="original"/>
        /// is zero length or a null reference, <paramref name="value"/> is returned alone.
        /// </summary>
        /// <param name="original">The value that will form the base of the returned <see cref="String" />.</param>
        /// <param name="value">A <see cref="String" /> to append to <paramref name="original" />.</param>
        /// <param name="delimiter">A <see cref="String" /> to delimit, or separate, <paramref name="original"/> and <paramref name="value" /></param>
        /// <returns></returns>
        public static String AppendDelimitedValue(this String original, String value, String delimiter)
        {
            if (String.IsNullOrEmpty(original)) return value;
            else return String.Format("{0}{1}{2}", original, delimiter, value);
        }

        /// <summary>
        /// Returns <paramref name="list"/> as a <see cref="String" />, with each value separated by <paramref name="delimiter"/>.
        /// </summary>
        /// <param name="list">An <see cref="IEnumerable" /> of values to delimit.</param>
        /// <param name="delimiter">The delimiter to separate each value.</param>
        /// <returns><paramref name="list"/> as a <see cref="String" />, with each value separated by <paramref name="delimiter"/>.</returns>
        public static String Join(this IEnumerable list, String delimiter)
        {
            StringBuilder result = new StringBuilder();
            foreach (Object obj in list)
            {
                result.AppendFormat("{0}{1}", result.Length == 0 ? String.Empty : delimiter, obj);
            }
            return result.ToString();
        }

        /// <summary>
        /// Returns a <see cref="String" /> representation of <paramref name="values" /> with no delimiter.
        /// </summary>
        /// <param name="values">An <see cref="IEnumerable" /> of values to flatten.</param>
        /// <returns>A <see cref="String" /> representation of <paramref name="values" /> with no delimiter.</returns>
        public static String Join(this IEnumerable values)
        {
            return values.Join(String.Empty);
        }

        /// <summary>
        /// Returns an acronym from <paramref name="text"/>.
        /// </summary>
        /// <param name="text">The words that will form the acronym.</param>
        /// <returns>An acronym from <paramref name="text"/>.</returns>
        public static String GetAcronym(this String text)
        {
            String[] words = text.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            String[] letters = words.Select(w => w.Substring(0, 1)).ToArray();

            return letters.Join().ToUpperInvariant();
        }
    }
}