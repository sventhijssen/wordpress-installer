using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wordpress
{
    internal class HelpCommand : ICommand
    {
        public HelpCommand(String commandName = "")
        {
            this.commandName = commandName;
        }

        public bool Execute()
        {
            if (this.commandName != "")
            {
                DocumentationReader.getCommandDocumentation(this.commandName);
                return false;
            }
            Console.WriteLine("For more information on a specific command, type COMMAND \"--help\"");
            DocumentationReader.getAllCommandDocumentation();
            return false;
        }

        private String commandName = "";

        private Dictionary<string, string> commands = new Dictionary<string, string>();
    }
}
