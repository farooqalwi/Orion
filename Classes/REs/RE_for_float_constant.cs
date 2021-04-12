using System;
using System.Text.RegularExpressions;

namespace Orion.Classes.REs
{
    class RE_for_float_constant
    {
        public static void float_RE()
        {
            Console.Write("Enter valid float: ");
            string input = Console.ReadLine();
            string pattern = "^[+-]?[0-9]*[.][0-9]+$";
            Regex regex = new Regex(pattern);
            bool result = regex.IsMatch(input);

            if (result)
            {
                Console.WriteLine($"{input} is a valid float.");
            }
            else
            {
                Console.WriteLine($"{input} is an invlid float.");
            }
        }
    }
}
