using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orion.Classes.SemanticAnalyzer.SymbolTable
{
    class MainTable
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string AM { get; set; }      // Access modifier
        public string NAM { get; set; }     // Non Access modifier to check class is final or not

    }
}
