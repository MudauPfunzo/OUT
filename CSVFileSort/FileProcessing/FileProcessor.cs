using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CSVFileSort.FileProcessing
{
    public class FileProcessor
    {
        public string FilePath { get; set; }
        public string OrderBy { get; set; }
        public string Sort { get; set; }


        private string ProjectDirectory = Path.GetDirectoryName(Directory.GetParent(Environment.CurrentDirectory).ToString());

        public FileProcessor() { }
        public FileProcessor(string path,string orderby ,string sort)
        {
            this.FilePath = Path.Combine(ProjectDirectory, path) ;
            this.OrderBy = orderby;
            this.Sort = sort;
        }

        public void SortFileByFrequency()
        {
            string savePath = @"resources\file\test.txt";
            string[] recordsFromFileWithHearder = new string[] { };

            recordsFromFileWithHearder = File.ReadAllLines(FilePath);
            var recordsFromFile = recordsFromFileWithHearder.Skip(1);
            var groupRecords = recordsFromFile
                                            .Select(record => new
                                            {
                                                FirstName = record.Split(',')[0],
                                                LastName = record.Split(',')[1]

                                            })
                                             .GroupBy(p => new
                                             {

                                                 p.LastName
                                             })
                                              .OrderBy(x => x.Key.LastName)
                                              .Select(x => new
                                              {

                                                  x.Key.LastName,
                                                  Frequency = x.Count()
                                              });
            List<string> records = groupRecords.Select(x => x.LastName + "," + x.Frequency.ToString()).ToList();

            File.WriteAllLines(Path.Combine(ProjectDirectory, savePath),records);
        }
       
    }
}
