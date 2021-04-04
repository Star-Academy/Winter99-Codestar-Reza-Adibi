using Moq;
using Project_05;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Xunit;

namespace Project_05Test {
    [ExcludeFromCodeCoverage]
    public class OrTests {
        [Fact]
        public void FilterTestwithNullStartingList() {
            var expectedResult = new List<string> { "file1", "file2", "file3" };
            var mockedDatabase = new Mock<IProgramDatabase>();
            var firstOperator = new Or("test");
            mockedDatabase = GeneralFunctions.SetupDatabaseTryGetTokenDocumentIDs(
                mockedDatabase,
                firstOperator.Token,
                new List<string> { "file1", "file2" },
                true
            );
            var secondOperator = new Or("test2");
            mockedDatabase = GeneralFunctions.SetupDatabaseTryGetTokenDocumentIDs(
                mockedDatabase,
                secondOperator.Token,
                new List<string> { "file1", "file3" },
                true
            );
            List<string> testResult = null;
            testResult = firstOperator.Filter(testResult, mockedDatabase.Object);
            testResult = secondOperator.Filter(testResult, mockedDatabase.Object);
            Assert.Equal(expectedResult, testResult);
        }

        [Fact]
        public void FilterTestwithEmptyStartingList() {
            var expectedResult = new List<string> { "file1", "file2", "file3" };
            var mockedDatabase = new Mock<IProgramDatabase>();
            var firstOperator = new Or("test");
            mockedDatabase = GeneralFunctions.SetupDatabaseTryGetTokenDocumentIDs(
                mockedDatabase,
                firstOperator.Token,
                new List<string> { "file1", "file2" },
                true
            );
            var secondOperator = new Or("test2");
            mockedDatabase = GeneralFunctions.SetupDatabaseTryGetTokenDocumentIDs(
                mockedDatabase,
                secondOperator.Token,
                new List<string> { "file1", "file3" },
                true
            );
            var testResult = new List<string>();
            testResult = firstOperator.Filter(testResult, mockedDatabase.Object);
            testResult = secondOperator.Filter(testResult, mockedDatabase.Object);
            Assert.Equal(expectedResult, testResult);
        }

        [Fact]
        public void FilterTestwithStartingList() {
            var expectedResult = new List<string> { "file4", "file1", "file2", "file3" };
            var mockedDatabase = new Mock<IProgramDatabase>();
            var firstOperator = new Or("test");
            mockedDatabase = GeneralFunctions.SetupDatabaseTryGetTokenDocumentIDs(
                mockedDatabase,
                firstOperator.Token,
                new List<string> { "file1", "file2" },
                true
            );
            var secondOperator = new Or("test2");
            mockedDatabase = GeneralFunctions.SetupDatabaseTryGetTokenDocumentIDs(
                mockedDatabase,
                secondOperator.Token,
                new List<string> { "file1", "file3" },
                true
            );
            var testResult = new List<string> { "file4" };
            testResult = firstOperator.Filter(testResult, mockedDatabase.Object);
            testResult = secondOperator.Filter(testResult, mockedDatabase.Object);
            Assert.Equal(expectedResult, testResult);
        }

        [Fact]
        public void EqualsTestNullInput() {
            IOperator testOperator = new Or("token");
            Assert.False(testOperator.Equals(null));
        }

        [Fact]
        public void EqualsTestDifferentTypes() {
            IOperator firestOperator = new Or("token");
            IOperator secondOperator = new Not("token");
            Assert.False(firestOperator.Equals(secondOperator));
        }

        [Fact]
        public void EqualsTestDifferentTokens() {
            IOperator firestOperator = new Or("token");
            IOperator secondOperator = new Or("token2");
            Assert.False(firestOperator.Equals(secondOperator));
        }

        [Fact]
        public void EqualsTestSameOperators() {
            IOperator firestOperator = new Or("token");
            IOperator secondOperator = new Or("token");
            Assert.True(firestOperator.Equals(secondOperator));
        }

        [Fact]
        public void GetHashCodeTest() {
            var expectedResult = HashCode.Combine(1, "token");
            var testOperator = new Or("token");
            var testResult = testOperator.GetHashCode();
            Assert.Equal(expectedResult, testResult);
        }
    }
}
