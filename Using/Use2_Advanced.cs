using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQExamples.Using
{
    internal class Use2_Advanced
    {
        public Use2_Advanced()
        {
            Console.WriteLine();

            // LINQ is extremely good at working with complex or custom data types
            // You can build and reuse collections to answer just about any question

            var dataSet = new List<Person>()
            {
                new Person("Mike", 22),
                new Person("Alice", 9),
                new Person("Bob", 45)
                {
                    Children = new List<Person>()
                    {
                        new Person("Jeff", 12),
                        new Person("Frank", 14)
                    }
                },
                new Person("Janelle", 38)
                {
                    Children = new List<Person>()
                    {
                        new Person("Abby", 3),
                        new Person("Luke", 1)
                    }
                },
                new Person("Steven", 73)
                {
                    Children = new List<Person>()
                    {
                        new Person("Jeremy", 35)
                        {
                            Children = new List<Person>()
                            {
                                new Person("Adam", 1)
                            }
                        },
                        new Person("Kristin", 30)
                        {
                            Children = new List<Person>()
                            {
                                new Person("Tommy", 4),
                                new Person("Heather", 2)
                            }
                        }
                    }
                }
            };

            // How many kids does Steven have?
            var numberOfStevensChildren = dataSet.First(p => p.Name == "Steven").Children.Count;
            Console.WriteLine($"Steven has {numberOfStevensChildren} children");
            Console.WriteLine();

            // How many people are in the entire data set?
            var everyone = dataSet.SelectMany(p => p.GetSelfAndChildren());
            Console.WriteLine($"There are {everyone.Count()} people in the data set");

            // Who are the children?
            var allChildren = dataSet.SelectMany(p => p.Children.SelectMany(p => p.GetSelfAndChildren()));
            // What are these childrens names?
            var childrenNames = allChildren.Select(p => p.Name);
            Console.WriteLine($"All children include: {string.Join(", ", childrenNames)}");

            // Of all children, who is over 8?
            var childrenOver8 = allChildren.Where(p => p.Age > 8);
            var nameAndAgeOver8 = childrenOver8.Select(p => $"{p.Name} is {p.Age}");
            Console.WriteLine($"Children over 8: {string.Join(", ", nameAndAgeOver8)}");

            // Who have no children excluding children?
            var noChildren = dataSet.Where(p => p.Children.Count == 0);
            Console.WriteLine($"People without children: {string.Join(", ", noChildren.Select(p => p.Name))}");

            // Whos name starts with the letter J?
            var namesWithJ = everyone.Where(p => p.Name.StartsWith('J')).Select(p => p.Name);
            Console.WriteLine($"People with names that start with J: {string.Join(", ", namesWithJ)}");

            // Who are the top 5 oldest people?
            var oldest = everyone.OrderByDescending(p => p.Age).Take(5);
            var oldestNameAndAge = oldest.Select(p => $"{p.Name} is {p.Age}");
            Console.WriteLine($"Oldest people are: {string.Join(", ", oldestNameAndAge)}");

            // Who are the top 5 youngest people
            var youngest = everyone.OrderBy(p => p.Age).Take(5);
            var youngestNameAndAge = youngest.Select(p => $"{p.Name} is {p.Age}");
            Console.WriteLine($"Youngest people are: {string.Join(", ", youngestNameAndAge)}");

            // Is anyone named Jeremy or Jonathan?
            var everyoneByName = everyone.ToDictionary(p => p.Name);
            Console.WriteLine($"Someone named Jeremy: {everyoneByName.ContainsKey("Jeremy")}");
            Console.WriteLine($"Someone named Jonathan: {everyoneByName.ContainsKey("Jonathan")}");

            // Who has a kid named Tommy?
            var parentsWithKidNamedTommy = everyone.Where(p => p.Children.Any(p => p.Name == "Tommy")).Select(p => p.Name);
            Console.WriteLine($"Someone with a kid named Tommy: {string.Join(", ", parentsWithKidNamedTommy)}");

            // What is the average age of everyone?
            Console.WriteLine($"Average age of everyone is: {everyone.Select(p => p.Age).Average()}");
        }
    }

    public class Person
    {
        public string Name { get; }
        public int Age { get; }
        public List<Person> Children { get; init; } = new List<Person>();

        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public IEnumerable<Person> GetSelfAndChildren()
        {
            var result = Children.SelectMany(p => p.GetSelfAndChildren()).ToList();
            result.Add(this);

            return result;
        }
    }
}
