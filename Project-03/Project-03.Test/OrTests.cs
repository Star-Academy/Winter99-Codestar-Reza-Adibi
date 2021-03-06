﻿using Moq;
using Project_03;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Xunit;

namespace Project_03Test {
    [ExcludeFromCodeCoverage]
    public class OrTests {
        [Fact]
        public void FilterTestwithNullStartingList() {
            var expectedResult = new List<string> { "file1", "file2", "file3" };
            var invertedIndex = GeneralFunctions.InitialInvertedIndex();
            IOperator firestOperator = new Or("test", invertedIndex);
            IOperator secondOperator = new Or("test2", invertedIndex);
            List<string> testResult = null;
            testResult = firestOperator.Filter(testResult);
            testResult = secondOperator.Filter(testResult);
            Assert.Equal(expectedResult, testResult);
        }
        [Fact]
        public void FilterTestwithEmptyStartingList() {
            var expectedResult = new List<string> { "file1", "file2", "file3" };
            var invertedIndex = GeneralFunctions.InitialInvertedIndex();
            IOperator firestOperator = new Or("test", invertedIndex);
            IOperator secondOperator = new Or("test2", invertedIndex);
            var testResult = new List<string>();
            testResult = firestOperator.Filter(testResult);
            testResult = secondOperator.Filter(testResult);
            Assert.Equal(expectedResult, testResult);
        }
        [Fact]
        public void FilterTestwithStartingList() {
            var expectedResult = new List<string> { "file1", "file2", "file3", "file4" };
            var invertedIndex = GeneralFunctions.InitialInvertedIndex();
            IOperator firestOperator = new Or("test", invertedIndex);
            IOperator secondOperator = new Or("test2", invertedIndex);
            var testResult = new List<string> { "file4" };
            testResult = firestOperator.Filter(testResult);
            testResult = secondOperator.Filter(testResult);
            testResult = testResult.OrderBy(item => item).ToList();
            Assert.Equal(expectedResult, testResult);
        }
        [Fact]
        public void EqualsTestNullInput() {
            var invertedIndex = new Mock<InvertedIndex>();
            IOperator testOperator = new Or("token", invertedIndex.Object);
            Assert.False(testOperator.Equals(null));
        }
        [Fact]
        public void EqualsTestDifferentTypes() {
            var invertedIndex = new Mock<InvertedIndex>();
            IOperator firestOperator = new Or("token", invertedIndex.Object);
            IOperator secondOperator = new Not("token", invertedIndex.Object);
            Assert.False(firestOperator.Equals(secondOperator));
        }
        [Fact]
        public void EqualsTestDifferentTokens() {
            var invertedIndex = new Mock<InvertedIndex>();
            IOperator firestOperator = new Or("token", invertedIndex.Object);
            IOperator secondOperator = new Or("token2", invertedIndex.Object);
            Assert.False(firestOperator.Equals(secondOperator));
        }
        [Fact]
        public void EqualsTestSameOperators() {
            var invertedIndex = new Mock<InvertedIndex>();
            IOperator firestOperator = new Or("token", invertedIndex.Object);
            IOperator secondOperator = new Or("token", invertedIndex.Object);
            Assert.True(firestOperator.Equals(secondOperator));
        }
    }
}
