using Project_03;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Project_03Test {
    [ExcludeFromCodeCoverage]
    public class SearcherTests {
        private InvertedIndex invertedIndex;
        private Searcher searcher;
        public SearcherTests() {
            invertedIndex = GeneralFunctions.InitialInvertedIndex();
            searcher = new Searcher();
        }
        [Fact]
        public void SearchTestInvertedIndexDataBase() {
            var expectedResult = new List<string> { "file2" };
            var testOperators = new List<IOperator> {
                new Or("test",invertedIndex),
                new And("test",invertedIndex),
                new Not("test2",invertedIndex)
            };
            var testResult = searcher.RunOperators(testOperators);
            Assert.Equal(expectedResult, testResult);
        }
        [Fact]
        public void SearchTestInvertedIndexDataBaseFirstOperatorIsAnd() {
            var expectedResult = new List<string> { "file2" };
            var testOperators = new List<IOperator> {
                new And("test",invertedIndex),
                new Not("test2",invertedIndex)
            };
            var testResult = searcher.RunOperators(testOperators);
            Assert.Equal(expectedResult, testResult);
        }
    }
}
