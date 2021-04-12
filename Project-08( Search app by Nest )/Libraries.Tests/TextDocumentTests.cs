using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Xunit;

namespace Libraries.Tests {
    public class TextDocumentTests : IDisposable {
        private static readonly string directoryPath = @"../../../../TestData/TextDocumentData";
        private bool disposedValue;
        private static int isRunningCount = 0;
        private static readonly Semaphore isRunningCountLock = new Semaphore(1, 1);

        public TextDocumentTests() {
            isRunningCountLock.WaitOne();
            isRunningCount++;
            isRunningCountLock.Release();
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
            File.WriteAllText(directoryPath + "/sample", "this is simple file");
            File.WriteAllText(directoryPath + "/sample2", "this is second document");
        }

        [Fact()]
        public void GetFomeFileTest() {
            var expectedResult = new TextDocument {
                Path = directoryPath + "/sample",
                DocText = "this is simple file"
            };
            var testResult = TextDocument.GetFomeFile(directoryPath + "/sample");
            Assert.Equal(expectedResult, testResult);
        }

        [Fact()]
        public void GetFomeFileTestBadPath() {
            TextDocument expectedResult = null;
            var testResult = TextDocument.GetFomeFile("badPath");
            Assert.Equal(expectedResult, testResult);
        }

        [Fact()]
        public void GetFomeDirectoryTest() {
            var expectedResult = new List<TextDocument>{
                new TextDocument {
                Path = directoryPath + "/sample",
                DocText = "this is simple file"
                },
                new TextDocument {
                    Path = directoryPath + "/sample2",
                    DocText = "this is second document"
                }
            };
            var testResult = TextDocument.GetFomeDirectory(directoryPath);
            Assert.Equal(expectedResult, testResult);
        }

        [Fact()]
        public void GetFomeDirectoryTestBadPath() {
            List<TextDocument> expectedResult = new List<TextDocument>();
            var testResult = TextDocument.GetFomeDirectory("badPath");
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

        ~TextDocumentTests() {
            Dispose(disposing: false);
        }
    }
}