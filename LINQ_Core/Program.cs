using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise5
{
    internal class Program
    {
        private static List<Person> persons;
        static void Main(string[] args)
        {
            persons = Person.readData1();

            Exercise5_3();

        }

        private static void Exercise5_2()
        {
            List<int> ints = new List<int>();

            ints.Add(1);
            ints.Add(2);
            ints.Add(13);
            ints.Add(14);
            ints.Add(15);
            ints.Add(17);
            ints.Add(16);  
            ints.Add(18);
            ints.Add(7);

            int last = ints.Last(i => i > 15);

            System.Console.WriteLine(last);
            
            int lastIndex = ints.LastIndexOf(last); //non linq
            Console.WriteLine(lastIndex);


            //Linq
            var tupler = ints.Select((x, i) => new
            {
                value = x,
                index = i
            });
            int result = tupler.Last(x => x.value > 15).index;
            Console.WriteLine(result);
            Console.ReadLine();

        }

        private static void Exercise5_3()
        {
            List<Person> under2 = persons.FindAll(p => p.Score < 2);
            Show("under2:", under2);

            List<Person> even = persons.FindAll(p => p.Score % 2 == 0);
            Show("even:", even);

            List<Person> evenOver60 = persons.FindAll(p => p.Score % 2 == 0 && p.Weight > 60);
            Show("even, weight over 60:", evenOver60);

            List<Person> divisibleBy3 = persons.FindAll(p => p.Score % 3 == 0);
            Show("Divisible by 3:", divisibleBy3);

        }

        private static void Show(String tekst, IEnumerable<Object> list
            )
        {
            Console.WriteLine(tekst);
            foreach (Object o in list)
            {
                Console.WriteLine(o.ToString());
            }
            Console.ReadLine();
        }

        private static void Exercise5_4()
        {
            int score3Index = persons.FindIndex(p => p.Score == 3);
            int score3IndexUnderAge10 = persons.FindIndex(p => p.Score == 3 && p.Age < 10);
            int countScore3IndexUnderAge10 = persons.FindAll(p => p.Score == 3 && p.Age < 10).Count();
            int indexAgeUnder3Score3 = persons.FindIndex(p => p.Score == 3 && p.Age < 8);
        }
        private static void Exercise5_5()
        {
            persons.Sort((p1, p2) => p1.Score.CompareTo(p2.Score));
            Show("Sorted by Score:", persons);

            persons.Sort((p1, p2) => p1.Age.CompareTo(p2.Age));
            Show("Sorted by Score:", persons);

        }
        private static void Exercise5_6()
        {
            persons.SetAccepted(p => p.Age > 34 && p.Age < 47);
            persons.Sort((p1, p2) => p1.Age.CompareTo(p2.Age));
            Show("Accepted between ages 34 and 47", persons);
        }
        private static void Exercise5_7()
        {
            IEnumerable<Person> personsSorted = persons.OrderBy(p => p.Age);
            Show("Sorted by age using Linq", personsSorted);
            Show("persons is still unsorted", persons);
            personsSorted = persons.OrderBy(p => p.Score);
            Show("Sorted by score using Linq", personsSorted);
            Show("persons is still unsorted", persons);
        }
        private static void Exercise5_8()
        {
            int[] numbers = { 34, 8, 56, 31, 79, 150, 88, 7, 200, 47, 88, 20 };
            IEnumerable<int> twoDigitsAsc = numbers.Where(i => i > 9 && i < 100).OrderBy(i => i);
            Show("twoDigitsAsc", twoDigitsAsc);

            IEnumerable<int> twoDigitsDesc = numbers.Where(i => i > 9 && i < 100).OrderByDescending(i => i);
            Show("twoDigitsDesc", twoDigitsDesc);

            IEnumerable<String> twoDigitsAscStrings = numbers.Where(i => i > 9 && i < 100).OrderBy(i => i).Select(i => i.ToString());
            Show("twoDigitsAscStrings", twoDigitsAscStrings);

            IEnumerable<String> twoDigitsDescEvenOrOdd = numbers.Where(i => i > 9 && i < 100).OrderByDescending(i => i).Select(i => i.ToString() + (i % 2 == 0 ? " even" : i.ToString() + " uneven"));
            Show("twoDigitsDescEvenOrOdd", twoDigitsDescEvenOrOdd);
        }
        private static void Show(String tekst, IEnumerable<int> ints)
        {
            Console.WriteLine(tekst);
            foreach (int i in ints)
            {
                Console.WriteLine(i);
            }
            Console.ReadLine();
        }

        private static void Exercise5_10()
        {
            Random rnd = new Random();
            List<int> numbers = new List<int>();
            for (int i = 0; i < 100; ++i)
            {
                numbers.Add(rnd.Next(100));
            }
            Show("All random numbers", numbers);

            Show("All random numbers sorted", numbers.OrderBy(i => i));

            int antalUlige = numbers.Where(i => i % 2 == 1).Count();
            Console.WriteLine("antalUlige: " + antalUlige);

            int antalUnikke = numbers.Distinct().Count();
            Console.WriteLine("antalUnikke: " + antalUnikke);

            Show("Tre første ulige tal: ", numbers.Where(i => i % 2 == 1).Take(3));

            Show("Alle unikke ulige tal:", numbers.Distinct().Where(i => i % 2 == 1));

        }

        private static void Exercise5_11()
        {
            IEnumerable<IGrouping<char, Person>> result = persons.GroupBy(p => p.Name.First());
            foreach (IGrouping<char, Person> g in result)
            {
                Console.WriteLine(g.Key + ": ");
                foreach (Person person in g)
                {
                    Console.WriteLine(person.Name);
                }
            }
            Console.ReadLine();
            // Eller man kan styre outputtet fra group by:
            IEnumerable<IGrouping<char, String>> resultStrings = persons.GroupBy(p => p.Name.First(), p => p.Name + " " + p.Age);
            foreach (IGrouping<char, String> g in resultStrings)
            {
                Console.WriteLine(g.Key + ": ");
                foreach (String s in g)
                {
                    Console.WriteLine(s);
                }
            }
            Console.ReadLine();

        }

        private static void Exercise5_12()
        {
            List<Person> persons2 = Person.readData2();
            var join = persons.Join(persons2, p => p.Name, p => p.Name, (p1, p2) => new { p1Name = p1.Name, p2Name = p2.Name, p1Age = p1.Age, p2Age = p2.Age });
            foreach (var v in join)
            {
                Console.WriteLine(v.p1Name + " " + v.p2Name + " " + v.p1Age + " " + v.p2Age);
            }
            Console.ReadLine();
        }

    }
}