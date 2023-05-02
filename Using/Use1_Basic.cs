using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LINQExamples.Using
{
    internal class Use1_Basic
    {
        public Use1_Basic()
        {
            // LINQ is a set of extension methods on an IEnumerable that make working with collections of anything a breeze

            // There are two types of LINQ methods:
            // 1. Method that returns an IEnumerable
            //      Ex: Select, Where, Skip, Take, Reverse, etc
            // 2. Method that finalizes a result (ie gets an answer)
            //      Ex: Sum, ToList, First, Last, Any, All, ToDictionary, etc

            // Methods that return an IEnumerable can be infinitely chained and don't actually produce a result
            IEnumerable<int> nothing = new List<int>().Skip(20).Take(3).Reverse();

            // LINQ Enumerables can be built over multiple lines or built conditionaly
            const int aFew = 3;
            nothing = nothing.Take(1).Take(2).Skip(aFew).Take(99).Take(100);
            
            if (aFew > 0)
            {
                nothing = nothing.Take(1);
            }


            // To actually get a result out of the IEnumerable you need to call one of the finalizers
            var result = nothing.ToList();

            // Notice how actions can be performed on empty collections without worry of exploding
            // All the error handling save for a null IEnumerable is handled for us

            // LINQ often produces code that is easier to read and maintain
            TestSum();
            TestConditionalSum();
            TestConversion();
        }

        private void TestSum()
        {
            Console.WriteLine();
            Console.WriteLine("Summing Numbers Example");
            var numbers = new int[] { 16, 42, 99, 57 };

            int withoutLINQ = 0;
            for (int i = 0; i < numbers.Length; i++)
            {
                withoutLINQ += numbers[i];
            }
            Console.WriteLine($"Without LINQ arrived at {withoutLINQ}");

            int withLINQ = numbers.Sum();
            Console.WriteLine($"With LINQ arrived at {withLINQ}");
        }

        private void TestConditionalSum()
        {
            var numbers = new int[] { 52, 12, 400, 95, 38 };
            const int minimumNumber = 50;

            Console.WriteLine();
            Console.WriteLine($"Summing numbers >= {minimumNumber}");

            int withoutLINQ = 0;
            for (int i = 0; i < numbers.Length; i++)
            {
                if (numbers[i] >= minimumNumber)
                {
                    withoutLINQ += numbers[i];
                }
            }
            Console.WriteLine($"Without LINQ arrived at {withoutLINQ}");

            int withLINQ = numbers.Where(i => i >= minimumNumber).Sum();
            Console.WriteLine($"With LINQ arrived at {withLINQ}");
        }

        private void TestConversion()
        {
            Console.WriteLine();
            Console.WriteLine("Convert to an array of floats and add 0.5");
            var numbers = new int[] { 16, 42, 99, 57 };

            var withoutLINQ = new float[numbers.Length];
            for (int i = 0; i < numbers.Length; i++)
            {
                withoutLINQ[i] = (float)numbers[i] + 0.5f;
            }

            var withLINQ = numbers.Select(i => (float)i + 0.5f).ToArray();
        }

    }
}
