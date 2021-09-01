using System;
using System.Text.RegularExpressions;

namespace Orion.Classes.LexicalAnalyzer
{
    class Lexical_Analyzer
    {
        static string[,] keywords = { { "num", "DT" }, { "decimal", "DT" }, { "char", "DT" }, { "string", "DT" }, { "loop", "loop" }, { "until", "until" }, { "when", "when" }, { "lest", "lest" }, { "refund", "refund" }, { "skip", "skip" }, { "jump", "jump" }, { "toggle", "toggle" }, { "check", "check" }, { "nonmatch", "nonmatch" }, { "main", "main" }, { "void", "void" }, { "none", "none" }, { "bool", "DT" }, { "true", "bool_const" }, { "false", "bool_const" }, { "general", "AM" }, { "personal", "AM" }, { "protected", "AM" }, { "class", "class" }, { "fresh", "fresh" }, { "inactive", "Non_AM" }, { "final", "Non_AM" }, { "symbolic", "Non_AM" }, { "THIS", "ref_var" }, { "BASE", "ref_var" }, { "inherits", "inherits" }, { "implements", "implements" }, { "interface", "interface" }, { "try", "try" }, { "except", "except" }, { "finally", "finally" }, { "series", "DS" }, { "table", "DS" }, { "tableD", "DS" }, { "@", "@" }, { "@@", "@@" } };
        static string[,] operators = { { "!", "!" }, { "++", "inc_dec" }, { "--", "inc_dec" }, { "+", "PM" }, { "-", "PM" }, { "*", "MDM" }, { "/", "MDM" }, { "%", "MDM" }, { "is", "ROP" }, { "not", "ROP" }, { "<", "ROP" }, { ">", "ROP" }, { "<=", "ROP" }, { ">=", "ROP" }, { "and", "and" }, { "or", "or" }, { "=", "assign_opr" }, { "+=", "comp_assign" }, { "-=", "comp_assign" }, { "*=", "comp_assign" }, { "/=", "comp_assign" } };
        static string[,] punctuators = { { ",", "," }, { ":", ":" }, { "(", "(" }, { ")", ")" }, { "{", "{" }, { "}", "}" }, { "[", "[" }, { "]", "]" }, { ".", "." }, { "#", "#" } };

        public static bool isDT(string word)
        {
            bool result = false;
            for (int i = 0; i < 41; i++)
            {
                if (word == keywords[i, 0] && keywords[i, 1] == "DT")
                {
                    result = true;
                }
            }

            return result;
        }

        public static bool isDS(string word)
        {
            bool result = false;
            for (int i = 0; i < 41; i++)
            {
                if (word == keywords[i, 0] && keywords[i, 1] == "DS")
                {
                    result = true;
                }
            }

            return result;
        }

        public static bool isBool(string word)
        {
            bool result = false;
            for (int i = 0; i < 41; i++)
            {
                if (word == keywords[i, 0] && keywords[i, 1] == "bool_const")
                {
                    result = true;
                }
            }

            return result;
        }

        public static bool isRef_Var(string word)
        {
            bool result = false;
            for (int i = 0; i < 41; i++)
            {
                if (word == keywords[i, 0] && keywords[i, 1] == "ref_var")
                {
                    result = true;
                }
            }

            return result;
        }

        public static bool isInt(string word)
        {

            string pattern = "^[+-]?[0-9]+$";
            Regex regex = new Regex(pattern);
            bool result = regex.IsMatch(word);

            return result;
        }

        public static bool isDecimal(string word)
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
            // first check ID may not any reserved words

            bool result = false;
            if (!isKW(word))
            {
                string pattern = "^[a-zA-Z_][\\w]*$";
                Regex regex = new Regex(pattern);
                result = regex.IsMatch(word);
            }

            return result;
        }

        public static bool isKW(string word)
        {
            bool result = false;
            for (int i = 0; i < 41; i++)
            {
                if (word == keywords[i, 0] || word == keywords[i, 1])
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

        public static bool isPM(string word)
        {
            bool result = false;
            for (int i = 0; i < 21; i++)
            {
                if (word == operators[i, 0] && operators[i, 1] == "PM")
                {
                    result = true;
                }
            }

            return result;
        }

        public static bool isMDM(string word)
        {
            bool result = false;
            for (int i = 0; i < 21; i++)
            {
                if (word == operators[i, 0] && operators[i, 1] == "MDM")
                {
                    result = true;
                }
            }

            return result;
        }

        public static bool isROP(string word)
        {
            bool result = false;
            for (int i = 0; i < 21; i++)
            {
                if (word == operators[i, 0] && operators[i, 1] == "ROP")
                {
                    result = true;
                }
            }

            return result;
        }

        public static bool isComp_assign(string word)
        {
            bool result = false;
            for (int i = 0; i < 21; i++)
            {
                if (word == operators[i, 0] && operators[i, 1] == "comp_assign")
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
            System.IO.StreamReader file = new System.IO.StreamReader(@"TokenFiles\words.txt");
            //Console.WriteLine("(classPart, valuePart, lineNo)\n------------------------------------------");
            while ((line = file.ReadLine()) != null)
            {
                //System.Console.WriteLine($"\nLine No {LineNo} > {line}");

                //char by char checking
                for (int i = 0; i < line.Length; i++)
                {
                    //this block is for string
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

                    //this block is for character
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

                    //this block is for single line comment
                    if (line[i] == '@' && line[i + 1] != '@')
                    {
                        break;
                    }

                    //this block is for multi line comments
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

                    //this block is for decimal No
                    if (line[i] == '.' || dPoint == ".")
                    {
                        // for terminator
                        if (line[i] == '#')
                        {
                            tokenizer((word + dPoint + dWord), LineNo);
                            word = "";
                            dPoint = "";
                            dWord = "";
                            tokenizer(line[i].ToString(), LineNo);
                            continue;
                        }
                        

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
            System.IO.StreamWriter file = new System.IO.StreamWriter(@"TokenFiles\tokens.txt", true);
            
            //(classPart, valuePart, lineNo)

            if (isInt(word))
            {
                //Console.WriteLine($"(int_const, {word}, {line})");
                file.WriteLine($"(int_const, {word}, {line})");
                SyntaxAnalyzer.Syntax_Analyzer.AddToken("int_const", line);
                SemanticAnalyzer.Semantic_Analyzer.AddToken("int_const", line);
            }
            else if (isDecimal(word))
            {
                //Console.WriteLine($"(dec_constant, {word}, {line})");
                file.WriteLine($"(dec_const, {word}, {line})");
                SyntaxAnalyzer.Syntax_Analyzer.AddToken("dec_const", line);
                SemanticAnalyzer.Semantic_Analyzer.AddToken("dec_const", line);
            }
            else if (isChar(word))
            {
                //Console.WriteLine($"(char_const, {word}, {line})");
                file.WriteLine($"(char_const, {word}, {line})");
                SyntaxAnalyzer.Syntax_Analyzer.AddToken("char_const", line);
                SemanticAnalyzer.Semantic_Analyzer.AddToken("char_const", line);
            }
            else if (isString(word))
            {
                //Console.WriteLine($"(str_const, {word}, {line})");
                file.WriteLine($"(str_const, {word}, {line})");
                SyntaxAnalyzer.Syntax_Analyzer.AddToken("str_const", line);
                SemanticAnalyzer.Semantic_Analyzer.AddToken("str_const", line);
            }
            else if (isBool(word))
            {
                //Console.WriteLine($"(str_const, {word}, {line})");
                file.WriteLine($"(bool_const, {word}, {line})");
                SyntaxAnalyzer.Syntax_Analyzer.AddToken("bool_const", line);
                SemanticAnalyzer.Semantic_Analyzer.AddToken("bool_const", line);
            }
            else if (isKW(word))
            {
                //Console.WriteLine($"({word}, , {line}) \t > KW");
                file.WriteLine($"({word}, , {line}) \t > KW");

                if (isDT(word))
                {                    
                    SyntaxAnalyzer.Syntax_Analyzer.AddToken("DT", line);
                    SemanticAnalyzer.Semantic_Analyzer.AddToken(word, line);
                }
                else if (isDS(word))
                {
                    SyntaxAnalyzer.Syntax_Analyzer.AddToken("DS", line);
                    SemanticAnalyzer.Semantic_Analyzer.AddToken("DS", line);
                }
                else if (isBool(word))
                {
                    SyntaxAnalyzer.Syntax_Analyzer.AddToken("bool_const", line);
                    SemanticAnalyzer.Semantic_Analyzer.AddToken("bool_const", line);
                }
                else if (isRef_Var(word))
                {
                    SyntaxAnalyzer.Syntax_Analyzer.AddToken("ref_var", line);
                    SemanticAnalyzer.Semantic_Analyzer.AddToken("ref_var", line);
                }
                else
                {
                    SyntaxAnalyzer.Syntax_Analyzer.AddToken(word, line);
                    SemanticAnalyzer.Semantic_Analyzer.AddToken(word, line);
                }
            }
            else if (isOperator(word))
            {
                //Console.WriteLine($"({word}, , {line}) \t > Oper");
                file.WriteLine($"({word}, , {line}) \t > Oper");

                if (isPM(word))
                {
                    SyntaxAnalyzer.Syntax_Analyzer.AddToken("PM", line);
                    SemanticAnalyzer.Semantic_Analyzer.AddToken("PM", line);
                }
                else if (isROP(word))
                {
                    SyntaxAnalyzer.Syntax_Analyzer.AddToken("ROP", line);
                    SemanticAnalyzer.Semantic_Analyzer.AddToken("ROP", line);
                }
                else if (isMDM(word))
                {
                    SyntaxAnalyzer.Syntax_Analyzer.AddToken("MDM", line);
                    SemanticAnalyzer.Semantic_Analyzer.AddToken("MDM", line);
                }
                else if (isComp_assign(word))
                {
                    SyntaxAnalyzer.Syntax_Analyzer.AddToken("comp_assign", line);
                    SemanticAnalyzer.Semantic_Analyzer.AddToken("comp_assign", line);
                }
                else
                {
                    SyntaxAnalyzer.Syntax_Analyzer.AddToken(word, line);
                    SemanticAnalyzer.Semantic_Analyzer.AddToken(word, line);
                }
            }
            else if (isPunctuator(word))
            {
                //Console.WriteLine($"({word}, , {line}) \t > Punct");
                file.WriteLine($"({word}, , {line}) \t > Punct");
                SyntaxAnalyzer.Syntax_Analyzer.AddToken(word, line);
                SemanticAnalyzer.Semantic_Analyzer.AddToken(word, line);
            }
            else if (isID(word))
            {
                //Console.WriteLine($"(ID, {word}, {line})");
                file.WriteLine($"(ID, {word}, {line})");
                SyntaxAnalyzer.Syntax_Analyzer.AddToken("ID", line);
                SemanticAnalyzer.Semantic_Analyzer.AddToken(word, line);
            }
            else
            {
                if (word != " ")
                {
                    Console.WriteLine($"(Invalid Token, {word}, {line})");
                    file.WriteLine($"(Invalid, {word}, {line})");
                }
            }

            file.Close();
            
        }

        public static void LexicalAnalyzer()
        {
            // for first line/hedaing in output file
            System.IO.StreamWriter file = new System.IO.StreamWriter(@"TokenFiles\tokens.txt");
            file.WriteLine("(classPart, valuePart, lineNo)\n------------------------------------------\n");
            file.Close();

            // call word breaker to generate tokens
            wordBreaker();
        }
    }
}
