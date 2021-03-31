using Moq;
using Project_05;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Project_05Test {
    [ExcludeFromCodeCoverage]
    public class NotTests {
        [Fact]
        public void FilterTestwithNullStartingList() {
            var expectedResult = new List<string>();
            var mockedDatabase = new Mock<IProgramDatabase>();
            var testOperator = new Not("test");
            mockedDatabase = GeneralFunctions.SetupDatabaseTryGetTokenDocumentIDs(
                mockedDatabase,
                testOperator.Token,
                new List<string> { "file1" },
                true
            );
            List<string> testResult = null;
            testResult = testOperator.Filter(testResult, mockedDatabase.Object);
            Assert.Equal(expectedResult, testResult);
        }
        [Fact]
        public void FilterTestWithEmptyStartingList() {
            var expectedResult = new List<string>();
            var mockedDatabase = new Mock<IProgramDatabase>();
            var testOperator = new Not("test");
            mockedDatabase = GeneralFunctions.SetupDatabaseTryGetTokenDocumentIDs(
                mockedDatabase,
                testOperator.Token,
                new List<string> { "file1" },
                true
            );
            var testResult = new List<string>();
            testResult = testOperator.Filter(testResult, mockedDatabase.Object);
            Assert.Equal(expectedResult, testResult);
        }
        [Fact]
        public void FilterTestwithStartingList() {
            var expectedResult = new List<string> { "file4" };
            var mockedDatabase = new Mock<IProgramDatabase>();
            var firstOperator = new Not("test");
            mockedDatabase = GeneralFunctions.SetupDatabaseTryGetTokenDocumentIDs(
                mockedDatabase,
                firstOperator.Token,
                new List<string> { "file1", "file2" },
                true
            );
            var secondOperator = new Not("test2");
            mockedDatabase = GeneralFunctions.SetupDatabaseTryGetTokenDocumentIDs(
                mockedDatabase,
                secondOperator.Token,
                new List<string> { "file1", "file3" },
                true
            );
            var testResult = new List<string> { "file1", "file2", "file3", "file4" };
            testResult = firstOperator.Filter(testResult, mockedDatabase.Object);
            testResult = secondOperator.Filter(testResult, mockedDatabase.Object);
            Assert.Equal(expectedResult, testResult);
        }
        [Fact]
        public void EqualsTestNullInput() {
            IOperator testOperator = new Not("token");
            Assert.False(testOperator.Equals(null));
        }
        [Fact]
        public void EqualsTestDifferentTypes() {
            IOperator firestOperator = new Not("token");
            IOperator secondOperator = new Or("token");
            Assert.False(firestOperator.Equals(secondOperator));
        }
        [Fact]
        public void EqualsTestDifferentTokens() {
            IOperator firestOperator = new Not("token");
            IOperator secondOperator = new Not("token2");
            Assert.False(firestOperator.Equals(secondOperator));
        }
        [Fact]
        public void EqualsTestSameOperators() {
            IOperator firestOperator = new Not("token");
            IOperator secondOperator = new Not("token");
            Assert.True(firestOperator.Equals(secondOperator));
        }
        [Fact]
        public void GetHashCodeTest() {
            var expectedResult = HashCode.Combine(3, "token");
            var testOperator = new Not("token");
            var testResult = testOperator.GetHashCode();
            Assert.Equal(expectedResult, testResult);
        }
    }
}
