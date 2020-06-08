using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.JScript;
using System.CodeDom.Compiler;
using System.IO;

namespace DynamicBuild.CodeDom
{
    public class CodeDomJScriptBuilder : ICodeDomBuilder
    {
        public List<string> IncludedLibraries { get; set; }
        public List<string> EmbeddedResources { get; set; }
        public List<string> LinkedResources { get; set; }
        public string OutputFile { get; set; }
        public string MainClass { get; set; }

        /// <summary>
        /// Default: false
        /// </summary>
        public bool GenerateInMemory { get; set; }
        /// <summary>
        /// Default: true
        /// </summary>
        public bool Executable { get; set; }
        /// <summary>
        /// Default: true
        /// </summary>
        public bool IncludeDebugInfo { get; set; }

        public CodeDomJScriptBuilder(string outputFile)
        {
            OutputFile = outputFile;
            MainClass = null;
            IncludedLibraries = new List<string>();
            IncludedLibraries.Add("mscorlib.dll");
            IncludedLibraries.Add("System.Core.dll");
            EmbeddedResources = new List<string>();
            LinkedResources = new List<string>();

            GenerateInMemory = false;
            Executable = true;
            IncludeDebugInfo = true;
        }

        public CompilerResults Build(string[] sourceCodes)
        {
            var csc = new JScriptCodeProvider();

            var parameters = new CompilerParameters();

            parameters.OutputAssembly = OutputFile;
            parameters.GenerateExecutable = Executable;
            parameters.GenerateInMemory = GenerateInMemory;
            parameters.IncludeDebugInformation = IncludeDebugInfo;

            parameters.ReferencedAssemblies.AddRange(IncludedLibraries.ToArray());
            parameters.EmbeddedResources.AddRange(EmbeddedResources.ToArray());
            parameters.LinkedResources.AddRange(LinkedResources.ToArray());

            if (MainClass != null)
                parameters.MainClass = MainClass;

            CompilerResults results = csc.CompileAssemblyFromSource(parameters, sourceCodes);
            return results;
        }
        public CompilerResults Build(string dirWithSrc)
        {
            string[] sources = GetSourcesFromDir(dirWithSrc, new string[] { ".cs" });

            var csc = new JScriptCodeProvider();

            var parameters = new CompilerParameters();

            parameters.OutputAssembly = OutputFile;
            parameters.GenerateExecutable = Executable;
            parameters.GenerateInMemory = GenerateInMemory;
            parameters.IncludeDebugInformation = IncludeDebugInfo;

            parameters.ReferencedAssemblies.AddRange(IncludedLibraries.ToArray());
            parameters.EmbeddedResources.AddRange(EmbeddedResources.ToArray());
            parameters.LinkedResources.AddRange(LinkedResources.ToArray());

            if (MainClass != null)
                parameters.MainClass = MainClass;

            CompilerResults results = csc.CompileAssemblyFromSource(parameters, sources);
            return results;
        }

        private string[] GetSourcesFromDir(string dirPath, string[] formats)
        {
            List<string> src = new List<string>();

            foreach (string file in Directory.GetFiles(dirPath))
            {
                if (IsFormat(file, formats))
                    src.Add(File.ReadAllText(file));
            }
            foreach (string dir in Directory.GetDirectories(dirPath))
            {
                src.AddRange(GetSourcesFromDir(dir, formats));
            }

            return src.ToArray();
        }
        private bool IsFormat(string filepath, string[] formats)
        {
            foreach (string format in formats)
            {
                if (filepath.EndsWith(format))
                    return true;
            }
            return false;
        }
    }
}
