using System;
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
                if (word == keywords[i, 0])
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
            int LineNo = 1;         //it is line no of source file
            string line;            //from source file
            bool comStart = false;  //for multi line comments
            string word = "";       //for general
            string sWord = "";      //for string
            string dWord = "";      //for decimal No
            string dPoint = "";    //decimal point

            // Read the file line by line.  
            System.IO.StreamReader file = new System.IO.StreamReader(@"D:\UoK\CSSE\CSSE-V\CC\Lab\Projects\words_test_file.txt");
            while ((line = file.ReadLine()) != null)
            {
                System.Console.WriteLine($"Line No {LineNo} > {line}");

                //char by char checking
                for (int i = 0; i < line.Length; i++)
                {
                    //this bloack is for single line comment
                    if (line[i] == '@' && line[i + 1] != '@')
                    {
                        break;
                    }

                    //this bloack is for multi line comments
                    if ((line[i] == '@' && line[i + 1] == '@') || comStart)
                    {
                        if (line[i] == '@' && line[i + 1] == '@')
                        {
                            i++;
                        }

                        //to end comments
                        try
                        {
                            if ((line[i - 1] == '@' && line[i] == '@') && comStart)
                            {
                                comStart = false;
                                continue;
                            }
                        }
                        catch (Exception)
                        {
                            continue;
                        }

                        //to start comments
                        comStart = true;
                        if (comStart)
                        {
                            continue;
                        }
                    }

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
                            if (line[i + 2] == '\'')
                            {
                                if (word != "")
                                {
                                    Console.WriteLine(word);
                                    word = "";
                                }

                                for (int j = 0; j < 3; j++)
                                {
                                    word += line[i + j].ToString();
                                }

                                if (word != "")
                                {
                                    Console.WriteLine(word);
                                    word = "";
                                }

                                i += 2;
                                continue;
                            }
                            else if (line[i + 1] == '\\' && line[i + 3] == '\'')
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
                                        word += line[i + j].ToString();
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
                                        word += line[i + j].ToString();
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

                    //this bloack is for decimal No
                    if (line[i] == '.' || dPoint == ".")
                    {
                        if (line[i] == '.' && dPoint != ".")
                        {
                            dPoint = line[i].ToString();     //for first decimal point
                            continue;
                        }
                        

                        if (word != "")
                        {
                            if (!isInt(word))
                            {
                                Console.WriteLine(word);
                                word = "";
                            }
                        }

                        if (line[i] != '.' && dPoint == ".")
                        {
                            if ((isInt(line[i].ToString()) && dWord == "") || dWord !="")
                            {
                                dWord += line[i].ToString();  //after ponit first occurrence must be integer
                                continue;
                            }
                            else if(!isInt(line[i].ToString()) && dWord == "")
                            {
                                if (word != "")
                                {
                                    Console.WriteLine(word);
                                    word = "";
                                }

                                Console.WriteLine(dPoint);
                                dPoint = "";
                                word += line[i].ToString();
                                continue;
                            }
                        }
                        else if(line[i] == '.' && dPoint == ".")
                        {
                            if (isInt(word))
                            {
                                Console.WriteLine(word + dPoint + dWord);
                                word = "";
                                dPoint = "";
                                dWord = "";
                                dPoint += line[i].ToString();
                                continue;
                            }

                            if (word == "")
                            {
                                Console.WriteLine(dPoint + dWord);
                                dPoint = "";
                                dWord = "";
                                dPoint += line[i].ToString();
                                continue;
                            }
                            
                        }
                    }

                    //for space, punc, oper and other words
                    if (line[i].ToString() != " " && !isOperator(line[i].ToString()) && !isPunctuator(line[i].ToString()))
                    {
                        //for alphabetic operators. i.e. is, or (length 2)
                        try
                        {
                            if ((line[i] == 'i' && line[i + 1] == 's') || (line[i] == 'o' && line[i + 1] == 'r'))
                            {
                                if (word != "")
                                {
                                    Console.WriteLine(word);
                                    word = "";
                                }

                                Console.WriteLine(line[i].ToString() + line[i + 1].ToString());
                                i++;
                                continue;
                            }
                        }
                        catch (Exception)
                        {
                            word += line[i].ToString();
                            Console.WriteLine(word);
                            word = "";
                            continue;
                        }

                        //for alphabetic operators. i.e. not, and (length 3)
                        try
                        {
                            if ((line[i] == 'n' && line[i + 1] == 'o' && line[i + 2] == 't') || (line[i] == 'a' && line[i + 1] == 'n' && line[i + 2] == 'd'))
                            {
                                if (word != "")
                                {
                                    Console.WriteLine(word);
                                    word = "";
                                }

                                Console.WriteLine(line[i].ToString() + line[i + 1].ToString() + line[i + 2].ToString());
                                i += 2;
                                continue;
                            }
                        }
                        catch (Exception)
                        {
                            word += line[i].ToString();
                            continue;
                        }

                        word += line[i].ToString();
                    }
                    else
                    {
                        if (word != "")
                        {
                            Console.WriteLine(word);
                            word = "";
                        }

                        try
                        {
                            if ((line[i] == '+' && line[i + 1] == '+') || (line[i] == '-' && line[i + 1] == '-') || (line[i] == '<' && line[i + 1] == '=') || (line[i] == '>' && line[i + 1] == '=') || (line[i] == '+' && line[i + 1] == '=') || (line[i] == '-' && line[i + 1] == '=') || (line[i] == '*' && line[i + 1] == '=') || (line[i] == '/' && line[i + 1] == '='))
                            {
                                Console.WriteLine(line[i].ToString() + line[i + 1].ToString());
                                i++;
                                continue;
                            }
                        }
                        catch (Exception)
                        {
                            Console.WriteLine(line[i].ToString());
                            continue;
                        }

                        if (line[i].ToString() != " ")
                        {
                            Console.WriteLine(line[i].ToString());
                            continue;
                        }
                    }

                }


                if (dPoint == ".")
                {
                    Console.WriteLine(word + dPoint + dWord);
                    word = "";
                    dWord = "";
                    dPoint = "";

                    continue;
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
