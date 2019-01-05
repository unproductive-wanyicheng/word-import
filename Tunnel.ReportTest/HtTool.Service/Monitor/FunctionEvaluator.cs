﻿using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace HtTool.Service.Monitor
{
    public class FunctionEvaluator
    {
        string m_text = string.Empty;
        bool m_compiled = false;
        object calc = null;

        CodeCompileUnit cu = new CodeCompileUnit();
        CodeNamespace ns = new CodeNamespace("HtTool.Service.Monitor");
        CodeMemberMethod method = new CodeMemberMethod();
        CodeTypeDeclaration cl = new CodeTypeDeclaration("Calculator");

        CompilerResults compile_result;
        ICodeCompiler compiler;

        void init_compiler()
        {
            Microsoft.CSharp.CSharpCodeProvider csharp = new Microsoft.CSharp.CSharpCodeProvider();//!!VBSubst   Dim vb As New Microsoft.VisualBasic.VBCodeProvider()
            compiler = csharp.CreateCompiler(); //!!VBSubst  compiler = vb.CreateCompiler()
        }

        void init()
        {
            cu.Namespaces.Add(ns);
            ns.Imports.Add(new CodeNamespaceImport("System"));
            ns.Types.Add(cl);
            method.Parameters.Add(new CodeParameterDeclarationExpression(typeof(double), "x"));
            method.ReturnType = new CodeTypeReference(typeof(double));

            cl.TypeAttributes = TypeAttributes.Public;
            cl.Members.Add(method);
            method.Attributes = MemberAttributes.Public;// | MemberAttributes.Static;
            method.Name = "Run";

            init_compiler();
        }

        public FunctionEvaluator()
        {
            init();
        }

        public string Text
        {
            get
            {
                return m_text;
            }
            set
            {
                if (!m_text.Equals(value))
                {
                    m_text = value;
                    m_compiled = false;
                }
            }
        }

        bool Compiled
        {
            get
            {
                return m_compiled;
            }
        }

        public double Invoke(double x)
        {
            if (!Compile())
                throw new Exception("Error during compile");

            Type test = compile_result.CompiledAssembly.GetType("HtTool.Service.Monitor.Calculator");
            MethodInfo m = test.GetMethod("Run");
            object[] args = (object[])Array.CreateInstance(typeof(object), 1);
            args[0] = x;
            return (double)m.Invoke(calc, args);
        }

        public bool Compile()
        {
            if (m_compiled)
                return true;

            method.Statements.Clear();
            string code;
            code = "return " + m_text + ";"; //!!VBSubst  code = "return " + m_text
            method.Statements.Add(new CodeExpressionStatement(new CodeSnippetExpression(code)));

            CompilerParameters compparams = new CompilerParameters(new string[] { "mscorlib.dll" });
            compparams.GenerateInMemory = true;

            if (compiler != null)
                compile_result = compiler.CompileAssemblyFromDom(compparams, cu);

            if (compile_result == null || compile_result.Errors.Count > 0)
                return false;

            m_compiled = true;
            calc = compile_result.CompiledAssembly.CreateInstance("HtTool.Service.Monitor.Calculator");
            return true;
        }

        public string[] Errors
        {
            get
            {
                if (m_compiled)
                    return null;
                int cnt = compile_result.Errors.Count;
                string[] err = new string[cnt];
                for (int i = 0; i < cnt; i++)
                    err[i] = compile_result.Errors[i].ErrorText;
                return err;
            }
        }
    }
}
