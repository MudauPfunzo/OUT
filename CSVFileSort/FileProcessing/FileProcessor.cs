using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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
        private string[] recordsFromFileWithHearder = new string[] { };
        private IEnumerable<string> recordsFromFile;
        private string ProjectDirectory = Path.GetDirectoryName(Directory.GetParent(Environment.CurrentDirectory).ToString());

        public FileProcessor() { }
        public FileProcessor(string path,string orderby ,string sort)
        {
            this.FilePath = Path.Combine(ProjectDirectory, path) ;
            this.OrderBy = orderby;
            this.Sort = sort;
        }
        private void ReadFile()
        {
            recordsFromFileWithHearder = File.ReadAllLines(FilePath);
            recordsFromFile = recordsFromFileWithHearder.Skip(1); 
        }

        public void SortFileByFrequency()
        {
            //Read the File
            ReadFile();


            string savePath = @"resources\file\OrderedByFrequency.txt";
            //Get the Firts Names from the file
            var FirstNames = recordsFromFile.Select(record => new { Name = record.Split(',')[0] });
            //Get the Last Names from the file
            var LastNames = recordsFromFile.Select(record => new { Name = record.Split(',')[1] });

            //Combine Last Names and First Names into one list
            var FirstAndLastNames = FirstNames.Concat(LastNames);
            //Group by the Name Field, and Count
            var groupRecords = FirstAndLastNames
                                            .GroupBy(p => new
                                             {
                                                 p.Name
                                              
                                            }).OrderBy(x=> x.Key.Name)
                                            .Select(x => new
                                             {

                                                 // x.Key.LastName,
                                                  x.Key.Name,
                                                  Frequency = x.Count()
                                             }).OrderByDescending(x => x.Frequency);
           
            //Get the Names and their count into a list 
            List<string> records = groupRecords.Select(x => x.Name + "," + x.Frequency.ToString()).ToList();
            //Write the names and count into a file
            File.WriteAllLines(Path.Combine(ProjectDirectory, savePath),records);
        }
        public void SortFileByStreetNameAsc()
        {
            //Read the File
            ReadFile();


            string savePath = @"resources\file\OrderedByStreetName.txt";
            //Get Address from the file
            var Address = recordsFromFile.Select(record => new {
                StreetNumber = new string(record.Split(',')[2].TakeWhile(Char.IsDigit).ToArray()),
                StreetName = Regex.Replace(record.Split(',')[2], @"[0-9\-]", string.Empty).Substring(1)
                                                               }).OrderBy(x=>x.StreetName);

          
            

            //Combine street number and street name
            List<string> records = Address.Select(x => x.StreetNumber + " " + x.StreetName).ToList();
            //Write the names and count into a file
            File.WriteAllLines(Path.Combine(ProjectDirectory, savePath), records);
        }

    }
}
