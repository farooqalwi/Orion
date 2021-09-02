using Orion.Classes.LexicalAnalyzer;
using Orion.Classes.SyntaxAnalyzer;
using Orion.Classes.SemanticAnalyzer;
using Orion.Classes.SemanticAnalyzer.SymbolTable;
using System;

namespace Orion
{
    class Program
    {
        static void Main(string[] args)
        {
            // calling Lexical Analyzer
            Lexical_Analyzer.LexicalAnalyzer();

            // calling Syntax Analyzer
            Syntax_Analyzer.SyntaxAnalyzer();

            // calling Semantic Analyzer
            Semantic_Analyzer.SemanticAnalyzer();

            //Semantic_Analyzer.iterate();

        }

    }
}
