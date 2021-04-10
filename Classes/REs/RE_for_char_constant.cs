using System;
using System.Text.RegularExpressions;

namespace Orion.Classes.REs
{
    class RE_for_char_constant
    {
        public static void char_RE()
        {
            Console.Write("Enter valid char: ");
            string input = Console.ReadLine();
            //\w = [a-zA-Z0-9_]
            string pattern = "^'(([\\w`~!@#$^&(){}<>^,?;:|\\[\\]]|[+]|[-]|[*]|[/]|[%]|[=]){1}|(\\\\)([b]|[n]|[r]|[t]|[']|[\"]|[.]|[\\\\]){1})'$";
            Regex regex = new Regex(pattern);
            bool result = regex.IsMatch(input);

            if (result)
            {
                Console.WriteLine($"{input} is a valid char.");
            }
            else
            {
                Console.WriteLine($"{input} is an invalid char.");
            }

        }
    }
}
