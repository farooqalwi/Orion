using Orion.Classes.LexicalAnalyzer;
using Orion.Classes.SemanticAnalyzer;
using Orion.Classes.SyntaxAnalyzer;

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

        }
    }
}
