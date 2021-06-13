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
            return false;
        }

        public static bool obj_dec_list(string token)
        {
            return false;
        }

        public static bool obj_dec_list1(string token)
        {
            return false;
        }

        public static bool obj_dec_list2(string token)
        {
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
            return false;
        }

        public static bool init(string token)
        {
            return false;
        }

        public static bool dec_list(string token)
        {
            return false;
        }

        public static bool func_def(string token)
        {


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
            return false;
        }

        public static bool ret_line(string token)
        {
            return false;
        }

        public static bool ret(string token)
        {
            return false;
        }

        public static bool func_call(string token)
        {
            return false;
        }

        public static bool Y(string token)
        {
            return false;
        }

        public static bool for_st(string token)
        {
            return false;
        }

        public static bool c1(string token)
        {
            return false;
        }

        public static bool c2(string token)
        {
            return false;

        }

        public static bool While_st(string token)
        {
            return false;

        }

        public static bool arr1(string token)
        {
            return false;
        }

        public static bool arr2(string token)
        {
            return false;
        }

        public static bool arr3(string token)
        {
            return false;
        }

        public static bool arr4(string token)
        {
            return false;

        }

        public static bool arr5(string token)
        {
            return false;

        }

        public static bool switch_case(string token)
        {
            return false;
        }

        //in CFG it is case not _case
        public static bool _case(string token)
        {
            return false;
        }

        public static bool case1(string token)
        {
            return false;
        }

        //in CFG it is default not _default
        public static bool _default(string token)
        {
            return false;

        }

        public static bool try_catch(string token)
        {
            return false;

        }

        public static bool S(string token)
        {
            return false;

        }

        public static bool CB(string token)
        {
            return false;

        }

        public static bool defs(string token)
        {
            return false;

        }

        public static bool fn(string token)
        {
            return false;

        }

        public static bool fn1(string token)
        {
            return false;

        }

        public static bool fn2(string token)
        {
            return false;

        }

        public static bool interface_dec(string token)
        {
            return false;

        }

        public static bool method(string token)
        {
            return false;

        }

        public static bool implem_dec(string token)
        {
            return false;

        }

        public static bool method_implem(string token)
        {
            return false;

        }

        public static bool SST(string token)
        {
            return false;

        }

        public static bool COM(string token)
        {
            return false;

        }

        public static bool call(string token)
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
