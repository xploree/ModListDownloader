using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace ModListDownloader
{
    internal class Program
    {

        public static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Mod-List Name : ");
            ResetColor();
            var modListName = Console.ReadLine();
            MainSequence(modListName);
        }

        public static bool DownloadFile(string url, string fileName)
        {
            using (var webClient = new WebClient())
            {
                webClient.DownloadFile(url, fileName + ".jar");
                return true;
            }
        }

        public static void ResetColor()
        {
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void MainSequence(string modListName)
        {
            int counter = 1;

            string[] lines = System.IO.File.ReadAllLines(modListName + ".txt");
            string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            if(Directory.Exists(appdata + "\\.minecraft\\mods"))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Successfully Located Mods Folder : " + appdata + "\\.minecraft\\mods");
                ResetColor();
            }
            foreach (string line in lines)
            {
                var status = DownloadFile(line, counter.ToString());
                if (status)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Successfully Downloaded : " + line);
                    ResetColor();
                    File.Move(counter + ".jar", appdata + "\\.minecraft\\mods\\" + counter + ".jar");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Successfully Moved : " + counter.ToString());
                    ResetColor();
                    counter++;
                }
            }

            Console.ReadLine();


        }
    }
}