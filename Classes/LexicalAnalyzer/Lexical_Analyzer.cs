using Orion.Classes.REs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Orion.Classes.LexicalAnalyzer
{
    class Lexical_Analyzer
    {
        static string[,] keywords = { { "num", "DT" }, { "decimal", "DT" }, { "char", "DT" }, { "string", "DT" }, { "loop", "loop" }, { "until", "until" }, { "when", "when" }, { "lest", "lest" }, { "refund", "refund" }, { "skip", "skip" }, { "jump", "jump" }, { "toggle", "toggle" }, { "check", "check" }, { "mismatch", "mismatch" }, { "main", "main" }, { "void", "void" }, { "none", "none" }, { "bool", "DT" }, { "true", "bool_const" }, { "false", "bool_const" }, { "general", "AM" }, { "personal", "AM" }, { "protected", "AM" }, { "class", "class" }, { "fresh", "fresh" }, { "inactive", "inactive" }, { "final", "final" }, { "symbolic", "symbolic" }, { "this", "ref_var" }, { "base", "ref_var" }, { "inherits", "inherits" }, { "implement", "implement" }, { "interface", "interface" }, { "try", "try" }, { "except", "except" }, { "finally", "finally" }, { "series", "DS" }, { "list", "DS" }, { "listD", "DS" }, { "@", "@" }, { "@@", "@@" } };
        static string[,] operators = { { "!", "!" }, { "++", "inc_dec" }, { "--", "inc_dec" }, { "+", "PM" }, { "-", "PM" }, { "*", "MDM" }, { "/", "MDM" }, { "%", "MDM" }, { "is", "RO" }, { "not", "RO" }, { "<", "RO" }, { ">", "RO" }, { "<=", "RO" }, { ">=", "RO" }, { "and", "and" }, { "or", "or" }, { "=", "Assign_opr" }, { "+=", "CAO" }, { "-=", "CAO" }, { "*=", "CAO" }, { "/=", "CAO" } };
        static string[,] punctuators = { { ",", "," }, { ":", ":" }, { "(", "(" }, { ")", ")" }, { "{", "{" }, { "}", "}" }, { "[", "[" }, { "]", "]" }, { ".", "." }, { "#", "#" } };

        public static bool isInt(string word)
        {

            string pattern = "^[+-]?[0-9]+$";
            Regex regex = new Regex(pattern);
            bool result = regex.IsMatch(word);

            return result;
        }

        public static bool isFloat(string word)
        {

            string pattern = "^[+-]?[0-9]+[.][0-9]+$";
            Regex regex = new Regex(pattern);
            bool result = regex.IsMatch(word);

            return result;
        }

        public static bool isChar(string word)
        {

            string pattern = "^'(([\\s\\w`~!@#$^&(){}<>^,?;:|\\[\\]]|[+]|[-]|[*]|[/]|[%]|[=]){1}|(\\\\)([b]|[n]|[r]|[t]|[']|[\"]|[.]|[\\\\]){1})'$";
            Regex regex = new Regex(pattern);
            bool result = regex.IsMatch(word);

            return result;
        }

        public static bool isString(string word)
        {

            string pattern = "^\"([\\s\\w`~!@#$^&(){}<>^,.?;:|\\[\\]]|[+]|[-]|[*]|[/]|[%]|[=]|(\\\\)([b]|[n]|[r]|[t]|[']|[\"]|[.]|[\\\\]))*\"$";
            Regex regex = new Regex(pattern);
            bool result = regex.IsMatch(word);

            return result;
        }

        public static bool isID(string word)
        {
            string pattern = "^[a-zA-Z_][\\w]*$";
            Regex regex = new Regex(pattern);
            bool result = regex.IsMatch(word);

            return result;
        }

        public static bool isKW(string word)
        {
            bool result = false;
            for (int i = 0; i < 41; i++)
            {
                if (word == keywords[i,0])
                {
                    result = true;
                }
            }

            return result;
        }

        public static bool isOperator(string word)
        {
            bool result = false;
            for (int i = 0; i < 21; i++)
            {
                if (word == operators[i, 0])
                {
                    result = true;
                }
            }

            return result;
        }

        public static bool isPunctuator(string word)
        {
            bool result = false;
            for (int i = 0; i < 10; i++)
            {
                if (word == punctuators[i, 0])
                {
                    result = true;
                }
            }

            return result;
        }

        public static void wordBreaker()
        {
            int LineNo = 1;
            string line;
            

            // Read the file and display it line by line.  
            System.IO.StreamReader file = new System.IO.StreamReader(@"D:\UoK\CSSE\CSSE-V\CC\Lab\Projects\words_test_file.txt");
            while ((line = file.ReadLine()) != null)
            {
                System.Console.WriteLine($"Line No {LineNo} > {line}");

                string pattern = "( )|(,)|(:)|(\")";
                string[] words = Regex.Split(line, pattern);

                //Console.WriteLine(substrings.Length);

                for (int i=0; i<words.Length; i++ )
                {
                    //Console.WriteLine("'{0}'", match);

                }

                LineNo++;
            }

            file.Close();
            //System.Console.WriteLine("There are {0} lines.", LineNo);


        }

        public static void tokenizer(string word, int line)
        {
            //(classPart, valuePart, lineNo)

            if (isInt(word))
            {
                Console.WriteLine($"(int_const, {word}, {line})");
            }
            else if (isFloat(word))
            {
                Console.WriteLine($"(float_const, {word}, {line})");
            }
            else if (isChar(word))
            {
                Console.WriteLine($"(char_const, {word}, {line})");
            }
            else if (isString(word))
            {
                Console.WriteLine($"(str_const, {word}, {line})");
            }
            else if (isKW(word))
            {
                Console.WriteLine($"(KW, {word}, {line})");
            }
            else if (isOperator(word))
            {
                Console.WriteLine($"(Oper, {word}, {line})");
            }
            else if (isPunctuator(word))
            {
                Console.WriteLine($"(Punct, {word}, {line})");
            }
            else if (isID(word))
            {
                Console.WriteLine($"(ID, {word}, {line})");
            }
            else
            {
                Console.WriteLine($"(Invalid, {word}, {line})");
            }
        }

    }
}
