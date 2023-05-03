using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQExamples.Intro
{
    internal class Intro1_WhatIsLINQ
    {
        public Intro1_WhatIsLINQ()
        {
            // LINQ = Language Integrated Query
            // Created 2007 released in .NET 3.5

            /* Born from 2 major realizations
             * 1. We have way too many ways of querying data
             * 2. Querying data in a string is very error prone
             */

            // A SQL database, an XML document, a list of customers all require different methods
            // Why cant I use a single language to query them all?


            Console.WriteLine("Querying data in a string with no compile time check");
            Console.WriteLine("select * from scores where value > 30");
            Console.WriteLine("select * from scores where vlue > 30"); // Whoops, I have a typo
            Console.WriteLine();


            // Popular misconceptions:

            // LINQ is just SQL:
            //  FALSE: LINQ to SQL was the first popular use case but LINQ is general purpose

            // LINQ is a new concept only to C#:
            //  FALSE: While its name is unique to C# it's idea and implementation comes from functional programming
            //  and variations can be found in many other languages pre and post dating.
            // Haskell monads (since 1990), Python Generator Expressions (2002), Java 8 Stream API (2014), Haxe Lambdas (mid 2010s)
            // Libraries available in Rust, GO, Kotlin, Clojure, Dart, Elixir

        }

        public void TwoFormatsOfLINQ()
        {
            int[] scores = { 97, 92, 81, 60 };

            // Without LINQ
            var manualResult = new List<int>();
            for (int i = 0; i < scores.Length; i++)
            {
                int score = scores[i];
                if (score > 80)
                {
                    manualResult.Add(score);
                }
            }

            Console.WriteLine("Results from manual iteration without LINQ");
            foreach (var result in manualResult)
            {
                Console.WriteLine(result);
            }

            ////////////////////////////

            // Query Syntax
            var resultFromQuery = 
                from score in scores
                where score > 80
                select score;

            Console.WriteLine("Results from Query syntax");
            foreach (var result in resultFromQuery)
            {
                Console.WriteLine(result);
            }

            /////////////////////////////

            // Method syntax
            var resultFromMethod = scores.Where(score => score > 80);

            Console.WriteLine("Results from Method syntax");
            foreach (var result in resultFromMethod)
            {
                Console.WriteLine(result);
            }

            // The Method syntax is preferred as it is less verbose and more natural using lambdas
        }
    }
}
