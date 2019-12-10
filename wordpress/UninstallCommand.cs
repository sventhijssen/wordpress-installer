using System;
using System.Collections.Generic;
using System.IO;

namespace wordpress
{
    internal class UninstallCommand : ICommand
    {
        private List<string> domains;

        public UninstallCommand(List<string> args)
        {
            this.domains = args;
        }

        public bool Execute()
        {
            Console.WriteLine("Are you sure you wish to continue (yes/no)?");
            string check = Console.ReadLine();

            if (check.ToLower().Equals("no")) {
                Console.WriteLine("Uninstall cancelled");
                return false;
            }

            if (!check.ToLower().Equals("yes"))
            {
                Console.WriteLine("Uninstall cancelled. Answer with \"yes/no\"");
                return false;
            }

            Console.WriteLine();
            Console.WriteLine("=== STARTED ===");

            int i = 0;
            foreach (string domain in domains)
            {
                string domainDirectory;

                Console.WriteLine();
                Console.WriteLine("> Domain {0}/{1}: {2}", i++, domains.Count, domain);
                domainDirectory = Globals.xamppPath + domain;

                // Delete the directory
                Console.WriteLine();
                Console.WriteLine("Deleting directory");
                Directory.Delete(domainDirectory, true);
                Console.WriteLine("Directory deleted");

                using (StreamWriter w = File.AppendText(Globals.virtualHostsConfFile))
                {
                    RemoveVirtualHost(domainDirectory, domain, w);
                }

                using (StreamWriter w = File.AppendText(Globals.hostsFile))
                {
                    RemoveLocalhostResoltion(0, domain, w);
                }


            }

            Console.WriteLine("=== FINISHED ===");
            return false;
        }


        public static void RemoveVirtualHost(string domainPath, string domainName, TextWriter w)
        {
            /*
             * TODO: Remove virtual host
             */
            //Console.WriteLine();
            //Console.WriteLine("Creating virtual host in httpd-vhosts.conf");
            //w.WriteLine("");
            //w.WriteLine("<VirtualHost *:80>");
            //w.WriteLine("\tDocumentRoot\t\"{0}\"", domainPath);
            //w.WriteLine("\tServerName\t\t{0}.test", domainName);
            //w.WriteLine("\tServerAlias\t\twww.{0}.test", domainName);
            //w.WriteLine("</VirtualHost>");
            //Console.WriteLine("Virtual host created");
        }

        public static void RemoveLocalhostResoltion(int lastIP, string domainName, TextWriter w)
        {
            /*
             * TODO: Remove local host resolution
             */
            //w.WriteLine("127.0.{0}.1\t{1}.test", lastIP + 1, domainName);
        }
    }
}