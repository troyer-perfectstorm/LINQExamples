using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQExamples
{
    [MemoryDiagnoser]
    public class PerformanceTest
    {
        private readonly int[] Data;

        private record class User(string Name, int Age);

        private readonly List<User> Users;

        public PerformanceTest()
        {
            var rand = new Random();
            Data = new int[64];
            for (int i = 0; i < Data.Length; i++)
            {
                Data[i] = rand.Next(100);
            }

            Users = new List<User>();
            for (int i = 0; i < 100; i++)
            {
                Users.Add(new User(Guid.NewGuid().ToString("N"), rand.Next(100)));
            }
        }

        [Benchmark]
        public List<int> FilterManual()
        {
            var result = new List<int>();
            for (int i = 0; i < Data.Length; i++)
            {
                if (Data[i] > 50)
                {
                    result.Add(Data[i]);
                }
            }

            return result;
        }

        [Benchmark]
        public List<int> FilterLINQ()
        {
            return Data.Where(i => i > 50).ToList();
        }

        [Benchmark]
        public double AverageManual()
        {
            int sum = 0;
            for (int i = 0; i < Data.Length; i++)
            {
                sum += Data[i];
            }

            return (double)sum / Data.Length;
        }

        [Benchmark]
        public double AverageLINQ()
        {
            return Data.Average();
        }

        [Benchmark]
        public List<string> ComplexManual()
        {
            // Get the names of users who are under 50 sorted from highest to lowest
            var under50 = new List<User>();
            for (int i = 0; i < Users.Count; i++)
            {
                if (Users[i].Age < 50)
                {
                    under50.Add(Users[i]);
                }
            }

            under50.Sort((a, b) => b.Age.CompareTo(a.Age));

            var result = new List<string>();
            for (int i = 0; i < under50.Count; i++)
            {
                result.Add(under50[i].Name);
            }

            return result;
        }

        [Benchmark]
        public List<string> ComplexLINQ()
        {
            return Users.Where(u => u.Age < 50)
                .OrderByDescending(u => u.Age)
                .Select(u => u.Name)
                .ToList();
        }
    }
}
