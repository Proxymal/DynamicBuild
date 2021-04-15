using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;

namespace DynamicBuild.Roslyn
{
    public class RoslynCSharpBuilder
    {
        public string OutputFile { get; set; }
        public OutputKind OutputType { get; set; }
        public LanguageVersion CSharpVersion { get; set; }
        public OptimizationLevel Optimization { get; set; }
        public Platform Platform { get; set; }
        public MetadataImportOptions MetadataImportOptions { get; set; }

        public bool OverflowCheck { get; set; }
        public bool AllowUnsafe { get; set; }

        public string RuntimePath { get; set; }

        public List<string> SourceCodes { get; set; }
        public List<string> DefaultNamespaces { get; set; }
        public List<MetadataReference> DefaultReferences { get; set; }

        public RoslynCSharpBuilder(string outputFile,string netFrameworkVersion="v4.5.1")
        {
            OutputFile = outputFile;
            OutputType = OutputKind.ConsoleApplication;
            CSharpVersion = LanguageVersion.CSharp6;
            Optimization = OptimizationLevel.Release;
            Platform = Platform.AnyCpu;
            MetadataImportOptions = MetadataImportOptions.All;
            OverflowCheck = true;
            AllowUnsafe = false;

            RuntimePath = $@"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\{netFrameworkVersion}";

            DefaultNamespaces = new List<string>()
            {
                "System",
                "System.IO",
                "System.Net",
                "System.Linq",
                "System.Text",
                "System.Text.RegularExpressions",
                "System.Collections.Generic"
            };
            DefaultReferences = new List<MetadataReference>()
            {
                MetadataReference.CreateFromFile(RuntimePath + @"\mscorlib.dll"),
                MetadataReference.CreateFromFile(RuntimePath + @"\System.dll"),
                MetadataReference.CreateFromFile(RuntimePath + @"\System.Core.dll")
            };
        }
        public SyntaxTree ParseSyntax(string code, CSharpParseOptions options = null)
        {
            return SyntaxFactory.ParseSyntaxTree(code, options);
        }

        public List<Diagnostic> Compile()
        {
            CSharpCompilationOptions DefaultCompilationOptions =
            new CSharpCompilationOptions(OutputType)
                    .WithOverflowChecks(OverflowCheck).WithOptimizationLevel(Optimization)
                    .WithUsings(DefaultNamespaces).WithPlatform(Platform).
                    WithAllowUnsafe(AllowUnsafe).WithMetadataImportOptions(MetadataImportOptions);

            List<SyntaxTree> trees = new List<SyntaxTree>();
            foreach (string sourceCode in SourceCodes)
                trees.Add(ParseSyntax(sourceCode, CSharpParseOptions.Default.WithLanguageVersion(CSharpVersion)));

            var compilation
                = CSharpCompilation.Create(OutputFile, trees.ToArray(), DefaultReferences, DefaultCompilationOptions);

            var result = compilation.Emit(OutputFile);
            return result.Diagnostics.ToList();
        }
    }
}
