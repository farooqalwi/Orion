using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orion.Classes.SemanticAnalyzer.SymbolTable
{
    class FieldsTable
    {
        public string Name { get; set; }

        // Scope means class name reference
        public string Scope { get; set; }
    }
}
