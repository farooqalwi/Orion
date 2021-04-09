using System;
using System.Text.RegularExpressions;

namespace Orion.Classes.REs
{
    class RE_for_url
    {
        public static void url_RE()
        {
            Console.Write("Enter valid url: ");
            string input = Console.ReadLine();
            string pattern = "^((https://www.|http://www.|www.)[a-z0-9]+|[a-z0-9]+)(.[a-z]+)+(/|(/[a-z0-9]+)*)$";
            Regex regex = new Regex(pattern);
            bool result = regex.IsMatch(input);

            if (result)
            {
                Console.WriteLine($"{input} is a valid url.");
            }
            else
            {
                Console.WriteLine($"{input} is an invlid url.");
            }
        }
    }
}
