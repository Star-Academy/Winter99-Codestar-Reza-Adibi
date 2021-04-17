using Project_05;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Project_05Test {
    [ExcludeFromCodeCoverage]
    public class InvertedIndexTests {
        private InvertedIndex invertedIndex;

        public InvertedIndexTests() {
            invertedIndex = new InvertedIndex();
        }

        [Fact]
        public void InsertDatasTestSingleData() {
            var dataMap = new List<DocToken> {
                new DocToken { DocumentID = "file1", Token = "hello" }
            };
            var expectedResult = new Dictionary<string, List<string>> { { "hello", new List<string> { "file1" } } };
            invertedIndex.InsertDataList(dataMap);
            var testResult = invertedIndex.TokenMap;
            Assert.Equal(expectedResult, testResult);
        }

        [Fact]
        public void InsertDatasTestMultiData() {
            var dataMap = new List<DocToken> {
                new DocToken { DocumentID = "file1", Token = "hello" },
                new DocToken { DocumentID = "file1", Token = "hello" },
                new DocToken { DocumentID = "file2", Token = "hello" },
                new DocToken { DocumentID = "file2", Token = "hello" },
                new DocToken { DocumentID = "file1", Token = "hello2" },
                new DocToken { DocumentID = "file2", Token = "hello2" }
            };
            var expectedResult = new Dictionary<string, List<string>> {
                { "hello", new List<string> { "file1", "file1", "file2", "file2" } },
                { "hello2", new List<string> { "file1", "file2" } }
            };
            invertedIndex.InsertDataList(dataMap);
            var testResult = invertedIndex.TokenMap;
            Assert.Equal(expectedResult, testResult);
        }

        [Fact]
        public void GetTokenDocumentIDsTestInvertedIndexContainsToken() {
            var dataMap = new List<DocToken> { new DocToken { DocumentID = "file1", Token = "hello" } };
            var expectedResult = new List<string> { "file1" };
            invertedIndex.InsertDataList(dataMap);
            invertedIndex.TryGetTokenDocumentIDs("hello", out var testResult);
            Assert.Equal(expectedResult, testResult);
        }

        [Fact]
        public void GetTokenDocumentIDsTestInvertedIndexDoesNotContainsToken() {
            var dataMap = new List<DocToken> { new DocToken { DocumentID = "file1", Token = "hello" } };
            invertedIndex.InsertDataList(dataMap);
            Assert.False(invertedIndex.TryGetTokenDocumentIDs("bye", out var testResult));
        }

        [Fact]
        public void GetTokenDocumentIDsTestInvertedIndexIsEmpty() {
            Assert.False(invertedIndex.TryGetTokenDocumentIDs("bye", out var testResult));
        }
    }
}
