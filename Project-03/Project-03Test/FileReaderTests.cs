using System;
using System.IO;
using Xunit;
using Project_03;

namespace Project_03Test {
    public class FileReaderTests {
        private readonly string directoryPath;
        private FileReaderTests() {
            directoryPath = @"..\..\..\TestData\data";
            File.WriteAllText(directoryPath + "\\sample", "this is simple file");
            File.WriteAllText(directoryPath + "\\sample2", "this is second document");
        }
        [Fact]
        public void GetRawDataTest() {
            string correctResult = "this is simple file\nthis is second document";
            FileReader fileReader = new FileReader(directoryPath);
            string testResult = fileReader.GetRawData();
            Assert.Equal(correctResult, testResult);
        }
        ~FileReaderTests() {
            File.Delete(directoryPath + "\\sample");
            File.Delete(directoryPath + "\\sample2");
        }
    }
}
