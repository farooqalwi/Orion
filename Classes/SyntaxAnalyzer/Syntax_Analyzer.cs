using System;
using System.Collections.Generic;

namespace Orion.Classes.SyntaxAnalyzer
{
    class Syntax_Analyzer
    {
        // to go forward token
        static int index = 0;

        //to store all tokens in a list for syntax analyzer to parse tokens
        public static List<string> tokens = new List<string>();
        public static List<int> lineNo = new List<int>();

        public static void AddToken(string word, int line)
        {
            SyntaxAnalyzer.Syntax_Analyzer.tokens.Add(word);
            SyntaxAnalyzer.Syntax_Analyzer.lineNo.Add(line);
        }

        // print tokens received from lexical analyzer
        public static void printTokens()
        {
            for (int i = 0; i < lineNo.Count; i++)
            {
                Console.WriteLine($"{i} > {lineNo[i]}, {tokens[i]}");
            }

            Console.WriteLine(tokens[tokens.Count - 1]);

        }

        public static bool OE(string token)
        {
            if (tokens[index] == "ID" || tokens[index] == "!" || tokens[index] == "++" || tokens[index] == "--" || tokens[index] == "(" || tokens[index] == "int_const" || tokens[index] == "dec_const" || tokens[index] == "str_const" || tokens[index] == "bool_const" || tokens[index] == "char_const" || tokens[index] == "none")
            {
                if (AE(tokens[index]))
                {
                    if (OE_(tokens[index]))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool OE_(string token)
        {
            if (tokens[index] == "or")
            {
                index++;
                if (AE(tokens[index]))
                {
                    if (OE_(tokens[index]))
                    {
                        return true;
                    }
                }
            }
            else if (tokens[index] == "," || tokens[index] == ")" || tokens[index] == "#")
            {
                return true;
            }

            return false;
        }

        public static bool AE(string token)
        {
            if (tokens[index] == "ID" || tokens[index] == "!" || tokens[index] == "++" || tokens[index] == "--" || tokens[index] == "(" || tokens[index] == "int_const" || tokens[index] == "dec_const" || tokens[index] == "str_const" || tokens[index] == "bool_const" || tokens[index] == "char_const" || tokens[index] == "none")
            {
                if (RE(tokens[index]))
                {
                    if (AE_(tokens[index]))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        public static bool AE_(string token)
        {
            if (tokens[index] == "and")
            {
                index++;
                if (RE(tokens[index]))
                {
                    if (AE_(tokens[index]))
                    {
                        return true;
                    }
                }
            }
            else if (tokens[index] == "or" || tokens[index] == ")" || tokens[index] == "," || tokens[index] == "#")
            {
                return true;
            }

            return false;
        }

        public static bool RE(string token)
        {
            if (tokens[index] == "ID" || tokens[index] == "!" || tokens[index] == "++" || tokens[index] == "--" || tokens[index] == "(" || tokens[index] == "int_const" || tokens[index] == "dec_const" || tokens[index] == "str_const" || tokens[index] == "bool_const" || tokens[index] == "char_const" || tokens[index] == "none")
            {
                if (E(tokens[index]))
                {
                    if (RE_(tokens[index]))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool RE_(string token)
        {
            if (tokens[index] == "ROP")
            {
                index++;
                if (E(tokens[index]))
                {
                    if (RE_(tokens[index]))
                    {
                        return true;
                    }
                }
            }
            else if (tokens[index] == "and" || tokens[index] == "or" || tokens[index] == ")" || tokens[index] == "," || tokens[index] == "#")
            {
                return true;
            }

            return false;
        }

        public static bool E(string token)
        {
            if (tokens[index] == "ID" || tokens[index] == "!" || tokens[index] == "++" || tokens[index] == "--" || tokens[index] == "(" || tokens[index] == "int_const" || tokens[index] == "dec_const" || tokens[index] == "str_const" || tokens[index] == "bool_const" || tokens[index] == "char_const" || tokens[index] == "none")
            {
                if (T(tokens[index]))
                {
                    if (E_(tokens[index]))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool E_(string token)
        {
            if (tokens[index] == "PM")
            {
                index++;
                if (T(tokens[index]))
                {
                    if (E_(tokens[index]))
                    {
                        return true;
                    }
                }
            }
            else if (tokens[index] == "ROP" || tokens[index] == "and" || tokens[index] == "or" || tokens[index] == ")" || tokens[index] == "," || tokens[index] == "#")
            {
                return true;
            }

            return false;
        }

        public static bool T(string token)
        {
            if (tokens[index] == "ID" || tokens[index] == "!" || tokens[index] == "++" || tokens[index] == "--" || tokens[index] == "(" || tokens[index] == "int_const" || tokens[index] == "dec_const" || tokens[index] == "str_const" || tokens[index] == "bool_const" || tokens[index] == "char_const" || tokens[index] == "none")
            {
                if (F(tokens[index]))
                {
                    if (T_(tokens[index]))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool T_(string token)
        {
            if (tokens[index] == "MDM")
            {
                index++;
                if (F(tokens[index]))
                {
                    if (T_(tokens[index]))
                    {
                        return true;
                    }
                }
            }
            else if (tokens[index] == "PM" || tokens[index] == "ROP" || tokens[index] == "and" || tokens[index] == "or" || tokens[index] == ")" || tokens[index] == "," || tokens[index] == "#")
            {
                return true;
            }

            return false;
        }

        public static bool F(string token)
        {
            if (tokens[index] == "ID")
            {
                index++;
                if (F_(tokens[index]))
                {
                    return true;
                }
            }
            else if (tokens[index] == "(")
            {
                if (OE(tokens[index]))
                {
                    return true;
                }
            }
            else if (tokens[index] == "int-const" || tokens[index] == "str-const" || tokens[index] == "dec-const" || tokens[index] == "bool-const" || tokens[index] == "char-const" || tokens[index] == "none")
            {
                if (CONST(tokens[index]))
                {
                    return true;
                }
            }
            else if (tokens[index] == "!")
            {
                index++;

                if (F(tokens[index]))
                {
                    return true;
                }
            }
            else if (tokens[index] == "++" || tokens[index] == "--")
            {                
                if (inc_dec(tokens[index]))
                {
                    if (tokens[index] == "ID")
                    {
                        index++;
                        if (X(tokens[index]))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public static bool F_(string token)
        {
            if (tokens[index] == ".")
            {
                index++;
                if (tokens[index] == "ID")
                {
                    index++;
                    if (F(tokens[index]))
                    {
                        return true;
                    }
                }
            }
            else if (tokens[index] == "(")
            {
                if (PL(tokens[index]))
                {
                    if (F_list(tokens[index]))
                    {
                        return true;
                    }
                }
            }
            else if (tokens[index] == "++" || tokens[index] == "--")
            {
                if (inc_dec(tokens[index]))
                {
                    return true;
                }
            }
            else if (tokens[index] == "MDM" || tokens[index] == "PM" || tokens[index] == "ROP" || tokens[index] == "and" || tokens[index] == "or" || tokens[index] == ")" || tokens[index] == "," || tokens[index] == "#")
            {
                return true;
            }

            return false;
        }

        public static bool F_list(string token)
        {
            if (tokens[index] == ".")
            {
                index++;
                if (tokens[index] == "ID")
                {
                    index++;
                    if (F_(tokens[index]))
                    {
                        return true;
                    }
                }

            }
            else if (tokens[index] == "MDM" || tokens[index] == "PM" || tokens[index] == "ROP" || tokens[index] == "and" || tokens[index] == "or" || tokens[index] == ")" || tokens[index] == "," || tokens[index] == "#")
            {
                return true;
            }

            return false;
        }

        //in CFG it is const not CONST
        public static bool CONST(string token)
        {
            if (tokens[index] == "int-const" || tokens[index] == "str-const" || tokens[index] == "dec-const" || tokens[index] == "bool-const" || tokens[index] == "char-const" || tokens[index] == "none")
            {
                index++;
                return true;
            }

            return false;
        }

        public static bool PL(string token)
        {
            if (tokens[index] == "ID" || tokens[index] == "!" || tokens[index] == "++" || tokens[index] == "--" || tokens[index] == "(" || tokens[index] == "int_const" || tokens[index] == "dec_const" || tokens[index] == "str_const" || tokens[index] == "bool_const" || tokens[index] == "char_const" || tokens[index] == "none")
            {
                if (OE(tokens[index]))
                {
                    if (PL2(tokens[index]))
                    {
                        return true;
                    }
                }
            }
            else if (tokens[index] == ")")
            {
                return true;
            }

            return false;
        }

        public static bool PL2(string token)
        {
            if (tokens[index] == ",")
            {
                index++;
                if (OE(tokens[index]))
                {
                    if (PL2(tokens[index]))
                    {
                        return true;
                    }
                }
            }
            else if (tokens[index] == ")")
            {
                return true;
            }

            return false;
        }

        public static bool assign_st(string token)
        {
            if (tokens[index] == "ID")
            {
                index++;
                if (X(tokens[index]))
                {
                    if (assign_opr(tokens[index]))
                    {
                        if (OE(tokens[index]))
                        {
                            if (tokens[index] == "#")
                            {
                                index++;
                                return true;
                            }
                        }
                    }
                }

            }

            return false;
        }

        public static bool assign_opr(string token)
        {
            if (tokens[index] == "=")
            {
                index++;
                return true;
            }
            else if (tokens[index] == "comp_assign")
            {
                index++;
                return true;

            }

            return false;
        }

        public static bool X(string token)
        {
            if (tokens[index] == ".")
            {
                index++;
                if (tokens[index] == "ID")
                {
                    index++;
                    if (X(tokens[index]))
                    {
                        return true;
                    }
                }
            }
            else if (tokens[index] == "[")
            {
                if (Index(tokens[index]))
                {
                    if (X3(tokens[index]))
                    {
                        return true;
                    }
                }
            }
            else if (tokens[index] == "(")
            {
                if (PL(tokens[index]))
                {
                    if (X2(tokens[index]))
                    {
                        return true;
                    }
                }
            }
            else if (tokens[index] == "MDM" || tokens[index] == "PM" || tokens[index] == "ROP" || tokens[index] == "and" || tokens[index] == "or" || tokens[index] == ")" || tokens[index] == "," || tokens[index] == "#" || tokens[index] == "=" || tokens[index] == "comp_assign" || tokens[index] == "++" || tokens[index] == "--")
            {
                return true;
            }

            return false;
        }

        public static bool X2(string token)
        {
            if (tokens[index] == ".")
            {
                index++;
                if (tokens[index] == "ID")
                {
                    index++;
                    if (X(tokens[index]))
                    {
                        return true;
                    }
                }
            }
            else if (tokens[index] == "[")
            {
                if (Index(tokens[index]))
                {
                    if (X3(tokens[index]))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool X3(string token)
        {
            if (tokens[index] == ".")
            {
                index++;
                if (tokens[index] == "ID")
                {
                    index++;
                    if (X(tokens[index]))
                    {
                        return true;
                    }
                }
            }
            else if (tokens[index] == "MDM" || tokens[index] == "PM" || tokens[index] == "ROP" || tokens[index] == "and" || tokens[index] == "or" || tokens[index] == ")" || tokens[index] == "," || tokens[index] == "#" || tokens[index] == "=" || tokens[index] == "comp_assign" || tokens[index] == "++" || tokens[index] == "--")
            {
                return true;
            }

            return false;
        }

        //in CFG it is index not Index
        public static bool Index(string token)
        {
            if (tokens[index] == "ID")
            {
                index++;
                return true;
            }
            else if (tokens[index] == "int_const")
            {
                index++;
                return true;
            }

            return false;
        }

        public static bool inc_dec_st(string token)
        {
            if (tokens[index] == "++" || tokens[index] == "--")
            {
                if (inc_dec(tokens[index]))
                {
                    if (tokens[index] == "ID")
                    {
                        index++;
                        if (X(tokens[index]))
                        {
                            if (tokens[index] == "#")
                            {
                                index++;
                                return true;
                            }
                        }

                    }
                }
            }
            else if (tokens[index] == "ID")
            {
                index++;
                if (X(tokens[index]))
                {
                    if (inc_dec(tokens[index]))
                    {
                        if (tokens[index] == "#")
                        {
                            return true;
                        }
                    }
                }

            }

            return false;
        }

        public static bool inc_dec(string token)
        {
            if (tokens[index] == "++" || tokens[index] == "--")
            {
                index++;
                return true;
            }

            return false;
        }

        public static bool if_else(string token)
        {
            return false;
        }

        //in CFG it is else not _else
        public static bool _else(string token)
        {
            return false;
        }

        public static bool obj_dec(string token)
        {
            if (tokens[index] == "ID")
            {
                index++;
                if (obj_dec_list(tokens[index]))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool obj_dec_list(string token)
        {
            if (tokens[index] == "ID")
            {
                index++;
                if (obj_dec_list1(tokens[index]))
                {
                    return true;
                }
            }
            else if (tokens[index] == "=")
            {
                index++;
                if (obj_dec_list2(tokens[index]))
                {
                    if (tokens[index] == "#")
                    {
                        index++;
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool obj_dec_list1(string token)
        {
            if (tokens[index] == "#")
            {
                index++;
                return true;
            }
            else if (tokens[index] == "=")
            {
                index++;
                if (obj_dec_list2(tokens[index]))
                {
                    return true;
                }
            }
            else if (tokens[index] == ",")
            {
                index++;
                if (tokens[index] == "ID")
                {
                    index++;
                    if (obj_dec_list1(tokens[index]))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool obj_dec_list2(string token)
        {
            if (tokens[index] == "fresh")
            {
                index++;
                if (tokens[index] == "ID")
                {
                    index++;
                    return true;
                }
            }
            else if (tokens[index] == "ID")
            {
                index++;
                return true;
            }

            return false;
        }

        public static bool Class_dec(string token)
        {
            if (tokens[index] == "general" || tokens[index] == "personal" || tokens[index] == "protected" || tokens[index] == "symbolic" || tokens[index] == "final" || tokens[index] == "inacive" || tokens[index] == "class")
            {
                if (AM(tokens[index]))
                {
                    if (Non_AM(tokens[index]))
                    {
                        if (tokens[index] == "class")
                        {
                            index++;
                            if (tokens[index] == "ID")
                            {
                                index++;
                                if (extends(tokens[index]))
                                {
                                    if (tokens[index] == "{")
                                    {
                                        index++;
                                        if (CB(tokens[index]))
                                        {
                                            if (tokens[index] == "}")
                                            {
                                                index++;
                                                return true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return false;
        }

        public static bool Non_AM(string token)
        {
            if (tokens[index] == "symbolic" || tokens[index] == "final" || tokens[index] == "inactive")
            {
                index++;
                return true;
            }
            else if (tokens[index] == "class" || tokens[index] == "void" || tokens[index] == "DT" || tokens[index] == "ID")
            {
                return true;
            }

            return false;
        }

        public static bool extends(string token)
        {
            if (tokens[index] == "inherits")
            {
                index++;
                if (tokens[index] == "ID")
                {
                    index++;
                    return true;
                }
            }
            else if (tokens[index] == "{")
            {
                return true;
            }

            return false;
        }

        public static bool dec(string token)
        {
            if (tokens[index] == "DT")
            {
                index++;
                if (tokens[index] == "ID")
                {
                    index++;
                    if (init(tokens[index]))
                    {
                        if (dec_list(tokens[index]))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public static bool init(string token)
        {
            if (tokens[index] == "=")
            {
                index++;
                if (OE(tokens[index]))
                {
                    return true;
                }
            }
            else if (tokens[index] == "#" || tokens[index] == ",")
            {
                return true;
            }

            return false;
        }

        public static bool dec_list(string token)
        {
            if (tokens[index] == "#")
            {
                index++;
                return true;
            }
            else if (tokens[index] == ",")
            {
                index++;
                if (tokens[index] == "ID")
                {
                    index++;
                    if (init(tokens[index]))
                    {
                        if (dec_list(tokens[index]))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public static bool func_def(string token)
        {
            if (tokens[index] == "general" || tokens[index] == "personal" || tokens[index] == "protected" || tokens[index] == "symbolic" || tokens[index] == "final" || tokens[index] == "inacive" || tokens[index] == "void" || tokens[index] == "DT" || tokens[index] == "ID")
            {
                if (AM(tokens[index]))
                {
                    if (Non_AM(tokens[index]))
                    {
                        if (func_ret_type(tokens[index]))
                        {
                            if (tokens[index] == "ID")
                            {
                                index++;
                                if (tokens[index] == "(")
                                {
                                    index++;
                                    if (define(tokens[index]))
                                    {
                                        if (tokens[index] == ")")
                                        {
                                            index++;
                                            if (tokens[index] == "{")
                                            {
                                                index++;
                                                if (MST(tokens[index]))
                                                {
                                                    if (tokens[index] == "}")
                                                    {
                                                        index++;
                                                        return true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return false;
        }

        private static bool define(string token)
        {
            if (tokens[index] == "DT")
            {
                index++;
                if (tokens[index] == "ID")
                {
                    index++;
                    if (define1(tokens[index]))
                    {
                        return true;
                    }
                }
            }
            else if (tokens[index] == ")")
            {
                return true;
            }

            return false;
        }

        private static bool define1(string token)
        {
            if (tokens[index] == ",")
            {
                index++;
                if (define1(tokens[index]))
                {
                    return true;
                }
            }
            else if (tokens[index] == ")")
            {
                return true;
            }

            return false;
        }

        public static bool AM(string token)
        {
            if (tokens[index] == "general" || tokens[index] == "personal" || tokens[index] == "protected")
            {
                index++;
                return true;
            }
            else if (tokens[index] == "symbolic" || tokens[index] == "final" || tokens[index] == "inactive" || tokens[index] == "class" || tokens[index] == "void" || tokens[index] == "DT" || tokens[index] == "ID")
            {
                return true;
            }

            return false;
        }

        public static bool func_ret_type(string token)
        {
            if (tokens[index] == "void" || tokens[index] == "DT")
            {
                index++;
                return true;
            }
            else if (tokens[index] == "ID")
            {
                return true;
            }

            return false;
        }

        public static bool ret_line(string token)
        {
            if (tokens[index] == "refund")
            {
                index++;
                if (ret(tokens[index]))
                {
                    if (tokens[index] == "#")
                    {
                        index++;
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool ret(string token)
        {
            if (tokens[index] == "ID" || tokens[index] == "!" || tokens[index] == "++" || tokens[index] == "--" || tokens[index] == "(" || tokens[index] == "int_const" || tokens[index] == "dec_const" || tokens[index] == "str_const" || tokens[index] == "bool_const" || tokens[index] == "char_const" || tokens[index] == "none")
            {
                if (OE(tokens[index]))
                {
                    return true;
                }
            }
            else if (tokens[index] == "#")
            {
                return true;
            }

            return false;
        }

        public static bool func_call(string token)
        {
            if (tokens[index] == "ID")
            {
                index++;
                if (Y(tokens[index]))
                {
                    if (tokens[index] == "(")
                    {
                        index++;
                        if (PL(tokens[index]))
                        {
                            if (tokens[index] == ")")
                            {
                                index++;
                                if (tokens[index] == "#")
                                {
                                    index++;
                                    return true;
                                }
                            }
                        }
                    }
                }
            }

            return false;
        }

        public static bool Y(string token)
        {
            if (tokens[index] == ".")
            {
                index++;
                if (tokens[index] == "ID")
                {
                    index++;
                    if (Y(tokens[index]))
                    {
                        return true;
                    }
                }
            }
            else if (tokens[index] == "(")
            {
                return true;
            }

            return false;
        }

        public static bool for_st(string token)
        {
            if (tokens[index] == "loop")
            {
                index++;
                if (tokens[index] == "(")
                {
                    index++;
                    if (c1(tokens[index]))
                    {
                        if (tokens[index] == ",")
                        {
                            index++;
                            if (c2(tokens[index]))
                            {
                                if (tokens[index] == ")")
                                {
                                    index++;
                                    if (body(tokens[index]))
                                    {
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return false;
        }

        public static bool c1(string token)
        {
            if (tokens[index] == "ID" || tokens[index] == "!" || tokens[index] == "++" || tokens[index] == "--" || tokens[index] == "(" || tokens[index] == "int_const" || tokens[index] == "dec_const" || tokens[index] == "str_const" || tokens[index] == "bool_const" || tokens[index] == "char_const" || tokens[index] == "none")
            {
                if (OE(tokens[index]))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool c2(string token)
        {
            if (tokens[index] == "ID")
            {
                index++;
                if (X(tokens[index]))
                {
                    if (c3(tokens[index]))
                    {
                        return true;
                    }
                }
            }
            else if (tokens[index] == "++" || tokens[index] == "--")
            {
                if (inc_dec(tokens[index]))
                {
                    if (tokens[index] == "ID")
                    {
                        index++;
                        if (X(tokens[index]))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private static bool c3(string token)
        {
            if (tokens[index] == "=" || tokens[index] == "comp_assign")
            {
                if (assign_opr(tokens[index]))
                {
                    if (OE(tokens[index]))
                    {
                        return true;
                    }
                }
            }
            else if (tokens[index] == "++" || tokens[index] == "--")
            {
                if (inc_dec(tokens[index]))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool While_st(string token)
        {
            if (tokens[index] == "until")
            {
                index++;
                if (tokens[index] == "(")
                {
                    index++;
                    if (OE(tokens[index]))
                    {
                        if (tokens[index] == ")")
                        {
                            index++;
                            if (body(tokens[index]))
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        public static bool arr1(string token)
        {
            if (tokens[index] == "DT")
            {
                index++;
                if (tokens[index] == "ID")
                {
                    index++;
                    if (arr2(tokens[index]))
                    {
                        if (tokens[index] == "=")
                        {
                            index++;
                            if (tokens[index] == "[")
                            {
                                index++;
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        public static bool arr2(string token)
        {
            if (tokens[index] == "int-const" || tokens[index] == "str-const" || tokens[index] == "dec-const" || tokens[index] == "bool-const" || tokens[index] == "char-const" || tokens[index] == "none")
            {
                if (CONST(tokens[index]))
                {
                    if (arr3(tokens[index]))
                    {
                        return true;
                    }
                }
            }
            else if (tokens[index] == "[")
            {
                index++;
                if (CONST(tokens[index]))
                {
                    if (arr4(tokens[index]))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool arr3(string token)
        {
            if (tokens[index] == ",")
            {
                index++;
                if (CONST(tokens[index]))
                {
                    if (arr3(tokens[index]))
                    {
                        return true;
                    }
                }
            }
            else if (tokens[index] == "]")
            {
                index++;
                if (tokens[index] == "#")
                {
                    index++;
                    return true;
                }
            }

            return false;
        }

        public static bool arr4(string token)
        {
            if (tokens[index] == ",")
            {
                index++;
                if (CONST(tokens[index]))
                {
                    if (arr4(tokens[index]))
                    {
                        return true;
                    }
                }
            }
            else if (tokens[index] == "]")
            {
                index++;
                if (tokens[index] == ",")
                {
                    index++;
                    if (tokens[index] == "[")
                    {
                        index++;
                        if (CONST(tokens[index]))
                        {
                            if (arr5(tokens[index]))
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        public static bool arr5(string token)
        {
            if (tokens[index] == ",")
            {
                index++;
                if (CONST(tokens[index]))
                {
                    if (arr5(tokens[index]))
                    {
                        return true;
                    }
                }
            }
            else if (tokens[index] == "]")
            {
                index++;
                if (tokens[index] == "]")
                {
                    index++;
                    if (tokens[index] == "#")
                    {
                        index++;
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool switch_case(string token)
        {
            if (tokens[index] == "toggle")
            {
                index++;
                if (tokens[index] == "(")
                {
                    index++;
                    if (OE(tokens[index]))
                    {
                        if (tokens[index] == ")")
                        {
                            index++;
                            if (tokens[index] == "{")
                            {
                                index++;
                                if (_case(tokens[index]))
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }

            return false;
        }

        //in CFG it is case not _case
        public static bool _case(string token)
        {
            if (tokens[index] == "check")
            {
                index++;
                if (CONST(tokens[index]))
                {
                    if (tokens[index] == ":")
                    {
                        index++;
                        if (body(tokens[index]))
                        {
                            if (tokens[index] == "skip")
                            {
                                index++;
                                if (tokens[index] == "#")
                                {
                                    index++;
                                    if (case1(tokens[index]))
                                    {
                                        if (_default(tokens[index]))
                                        {
                                            if (tokens[index] == "}")
                                            {
                                                index++;
                                                return true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return false;
        }

        public static bool case1(string token)
        {
            if (tokens[index] == "check")
            {
                index++;
                if (CONST(tokens[index]))
                {
                    if (tokens[index] == ":")
                    {
                        index++;
                        if (body(tokens[index]))
                        {
                            if (tokens[index] == "skip")
                            {
                                index++;
                                if (tokens[index] == "#")
                                {
                                    index++;
                                    if (case1(tokens[index]))
                                    {
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else if (tokens[index] == "mismatch" || tokens[index] == "}")
            {
                return true;
            }

            return false;
        }

        //in CFG it is default not _default
        public static bool _default(string token)
        {
            if (tokens[index] == "mismatch")
            {
                index++;
                if (tokens[index] == ":")
                {
                    index++;
                    if (body(tokens[index]))
                    {
                        return true;
                    }
                }
            }
            else if (tokens[index] == "}")
            {
                return true;
            }

            return false;
        }

        public static bool try_catch(string token)
        {
            if (tokens[index] == "try")
            {
                index++;
                if (body(tokens[index]))
                {
                    if (tokens[index] == "except")
                    {
                        index++;
                        if (body(tokens[index]))
                        {
                            if (tokens[index] == "finally")
                            {
                                index++;
                                if (body(tokens[index]))
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }

            return false;
        }

        public static bool S(string token)
        {
            if (tokens[index] == "general" || tokens[index] == "personal" || tokens[index] == "protected" || tokens[index] == "symbolic" || tokens[index] == "final" || tokens[index] == "inactive" || tokens[index] == "class")
            {
                if (AM(tokens[index]))
                {
                    if (Non_AM(tokens[index]))
                    {
                        if (tokens[index] == "class")
                        {
                            index++;
                            if (tokens[index] == "ID")
                            {
                                index++;
                                if (extends(tokens[index]))
                                {
                                    if (tokens[index] == "{")
                                    {
                                        index++;
                                        if (tokens[index] == "general")
                                        {
                                            index++;
                                            if (tokens[index] == "inactive")
                                            {
                                                index++;
                                                if (tokens[index] == "void")
                                                {
                                                    index++;
                                                    if (tokens[index] == "main")
                                                    {
                                                        index++;
                                                        if (tokens[index] == "(")
                                                        {
                                                            index++;
                                                            if (tokens[index] == ")")
                                                            {
                                                                index++;
                                                                if (tokens[index] == "{")
                                                                {
                                                                    index++;
                                                                    if (MST(tokens[index]))
                                                                    {
                                                                        if (tokens[index] == "}")
                                                                        {
                                                                            index++;
                                                                            if (CB(tokens[index]))
                                                                            {
                                                                                if (tokens[index] == "}")
                                                                                {
                                                                                    index++;
                                                                                    if (def(tokens[index]))
                                                                                    {
                                                                                        return true;
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return false;
        }

        private static bool def(string token)
        {
            return false;
        }

        public static bool CB(string token)
        {
            if (tokens[index] == "general" || tokens[index] == "personal" || tokens[index] == "protected" || tokens[index] == "symbolic" || tokens[index] == "final" || tokens[index] == "inactive" || tokens[index] == "void" || tokens[index] == "DT" || tokens[index] == "ID")
            {
                if (AM(tokens[index]))
                {
                    if (Non_AM(tokens[index]))
                    {
                        if (func_ret_type(tokens[index]))
                        {
                            if (tokens[index] == "ID")
                            {
                                index++;
                                if (fn(tokens[index]))
                                {
                                    if (CB(tokens[index]))
                                    {
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else if (tokens[index] == "}")
            {
                return true;
            }

            return false;
        }

        public static bool defs(string token)
        {
            if (tokens[index] == "general" || tokens[index] == "personal" || tokens[index] == "protected" || tokens[index] == "symbolic" || tokens[index] == "final" || tokens[index] == "inactive" || tokens[index] == "class")
            {
                if (Class_dec(tokens[index]))
                {
                    if (defs(tokens[index]))
                    {
                        return true;
                    }
                }
            }
            else if (tokens[index] == "$")
            {
                return true;
            }

            return false;
        }

        public static bool fn(string token)
        {
            if (tokens[index] == "(")
            {
                index++;
                if (PL(tokens[index]))
                {
                    if (tokens[index] == ")")
                    {
                        index++;
                        if (tokens[index] == "{")
                        {
                            index++;
                            if (MST(tokens[index]))
                            {
                                if (tokens[index] == "}")
                                {
                                    index++;
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            else if (tokens[index] == ",")
            {
                index++;
                if (tokens[index] == "ID")
                {
                    index++;
                    if (fn2(tokens[index]))
                    {
                        return true;
                    }
                }
            }
            else if (tokens[index] == "comp_assign")
            {
                index++;
                if (OE(tokens[index]))
                {
                    if (tokens[index] == "#")
                    {
                        index++;
                        return true;
                    }
                }
            }
            else if (tokens[index] == "=")
            {
                index++;
                if (fn1(tokens[index]))
                {
                    if (tokens[index] == "#")
                    {
                        index++;
                        return true;
                    }
                }
            }
            else if (tokens[index] == "#")
            {
                return true;
            }

            return false;
        }

        public static bool fn1(string token)
        {
            if (tokens[index] == "ID" || tokens[index] == "!" || tokens[index] == "++" || tokens[index] == "--" || tokens[index] == "(" || tokens[index] == "int_const" || tokens[index] == "dec_const" || tokens[index] == "str_const" || tokens[index] == "bool_const" || tokens[index] == "char_const" || tokens[index] == "none")
            {
                if (OE(tokens[index]))
                {
                    return true;
                }
            }
            else if (tokens[index] == "fresh")
            {
                index++;
                if (tokens[index] == "ID")
                {
                    index++;
                    return true;
                }
            }

            return false;
        }

        public static bool fn2(string token)
        {
            if (tokens[index] == "=")
            {
                index++;
                if (A(tokens[index]))
                {
                    return true;
                }
            }
            else if (tokens[index] == ",")
            {
                index++;
                if (tokens[index] == "ID")
                {
                    index++;
                    if (obj_dec_list1(tokens[index]))
                    {
                        return true;
                    }
                }
            }
            else if (tokens[index] == "#")
            {
                index++;
                return true;
            }

            return false;
        }

        public static bool A(string token)
        {
            if (tokens[index] == "ID" || tokens[index] == "!" || tokens[index] == "++" || tokens[index] == "--" || tokens[index] == "(" || tokens[index] == "int_const" || tokens[index] == "dec_const" || tokens[index] == "str_const" || tokens[index] == "bool_const" || tokens[index] == "char_const" || tokens[index] == "none")
            {
                if (OE(tokens[index]))
                {
                    if (B(tokens[index]))
                    {
                        return true;
                    }
                }
            }
            else if (tokens[index] == "fresh")
            {
                index++;
                if (tokens[index] == "ID")
                {
                    index++;
                    if (tokens[index] == "#")
                    {
                        index++;
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool B(string token)
        {
            if (tokens[index] == ",")
            {
                index++;
                if (tokens[index] == "ID")
                {
                    index++;
                    if (init(tokens[index]))
                    {
                        if (dec_list(tokens[index]))
                        {
                            return true;
                        }
                    }
                }
            }
            else if (tokens[index] == "#")
            {
                index++;
                return true;
            }

            return true;
        }

        public static bool interface_dec(string token)
        {
            if (tokens[index] == "interface")
            {
                index++;
                if (tokens[index] == "ID")
                {
                    index++;
                    if (tokens[index] == "{")
                    {
                        index++;
                        if (method(tokens[index]))
                        {
                            if (tokens[index] == "}")
                            {
                                index++;
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        public static bool method(string token)
        {
            if (tokens[index] == "general")
            {
                index++;
                if (tokens[index] == "void")
                {
                    index++;
                    if (tokens[index] == "ID")
                    {
                        index++;
                        if (tokens[index] == "(")
                        {
                            index++;
                            if (define(tokens[index]))
                            {
                                if (tokens[index] == ")")
                                {
                                    index++;
                                    if (tokens[index] == "#")
                                    {
                                        index++;
                                        if (method(tokens[index]))
                                        {
                                            return true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else if (tokens[index] == "}")
            {
                return true;
            }

            return false;
        }

        public static bool implem_dec(string token)
        {
            if (tokens[index] == "general" || tokens[index] == "personal" || tokens[index] == "protected" || tokens[index] == "symbolic" || tokens[index] == "final" || tokens[index] == "inactive" || tokens[index] == "class")
            {
                if (AM(tokens[index]))
                {
                    if (Non_AM(tokens[index]))
                    {
                        if (tokens[index] == "class")
                        {
                            index++;
                            if (tokens[index] == "ID")
                            {
                                index++;
                                if (tokens[index] == "implement")
                                {
                                    index++;
                                    if (tokens[index] == "ID")
                                    {
                                        index++;
                                        if (tokens[index] == "{")
                                        {
                                            index++;
                                            if (method_implem(tokens[index]))
                                            {
                                                if (tokens[index] == "}")
                                                {
                                                    index++;
                                                    return true;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return false;
        }

        public static bool method_implem(string token)
        {
            if (tokens[index] == "general" || tokens[index] == "personal" || tokens[index] == "protected" || tokens[index] == "symbolic" || tokens[index] == "final" || tokens[index] == "inactive" || tokens[index] == "void" || tokens[index] == "DT" || tokens[index] == "ID")
            {
                if (func_def(tokens[index]))
                {
                    if (method_implem(tokens[index]))
                    {
                        return true;
                    }
                }
            }
            else if (tokens[index] == "}")
            {
                return true;
            }

            return false;
        }

        public static bool SST(string token)
        {
            if (tokens[index] == "DT")
            {
                index++;
                if (tokens[index] == "ID")
                {
                    index++;
                    if (C(tokens[index]))
                    {
                        return true;
                    }
                }
            }
            else if (tokens[index] == "loop")
            {
                if (for_st(tokens[index]))
                {
                    return true;
                }
            }
            else if (tokens[index] == "when")
            {
                if (if_else(tokens[index]))
                {
                    return true;
                }
            }
            else if (tokens[index] == "until")
            {
                if (While_st(tokens[index]))
                {
                    return true;
                }
            }
            else if (tokens[index] == "++" || tokens[index] == "--")
            {
                if (inc_dec(tokens[index]))
                {
                    if (tokens[index] == "ID")
                    {
                        index++;
                        if (X(tokens[index]))
                        {
                            if (tokens[index] == "#")
                            {
                                index++;
                                return true;
                            }
                        }
                    }
                }
            }
            else if (tokens[index] == "jump" || tokens[index] == "skip")
            {
                index++;
                if (tokens[index] == "#")
                {
                    index++;
                    return true;
                }
            }
            else if (tokens[index] == "ID")
            {
                index++;
                if (COM(tokens[index]))
                {
                    return true;
                }
            }
            else if (tokens[index] == "this" || tokens[index] == "base")
            {
                index++;
                if (tokens[index] == ".")
                {
                    index++;
                    if (call(tokens[index]))
                    {
                        return true;
                    }
                }
            }
            else if (tokens[index] == "toggle")
            {
                if (switch_case(tokens[index]))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool C(string token)
        {
            if (tokens[index] == "=" || tokens[index] == "#" || tokens[index] == ",")
            {
                if (init(tokens[index]))
                {
                    if (dec_list(tokens[index]))
                    {
                        return true;
                    }
                }
            }
            else if (tokens[index] == "int-const" || tokens[index] == "str-const" || tokens[index] == "dec-const" || tokens[index] == "bool-const" || tokens[index] == "char-const" || tokens[index] == "none" || tokens[index] == "[")
            {
                if (arr2(tokens[index]))
                {
                    if (tokens[index] == "=")
                    {
                        index++;
                        if (tokens[index] == "[")
                        {
                            index++;
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public static bool COM(string token)
        {
            if (tokens[index] == "=")
            {
                index++;
                if (COM1(tokens[index]))
                {
                    return true;
                }

            }
            else if (tokens[index] == "comp_assign")
            {
                index++;
                if (OE(tokens[index]))
                {
                    if (tokens[index] == "#")
                    {
                        index++;
                        return true;
                    }
                }
            }
            else if (tokens[index] == "++" || tokens[index] == "--")
            {
                if (inc_dec(tokens[index]))
                {
                    if (tokens[index] == "#")
                    {
                        index++;
                        return true;
                    }
                }
            }
            else if (tokens[index] == "." || tokens[index] == "(")
            {
                if (Y(tokens[index]))
                {
                    if (tokens[index] == "(")
                    {
                        index++;
                        if (PL(tokens[index]))
                        {
                            if (tokens[index] == ")")
                            {
                                index++;
                                if (tokens[index] == "#")
                                {
                                    index++;
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            else if (tokens[index] == ",")
            {
                index++;
                if (tokens[index] == "ID")
                {
                    index++;
                    if (obj_dec_list1(tokens[index]))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool COM1(string token)
        {
            if (tokens[index] == "ID" || tokens[index] == "!" || tokens[index] == "++" || tokens[index] == "--" || tokens[index] == "(" || tokens[index] == "int_const" || tokens[index] == "dec_const" || tokens[index] == "str_const" || tokens[index] == "bool_const" || tokens[index] == "char_const" || tokens[index] == "none")
            {
                if (OE(tokens[index]))
                {
                    if (tokens[index] == "#")
                    {
                        index++;
                        return true;
                    }
                }
            }
            else if (tokens[index] == "fresh")
            {
                index++;
                if (tokens[index] == "ID")
                {
                    index++;
                    if (tokens[index] == "#")
                    {
                        index++;
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool call(string token)
        {
            if (tokens[index] == "." || tokens[index] == "ID")
            {
                index++;
                if (G(tokens[index]))
                {
                    return true;
                }
            }
            else if (tokens[index] == "++" || tokens[index] == "--")
            {
                if (inc_dec(tokens[index]))
                {
                    if (tokens[index] == "ID")
                    {
                        index++;
                        if (X(tokens[index]))
                        {
                            if (tokens[index] == "#")
                            {
                                index++;
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        public static bool G(string token)
        {


            return false;
        }

        public static bool body(string token)
        {
            return false;
        }

        public static bool MST(string token)
        {
            return false;
        }

    }
}
