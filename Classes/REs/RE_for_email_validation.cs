using System;
using System.Text.RegularExpressions;

namespace Orion.Classes.REs
{
    class RE_for_email_validation
    {
        public static void email_RE()
        {
            Console.Write("Enter valid email address: ");
            string input = Console.ReadLine();
            string pattern = @"^[a-z0-9]+(.[a-z0-9]+)*@([a-z]+.)+[a-z]+$";
            Regex regex = new Regex(pattern);
            bool result = regex.IsMatch(input);

            if (result)
            {
                Console.WriteLine($"{input} is a valid email address.");
            }
            else
            {
                Console.WriteLine($"{input} is an invlid email address.");
            }
        }
    }
}
