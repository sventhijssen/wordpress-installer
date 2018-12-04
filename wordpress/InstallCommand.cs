using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;

namespace wordpress
{

    internal class InstallCommand : ICommand
    {
        private List<string> domains;


        public InstallCommand(List<string> args)
        {
            this.domains = args;
        }

        public bool Execute()
        {
            string wordpressURL = @"https://wordpress.org/latest.zip";
            string downloadPath = @"C:\Users\User\Downloads\wordpress.zip";
            string extractPath = @"C:\Users\User\Downloads\";
            string wordpressDirectory = extractPath + "wordpress";
            string domainDirectory;

            Console.WriteLine();
            Console.WriteLine("=== STARTED ===");

            Console.WriteLine();
            Console.WriteLine("Download Wordpress started");
            Console.WriteLine("{0} -> {1}", wordpressURL, downloadPath);
            // Download latest Wordpress zip file from urlPath into the download path, i.e. Downloads folder
            using (var client = new WebClient())
            {
                client.DownloadFile(wordpressURL, downloadPath);
            }
            Console.WriteLine("Download completed");

            // For each domain, download Wordpress and install
            int i = 1;
            foreach (string domain in domains)
            {
                Console.WriteLine();
                Console.WriteLine("> Domain {0}/{1}: {2}", i++, domains.Count, domain);
                domainDirectory = Globals.xamppPath + domain;

                Console.WriteLine();
                Console.WriteLine("Extraction started");
                Console.WriteLine("{0} -> {1}", downloadPath, extractPath);
                // Exctract the downloaded zip in Downloads folder
                ZipFile.ExtractToDirectory(downloadPath, extractPath);
                Console.WriteLine("Extraction completed");

                Console.WriteLine();
                Console.WriteLine("Moving files to domain directory in xampp");
                Console.WriteLine("{0} -> {1}", wordpressDirectory, domainDirectory);
                // Move the files to the domain folder
                Directory.Move(wordpressDirectory, domainDirectory);
                Console.WriteLine("Moving completed");

                using (StreamWriter w = File.AppendText(Globals.virtualHostsConfFile))
                {
                    CreateVirtualHost(domainDirectory, domain, w);
                }

                int lastIP = GetLastIP();

                using (StreamWriter w = File.AppendText(Globals.hostsFile))
                {
                    CreateLocalhostResoltion(lastIP, domain, w);
                }

                Console.WriteLine("Setup completed");
                Console.WriteLine();

            }

            Console.WriteLine("=== FINISHED ===");
            return false;
        }

        public static void CreateVirtualHost(string domainPath, string domainName, TextWriter w)
        {
            Console.WriteLine();
            Console.WriteLine("Creating virtual host in httpd-vhosts.conf");
            w.WriteLine("");
            w.WriteLine("<VirtualHost *:80>");
            w.WriteLine("\tDocumentRoot\t\"{0}\"", domainPath);
            w.WriteLine("\tServerName\t\t{0}.test", domainName);
            w.WriteLine("\tServerAlias\t\twww.{0}.test", domainName);
            w.WriteLine("</VirtualHost>");
            Console.WriteLine("Virtual host created");
        }

        public static void CreateLocalhostResoltion(int lastIP, string domainName, TextWriter w)
        {
            w.WriteLine("127.0.{0}.1\t{1}.test", lastIP+1, domainName);
        }

        public static int GetLastIP()
        {
            var lastLine = File.ReadLines(Globals.hostsFile).Last();
            string ip = lastLine.Split(' ').ToList().First();
            return Int32.Parse(ip.Split('.').ToList().ElementAt(2));
        }
    }
}