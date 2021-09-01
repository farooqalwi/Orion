using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orion.Classes.SemanticAnalyzer.SymbolTable
{
    class FunctionTable
    {
        // function Name
        public string Name { get; set; }

        // Type means function return type
        public string Type { get; set; }

        // Scope means class name reference
        public string Scope { get; set; }
    }
}
