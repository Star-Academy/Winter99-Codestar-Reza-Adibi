using Moq;
using Project_05;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Project_05Test {
    [ExcludeFromCodeCoverage]
    public class SearcherTests {
        private IProgramDatabase database;
        private Searcher searcher;
        public SearcherTests() {
            var mockedDatabase = new Mock<IProgramDatabase>();
            mockedDatabase = GeneralFunctions.SetupDatabaseTryGetTokenDocumentIDs(
                mockedDatabase,
                "test",
                new List<string> { "file1", "file2" },
                true
            );
            mockedDatabase = GeneralFunctions.SetupDatabaseTryGetTokenDocumentIDs(
                mockedDatabase,
                "test2",
                new List<string> { "file1", "file3" },
                true
            );
            database = mockedDatabase.Object;
            searcher = new Searcher();
        }
        [Fact]
        public void SearchTestInvertedIndexDataBase() {
            var expectedResult = new List<string> { "file2" };
            var testOperators = new List<IOperator> {
                new Or("test"),
                new And("test"),
                new Not("test2")
            };
            var testResult = searcher.RunOperators(testOperators, database);
            Assert.Equal(expectedResult, testResult);
        }
        [Fact]
        public void SearchTestInvertedIndexDataBaseFirstOperatorIsAnd() {
            var expectedResult = new List<string> { "file2" };
            var testOperators = new List<IOperator> {
                new And("test"),
                new Not("test2")
            };
            var testResult = searcher.RunOperators(testOperators, database);
            Assert.Equal(expectedResult, testResult);
        }
    }
}
