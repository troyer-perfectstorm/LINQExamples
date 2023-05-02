using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQExamples.Intro
{
    internal class Intro3_WhatIsAMonad
    {
        public Intro3_WhatIsAMonad()
        {
            // A monad is a structure of functions that wraps the output of those functions so they are composable

        }

        public class Maybe<T>
        {
            public bool Exists { get; private set; }
            private T? Result { get; set; }

            public static readonly Maybe<T> None = new Maybe<T>();

            // Constructor for creating a Maybe with a valid result
            public Maybe(T result)
            {
                Result = result;
                Exists = true;
            }

            // Constructor for creating a Maybe with no result
            private Maybe()
            {
                Exists = false;
            }

            // Method to call if object exists or failed (optionally)
            public Maybe<T> Match(Func<T, Maybe<T>> success, Action? failure = null)
            {
                if (Exists)
                {
                    return success(Result!);
                }

                failure?.Invoke();

                return None;
            }

            // implicit operator to make it look clearer
            public static implicit operator Maybe<T>(T? result)
            {
                if (result == null)
                {
                    return None;
                }

                return new Maybe<T>(result);
            }
        }

        public class MyMonad<T>
        {
            private List<Func<T, Maybe<T>>> Functions { get; } = new List<Func<T, Maybe<T>>>();

            public MyMonad<T> Join(Func<T, Maybe<T>> func)
            {
                Functions.Add(func);
                return this;
            }

            public Maybe<T> Execute(T initializer)
            {
                var last = new Maybe<T>(initializer);

                for (int i = 0; i < Functions.Count; i++)
                {
                    last = last.Match(Functions[i]);
                }

                return last;
            }
        }

        public void TestMonad()
        {
            Console.WriteLine("Building a monad that solves y = ((x + 3)* 2) - 1");
            var myMonad = new MyMonad<int>()
                .Join(i => i + 3)
                .Join(i => i * 2)
                .Join(i => i - 1);

            myMonad.Execute(2).Match(i =>
            {
                Console.WriteLine($"Input 2 and got {i}");
                return i;
            });

            myMonad.Execute(10).Match(i =>
            {
                Console.WriteLine($"Input 10 and got {i}");
                return i;
            });

            Console.WriteLine("Build a monad that does nothing if x + 10 > 0");
            myMonad = new MyMonad<int>()
                .Join(i => i + 10)
                .Join(i =>
                {
                    if (i > 0)
                        return Maybe<int>.None;

                    return i;
                });

            var result = myMonad.Execute(10);

            // Match should only be called for failure case
            result.Match(i =>
            {
                Console.WriteLine("This should not execute");
                return i;
            }, failure: () =>
            {
                Console.WriteLine("Input 10 and got no result");
            });

            result = myMonad.Execute(-20).Match(i =>
            {
                Console.WriteLine($"Input -20 and got {i}");
                return i;
            });
        }
    }
}
