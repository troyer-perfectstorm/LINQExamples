using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQExamples.Intro
{
    internal class Intro2_WhatIsIEnumerable
    {
        public Intro2_WhatIsIEnumerable()
        {
            // An IEnumerable is an interface that allows iterating over a collection
            // Almost every collection in C# you'll use implements the interface
            IEnumerable<int> listEnumerable = new List<int>() { 4, 3, 2, 1 };
            IEnumerable<int> arrayEnumerable = new int[10];
            IEnumerable<int> hashEnumerable = new HashSet<int>();
            IEnumerable<int> stackEnumerable = new Stack<int>();
            IEnumerable<int> queueEnumerable = new Queue<int>();
            IEnumerable<KeyValuePair<string, int>> dictionaryEnumerable = new Dictionary<string, int>();
            // Plus all the sorted, immutable, and concurrent collections

            // IEnumerable has one method: GetEnumerator which returns an IEnumerator
            var enumerator = listEnumerable.GetEnumerator();

            // An Enumerator can be thought of as a pointer to a current item in the collection plus a
            // method to advance that pointer (MoveNext)
            // By default the enumerator starts pointing at the default type
            Console.WriteLine("Iterating over list");
            while (enumerator.MoveNext())   // Returns false when no more items
            {
                Console.WriteLine(enumerator.Current);
            }

            // You can create as many enumerators as you want and they will each point to their own instance
            Console.WriteLine("Multiple enumerators");
            var skipTwo = listEnumerable.GetEnumerator();
            var skipOne = listEnumerable.GetEnumerator();

            skipTwo.MoveNext(); // 4
            skipTwo.MoveNext(); // 3
            skipTwo.MoveNext(); // 2
            Console.WriteLine($"Skip two is on item: {skipTwo.Current}");

            skipOne.MoveNext(); // 4
            skipOne.MoveNext(); // 3
            Console.WriteLine($"Skip one is on item: {skipOne.Current}");

            // C# built-in foreach loop works automatically with ANY IEnumerable
            foreach (var number in listEnumerable)
            {
                // An enumerator is allocated for you and number is filled with Current
                // This is why foreach is slower than 'for' with direct memory access
            }
        }

        public void TestCustomEnumerable()
        {
            Console.WriteLine("Custom Enumerable iterating in reverse");
            var myEnumerable = new CustomEnumerable();
            foreach (var number in myEnumerable)
            {
                Console.WriteLine(number);
            }
        }

        private class CustomEnumerable : IEnumerable<int>
        {
            private int[] Data { get; } = new int[] { 1, 2, 3, 4 };

            public IEnumerator<int> GetEnumerator()
            {
                return new CustomEnumerator(Data);
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        private class CustomEnumerator : IEnumerator<int>
        {
            private int[] Data { get; }
            private int Index { get; set; }

            public CustomEnumerator(int[] data)
            {
                Data = data;
                Index = Data.Length;
            }

            public int Current => (Index < Data.Length) ? Data[Index] : 0;

            object IEnumerator.Current => throw new NotImplementedException();

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                // Starts at the end and iterates in reverse
                Index--;
                return Index >= 0;
            }

            public void Reset()
            {
                Index = Data.Length;
            }
        }
    }
}
