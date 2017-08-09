using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CsvFileSortingTest
{
    [TestClass]
    public class CsvFileSortingTest
    {
        string dataFilePath = @"resources\file\data.csv";
        [TestMethod]
        public void Test_SortFileByFrequency()
        {
            string fileSavedPath = @"resources\file\OrderedByFrequency.txt";

            Assert.IsTrue(File.Exists(dataFilePath),"Data File does not exist");
            CSVFileSort.FileProcessing.FileProcessor fp = new CSVFileSort.FileProcessing.FileProcessor(dataFilePath);
            Assert.IsNotNull(fp);
            fp.SortFileByFrequency();
            Assert.IsTrue(File.Exists(fileSavedPath), "OrderedByFrequency File not created");
        }
        [TestMethod]
        public void Test_SortFileByStreetNameAsc()
        {

            string fileSavedPath = @"resources\file\OrderedByStreetName.txt";
            Assert.IsTrue(File.Exists(dataFilePath), "Data File does not exist");
            CSVFileSort.FileProcessing.FileProcessor fp = new CSVFileSort.FileProcessing.FileProcessor(dataFilePath);
            Assert.IsNotNull(fp);
            fp.SortFileByStreetNameAsc();
            Assert.IsTrue(File.Exists(fileSavedPath), "OrderedByStreetName File not Created");
        }
    }
}
