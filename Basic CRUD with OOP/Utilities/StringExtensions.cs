using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_CRUD_with_OOP.Utilities
{
    public static class StringExtensions
    {
        public static IEnumerable<string> SplitOnLength(this string input, int length)
        {
            int index = 0;
            while (index < input.Length)
            {
                if (index + length < input.Length)
                    yield return input.Substring(index, length);
                else
                    yield return input.Substring(index);

                index += length;
            }
        }
        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength) + "...";
        }
        public static readonly string Banner = @"
                     )                              (
                    (                                 )
            ________[]_                              []
           /^=^-^-^=^-^\                   /^~^~^~^~^~^~\
          /^-^-^-^-^-^-^\                 /^ ^ ^  ^ ^ ^ ^\
         /__^_^_^_^^_^_^_\               /_^_^_^^_^_^_^_^_\
          |  .==.       |       ___       |        .--.  |
        ^^|  |LI|  [}{] |^^^^^ /___\ ^^^^^|  [}{]  |[]|  |^^^^^
        &&|__|__|_______|&&   .  |  .   88|________|__|__|888
             ==== (o_ | _o) ====
              ==== u   u              ====
        ";
    }
}
