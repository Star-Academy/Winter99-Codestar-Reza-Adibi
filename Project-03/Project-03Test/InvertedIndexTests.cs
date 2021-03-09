using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Project_03;

namespace Project_03Test {
    public class InvertedIndexTests {
        [Fact]
        public void InsertDatasTestSingleData() {
            List<Tuple<string, string>> dataMap = new List<Tuple<string, string>> { new Tuple<string, string>("file1", "hello") };
            Dictionary<string, List<string>> expectedResult = new Dictionary<string, List<string>> { { "hello", new List<string> { "file1" } } };
            InvertedIndex invertedIndex = new InvertedIndex();
            invertedIndex.InsertDatas(dataMap);
            Dictionary<string, List<string>> testResult = invertedIndex.TokenMap;
            Assert.Equal(expectedResult, testResult);
        }
        [Fact]
        public void InsertDatasTestMultiData() {
            List<Tuple<string, string>> dataMap = new List<Tuple<string, string>> {
                new Tuple<string, string>( "file1", "hello" ),
                new Tuple<string, string>( "file1", "hello" ),
                new Tuple<string, string>( "file2", "hello" ),
                new Tuple<string, string>( "file2", "hello" ),
                new Tuple<string, string>("file1", "hello2" ),
                new Tuple<string, string>("file2", "hello2" )
            };
            Dictionary<string, List<string>> expectedResult = new Dictionary<string, List<string>> {
                { "hello", new List<string> { "file1", "file1", "file2", "file2" } },
                { "hello2", new List<string> { "file1", "file2" } }
            };
            InvertedIndex invertedIndex = new InvertedIndex();
            invertedIndex.InsertDatas(dataMap);
            Dictionary<string, List<string>> testResult = invertedIndex.TokenMap;
            Assert.Equal(expectedResult, testResult);
        }
        [Fact]
        public void GetTokenDocumentIDsTestInvertedIndexContainsToken() {
            List<Tuple<string, string>> dataMap = new List<Tuple<string, string>> { new Tuple<string, string>("file1", "hello") };
            List<string> expectedResult = new List<string> { "file1" };
            InvertedIndex invertedIndex = new InvertedIndex();
            invertedIndex.InsertDatas(dataMap);
            List<string> testResult;
            invertedIndex.TryGetTokenDocumentIDs("hello", out testResult);
            Assert.Equal(expectedResult, testResult);
        }
        [Fact]
        public void GetTokenDocumentIDsTestInvertedIndexDoesNotContainsToken() {
            List<Tuple<string, string>> dataMap = new List<Tuple<string, string>> { new Tuple<string, string>("file1", "hello") };
            List<string> expectedResult = new List<string> { "file1" };
            InvertedIndex invertedIndex = new InvertedIndex();
            invertedIndex.InsertDatas(dataMap);
            List<string> testResult;
            Assert.False(invertedIndex.TryGetTokenDocumentIDs("bye", out testResult));
        }
        [Fact]
        public void GetTokenDocumentIDsTestInvertedIndexIsEmpty() {
            List<string> expectedResult = new List<string> { "file1" };
            InvertedIndex invertedIndex = new InvertedIndex();
            List<string> testResult;
            Assert.False(invertedIndex.TryGetTokenDocumentIDs("bye", out testResult));
        }
    }
}
