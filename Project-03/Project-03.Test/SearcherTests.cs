using Project_03;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Project_03Test {
    [ExcludeFromCodeCoverage]
    public class SearcherTests {
        [Fact]
        public void SearchTestInvertedIndexDataBase() {
            List<string> expectedResult = new List<string> { "file2" };
            InvertedIndex invertedIndex = GeneralFunctions.InitialInvertedIndex();
            Searcher searcher = new Searcher();
            List<IOperator> testOperators = new List<IOperator> {
                new Or("test",invertedIndex),
                new And("test",invertedIndex),
                new Not("test2",invertedIndex)
            };
            List<string> testResult = searcher.RunOperators(testOperators);
            Assert.Equal(expectedResult, testResult);
        }
    }
}
