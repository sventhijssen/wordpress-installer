using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace wordpress
{
    class AddPluginCommand : ICommand
    {
        private string domain;
        private List<string> pluginReferenceNames;

        public AddPluginCommand(List<string> args)
        {
            // The command must containe one domain name and at least one plugin reference name
            if (args.Count < 2)
            {
                // Console.WriteLine("Provide one domain name and at least one plugin name");
                throw new System.ArgumentException("Provide one domain name and at least one plugin name.\n");
            }
            domain = args[0];
            pluginReferenceNames = args.GetRange(1, args.Count()-1);
        }

        public bool Execute()
        {
            string pluginsURL;
            string pluginsBaseURL = @"https://wordpress.org/plugins/";
            string pluginsSubPath = @"\wp-content\plugins\";
            string pluginsPath;
            string pluginsZipPath;
            string pluginsExtractPath;
            string pluginsDownloadURL;

            // The path to the file with the name mapping of plugins
            string pluginNameMapFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"plugins.json");

            // Reference to JSON deserialization from a file:
            // https://www.newtonsoft.com/json/help/html/DeserializeWithJsonSerializerFromFile.htm
            // The dictionary contains a mapping between a plugin reference name and the genuine plugin name
            Dictionary<string, string> pluginReferenceNamesDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(pluginNameMapFile));

            string plugin;

            int i = 1;
            foreach (string pluginReferenceName in pluginReferenceNames)
            {

                // The dictionary contains the reference name as a key
                if (pluginReferenceNamesDictionary.ContainsKey(pluginReferenceName))
                {
                    plugin = pluginReferenceNamesDictionary[pluginReferenceName];
                } else
                {
                    plugin = pluginReferenceName;
                }

                Console.WriteLine();
                Console.WriteLine("=== STARTED ===");

                Console.WriteLine();
                Console.WriteLine("Download Plugins started");
                // The plugin path is the concatenation of path of the xampp folder, domain, plugins subfolder and plugin name
                pluginsPath = Globals.xamppPath + domain + pluginsSubPath + plugin;
                
                pluginsZipPath = Globals.xamppPath + domain + pluginsSubPath + plugin + @".zip";
                pluginsExtractPath = Globals.xamppPath + domain + pluginsSubPath;// + plugin;
                // The URL of the plugin is the concatenation of the base URL and the plugin name
                pluginsURL = pluginsBaseURL + plugin;

                pluginsDownloadURL = getPluginDownloadURL(pluginsURL);

                Console.WriteLine("{0} -> {1}", pluginsURL, pluginsZipPath);

                // Download plugin zip file from pluginsURL into the plugins path, i.e. pluginsPath
                using (var client = new WebClient())
                {
                    client.DownloadFile(pluginsDownloadURL, pluginsZipPath);
                }
                Console.WriteLine("Download completed");

                Console.WriteLine();
                Console.WriteLine("> Plugin {0}/{1}: {2}", i++, pluginReferenceNames.Count, plugin);

                Console.WriteLine();
                Console.WriteLine("Extraction started");
                Console.WriteLine("{0} -> {1}", pluginsZipPath, pluginsExtractPath);
                // Exctract the downloaded zip in Downloads folder
                ZipFile.ExtractToDirectory(pluginsZipPath, pluginsExtractPath);
                Console.WriteLine("Extraction completed");

                //Console.WriteLine();
                //Console.WriteLine("Moving files to domain directory in xampp");
                //Console.WriteLine("{0} -> {1}", wordpressDirectory, domainDirectory);
                //// Move the files to the domain folder
                //Directory.Move(wordpressDirectory, domainDirectory);
                Console.WriteLine("Moving completed");

                File.Delete(pluginsZipPath);

                Console.WriteLine("Setup completed");
                Console.WriteLine();

            }

            Console.WriteLine("=== FINISHED ===");
            return false;
        }

        private string getPluginDownloadURL(string pluginsURL)
        {
            Console.WriteLine("Retrieving download URL");
            var web = new HtmlWeb();
            var doc = web.Load(pluginsURL);

            HtmlNode node = doc.DocumentNode.SelectSingleNode("//a[@class='plugin-download button download-button button-large']");
            string downloadURL = node.GetAttributeValue("href", null);
            Console.WriteLine("node:" + downloadURL);
            return downloadURL;
        }
    }
}
