using System;
using System.Collections;

namespace Pattiyaan
{
    class NumStack : Stack
    {
        public NumStack(int stackSize) : base(stackSize)
        {
            
        }

        new public void Push(int num)
        {
            base.Push(num);
        }

        new public int Pop()
        {
            return (int) base.Pop();
        }

        new public int Peek()
        {
            return (int) base.Peek();
        }
    }
}