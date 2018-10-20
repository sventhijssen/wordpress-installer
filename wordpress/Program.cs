
using System;
using System.Linq;
using System.Text;

namespace wordpress
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 1)
            {
                run(String.Join(" ", args.Where(v => v != "wordpress").ToArray()));
            }
            else
            {
                Console.OutputEncoding = Encoding.UTF8;
                Console.WriteLine("wordpress © 2018 User");
                Console.WriteLine("This program comes with ABSOLUTELY NO WARRANTY. This free software.");
                Console.WriteLine();
                Console.WriteLine("Type \"help\" for more information.");
                Console.WriteLine();
                run();
            }
        }

        public static void run(String args = "")
        {
            bool passed = false;
            String line = args != "" ? line = args : Console.ReadLine();
            var exit = false;
            while (exit == false)
            {
                try
                {
                    if (passed)
                        line = Console.ReadLine();
                    var command = Parser.Parse(line);
                    exit = command.Execute();
                    Console.WriteLine();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                passed = true;
            }
        }
    }
}
