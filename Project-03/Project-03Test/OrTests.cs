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
            var invertedIndex = new InvertedIndex();
            invertedIndex.InsertDatas(new List<Tuple<string, string>> {
                new Tuple<string, string>("file1","test"),
                new Tuple<string, string>("file2","test"),
                new Tuple<string, string>("file1","test2"),
                new Tuple<string, string>("file3","test2"),
            });
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
            var invertedIndex = new InvertedIndex();
            invertedIndex.InsertDatas(new List<Tuple<string, string>> {
                new Tuple<string, string>("file1","test"),
                new Tuple<string, string>("file2","test"),
                new Tuple<string, string>("file1","test2"),
                new Tuple<string, string>("file3","test2"),
            });
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
