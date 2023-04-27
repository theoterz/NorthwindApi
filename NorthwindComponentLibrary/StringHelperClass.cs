using System.Text.RegularExpressions;

namespace NorthwindComponentLibrary
{
    //This class contains methods that use regular expressions in order to split camel case property names.
    internal static class StringHelperClass
    {
        public static string SplitCamelCase(string input)
        {
            /*
             * The first capturing group \P{Lu} matches any Unicode character that is not an uppercase letter.
             * The second capturing group \p{Lu}  matches any Unicode uppercase letter.
             * 
             * The replacement string "$1 $2" is used to replace the matched text. $1 matches the lowercase letter and $2 the uppercase letter.
             */

            return Regex.Replace(input, @"(\P{Lu})(\p{Lu})", "$1 $2");
        }

        public static string GetEntityType(Object Entity)
        {
            /*
             * The ^ assertion corresponds to the start of the line.
             * ?<! is a negative lookbehind expression., which asserts that what immediately precedes the current position in the string is not the following character group
             * So (?<!^) matches any position that is not at the start of the string.
             * 
             * ?= is a positive loockahead expretion.
             * So (?=[A-Z]) matches any position that is immediately followed by a capital letter. 
             * 
            */

            string[] output = Regex.Split(Entity.GetType().Name, @"(?<!^)(?=[A-Z])");
            return output[0];
        }
    }
}
