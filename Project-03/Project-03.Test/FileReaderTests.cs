using System.IO;
using Xunit;
using System.Collections.Generic;
using Project_03;
using System.Diagnostics.CodeAnalysis;

namespace Project_03Test {
    [ExcludeFromCodeCoverage]
    public class FileReaderTests {
        private readonly string directoryPath;
        private readonly FileReader fileReader;
        public FileReaderTests() {
            directoryPath = @"../../../../TestData/data";
            File.WriteAllText(directoryPath + "/sample", "this is simple file");
            File.WriteAllText(directoryPath + "/sample2", "this is second document");
            fileReader = new FileReader(directoryPath);
        }
        [Fact]
        public void GetRawDataTest() {
            Dictionary<string, string> expectedResult = new Dictionary<string, string> {
                { directoryPath + "/sample", "this is simple file" },
                { directoryPath + "/sample2", "this is second document" }
            };
            Dictionary<string, string> testResult = fileReader.GetRawData();
            Assert.Equal(expectedResult, testResult);
        }
        [Fact]
        public void GetRawDataTestWrongPath() {
            Dictionary<string, string> expectedResult = new Dictionary<string, string>();
            FileReader fileReader = new FileReader("wrong path");
            Dictionary<string, string> testResult = fileReader.GetRawData();
            Assert.Equal(expectedResult, testResult);
        }
        ~FileReaderTests() {
            File.Delete(directoryPath + "/sample");
            File.Delete(directoryPath + "/sample2");
        }
    }
}
