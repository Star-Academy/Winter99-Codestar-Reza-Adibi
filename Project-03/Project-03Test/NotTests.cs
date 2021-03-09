using Project_03;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Project_03Test {
    public class NotTests {
        [Fact]
        public void NotTest() {
            List<string> expectedResult = new List<string>();
            var invertedIndex = GeneralFunctions.InitialInvertedIndex();
            IOperator firesOperator = new Not("test", invertedIndex);
            List<string> testResult = new List<string>();
            testResult = firesOperator.Filter(testResult);
            Assert.Equal(expectedResult, testResult);
        }
        [Fact]
        public void OrTestwithStartingList() {
            List<string> expectedResult = new List<string> { "file4" };
            var invertedIndex = GeneralFunctions.InitialInvertedIndex();
            IOperator firesOperator = new Not("test", invertedIndex);
            IOperator secondOperator = new Not("test2", invertedIndex);
            List<string> testResult = new List<string> { "file1", "file2", "file3", "file4" };
            testResult = firesOperator.Filter(testResult);
            testResult = secondOperator.Filter(testResult);
            Assert.Equal(expectedResult, testResult);
        }
    }
}
