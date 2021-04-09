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
            string pattern = "^$";
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
