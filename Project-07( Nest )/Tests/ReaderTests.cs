using Libraries;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Xunit;

namespace Libraries.Tests {
    public class ReaderTests : IDisposable {
        private readonly string testFilesDirectory = "../../../testFiles";
        private static int isRunningCount = 0;
        private static readonly Semaphore isRunningCountLock = new Semaphore(1, 1);
        private bool disposedValue;

        public ReaderTests() {
            testFilesDirectory += Process.GetCurrentProcess().Id.ToString() + "/";
            isRunningCountLock.WaitOne();
            isRunningCount++;
            isRunningCountLock.Release();
            if (!Directory.Exists(testFilesDirectory))
                Directory.CreateDirectory(testFilesDirectory);
        }

        [Fact]
        public void ConstructorTestBadFilePath() {
            Assert.Throws<Exception>(() => new Reader("bad path"));
        }

        [Fact]
        public void ConstructorTestEmptyFilePath() {
            Assert.Throws<Exception>(() => new Reader(""));
        }

        [Fact]
        public void ConstructorTestWhiteSpaseFilePath() {
            Assert.Throws<Exception>(() => new Reader("   "));
        }

        [Fact]
        public void ConstructorTestNullFilePath() {
            Assert.Throws<Exception>(() => new Reader(null));
        }

        [Fact]
        public void ConstructorTestOkFilePath() {
            var path = "testFile1";
            GeneralFunctions.CreateFile(testFilesDirectory + path);
            try {
                new Reader(testFilesDirectory + path);
            }
            catch (Exception) {
                Assert.True(false);
            }
        }

        [Fact]
        public void PeopelTestEmptyFile() {
            IEnumerable<Person> expectedResult = new List<Person>();
            var text = "";
            var filePath = testFilesDirectory + "textFile2";
            GeneralFunctions.CreateFile(filePath, text);
            var reader = new Reader(filePath);
            var testResult = reader.People;
            Assert.Equal(expectedResult, testResult);
        }
        [Fact]
        public void PeopelTest() {
            IEnumerable<Person> expectedResult = new List<Person> {
                new Person{
                    About="In laboris in anim eiusmod consectetur elit. Ut incididunt eiusmod incididunt anim incididunt laborum minim qui consequat incididunt ut nisi fugiat. Fugiat ipsum consectetur adipisicing enim amet.\r\n",
                    Address="486 Apollo Street, Blodgett, Northern Mariana Islands, 1875",
                    Age=26,
                    Company="NETPLAX",
                    Email="sykesrivera@netplax.com",
                    EyeColor="blue",
                    Gender="male",
                    Latitude=-21.459832,
                    Longitude=-148.208613,
                    Name="Sykes Rivera",
                    Phone="+1 (946) 596-2890",
                    RegistrationDate="2015/02/16 11:11:31",
                }
            };
            var text = @"
                [
                  {
                    ""age"": 26,
                    ""eyeColor"": ""blue"",
                    ""name"": ""Sykes Rivera"",
                    ""gender"": ""male"",
                    ""company"": ""NETPLAX"",
                    ""email"": ""sykesrivera@netplax.com"",
                    ""phone"": ""+1 (946) 596-2890"",
                    ""address"": ""486 Apollo Street, Blodgett, Northern Mariana Islands, 1875"",
                    ""about"": ""In laboris in anim eiusmod consectetur elit. Ut incididunt eiusmod incididunt anim incididunt laborum minim qui consequat incididunt ut nisi fugiat. Fugiat ipsum consectetur adipisicing enim amet.\r\n"",
                    ""registration_date"": ""2015/02/16 11:11:31"",
                    ""latitude"": -21.459832,
                    ""longitude"": -148.208613
                  }
                ]";
            var filePath = testFilesDirectory + "textFile3";
            GeneralFunctions.CreateFile(filePath, text);
            var reader = new Reader(filePath);
            var testResult = reader.People;
            Assert.Equal(expectedResult, testResult);
        }

        protected virtual void Dispose(bool disposing) {
            if (!disposedValue) {
                isRunningCountLock.WaitOne();
                isRunningCount--;
                if (isRunningCount == 0)
                    Directory.Delete(testFilesDirectory, true);
                isRunningCountLock.Release();
                disposedValue = true;
            }
        }

        public void Dispose() {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        ~ReaderTests() {
            Dispose(false);
        }
    }
}
