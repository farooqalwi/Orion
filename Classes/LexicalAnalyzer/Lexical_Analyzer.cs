using System;
using System.Linq;
using System.Collections.Generic;
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
            string word = "";       
            string sWord = "";     //for string

            // Read the file line by line.  
            System.IO.StreamReader file = new System.IO.StreamReader(@"D:\UoK\CSSE\CSSE-V\CC\Lab\Projects\words_test_file.txt");
            while ((line = file.ReadLine()) != null)
            {
                System.Console.WriteLine($"Line No {LineNo} > {line}");
                

                for (int i = 0; i < line.Length; i++)
                {
                    //this bloack is for string
                    if (line[i].ToString() == "\"" || sWord.Contains("\""))
                    {
                        sWord += line[i].ToString();

                        if (isString(sWord))
                        {
                            Console.WriteLine(word);
                            Console.WriteLine(sWord);
                            word = "";
                            sWord = "";
                        }

                        continue;
                    }

                    //this bloack is for character
                    if (line[i] == '\'')
                    {
                        //try to handle index out of bound error
                        try
                        {
                        //for length of 3 char. i.e. 's'
                        if (line[i+2] == '\'')
                        {
                            if (word != "")
                            {
                                Console.WriteLine(word);
                                word = "";
                            }

                            for (int j = 0; j < 3; j++)
                            {
                                word += line[i+j].ToString();
                            }

                            if (word != "")
                            {
                                Console.WriteLine(word);
                                word = "";
                            }

                            i += 2;
                            continue;
                        }
                        else if (line[i+1] == '\\' && line[i + 3] == '\'')
                        {
                            //for length of 4 char. i.e. '\n'

                            //for only /n, /b, /r, /t chars
                            if (line[i + 2] == 'n' || line[i + 2] == 'b' || line[i + 2] == 'r' || line[i + 2] == 't')
                            {
                                if (word != "")
                                {
                                    Console.WriteLine(word);
                                    word = "";
                                }

                                for (int j = 0; j < 4; j++)
                                {
                                    word += line[i+j].ToString();
                                }

                                if (word != "")
                                {
                                    Console.WriteLine(word);
                                    word = "";
                                }

                                i += 3;
                                continue;
                            }
                            else
                            {
                                //for other than /n, /b, /r, /t chars

                                if (word != "")
                                {
                                    Console.WriteLine(word);
                                    word = "";
                                }

                                for (int j = 0; j < 3; j++)
                                {
                                    word += line[i+j].ToString();
                                }

                                if (word != "")
                                {
                                    Console.WriteLine(word);
                                    word = "";
                                }

                                i += 2;
                                continue;
                            }
                            
                        }
                        else
                        {
                            //for invalid tocken

                            if (word != "")
                            {
                                Console.WriteLine(word);
                                word = "";
                            }

                            word += line[i].ToString();

                            continue;
                        }

                        }
                        catch (Exception)
                        {
                            if (word != "")
                            {
                                Console.WriteLine(word);
                                word = "";
                            }

                            word += line[i].ToString();

                            continue;
                        }
                    }

                    if (line[i].ToString() != " " && !isOperator(line[i].ToString()) && !isPunctuator(line[i].ToString()))
                    {
                        word += line[i].ToString();
                    }
                    else
                    {
                        Console.WriteLine(word);
                        word = "";
                    }
                }

                if (word != "")
                {
                    Console.WriteLine(word);
                    word = "";
                }
                
                if (sWord != "")
                {
                    Console.WriteLine(sWord);
                    sWord = "";
                }


                LineNo++;
            }

            file.Close();

            
        }

        public static bool StringToInt(string word)
        {
            int num;
            try
            {
                num = Int32.Parse(word);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
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
                if (word != " ")
                {
                    Console.WriteLine($"(Invalid, {word}, {line})");
                }
                
            }
        }

    }
}
