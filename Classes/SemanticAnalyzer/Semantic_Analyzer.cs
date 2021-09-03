using Orion.Classes.SemanticAnalyzer.SymbolTable;
using System;
using System.Collections.Generic;

namespace Orion.Classes.SemanticAnalyzer
{
    class Semantic_Analyzer
    {

        // to go forward token
        static int index = 0;

        //to store all tokens in a list for semantic analyzer to parse tokens
        public static List<string> tokens = new List<string>();
        public static List<int> lineNo = new List<int>();


        // to iterate all tokens
        public static void iterate()
        {
            foreach (var item in tokens)
            {
                Console.WriteLine(item);
            }
        }

        //to store all class names to check duplicate
        public static List<string> className = new List<string>();

        // main table object
        //MainTable mainTable = new MainTable();

        // list to maintain main table/class objects
        public static List<MainTable> mainTables = new List<MainTable>();

        // list to maintain function table objects
        public static List<FunctionTable> functionTables = new List<FunctionTable>();

        // list to maintain fields table objects
        public static List<FieldsTable> FieldsTables = new List<FieldsTable>();


        public static void AddToken(string word, int line)
        {
            Semantic_Analyzer.tokens.Add(word);
            Semantic_Analyzer.lineNo.Add(line);
        }

        // to check duplicate class name, if exists it return true
        public static bool isClassExist(string word)
        {
            for (int i = 0; i < mainTables.Count; i++)
            {
                if (word == mainTables[i].Name)
                {
                    return true;
                }
            }

            return false;
        }

        // to check duplicate function name, if exists it return true
        public static bool isFuncExist(string word, string className, string funcReturnType)
        {
            for (int i = 0; i < functionTables.Count; i++)
            {
                if (word == functionTables[i].Name && className == functionTables[i].Scope && functionTables[i].Type == funcReturnType)
                {                    
                    return true;
                }
            }

            return false;
        }

        // to check duplicate field name, scop may be class/function reference if exists it return true
        public static bool isFieldExist(string word, string scope)
        {
            for (int i = 0; i < FieldsTables.Count; i++)
            {
                if (word == FieldsTables[i].Name && scope == FieldsTables[i].Scope)
                {
                    return true;
                }
            }

            return false;
        }

        // to throw error incase final class inheritance
        public static bool isClassFinal(string className, string modifier)
        {
            for (int i = 0; i < mainTables.Count; i++)
            {
                if (className == mainTables[i].Name && mainTables[i].NAM == "final")
                {
                    return true;
                }
            }

            return false;
        }

        public static void SemanticAnalyzer()
        {
            // it ends the token
            tokens.Add("$");

            
            // programs strat here
            if (S(tokens[index]))
            {
                if (tokens[index] == "$")
                {
                    Console.WriteLine("There is no Semantic Error.\n");
                }
            }
        }

        // CFG mapping starts here ------->>>>>>>
        public static bool OE(string token)
        {
            if (LexicalAnalyzer.Lexical_Analyzer.isID(tokens[index]) || tokens[index] == "!" || tokens[index] == "++" || tokens[index] == "--" || tokens[index] == "(" || tokens[index] == "int_const" || tokens[index] == "dec_const" || tokens[index] == "str_const" || tokens[index] == "bool_const" || tokens[index] == "char_const" || tokens[index] == "none")
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
            if (LexicalAnalyzer.Lexical_Analyzer.isID(tokens[index]) || tokens[index] == "!" || tokens[index] == "++" || tokens[index] == "--" || tokens[index] == "(" || tokens[index] == "int_const" || tokens[index] == "dec_const" || tokens[index] == "str_const" || tokens[index] == "bool_const" || tokens[index] == "char_const" || tokens[index] == "none")
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
            if (LexicalAnalyzer.Lexical_Analyzer.isID(tokens[index]) || tokens[index] == "!" || tokens[index] == "++" || tokens[index] == "--" || tokens[index] == "(" || tokens[index] == "int_const" || tokens[index] == "dec_const" || tokens[index] == "str_const" || tokens[index] == "bool_const" || tokens[index] == "char_const" || tokens[index] == "none")
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
            if (LexicalAnalyzer.Lexical_Analyzer.isID(tokens[index]) || tokens[index] == "!" || tokens[index] == "++" || tokens[index] == "--" || tokens[index] == "(" || tokens[index] == "int_const" || tokens[index] == "dec_const" || tokens[index] == "str_const" || tokens[index] == "bool_const" || tokens[index] == "char_const" || tokens[index] == "none")
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
            if (LexicalAnalyzer.Lexical_Analyzer.isID(tokens[index]) || tokens[index] == "!" || tokens[index] == "++" || tokens[index] == "--" || tokens[index] == "(" || tokens[index] == "int_const" || tokens[index] == "dec_const" || tokens[index] == "str_const" || tokens[index] == "bool_const" || tokens[index] == "char_const" || tokens[index] == "none")
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
            if (LexicalAnalyzer.Lexical_Analyzer.isID(tokens[index]))
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
            else if (tokens[index] == "int_const" || tokens[index] == "str_const" || tokens[index] == "dec_const" || tokens[index] == "bool_const" || tokens[index] == "char_const" || tokens[index] == "none")
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
                    if (LexicalAnalyzer.Lexical_Analyzer.isID(tokens[index]))
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
                if (LexicalAnalyzer.Lexical_Analyzer.isID(tokens[index]))
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
                if (LexicalAnalyzer.Lexical_Analyzer.isID(tokens[index]))
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
            if (tokens[index] == "int_const" || tokens[index] == "str_const" || tokens[index] == "dec_const" || tokens[index] == "bool_const" || tokens[index] == "char_const" || tokens[index] == "none")
            {
                index++;
                return true;
            }

            return false;
        }

        public static bool PL(string token)
        {
            if (LexicalAnalyzer.Lexical_Analyzer.isID(tokens[index]) || tokens[index] == "!" || tokens[index] == "++" || tokens[index] == "--" || tokens[index] == "(" || tokens[index] == "int_const" || tokens[index] == "dec_const" || tokens[index] == "str_const" || tokens[index] == "bool_const" || tokens[index] == "char_const" || tokens[index] == "none")
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
            if (LexicalAnalyzer.Lexical_Analyzer.isID(tokens[index]))
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
                if (LexicalAnalyzer.Lexical_Analyzer.isID(tokens[index]))
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
                if (LexicalAnalyzer.Lexical_Analyzer.isID(tokens[index]))
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
                if (LexicalAnalyzer.Lexical_Analyzer.isID(tokens[index]))
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
            if (LexicalAnalyzer.Lexical_Analyzer.isID(tokens[index]))
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
                    if (LexicalAnalyzer.Lexical_Analyzer.isID(tokens[index]))
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
            else if (LexicalAnalyzer.Lexical_Analyzer.isID(tokens[index]))
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

        public static bool if_else(string token, string scope)
        {
            if (tokens[index] == "when")
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
                            if (body(tokens[index], scope))
                            {
                                if (_else(tokens[index], scope))
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

        //in CFG it is else not _else
        public static bool _else(string token, string scope)
        {
            if (tokens[index] == "lest")
            {
                index++;
                if (body(tokens[index], scope))
                {
                    return true;
                }
            }
            else if (LexicalAnalyzer.Lexical_Analyzer.isDT(tokens[index]) || tokens[index] == "DS" || tokens[index] == "loop" || tokens[index] == "when" || tokens[index] == "until" || tokens[index] == "++" || tokens[index] == "--" || tokens[index] == "jump" || tokens[index] == "skip" || LexicalAnalyzer.Lexical_Analyzer.isID(tokens[index]) || tokens[index] == "ref_var" || tokens[index] == "toggle" || tokens[index] == "general" || tokens[index] == "personal" || tokens[index] == "protected" || tokens[index] == "symbolic" || tokens[index] == "final" || tokens[index] == "inacive" || tokens[index] == "refund" || tokens[index] == "}")
            {
                return true;
            }

            return false;
        }

        public static bool obj_dec(string token)
        {
            if (LexicalAnalyzer.Lexical_Analyzer.isID(tokens[index]))
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
            if (LexicalAnalyzer.Lexical_Analyzer.isID(tokens[index]))
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
                    if (tokens[index] == "#")
                    {
                        index++;
                        return true;
                    }
                }
            }
            else if (tokens[index] == ",")
            {
                index++;
                if (LexicalAnalyzer.Lexical_Analyzer.isID(tokens[index]))
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
                if (LexicalAnalyzer.Lexical_Analyzer.isID(tokens[index]))
                {
                    index++;
                    return true;
                }
            }
            else if (LexicalAnalyzer.Lexical_Analyzer.isID(tokens[index]))
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
                if (tokens[index] == "class")
                {
                    if (implem_dec(tokens[index]))
                    {
                        return true;
                    }
                }
                else if (AM(tokens[index]))
                {
                    string NonAM = tokens[index];

                    if (Non_AM(tokens[index]))
                    {                        
                        if (tokens[index] == "class")
                        {
                            MainTable mainTable = new MainTable();

                            index++;
                            if (LexicalAnalyzer.Lexical_Analyzer.isID(tokens[index]))
                            {
                                if (isClassExist(tokens[index]))
                                {
                                    Console.WriteLine($"Oops: Semantic Error occured at line no: {lineNo[index]}");
                                    Console.WriteLine($"The class \"{tokens[index]}\" is already exist.\n");
                                    return false;
                                }
                                else
                                {                                    
                                    mainTable.Name = tokens[index];
                                    mainTable.NAM = NonAM;
                                    mainTables.Add(mainTable);
                                }

                                index++;
                                if (extends(tokens[index], NonAM))
                                {
                                    if (tokens[index] == "{")
                                    {
                                        index++;
                                        if (CB(tokens[index], mainTable.Name))
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
            else if (tokens[index] == "class" || tokens[index] == "void" || LexicalAnalyzer.Lexical_Analyzer.isDT(tokens[index]) || LexicalAnalyzer.Lexical_Analyzer.isID(tokens[index]))
            {
                return true;
            }

            return false;
        }

        public static bool extends(string token, string modifier)
        {
            if (tokens[index] == "inherits")
            {
                index++;
                if (LexicalAnalyzer.Lexical_Analyzer.isID(tokens[index]))
                {
                    if (!isClassExist(tokens[index]))
                    {
                        Console.WriteLine($"Oops: Semantic Error occured at line no: {lineNo[index]}");
                        Console.WriteLine($"The class/interface name \"{tokens[index]}\" could not be found.\n");
                        return false;
                    }

                    if (isClassFinal(tokens[index], modifier))
                    {
                        Console.WriteLine($"Oops: Semantic Error occured at line no: {lineNo[index]}");
                        Console.WriteLine($"cannot inherit from final \"{tokens[index]}\" \n");
                        return false;
                    }

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

        public static bool dec(string token, string scope)
        {
            if (tokens[index] == "general" || tokens[index] == "personal" || tokens[index] == "protected" || tokens[index] == "symbolic" || tokens[index] == "final" || tokens[index] == "inacive" || LexicalAnalyzer.Lexical_Analyzer.isDT(tokens[index]))
            {
                if (AM(tokens[index]))
                {
                    if (Non_AM(tokens[index]))
                    {
                        if (LexicalAnalyzer.Lexical_Analyzer.isDT(tokens[index]))
                        {
                            index++;
                            if (LexicalAnalyzer.Lexical_Analyzer.isID(tokens[index]))
                            {
                                if (isFieldExist(tokens[index], scope))
                                {
                                    Console.WriteLine($"Oops: Semantic Error occured at line no: {lineNo[index]}");
                                    Console.WriteLine($"The Field \"{tokens[index]}\" is already exist.\n");
                                    return false;
                                }
                                else
                                {
                                    FieldsTable fieldsTable = new FieldsTable();
                                    fieldsTable.Name = tokens[index];
                                    fieldsTable.Scope = scope;
                                    FieldsTables.Add(fieldsTable);
                                }

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
                if (LexicalAnalyzer.Lexical_Analyzer.isID(tokens[index]))
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
            if (tokens[index] == "general" || tokens[index] == "personal" || tokens[index] == "protected" || tokens[index] == "symbolic" || tokens[index] == "final" || tokens[index] == "inacive" || tokens[index] == "void" || LexicalAnalyzer.Lexical_Analyzer.isDT(tokens[index]) || LexicalAnalyzer.Lexical_Analyzer.isID(tokens[index]))
            {
                if (AM(tokens[index]))
                {
                    if (Non_AM(tokens[index]))
                    {
                        if (func_ret_type(tokens[index]))
                        {
                            if (LexicalAnalyzer.Lexical_Analyzer.isID(tokens[index]))
                            {
                                string scope = tokens[index];
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
                                                if (MST(tokens[index], scope))
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
            if (LexicalAnalyzer.Lexical_Analyzer.isDT(tokens[index]))
            {
                index++;
                if (LexicalAnalyzer.Lexical_Analyzer.isID(tokens[index]))
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
                if (define(tokens[index]))
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
            else if (tokens[index] == "symbolic" || tokens[index] == "final" || tokens[index] == "inactive" || tokens[index] == "class" || tokens[index] == "void" || LexicalAnalyzer.Lexical_Analyzer.isDT(tokens[index]) || LexicalAnalyzer.Lexical_Analyzer.isID(tokens[index]))
            {
                return true;
            }

            return false;
        }

        public static bool func_ret_type(string token)
        {
            if (token == "void" || LexicalAnalyzer.Lexical_Analyzer.isDT(token))
            {
                index++;
                return true;
            }
            else if (LexicalAnalyzer.Lexical_Analyzer.isID(tokens[index]))
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
            if (LexicalAnalyzer.Lexical_Analyzer.isID(tokens[index]) || tokens[index] == "!" || tokens[index] == "++" || tokens[index] == "--" || tokens[index] == "(" || tokens[index] == "int_const" || tokens[index] == "dec_const" || tokens[index] == "str_const" || tokens[index] == "bool_const" || tokens[index] == "char_const" || tokens[index] == "none")
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
            if (LexicalAnalyzer.Lexical_Analyzer.isID(tokens[index]))
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
                if (LexicalAnalyzer.Lexical_Analyzer.isID(tokens[index]))
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

        public static bool for_st(string token, string scope)
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
                            if (c1(tokens[index]))
                            {
                                if (tokens[index] == ")")
                                {
                                    index++;
                                    if (body(tokens[index], scope))
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
            if (tokens[index] == "int_const")
            {
                index++;
                return true;
            }

            return false;
        }



        public static bool While_st(string token, string scope)
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
                            if (body(tokens[index], scope))
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        public static bool arr1(string token, string scope)
        {
            if (tokens[index] == "DS")
            {
                index++;
                if (LexicalAnalyzer.Lexical_Analyzer.isID(tokens[index]))
                {
                    if (isFieldExist(tokens[index], scope))
                    {
                        Console.WriteLine($"Oops: Semantic Error occured at line no: {lineNo[index]}");
                        Console.WriteLine($"The Field \"{tokens[index]}\" is already exist.\n");
                        return false;
                    }
                    else
                    {
                        FieldsTable fieldsTable = new FieldsTable();
                        fieldsTable.Name = tokens[index];
                        fieldsTable.Scope = scope;
                        FieldsTables.Add(fieldsTable);
                    }

                    index++;
                    if (tokens[index] == "=")
                    {
                        index++;
                        if (tokens[index] == "[")
                        {
                            index++;
                            if (arr2(tokens[index]))
                            {
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
            if (tokens[index] == "int_const" || tokens[index] == "str_const" || tokens[index] == "dec_const" || tokens[index] == "bool_const" || tokens[index] == "char_const" || tokens[index] == "none")
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
                if (tokens[index] == "int_const")
                {
                    index++;
                    if (tokens[index] == ":")
                    {
                        index++;
                        if (tokens[index] == "{")
                        {
                            index++;

                            if (tokens[index] != "skip")
                            {

                            }

                            if (tokens[index] == "skip")
                            {
                                index++;
                                if (tokens[index] == "#")
                                {
                                    index++;
                                    if (tokens[index] == "}")
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
                }
            }
            else if (tokens[index] == "nonmatch")
            {
                if (_default(tokens[index]))
                {
                    return true;
                }
            }

            return false;
        }

        //in CFG it is default not _default
        public static bool _default(string token)
        {
            if (tokens[index] == "nonmatch")
            {
                index++;
                if (tokens[index] == ":")
                {
                    index++;
                    if (tokens[index] == "{")
                    {
                        index++;
                        if (tokens[index] == "skip")
                        {
                            index++;
                            if (tokens[index] == "#")
                            {
                                index++;
                                if (tokens[index] == "}")
                                {
                                    index++;
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

            return false;
        }

        public static bool try_catch(string token, string scope)
        {
            if (tokens[index] == "try")
            {
                index++;
                if (body(tokens[index], scope))
                {
                    if (tokens[index] == "except")
                    {
                        index++;
                        if (body(tokens[index], scope))
                        {
                            if (tokens[index] == "finally")
                            {
                                index++;
                                if (body(tokens[index], scope))
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
                    string NonAM = tokens[index];

                    if (Non_AM(tokens[index]))
                    {
                        if (tokens[index] == "class")
                        {
                            index++;
                            if (LexicalAnalyzer.Lexical_Analyzer.isID(tokens[index]))
                            {
                                MainTable mainTable = new MainTable();
                                mainTable.Name = tokens[index];
                                mainTables.Add(mainTable);

                                index++;
                                if (extends(tokens[index], NonAM))
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
                                                        string scope = tokens[index];

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
                                                                    if (MST(tokens[index], scope))
                                                                    {
                                                                        if (tokens[index] == "}")
                                                                        {
                                                                            index++;
                                                                            if (CB(tokens[index], mainTable.Name))
                                                                            {
                                                                                if (tokens[index] == "}")
                                                                                {
                                                                                    index++;
                                                                                    if (defs(tokens[index]))
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

        public static bool CB(string token, string className)
        {
            if (tokens[index] == "general" || tokens[index] == "personal" || tokens[index] == "protected" || tokens[index] == "symbolic" || tokens[index] == "final" || tokens[index] == "inactive" || tokens[index] == "void" || LexicalAnalyzer.Lexical_Analyzer.isDT(tokens[index]) || LexicalAnalyzer.Lexical_Analyzer.isID(tokens[index]))
            {
                if (AM(tokens[index]))
                {
                    if (Non_AM(tokens[index]))
                    {
                        string funcReturnType = tokens[index];

                        if (func_ret_type(tokens[index]))
                        {                            
                            if (LexicalAnalyzer.Lexical_Analyzer.isID(tokens[index]))
                            {
                                if (isFuncExist(tokens[index], className, funcReturnType))
                                {
                                    Console.WriteLine($"Oops: Semantic Error occured at line no: {lineNo[index]}");
                                    Console.WriteLine($"The Function \"{tokens[index]}\" is already exist.\n");
                                    return false;
                                }
                                else
                                {
                                    FunctionTable functionTable = new FunctionTable();
                                    functionTable.Name = tokens[index];
                                    functionTable.Type = funcReturnType;
                                    functionTable.Scope = className;
                                    functionTables.Add(functionTable);
                                }

                                index++;
                                if (fn(tokens[index], className))
                                {
                                    if (CB(tokens[index], className))
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
            else if (tokens[index] == "interface")
            {
                if (interface_dec(tokens[index]))
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

        public static bool fn(string token, string scope)
        {
            if (tokens[index] == "(")
            {
                index++;
                if (fn0(tokens[index], scope))
                {
                    return true;
                }
            }
            else if (tokens[index] == ",")
            {
                index++;
                if (LexicalAnalyzer.Lexical_Analyzer.isID(tokens[index]))
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

        public static bool fn0(string token, string scope)
        {
            if (LexicalAnalyzer.Lexical_Analyzer.isID(tokens[index]) || tokens[index] == "!" || tokens[index] == "++" || tokens[index] == "--" || tokens[index] == "(" || tokens[index] == "int_const" || tokens[index] == "dec_const" || tokens[index] == "str_const" || tokens[index] == "bool_const" || tokens[index] == "char_const" || tokens[index] == "none")
            {
                if (OE(tokens[index]))
                {
                    if (tokens[index] == ")")
                    {
                        index++;
                        if (tokens[index] == "{")
                        {
                            index++;
                            if (MST(tokens[index], scope))
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
            else if (LexicalAnalyzer.Lexical_Analyzer.isDT(tokens[index]))
            {
                index++;
                if (LexicalAnalyzer.Lexical_Analyzer.isID(tokens[index]))
                {
                    index++;
                    if (define1(tokens[index]))
                    {
                        if (tokens[index] == ")")
                        {
                            index++;
                            if (tokens[index] == "{")
                            {
                                index++;
                                if (MST(tokens[index], scope))
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
            else if (tokens[index] == ")")
            {
                index++;
                if (tokens[index] == "#")
                {
                    index++;
                    return true;
                }
                else if (tokens[index] == "{")
                {
                    index++;
                    if (MST(tokens[index], scope))
                    {
                        if (tokens[index] == "}")
                        {
                            index++;
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public static bool fn1(string token)
        {
            if (LexicalAnalyzer.Lexical_Analyzer.isID(tokens[index]) || tokens[index] == "!" || tokens[index] == "++" || tokens[index] == "--" || tokens[index] == "(" || tokens[index] == "int_const" || tokens[index] == "dec_const" || tokens[index] == "str_const" || tokens[index] == "bool_const" || tokens[index] == "char_const" || tokens[index] == "none")
            {
                if (OE(tokens[index]))
                {
                    return true;
                }
            }
            else if (tokens[index] == "fresh")
            {
                index++;
                if (LexicalAnalyzer.Lexical_Analyzer.isID(tokens[index]))
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
                if (LexicalAnalyzer.Lexical_Analyzer.isID(tokens[index]))
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
            if (LexicalAnalyzer.Lexical_Analyzer.isID(tokens[index]) || tokens[index] == "!" || tokens[index] == "++" || tokens[index] == "--" || tokens[index] == "(" || tokens[index] == "int_const" || tokens[index] == "dec_const" || tokens[index] == "str_const" || tokens[index] == "bool_const" || tokens[index] == "char_const" || tokens[index] == "none")
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
                if (LexicalAnalyzer.Lexical_Analyzer.isID(tokens[index]))
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
                if (LexicalAnalyzer.Lexical_Analyzer.isID(tokens[index]))
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
                if (LexicalAnalyzer.Lexical_Analyzer.isID(tokens[index]))
                {

                    if (isClassExist(tokens[index]))
                    {
                        Console.WriteLine($"Oops: Semantic Error occured at line no: {lineNo[index]}");
                        Console.WriteLine($"The interface/class \"{tokens[index]}\" is already exist.\n");
                        return false;
                    }
                    else
                    {
                        MainTable mainTable = new MainTable();
                        mainTable.Name = tokens[index];
                        mainTables.Add(mainTable);
                    }

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
                    if (LexicalAnalyzer.Lexical_Analyzer.isID(tokens[index]))
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
                            if (LexicalAnalyzer.Lexical_Analyzer.isID(tokens[index]))
                            {
                                index++;
                                if (tokens[index] == "implements")
                                {
                                    index++;
                                    if (LexicalAnalyzer.Lexical_Analyzer.isID(tokens[index]))
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
            if (tokens[index] == "general" || tokens[index] == "personal" || tokens[index] == "protected" || tokens[index] == "symbolic" || tokens[index] == "final" || tokens[index] == "inactive" || tokens[index] == "void" || LexicalAnalyzer.Lexical_Analyzer.isDT(tokens[index]) || LexicalAnalyzer.Lexical_Analyzer.isID(tokens[index]))
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

        public static bool SST(string token, string scope)
        {
            if (tokens[index] == "general" || tokens[index] == "personal" || tokens[index] == "protected" || tokens[index] == "symbolic" || tokens[index] == "final" || tokens[index] == "inacive" || LexicalAnalyzer.Lexical_Analyzer.isDT(tokens[index]))
            {
                if (dec(tokens[index], scope))
                {
                    return true;
                }
            }
            else if (tokens[index] == "DS")
            {
                if (arr1(tokens[index], scope))
                {
                    return true;
                }
            }
            else if (tokens[index] == "refund")
            {
                if (ret_line(tokens[index]))
                {
                    return true;
                }
            }
            else if (tokens[index] == "loop")
            {
                if (for_st(tokens[index], scope))
                {
                    return true;
                }
            }
            else if (tokens[index] == "when")
            {
                if (if_else(tokens[index], scope))
                {
                    return true;
                }
            }
            else if (tokens[index] == "until")
            {
                if (While_st(tokens[index], scope))
                {
                    return true;
                }
            }
            else if (tokens[index] == "++" || tokens[index] == "--")
            {
                if (inc_dec(tokens[index]))
                {
                    if (LexicalAnalyzer.Lexical_Analyzer.isID(tokens[index]))
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
            else if (LexicalAnalyzer.Lexical_Analyzer.isID(tokens[index]))
            {
                index++;
                if (COM(tokens[index]) || obj_dec(tokens[index]))
                {
                    return true;
                }
            }
            else if (tokens[index] == "ref_var")
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
            else if (tokens[index] == "try")
            {
                if (try_catch(tokens[index], scope))
                {
                    return true;
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
                if (LexicalAnalyzer.Lexical_Analyzer.isID(tokens[index]))
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
            if (LexicalAnalyzer.Lexical_Analyzer.isID(tokens[index]) || tokens[index] == "!" || tokens[index] == "++" || tokens[index] == "--" || tokens[index] == "(" || tokens[index] == "int_const" || tokens[index] == "dec_const" || tokens[index] == "str_const" || tokens[index] == "bool_const" || tokens[index] == "char_const" || tokens[index] == "none")
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
                if (LexicalAnalyzer.Lexical_Analyzer.isID(tokens[index]))
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
            if (LexicalAnalyzer.Lexical_Analyzer.isID(tokens[index]))
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
                    if (LexicalAnalyzer.Lexical_Analyzer.isID(tokens[index]))
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
            if (tokens[index] == "(")
            {
                index++;
                if (PL(tokens[index]))
                {
                    if (tokens[index] == ")")
                    {
                        index++;
                        if (H(tokens[index]))
                        {
                            return true;
                        }
                    }
                }
            }
            else if (tokens[index] == "[")
            {
                index++;
                if (K(tokens[index]))
                {
                    return true;
                }
            }
            else if (tokens[index] == ".")
            {
                index++;
                if (LexicalAnalyzer.Lexical_Analyzer.isID(tokens[index]))
                {
                    index++;
                    if (X(tokens[index]))
                    {
                        if (J(tokens[index]))
                        {
                            return true;
                        }
                    }
                }
            }
            else if (tokens[index] == "=")
            {
                index++;
                if (OE(tokens[index]))
                {
                    if (Q(tokens[index]))
                    {
                        return true;
                    }
                }
            }
            else if (tokens[index] == "," || tokens[index] == "#")
            {
                if (dec_list(tokens[index]))
                {
                    return true;
                }
            }
            else if (tokens[index] == "int_const" || tokens[index] == "none")
            {
                if (CONST(tokens[index]))
                {
                    if (arr3(tokens[index]))
                    {
                        return true;
                    }
                }
            }
            else if (tokens[index] == "++" || tokens[index] == "-")
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

            return false;
        }

        public static bool Q(string token)
        {
            if (tokens[index] == "#")
            {
                index++;
                return true;
            }
            else if (tokens[index] == ",")
            {
                index++;
                if (LexicalAnalyzer.Lexical_Analyzer.isID(tokens[index]))
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

        public static bool J(string token)
        {
            if (tokens[index] == "++" || tokens[index] == "--")
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
            else if (tokens[index] == "=" || tokens[index] == "comp_assign")
            {
                if (assign_opr(tokens[index]))
                {
                    if (OE(tokens[index]))
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

        public static bool K(string token)
        {
            if (tokens[index] == "int_const")
            {
                index++;
                if (M(tokens[index]))
                {
                    return true;
                }
            }
            else if (LexicalAnalyzer.Lexical_Analyzer.isID(tokens[index]))
            {
                index++;
                if (tokens[index] == "]")
                {
                    index++;
                    if (J(tokens[index]))
                    {
                        return true;
                    }
                }
            }
            else if (tokens[index] == "dec_const" || tokens[index] == "char_const" || tokens[index] == "str_const" || tokens[index] == "bool_const" || tokens[index] == "none")
            {
                index++;
                if (arr4(tokens[index]))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool M(string token)
        {
            if (tokens[index] == "]")
            {
                index++;
                if (N(tokens[index]))
                {
                    return true;
                }
            }
            else if (tokens[index] == ",")
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

            return false;
        }

        public static bool N(string token)
        {
            if (tokens[index] == "++" || tokens[index] == "--" || tokens[index] == "=" || tokens[index] == "comp_assign")
            {
                if (J(tokens[index]))
                {
                    return true;
                }
            }
            else if (tokens[index] == ",")
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

            return false;
        }

        public static bool H(string token)
        {
            if (tokens[index] == "." || tokens[index] == "[")
            {
                if (X2(tokens[index]))
                {
                    if (J(tokens[index]))
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

        public static bool body(string token, string scope)
        {
            if (tokens[index] == "#")
            {
                index++;
                return true;
            }
            else if (tokens[index] == "{")
            {
                index++;
                if (MST(tokens[index], scope))
                {
                    if (tokens[index] == "}")
                    {
                        index++;
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool MST(string token, string scope)
        {
            if (LexicalAnalyzer.Lexical_Analyzer.isID(tokens[index]) || LexicalAnalyzer.Lexical_Analyzer.isDT(tokens[index]) || tokens[index] == "DS" || tokens[index] == "loop" || tokens[index] == "when" || tokens[index] == "until" || tokens[index] == "++" || tokens[index] == "--" || tokens[index] == "jump" || tokens[index] == "skip" || tokens[index] == "ref_var" || tokens[index] == "toggle" || tokens[index] == "general" || tokens[index] == "personal" || tokens[index] == "protected" || tokens[index] == "symbolic" || tokens[index] == "final" || tokens[index] == "inacive" || tokens[index] == "refund" || tokens[index] == "try")
            {
                if (SST(tokens[index], scope))
                {
                    if (MST(tokens[index], scope))
                    {
                        return true;
                    }
                }
            }
            else if (tokens[index] == "}")
            {
                // to removel all elements from fields table
                FieldsTables.Clear();

                return true;
            }


            return false;
        }

    }

    
}
