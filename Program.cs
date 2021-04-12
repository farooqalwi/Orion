using Orion.Classes.LexicalAnalyzer;
using Orion.Classes.REs;


namespace Orion
{
    class Program
    {
        static void Main(string[] args)
        {
            //to validate REs
            //new REs_Validation();

            System.Console.WriteLine(Lexical_Analyzer.isKW("num"));
            System.Console.WriteLine(Lexical_Analyzer.isOperator("+"));
            System.Console.WriteLine(Lexical_Analyzer.isPunctuator("}"));
        }
    }
}
