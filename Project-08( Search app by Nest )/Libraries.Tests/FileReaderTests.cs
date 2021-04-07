using Project_05;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading;
using Xunit;

namespace Project_05Test {
    [ExcludeFromCodeCoverage]
    public class FileReaderTests : IDisposable {
        private static readonly string directoryPath = @"../../../../TestData/data";
        private readonly FileReader fileReader;
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
