using System;
using System.Text.RegularExpressions;

namespace Orion.Classes.REs
{
    class RE_for_integer_constant
    {
        public static void integer_RE()
        {
            Console.Write("Enter valid integer: ");
            string input = Console.ReadLine();
            string pattern = "^[+-]?[0-9]+$";
            Regex regex = new Regex(pattern);
            bool result = regex.IsMatch(input);

            if (result)
            {
                Console.WriteLine($"{input} is a valid integer.");
            }
            else
            {
                Console.WriteLine($"{input} is an invlid integer.");
            }
        }
    }
}
