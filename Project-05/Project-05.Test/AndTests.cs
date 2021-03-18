using Moq;
using Project_05;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Project_03Test {
    [ExcludeFromCodeCoverage]
    public class AndTests {
        [Fact]
        public void FilterTestwithNullStartingList() {
            var expectedResult = new List<string>();
            var mickedDatabase = new Mock<ProgramDatabase>();
            var testOperator = new And("test");
            mickedDatabase = GeneralFunctions.SetupDatabaseTryGetTokenDocumentIDs(
                mickedDatabase,
                testOperator.Token,
                new List<string> { "file1" },
                true
            );
            List<string> testResult = null;
            testResult = testOperator.Filter(testResult, mickedDatabase.Object);
            Assert.Equal(expectedResult, testResult);
        }
        [Fact]
        public void FilterTestWithEmptyStartingList() {
            var expectedResult = new List<string>();
            var mockedDatabase = new Mock<ProgramDatabase>();
            var testOperator = new And("test");
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
        public void FilterTestInvalidToken() {
            var expectedResult = new List<string>();
            var mockedDatabase = new Mock<ProgramDatabase>();
            var testOperator = new And("invalid");
            mockedDatabase = GeneralFunctions.SetupDatabaseTryGetTokenDocumentIDs(
                mockedDatabase,
                testOperator.Token,
                new List<string>(),
                false
            );
            var testResult = new List<string> { "file1", "file2", "file3" };
            testResult = testOperator.Filter(testResult, mockedDatabase.Object);
            Assert.Equal(expectedResult, testResult);
        }
        [Fact]
        public void FilterTestStartingList() {
            var expectedResult = new List<string> { "file1" };
            var mockedDatabase = new Mock<ProgramDatabase>();
            var firstOperator = new And("test");
            mockedDatabase = GeneralFunctions.SetupDatabaseTryGetTokenDocumentIDs(
                mockedDatabase,
                firstOperator.Token,
                new List<string> { "file1", "file3" },
                true
            );
            var secondOperator = new And("test2");
            mockedDatabase = GeneralFunctions.SetupDatabaseTryGetTokenDocumentIDs(
                mockedDatabase,
                secondOperator.Token,
                new List<string> { "file1", "file2" },
                true
            );
            var testResult = new List<string> { "file1", "file2", "file3" };
            testResult = firstOperator.Filter(testResult, mockedDatabase.Object);
            testResult = secondOperator.Filter(testResult, mockedDatabase.Object);
            Assert.Equal(expectedResult, testResult);
        }
        [Fact]
        public void EqualsTestNullInput() {
            var testOperator = new And("token");
            Assert.False(testOperator.Equals(null));
        }
        [Fact]
        public void EqualsTestDifferentTypes() {
            var firestOperator = new And("token");
            var secondOperator = new Not("token");
            Assert.False(firestOperator.Equals(secondOperator));
        }
        [Fact]
        public void EqualsTestDifferentTokens() {
            var firestOperator = new And("token");
            var secondOperator = new And("token2");
            Assert.False(firestOperator.Equals(secondOperator));
        }
        [Fact]
        public void EqualsTestSameOperators() {
            var firestOperator = new And("token");
            var secondOperator = new And("token");
            Assert.True(firestOperator.Equals(secondOperator));
        }
    }
}