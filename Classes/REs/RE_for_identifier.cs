using System;
using System.Text.RegularExpressions;

namespace Orion.Classes.REs
{
    class RE_for_identifier
    {
        public static void identifier_RE()
        {
            Console.Write("Enter valid identifier: ");
            string input = Console.ReadLine();
            //\w = [a-zA-Z0-9_]
            string pattern = "^[a-zA-Z_][\\w]*$";
            Regex regex = new Regex(pattern);
            bool result = regex.IsMatch(input);

            if (result)
            {
                Console.WriteLine($"{input} is a valid identifier.");
            }
            else
            {
                Console.WriteLine($"{input} is an invlid identifier.");
            }
        }
    }
}
