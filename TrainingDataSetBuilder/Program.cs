using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace TrainingDataSetBuilder
{
    class Program
    {
        static void Main(string[] args)
        {

            List<Snippet> snippets = new List<Snippet>();

            Console.WriteLine("Provide root directory");

            var rootDir = Console.ReadLine();

            if(!Directory.Exists(rootDir))
                Console.WriteLine("Invalid Directory Path");

            var dirInfo = new DirectoryInfo(rootDir);
            foreach (var folder in dirInfo.GetDirectories())
            {
                var dirName = folder.Name;

                foreach (var file in folder.GetFiles())
                {
                    var content = File.ReadAllText(file.FullName).Replace(Environment.NewLine, " ");

                    if (!string.IsNullOrEmpty(content))
                    {
                        content.Trim();

                        var snippet = new Snippet() { Language = dirName, Content = $"'{content}'"};
                        snippets.Add(snippet);
                    }
                }
            }

            using (var writer = new StreamWriter($"{rootDir}\\merged.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(snippets);
            }
        }
    }
}
