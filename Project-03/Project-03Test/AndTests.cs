using System.Collections.Generic;
using Project_03;
using Moq;
using Xunit;
using System;

namespace Project_03Test {
    public class AndTests {
        [Fact]
        public void AndTest() {
            List<string> expectedResult = new List<string> { "file1" };
            var invertedIndex = new InvertedIndex();
            invertedIndex.InsertDatas(new List<Tuple<string, string>> {
                new Tuple<string, string>("file1","test"),
                new Tuple<string, string>("file2","test"),
                new Tuple<string, string>("file1","test2"),
                new Tuple<string, string>("file3","test2"),
            });
            IOperator firesOperator = new And("test", invertedIndex);
            IOperator secondOperator = new And("test2", invertedIndex);
            List<string> testResult = new List<string> { "file1", "file2", "file3" };
            testResult = firesOperator.Filter(testResult);
            testResult = secondOperator.Filter(testResult);
            Assert.Equal(expectedResult, testResult);
        }
    }
}