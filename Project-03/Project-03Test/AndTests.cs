using System.Collections.Generic;
using Project_03;
using Xunit;

namespace Project_03Test {
    public class AndTests {
        [Fact]
        public void AndTest() {
            List<string> expectedResult = new List<string> { "file1" };
            var invertedIndex = GeneralFunctions.InitialInvertedIndex();
            IOperator firesOperator = new And("test", invertedIndex);
            IOperator secondOperator = new And("test2", invertedIndex);
            List<string> testResult = new List<string> { "file1", "file2", "file3" };
            testResult = firesOperator.Filter(testResult);
            testResult = secondOperator.Filter(testResult);
            Assert.Equal(expectedResult, testResult);
        }
        [Fact]
        public void AndTestEmptyStartList() {
            List<string> expectedResult = new List<string>();
            var invertedIndex = GeneralFunctions.InitialInvertedIndex();
            IOperator testOperator = new And("test", invertedIndex);
            List<string> testResult = new List<string>();
            testResult = testOperator.Filter(testResult);
            Assert.Equal(expectedResult, testResult);

        }
    }
}