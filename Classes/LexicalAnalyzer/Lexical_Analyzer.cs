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

            string pattern = "^[+-]?[0-9]*[.][0-9]+$";
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

        // to match only " in first and last palce to validate string in word breaker
        public static bool toMatchString(string word)
        {
            string pattern = "^\"([\\s\\w`~!@#$^&(){}<>^,.?;:\\\\|\\[\\]]|[+]|[-]|[*]|[/]|[%]|[=])*\"$";
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

            // to read the file line by line
            System.IO.StreamReader file = new System.IO.StreamReader(@"D:\UoK\CSSE\CSSE-V\CC\Lab\Projects\Orion\TokenFiles\words.txt");
            Console.WriteLine("(classPart, valuePart, lineNo)\n------------------------------------------");
            while ((line = file.ReadLine()) != null)
            {
                System.Console.WriteLine($"\nLine No {LineNo} > {line}");

                //char by char checking
                for (int i = 0; i < line.Length; i++)
                {
                    //this bloack is for string
                    if ((line[i].ToString() == "\"" || sWord.Contains("\"")) && !comStart)
                    {
                        sWord += line[i].ToString();

                        if (toMatchString(sWord))
                        {
                            if (word != "")
                            {
                                tokenizer(word, LineNo);
                                word = "";
                            }

                            if (dPoint == ".")
                            {
                                tokenizer(dPoint, LineNo);
                                dPoint = "";
                            }

                            if (sWord != "")
                            {
                                tokenizer(sWord, LineNo);
                                sWord = "";
                            }
                        }

                        continue;
                    }

                    //this bloack is for character
                    if (line[i] == '\'' && !comStart)
                    {
                        //try to handle index out of bound error
                        try
                        {
                            //for length of 3 char. i.e. 's'
                            if (line[i + 2] == '\'')
                            {
                                if (word != "")
                                {
                                    tokenizer(word, LineNo);
                                    word = "";
                                }

                                for (int j = 0; j < 3; j++)
                                {
                                    word += line[i + j].ToString();
                                }

                                if (word != "")
                                {
                                    tokenizer(word, LineNo);
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
                                        tokenizer(word, LineNo);
                                        word = "";
                                    }

                                    for (int j = 0; j < 4; j++)
                                    {
                                        word += line[i + j].ToString();
                                    }

                                    if (word != "")
                                    {
                                        tokenizer(word, LineNo);
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
                                        tokenizer(word, LineNo);
                                        word = "";
                                    }

                                    for (int j = 0; j < 3; j++)
                                    {
                                        word += line[i + j].ToString();
                                    }

                                    if (word != "")
                                    {
                                        tokenizer(word, LineNo);
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
                                    tokenizer(word, LineNo);
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
                                tokenizer(word, LineNo);
                                word = "";
                            }

                            word += line[i].ToString();

                            continue;
                        }
                    }
                    
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
                                tokenizer(word, LineNo);
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
                                    tokenizer(word, LineNo);
                                    word = "";
                                }

                                tokenizer(dPoint, LineNo);
                                dPoint = "";
                                word += line[i].ToString();
                                continue;
                            }
                        }
                        else if(line[i] == '.' && dPoint == ".")
                        {
                            if (isInt(word))
                            {
                                tokenizer((word + dPoint + dWord), LineNo);
                                word = "";
                                dPoint = "";
                                dWord = "";
                                dPoint += line[i].ToString();
                                continue;
                            }

                            if (word == "")
                            {
                                tokenizer((dPoint + dWord), LineNo);
                                dPoint = "";
                                dWord = "";
                                dPoint += line[i].ToString();
                                continue;
                            }
                            
                        }
                    }

                    //for space, punc, oper and other words
                    if (line[i].ToString() != " " && line[i].ToString() != "\t" && !isOperator(line[i].ToString()) && !isPunctuator(line[i].ToString()))
                    {
                        //for alphabetic operators. i.e. is, or (length 2)
                        try
                        {
                            if ((line[i] == 'i' && line[i + 1] == 's') || (line[i] == 'o' && line[i + 1] == 'r'))
                            {
                                if (word != "")
                                {
                                    tokenizer(word, LineNo);
                                    word = "";
                                }

                                tokenizer((line[i].ToString() + line[i + 1].ToString()), LineNo);
                                i++;
                                continue;
                            }
                        }
                        catch (Exception)
                        {
                            word += line[i].ToString();
                            tokenizer(word, LineNo);
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
                                    tokenizer(word, LineNo);
                                    word = "";
                                }

                                tokenizer((line[i].ToString() + line[i + 1].ToString() + line[i + 2].ToString()), LineNo);
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
                            tokenizer(word, LineNo);
                            word = "";
                        }

                        try
                        {
                            if ((line[i] == '+' && line[i + 1] == '+') || (line[i] == '-' && line[i + 1] == '-') || (line[i] == '<' && line[i + 1] == '=') || (line[i] == '>' && line[i + 1] == '=') || (line[i] == '+' && line[i + 1] == '=') || (line[i] == '-' && line[i + 1] == '=') || (line[i] == '*' && line[i + 1] == '=') || (line[i] == '/' && line[i + 1] == '='))
                            {
                                tokenizer((line[i].ToString() + line[i + 1].ToString()), LineNo);
                                i++;
                                continue;
                            }
                        }
                        catch (Exception)
                        {
                            tokenizer(line[i].ToString(), LineNo);
                            continue;
                        }

                        if (line[i].ToString() != " " && line[i].ToString() != "\t")
                        {
                            tokenizer(line[i].ToString(), LineNo);
                            continue;
                        }
                    }

                }


                if (dPoint == ".")
                {
                    tokenizer((word + dPoint + dWord), LineNo);
                    word = "";
                    dWord = "";
                    dPoint = "";
                }
                

                if (word != "")
                {
                    tokenizer(word, LineNo);
                    word = "";
                }

                if (sWord != "")
                {
                    tokenizer(sWord, LineNo);
                    sWord = "";
                }

                LineNo++;
            }

            file.Close();
        }

        public static void tokenizer(string word, int line)
        {
            // to write output in a text file
            System.IO.StreamWriter file = new System.IO.StreamWriter(@"D:\UoK\CSSE\CSSE-V\CC\Lab\Projects\Orion\TokenFiles\tokens.txt", true);
            
            //(classPart, valuePart, lineNo)

            if (isInt(word))
            {
                Console.WriteLine($"(int_const, {word}, {line})");
                file.WriteLine($"(int_const, {word}, {line})");
            }
            else if (isFloat(word))
            {
                Console.WriteLine($"(float_const, {word}, {line})");
                file.WriteLine($"(float_const, {word}, {line})");
            }
            else if (isChar(word))
            {
                Console.WriteLine($"(char_const, {word}, {line})");
                file.WriteLine($"(char_const, {word}, {line})");
            }
            else if (isString(word))
            {
                Console.WriteLine($"(str_const, {word}, {line})");
                file.WriteLine($"(str_const, {word}, {line})");
            }
            else if (isKW(word))
            {
                Console.WriteLine($"({word}, , {line}) \t > KW");
                file.WriteLine($"({word}, , {line}) \t > KW");
            }
            else if (isOperator(word))
            {
                Console.WriteLine($"({word}, , {line}) \t > Oper");
                file.WriteLine($"({word}, , {line}) \t > Oper");
            }
            else if (isPunctuator(word))
            {
                Console.WriteLine($"({word}, , {line}) \t > Punct");
                file.WriteLine($"({word}, , {line}) \t > Punct");
            }
            else if (isID(word))
            {
                Console.WriteLine($"(ID, {word}, {line})");
                file.WriteLine($"(ID, {word}, {line})");
            }
            else
            {
                if (word != " ")
                {
                    Console.WriteLine($"(Invalid, {word}, {line})");
                    file.WriteLine($"(Invalid, {word}, {line})");
                }
            }

            file.Close();
        }

    }
}
