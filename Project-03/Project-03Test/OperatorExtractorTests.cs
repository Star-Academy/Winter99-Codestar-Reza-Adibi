using Moq;
using Project_03;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Xunit;

namespace Project_03Test {
    [ExcludeFromCodeCoverage]
    public class OperatorExtractorTests {
        [Fact]
        public void GetNextOperatorTestSingleOr() {
            var invertedIndex = new Mock<InvertedIndex>();
            IOperator expectedResult = new Or("hi", invertedIndex.Object);
            string inputText = "+hi";
            OperatorExtractor operatorExtractor = new OperatorExtractor(inputText);
            IOperator testResult = operatorExtractor.GetNextOperator(invertedIndex.Object);
            Assert.Equal(expectedResult, testResult);
        }
        [Fact]
        public void GetNextOperatorTestSingleAnd() {
            var invertedIndex = new Mock<InvertedIndex>();
            IOperator expectedResult = new And("hi", invertedIndex.Object);
            string inputText = "hi";
            OperatorExtractor operatorExtractor = new OperatorExtractor(inputText);
            IOperator testResult = operatorExtractor.GetNextOperator(invertedIndex.Object);
            Assert.Equal(expectedResult, testResult);
        }
        [Fact]
        public void GetNextOperatorTestSingleNot() {
            var invertedIndex = new Mock<InvertedIndex>();
            IOperator expectedResult = new Not("hi", invertedIndex.Object);
            string inputText = "-hi";
            OperatorExtractor operatorExtractor = new OperatorExtractor(inputText);
            IOperator testResult = operatorExtractor.GetNextOperator(invertedIndex.Object);
            Assert.Equal(expectedResult, testResult);
        }
        [Fact]
        public void GetNextOperatorTestMiltiOperators() {
            var invertedIndex = new Mock<InvertedIndex>();
            List<IOperator> expectedResult = new List<IOperator> {
            new Or("hi",invertedIndex.Object),
            new And("hi",invertedIndex.Object),
            new Not("hi",invertedIndex.Object)
            };
            string text = "hi +hi -hi";
            OperatorExtractor operatorExtractor = new OperatorExtractor(text);
            List<IOperator> testResult = new List<IOperator>();
            while (!operatorExtractor.EndOfText()) {
                testResult.Add(operatorExtractor.GetNextOperator(invertedIndex.Object));
            }
            testResult = testResult.OrderBy(item => item.Priority).ToList();
            Assert.Equal(expectedResult, testResult);
        }
    }
}
