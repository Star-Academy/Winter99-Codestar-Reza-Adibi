using Moq;
using Project_03;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Project_03Test {
    [ExcludeFromCodeCoverage]
    public class AndTests {
        [Fact]
        public void FilterTestwithNullStartingList() {
            var expectedResult = new List<string>();
            var invertedIndex = GeneralFunctions.InitialInvertedIndex();
            IOperator firestOperator = new And("test", invertedIndex);
            List<string> testResult = null;
            testResult = firestOperator.Filter(testResult);
            Assert.Equal(expectedResult, testResult);
        }
        [Fact]
        public void FilterTestWithEmptyStartingList() {
            var expectedResult = new List<string>();
            var invertedIndex = GeneralFunctions.InitialInvertedIndex();
            IOperator firestOperator = new And("test", invertedIndex);
            var testResult = new List<string>();
            testResult = firestOperator.Filter(testResult);
            Assert.Equal(expectedResult, testResult);
        }
        [Fact]
        public void FilterTestInvalidToken() {
            var expectedResult = new List<string>();
            var invertedIndex = GeneralFunctions.InitialInvertedIndex();
            IOperator firestOperator = new And("invalid", invertedIndex);
            var testResult = new List<string> { "file1", "file2", "file3" };
            testResult = firestOperator.Filter(testResult);
            Assert.Equal(expectedResult, testResult);
        }
        [Fact]
        public void FilterTestStartingList() {
            var expectedResult = new List<string> { "file1" };
            var invertedIndex = GeneralFunctions.InitialInvertedIndex();
            IOperator firestOperator = new And("test", invertedIndex);
            IOperator secondOperator = new And("test2", invertedIndex);
            var testResult = new List<string> { "file1", "file2", "file3" };
            testResult = firestOperator.Filter(testResult);
            testResult = secondOperator.Filter(testResult);
            Assert.Equal(expectedResult, testResult);
        }
        [Fact]
        public void EqualsTestNullInput() {
            var invertedIndex = new Mock<InvertedIndex>();
            IOperator testOperator = new And("token", invertedIndex.Object);
            Assert.False(testOperator.Equals(null));
        }
        [Fact]
        public void EqualsTestDifferentTypes() {
            var invertedIndex = new Mock<InvertedIndex>();
            IOperator firestOperator = new And("token", invertedIndex.Object);
            IOperator secondOperator = new Not("token", invertedIndex.Object);
            Assert.False(firestOperator.Equals(secondOperator));
        }
        [Fact]
        public void EqualsTestDifferentTokens() {
            var invertedIndex = new Mock<InvertedIndex>();
            IOperator firestOperator = new And("token", invertedIndex.Object);
            IOperator secondOperator = new And("token2", invertedIndex.Object);
            Assert.False(firestOperator.Equals(secondOperator));
        }
        [Fact]
        public void EqualsTestSameOperators() {
            var invertedIndex = new Mock<InvertedIndex>();
            IOperator firestOperator = new And("token", invertedIndex.Object);
            IOperator secondOperator = new And("token", invertedIndex.Object);
            Assert.True(firestOperator.Equals(secondOperator));
        }

    }
}