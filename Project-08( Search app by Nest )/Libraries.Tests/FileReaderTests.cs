using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading;
using Xunit;

namespace Libraries.Tests {
    [ExcludeFromCodeCoverage]
    public class FileReaderTests : IDisposable {
        private static readonly string directoryPath = @"../../../../TestData/FileReaderData";
        private bool disposedValue;
        private static int isRunningCount = 0;
        private static readonly Semaphore isRunningCountLock = new Semaphore(1, 1);

        public FileReaderTests() {
            isRunningCountLock.WaitOne();
            isRunningCount++;
            isRunningCountLock.Release();
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
            File.WriteAllText(directoryPath + "/sample", "this is simple file");
            File.WriteAllText(directoryPath + "/sample2", "this is second document");
        }

        [Fact]
        public void ReadFromDirectoryTest() {
            var expectedResult = new List<Tuple<string, string>> {
                new Tuple<string, string>( directoryPath + "/sample", "this is simple file" ),
                new Tuple<string, string>( directoryPath + "/sample2", "this is second document" )
            };
            var testResult = FileReader.ReadFromDirectory(directoryPath);
            Assert.Equal(expectedResult, testResult);
        }

        [Fact]
        public void ReadFromDirectoryTestWrongPath() {
            var expectedResult = new List<Tuple<string, string>>();
            var testResult = FileReader.ReadFromDirectory("wrong path");
            Assert.Equal(expectedResult, testResult);
        }

        [Fact]
        public void ReadFromFileTest() {
            var expectedResult = new Tuple<string, string>(directoryPath + "/sample", "this is simple file");
            var testResult = FileReader.ReadFromFile(directoryPath + "/sample");
            Assert.Equal(expectedResult, testResult);
        }

        [Fact]
        public void ReadFromFileTestWrongPath() {
            Tuple<string, string> expectedResult = null;
            var testResult = FileReader.ReadFromFile("wrong path");
            Assert.Equal(expectedResult, testResult);
        }

        protected virtual void Dispose(bool disposing) {
            if (!disposedValue) {
                if (disposing) {
                    isRunningCountLock.WaitOne();
                    isRunningCount--;
                    if (isRunningCount == 0)
                        Directory.Delete(directoryPath, true);
                    isRunningCountLock.Release();
                }
                disposedValue = true;
            }
        }

        public void Dispose() {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        ~FileReaderTests() {
            Dispose(disposing: false);
        }
    }
}
