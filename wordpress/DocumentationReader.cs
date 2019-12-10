using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wordpress
{
    abstract class DocumentationReader
    {
        public static void getCommandDocumentation(String command)
        {
            Command cmd = commands.First(s => s.ShortName == command || s.LongName == command);
            Console.WriteLine("Usage: " + cmd.Usage);
            Console.WriteLine(cmd.Description);
            if (cmd.Flags.Length > 0)
                Console.WriteLine("Options and arguments");
            foreach (Flag flag in cmd.Flags)
            {
                Console.Write("".PadRight(2));
                Console.Write((flag.ShortName + " " + flag.Argument).PadRight(20));
                Console.Write(flag.Description);
                Console.Write("\n");
            }
            Console.WriteLine();
            Console.WriteLine("Mandatory arguments to long options are mandatory for short options too.");
        }

        public static void getAllCommandDocumentation()
        {
            foreach (Command cmd in commands)
            {
                Console.Write("".PadLeft(2)); // 2chars padding left
                Console.Write(cmd.ShortName.PadRight(8)); // 8chars for ShortName + previous = 10chars
                Console.Write(cmd.LongName.PadRight(20)); // 12chars for LongName + 10chars previous = 18chars
                Console.Write(cmd.Description);
                Console.Write("\n");
            }
        }

        private static Command[] commands = {
            new Command {
                ShortName = "exit",
                LongName = "",
                Usage = "exit",
                Description = "Closes this console application.",
                Flags = new Flag[] {}
            },
            new Command {
                ShortName = "help",
                LongName = "",
                Usage = "help",
                Description = "Lists all commands this application can execute with their description. For more information on a specific command, type COMMAND \"--help\"",
                Flags = new Flag[] {}
            },
            new Command
            {
                ShortName = "clear",
                LongName = "",
                Usage = "clear",
                Description = "Clears the whole console application.",
                Flags = new Flag[] {}
            },
            new Command
            {
                ShortName = "license",
                LongName = "",
                Usage = "license",
                Description = "Shows the license under which this console application is released.",
                Flags = new Flag[] {}
            },
            new Command
            {
                ShortName = "install",
                LongName = "",
                Usage = "install [DOMAIN...]",
                Description = "Installs Wordpress on a locally created domain [DOMAIN]. Multiple domains can be created at once, e.g. \"install test1 test2\"",
                Flags = new Flag[] {}
            },
            new Command
            {
                ShortName = "uninstall",
                LongName = "",
                Usage = "uninstal [DOMAIN...]",
                Description = "Uninstalls the local domain, removes it from the hosts file and virtual hosts, e.g. \"uninstall test1 test2 \"",
                Flags = new Flag[] {}
            },
            new Command {
                ShortName = "add",
                LongName = "",
                Usage = "add DOMAIN [PLUGIN...]",
                Description = "Adds the list of plugins to the local domain, e.g. \"add test plugin1 plugin2 \"",
                Flags = new Flag[] {}
            }
        };
    }

    public class Command
    {
        public string ShortName { get; set; }
        public string LongName { get; set; }
        public string Usage { get; set; }
        public string Description { get; set; }
        public Flag[] Flags;
    }

    public class Flag
    {
        public string ShortName { get; set; }
        public string LongName { get; set; }
        public string Description { get; set; }
        public string Argument { get; set; }
    }
}
