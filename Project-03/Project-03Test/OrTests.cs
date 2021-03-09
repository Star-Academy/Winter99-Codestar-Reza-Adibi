using Project_03;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Project_03Test {
    public class OrTests {
        [Fact]
        public void OrTest() {
            List<string> expectedResult = new List<string> { "file1", "file2", "file3" };
            var invertedIndex = GeneralFunctions.InitialInvertedIndex();
            IOperator firesOperator = new Or("test", invertedIndex);
            IOperator secondOperator = new Or("test2", invertedIndex);
            List<string> testResult = new List<string>();
            testResult = firesOperator.Filter(testResult);
            testResult = secondOperator.Filter(testResult);
            Assert.Equal(expectedResult, testResult);
        }
        [Fact]
        public void OrTestwithStartingList() {
            List<string> expectedResult = new List<string> { "file1", "file2", "file3", "file4" };
            var invertedIndex = GeneralFunctions.InitialInvertedIndex();
            IOperator firesOperator = new Or("test", invertedIndex);
            IOperator secondOperator = new Or("test2", invertedIndex);
            List<string> testResult = new List<string> { "file4" };
            testResult = firesOperator.Filter(testResult);
            testResult = secondOperator.Filter(testResult);
            testResult = testResult.OrderBy(item => item).ToList();
            Assert.Equal(expectedResult, testResult);
        }
    }
}
