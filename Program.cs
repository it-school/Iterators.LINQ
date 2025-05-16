using System.Collections;

namespace LINQ
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Example1();
            Console.WriteLine("\n----------------------------------------------------------------\n");
            Example2();
            Console.WriteLine("\n----------------------------------------------------------------\n");
            Example3();
            Console.WriteLine("\n----------------------------------------------------------------\n");
            Example4();
            Console.WriteLine("\n----------------------------------------------------------------\n");
            Example5();
            Console.WriteLine("\n----------------------------------------------------------------\n");
            Example6();

            Console.WriteLine("\n");
        }


        public class DaysOfTheWeek : IEnumerable
        {
            private readonly string[] _days = ["Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun"];

            public IEnumerator GetEnumerator()
            {
                for (int index = _days.Length - 1; index >= 0; index--)
                {
                    // Yield each day of the week
                    yield return _days[index];
                }
            }
        }

        /// <summary>
        /// Simply yield work demonstration
        /// </summary>
        private static void Example1()
        {
            DaysOfTheWeek days = new DaysOfTheWeek();

            Console.WriteLine();
            foreach (string day in days)
            {
                Console.Write(day + " ");
            }

            Console.WriteLine("\n");
        }


        /// <summary>
        /// From old-school to LINQ style
        /// </summary>
        private static void Example2()
        {
            string[] names = ["Burns", "Connor", "Frank", "Everett", "Albert", "George", "Harris", "David"];

            // Old-school style
            var selectedNames = new List<string>();
            foreach (string name in names)
            {
                if (name.Length == 5)
                    selectedNames.Add(name.ToUpper());
            }

            selectedNames.Sort();

            foreach (string name in selectedNames)
                Console.WriteLine(name);

            // LINQ methods style
            var selectedNames2 = names.Where(name => name.Length == 5).OrderBy(name => name);
            foreach (var name in selectedNames2)
                Console.WriteLine(name.ToUpper());
        }


        /// <summary>
        /// OrderBy, GroupJoin
        /// </summary>
        private static void Example3()
        {
            string[] input = ["Select", "Where", "OrderBy", "GroupBy", "Args"];
            Console.WriteLine("\n\nOriginal words list:");
            foreach (var v in input)
            {
                Console.Write(v + "\t");
            }

            Console.WriteLine("\n\nsorted by alphabet:");
            var output = input.OrderBy(s => s);
            foreach (var v in output)
            {
                Console.Write(v + " ");
            }

            Console.WriteLine("\n\nsorted by length:");
            output = input.OrderBy(s => s.Length);
            foreach (var v in output)
            {
                Console.Write(v + " ");
            }

            Console.WriteLine("\n\nsorted by pre-last char of word:");
            output = input.OrderBy(s => s[^2]);
            foreach (var v in output)
            {
                Console.Write(v + " ");
            }

            Console.WriteLine("\n");
            string[] boys = ["Alex", "Bob", "Charles", "Dan"];
            string[] girls = ["Caroline", "Barbara", "Ann", "Adel"];
            var result = boys.GroupJoin(girls, x => x[0], y => y[0], (x, matches) => new { Boy = x, Girls = matches });
            foreach (var pair in result)
            {
                Console.WriteLine(pair.Boy);
                foreach (var girl in pair.Girls)
                {
                    Console.WriteLine("   " + girl);
                }
            }
        }


        public class Book(string author, string title, int year)
        {
            private string Author { get; set; } = author;
            private string Title { get; set; } = title;
            private int Year { get; set; } = year;

            public override string? ToString()
            {
                return $"{Author} - {Title} ({Year})";
            }
        }

        /// <summary>
        /// OfType, Cast, Take
        /// </summary>
        public static void Example4()
        {
            Book[] books =
            {
                new("Albahari", "LINQ: pocketbook", 2016),
                new("Rattz", "LINQ: query language", 2008),
                new("Kimmel", "LINQ Unleashed", 2020)
            };
            ArrayList bookList = new ArrayList(books);

            var result1 = bookList.OfType<Book>().Take(2);
            foreach (var v in result1)
            {
                Console.Write(v + "\t");
            }

            Console.WriteLine("\n");

            var result2 = books.OfType<Book>().Take(2);
            foreach (var v in result2)
            {
                Console.Write(v + "\t");
            }

            Console.WriteLine("\n");

            var result3 = books.Cast<Book>().Take(2);
            foreach (var v in result3)
            {
                Console.Write(v + "\t");
            }

            Console.WriteLine("\n");

            var result4 = books.Take(2);
            foreach (var v in result4)
            {
                Console.Write(v + "\t");
            }

            Console.WriteLine("\n");
        }


        /// <summary>
        /// Group query
        /// </summary>
        private static void Example5()
        {
            Person[] people =
            [
                new("Tom", "Microsoft"),
                new("Sam", "Google"),
                new("Bob", "JetBrains"),
                new("Mike", "Microsoft"),
                new("Kate", "JetBrains"),
                new("Alice", "Microsoft"),
            ];

            var companies = from person in people
                            group person by person.Company
                into g
                            select new { Name = g.Key, Count = g.Count() };

            foreach (var company in companies)
            {
                Console.WriteLine($"{company.Name} : {company.Count}");
            }
        }


        record Person(string Name, string Company);

        record Company(string Title, string Language);

        /// <summary>
        /// Join subquery
        /// </summary>
        public static void Example6()
        {
            Person[] people =
            [
                new("Bill", "Microsoft"), new("Alex", "Google"),
                new("Bob", "JetBrains"), new("Mike", "Microsoft"),
            ];
            Company[] companies =
            [
                new("Microsoft", "C#"),
                new("Google", "Go"),
                new("Oracle", "Java")
            ];
            var employees = from p in people
                            join c in companies on p.Company equals c.Title
                            select new { Name = p.Name, Company = c.Title, Language = c.Language };

            foreach (var emp in employees)
                Console.WriteLine($"{emp.Name} - {emp.Company} ({emp.Language})");
        }
    }
}