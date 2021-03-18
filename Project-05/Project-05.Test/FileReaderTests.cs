using Project_05;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Xunit;

namespace Project_05Test {
    [ExcludeFromCodeCoverage]
    public class FileReaderTests : IDisposable {
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
            var expectedResult = new Dictionary<string, string> {
                { directoryPath + "/sample", "this is simple file" },
                { directoryPath + "/sample2", "this is second document" }
            };
            var testResult = fileReader.GetRawData();
            Assert.Equal(expectedResult, testResult);
        }
        [Fact]
        public void GetRawDataTestWrongPath() {
            var expectedResult = new Dictionary<string, string>();
            var fileReader = new FileReader("wrong path");
            var testResult = fileReader.GetRawData();
            Assert.Equal(expectedResult, testResult);
        }

        public void Dispose() {
            File.Delete(directoryPath + "/sample");
            File.Delete(directoryPath + "/sample2");
        }

        ~FileReaderTests() {
            Dispose();
        }
    }
}
