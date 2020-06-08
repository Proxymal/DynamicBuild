using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System.CodeDom;
using System.CodeDom.Compiler;
using Microsoft.CSharp;

namespace DynamicBuild.CodeDom
{
    public class CodeDomCodeGenerator
    {
        public CodeCompileUnit CompileUnit { get; set; }

        public CodeDomCodeGenerator()
        {
            CompileUnit = new CodeCompileUnit();
        }

        public CodeNamespace CreateNamespace(string name,string[] imports)
        {
            CodeNamespace ns = new CodeNamespace(name);
            foreach (string import in imports)
                ns.Imports.Add(new CodeNamespaceImport(import));
            CompileUnit.Namespaces.Add(ns);
            return ns;
        }
        public CodeTypeDeclaration CreateClass(CodeNamespace ns, string name,TypeAttributes attr)
        {
            CodeTypeDeclaration cl = new CodeTypeDeclaration(name);
            cl.IsClass = true;
            cl.TypeAttributes = attr;
            ns.Types.Add(cl);
            return cl;
        }
        
        public enum Language { CSharp, VB, Cpp, JScript }
        public string Generate(Language lang)
        {
            CodeDomProvider provider = CodeDomProvider.CreateProvider(lang.ToString());
            CodeGeneratorOptions options = new CodeGeneratorOptions();
            options.BracingStyle = "C";
            using (StreamWriter sourceWriter = new StreamWriter("_GENTEMP.genc"))
            {
                provider.GenerateCodeFromCompileUnit(
                    CompileUnit, sourceWriter, options);
            }
            string res = File.ReadAllText("_GENTEMP.genc");
            File.Delete("_GENTEMP.genc");
            return res;
        }
        public void Compile(ICodeDomBuilder builder)
        {
            if(builder is CodeDomCSharpBuilder)
            {
                ((CodeDomCSharpBuilder)builder).Build(new string[] { Generate(Language.CSharp) });
            }
            else if (builder is CodeDomVisualBasicBuilder)
            {
                ((CodeDomVisualBasicBuilder)builder).Build(new string[] { Generate(Language.VB) });
            }
            else if (builder is CodeDomCppBuilder)
            {
                ((CodeDomCppBuilder)builder).Build(new string[] { Generate(Language.Cpp) });
            }
            else if (builder is CodeDomJScriptBuilder)
            {
                ((CodeDomJScriptBuilder)builder).Build(new string[] { Generate(Language.JScript) });
            }
        }
    }
}
