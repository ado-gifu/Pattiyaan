using System;

namespace Pattiyaan
{
    public partial class Interpreter
    {
        private void AddNativeWords()
        {
            // Base setters.
            NativeWords.Add("hex", WordHex);
            NativeWords.Add("digi", WordDigi);
            NativeWords.Add("octal", WordOctal);
            NativeWords.Add("bin", WordBin);

            NativeWords.Add(".", WordDisplay);

            NativeWords.Add("+", WordAdd);
            NativeWords.Add("-", WordSub);
            NativeWords.Add("*", WordMul);
            NativeWords.Add("/", WordDiv);
        }

        private void WordHex()
        {
            NumericBase = 16;
        }

        private void WordDigi()
        {
            NumericBase = 10;
        }

        private void WordOctal()
        {
            NumericBase = 8;
        }

        private void WordBin()
        {
            NumericBase = 2;
        }

        private void WordDisplay()
        {
            var value = NumStack.Pop();

            Console.Write($"{value} ");
        }
    
        private void WordAdd()
        {
            var a = NumStack.Pop();
            var b = NumStack.Pop();

            NumStack.Push(a + b);
        }

        private void WordSub()
        {
            var a = NumStack.Pop();
            var b = NumStack.Pop();

            NumStack.Push(a - b);
        }

        private void WordMul()
        {
            var a = NumStack.Pop();
            var b = NumStack.Pop();

            NumStack.Push(a * b);
        }

        private void WordDiv()
        {
            var a = NumStack.Pop();
            var b = NumStack.Pop();

            NumStack.Push(a / b);
        }
    }
}