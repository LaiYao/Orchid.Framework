using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Formatting;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CSharp.RuntimeBinder;

namespace GenerateAssemblyWithGrpc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var compilation =CSharpCompilation.Create("test.dll");
        }
    }
}
