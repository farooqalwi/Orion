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

            //string name = "sdfs\"faf\"sfefe";
            //var count = name.Count(x => x == '"');
            //System.Console.WriteLine(count == 1);
        }
    }
}
