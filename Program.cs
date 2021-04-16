using Microsoft.SqlServer.Server;
using Orion.Classes.LexicalAnalyzer;
using System.Linq;

namespace Orion
{
    class Program
    {
        static void Main(string[] args)
        {

            Lexical_Analyzer.wordBreaker();

            //string name = "class abf\\\"adab";
            //var count = name.Count(x => x == '\"');
            //System.Console.WriteLine(count == 2);

            //System.Console.WriteLine(name[9]);
        }
    }
}
