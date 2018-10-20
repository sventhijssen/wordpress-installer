using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace wordpress
{
    internal class LicenseCommand : ICommand
    {
        public bool Execute()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            StreamReader reader = new StreamReader(assembly.GetManifestResourceStream("wordpress.License.txt"));
            Console.WriteLine(reader.ReadToEnd());
            return false;
        }
    }
}