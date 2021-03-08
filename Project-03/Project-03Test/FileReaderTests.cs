using System.IO;
using Xunit;
using Project_03;

namespace Project_03Test {
    public class FileReaderTests {
        private readonly string directoryPath;
        private readonly FileReader fileReader;
        public FileReaderTests() {
            directoryPath = @"..\..\..\..\TestData\data";
            File.WriteAllText(directoryPath + "\\sample", "this is simple file");
            File.WriteAllText(directoryPath + "\\sample2", "this is second document");
            fileReader = new FileReader(directoryPath);
        }
        [Fact]
        public void GetRawDataTest() {
            string expectedResult = "this is simple file\nthis is second document";
            string testResult = fileReader.GetRawData();
            Assert.Equal(expectedResult, testResult);
        }
        ~FileReaderTests() {
            File.Delete(directoryPath + "\\sample");
            File.Delete(directoryPath + "\\sample2");
        }
    }
}
