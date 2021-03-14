using Moq;
using Project_03;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Project_03Test {
    [ExcludeFromCodeCoverage]
    public class NotTests {
        [Fact]
        public void FilterTestwithNullStartingList() {
            List<string> expectedResult = new List<string>();
            var invertedIndex = GeneralFunctions.InitialInvertedIndex();
            IOperator firestOperator = new Not("test", invertedIndex);
            List<string> testResult = null;
            testResult = firestOperator.Filter(testResult);
            Assert.Equal(expectedResult, testResult);
        }
        [Fact]
        public void FilterTestWithEmptyStartingList() {
            List<string> expectedResult = new List<string>();
            var invertedIndex = GeneralFunctions.InitialInvertedIndex();
            IOperator firestOperator = new Not("test", invertedIndex);
            List<string> testResult = new List<string>();
            testResult = firestOperator.Filter(testResult);
            Assert.Equal(expectedResult, testResult);
        }
        [Fact]
        public void FilterTestwithStartingList() {
            List<string> expectedResult = new List<string> { "file4" };
            var invertedIndex = GeneralFunctions.InitialInvertedIndex();
            IOperator firestOperator = new Not("test", invertedIndex);
            IOperator secondOperator = new Not("test2", invertedIndex);
            List<string> testResult = new List<string> { "file1", "file2", "file3", "file4" };
            testResult = firestOperator.Filter(testResult);
            testResult = secondOperator.Filter(testResult);
            Assert.Equal(expectedResult, testResult);
        }
        [Fact]
        public void EqualsTestNullInput() {
            var invertedIndex = new Mock<InvertedIndex>();
            IOperator testOperator = new Not("token", invertedIndex.Object);
            Assert.False(testOperator.Equals(null));
        }
        [Fact]
        public void EqualsTestDifferentTypes() {
            var invertedIndex = new Mock<InvertedIndex>();
            IOperator firestOperator = new Not("token", invertedIndex.Object);
            IOperator secondOperator = new Or("token", invertedIndex.Object);
            Assert.False(firestOperator.Equals(secondOperator));
        }
        [Fact]
        public void EqualsTestDifferentTokens() {
            var invertedIndex = new Mock<InvertedIndex>();
            IOperator firestOperator = new Not("token", invertedIndex.Object);
            IOperator secondOperator = new Not("token2", invertedIndex.Object);
            Assert.False(firestOperator.Equals(secondOperator));
        }
        [Fact]
        public void EqualsTestSameOperators() {
            var invertedIndex = new Mock<InvertedIndex>();
            IOperator firestOperator = new Not("token", invertedIndex.Object);
            IOperator secondOperator = new Not("token", invertedIndex.Object);
            Assert.True(firestOperator.Equals(secondOperator));
        }
    }
}
