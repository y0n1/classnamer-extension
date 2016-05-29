using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace App.Utils
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Get the array slice between the two indexes.
        /// ... Inclusive for start index, exclusive for end index.
        /// <param name="source"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <para>
        /// Source: <see cref="http://www.dotnetperls.com/array-slice"/>
        /// </para>
        /// </summary>
        public static T[] Slice<T>(this T[] source, int start, int end)
        {
            // Handles negative ends.
            if (end < 0)
            {
                end = source.Length + end;
            }
            int len = end - start;

            // Return new array.
            T[] res = new T[len];
            for (int i = 0; i < len; i++)
            {
                res[i] = source[i + start];
            }
            return res;
        }

        /// <summary>
        /// Splits a camel case string into separate words.
        /// </summary>
        /// <param name="camelCaseString"></param>
        /// <returns></returns>
        public static string[] SplitCamelCase(this string camelCaseString)
        {
            MatchEvaluator evaluator = (Match match) => { return string.Format("{0} ", match.ToString()); };

            return Regex.Replace(camelCaseString, @"[A-Za-z0-9][a-z0-9]+", evaluator).Split(' ').Slice(0, -1);
        }
    }
}
