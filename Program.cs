using Orion.Classes.LexicalAnalyzer;
using Orion.Classes.SyntaxAnalyzer;

namespace Orion
{
    class Program
    {
        static void Main(string[] args)
        {
            // for first line/hedaing in output file
            System.IO.StreamWriter file = new System.IO.StreamWriter(@"D:\UoK\CSSE\CSSE-V\CC\Lab\Projects\Orion\TokenFiles\tokens.txt");
            file.WriteLine("(classPart, valuePart, lineNo)\n------------------------------------------\n");
            file.Close();

            // call word breaker to generate tokens
            Lexical_Analyzer.wordBreaker();

            // it ends the token
            Syntax_Analyzer.tokens.Add("$");


            // for ending tokens with dollar sign
            Syntax_Analyzer.printTokens();

        }
    }
}
