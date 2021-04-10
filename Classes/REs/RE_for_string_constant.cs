using System;
using System.Text.RegularExpressions;

namespace Orion.Classes.REs
{
    class RE_for_string_constant
    {
        public static void string_RE()
        {
            Console.Write("Enter valid string: ");
            string input = Console.ReadLine();
            //\w = [a-zA-Z0-9_]
            //\s (space) matches any single whitespace
            //string s = ""; //empty is allowed
            //string s = " "; //single and multiple spaces are allowed
            string pattern = "^\"([\\s\\w`~!@#$^&(){}<>^,?;:|\\[\\]]|[+]|[-]|[*]|[/]|[%]|[=]|(\\\\)([b]|[n]|[r]|[t]|[']|[\"]|[.]|[\\\\]))*\"$";
            Regex regex = new Regex(pattern);
            bool result = regex.IsMatch(input);

            if (result)
            {
                Console.WriteLine($"{input} is a valid string.");
            }
            else
            {
                Console.WriteLine($"{input} is an invalid string.");
            }
        }
    }
}
