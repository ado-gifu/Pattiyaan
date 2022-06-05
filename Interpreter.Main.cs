using System;
using System.Text;
using System.Collections.Generic;
using System.Collections;

namespace Pattiyaan
{
    public partial class Interpreter
    {
        const int MaxStackSize = 1000;

        bool IsCompiling = false;
        string CompWord = ""; // Word for which compilation is being done.
        string CompToken = ""; // Token for which code is being compiled.
        int NumericBase = 10; // The default, but can be changed. Only 2, 8, 10, or 16 are allowed.
        NumStack NumStack = new NumStack(MaxStackSize);

        Dictionary<string, Action> NativeWords = new Dictionary<string, Action>();
        Dictionary<string, List<string>> CompiledWords = new Dictionary<string, List<string>>();

        public Interpreter()
        {
            AddNativeWords();
        }

        private List<string> ProcessRaw(string rawStr)
        {
            // Processes the raw input string and outputs a list of tokens it contains.
            var sb = new StringBuilder(rawStr.ToLower());

            sb.Replace('\t', ' ');
            sb.Replace('\n', ' ');
            sb.Replace('\r', ' ');

            var tokens = sb.ToString().Split();
            var finalTokens = new List<string>();

            foreach(var token in tokens)
            {
                if(token.Length != 0)
                    finalTokens.Add(token);
            }

            return finalTokens;
        }

        public void Execute(string rawStr)
        {
            var tokens = ProcessRaw(rawStr);

            ExecuteTokens(tokens);
        }

        private bool IsNumeric(string token)
        {
            var isNumeric = true;

            try
            {
                Convert.ToInt32(token, NumericBase);
            }
            catch(FormatException e)
            {
                isNumeric = false;
            }

            return isNumeric;
        }

        private void ExecuteTokens(List<string> tokens)
        {
            foreach(var token in tokens)
            {
                try
                {
                    if(IsCompiling)
                    {
                        if(token == ";")
                        {
                            // End compilation.
                            CompWord = "";

                            IsCompiling = false;
                        }
                        else if(CompWord == "")
                        {
                            // Below will actually allow recompilation.
                            CompWord = token;

                            CompiledWords[CompWord] = new List<string>();
                        }
                        else
                        {
                            CompiledWords[CompWord].Add(token);
                        }
                    }
                    else
                    {
                        if(token == ":")
                        {
                            IsCompiling = true;
                        }
                        else if(IsNumeric(token))
                        {
                            // Check if token is a number before doing anything else.
                            // If so, push it onto the stack.
                            NumStack.Push( Convert.ToInt32(token, NumericBase));
                        }
                        else if(CompiledWords.ContainsKey(token))
                        {
                            var subTokens = CompiledWords[token];

                            ExecuteTokens(subTokens);
                        }
                        else if(NativeWords.ContainsKey(token))
                        {
                            var action = NativeWords[token];

                            action();
                        }
                        else
                        {
                            throw new InterpretationException($"Unknown token: {token}");
                        }
                    }
                }
                catch(Exception e)
                {
                    // Currently, all errors are considered interpretation errors.
                    // That may need to change.
                    throw new InterpretationException(e.Message);
                }
            }
        }
    }
}