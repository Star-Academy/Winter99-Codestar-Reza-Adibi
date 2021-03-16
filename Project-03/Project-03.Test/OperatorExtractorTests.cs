using Moq;
using Project_03;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Project_03Test {
    [ExcludeFromCodeCoverage]
    public class OperatorExtractorTests {
        [Fact]
        public void GetNextOperatorTestSingleOr() {
            var invertedIndex = new Mock<InvertedIndex>();
            IOperator expectedResult = new Or("hi", invertedIndex.Object);
            var inputText = "+hi";
            var operatorExtractor = new OperatorExtractor(inputText);
            var testResult = operatorExtractor.GetNextOperator(invertedIndex.Object);
            Assert.Equal(expectedResult, testResult);
        }
        [Fact]
        public void GetNextOperatorTestSingleAnd() {
            var invertedIndex = new Mock<InvertedIndex>();
            IOperator expectedResult = new And("hi", invertedIndex.Object);
            var inputText = "hi";
            var operatorExtractor = new OperatorExtractor(inputText);
            var testResult = operatorExtractor.GetNextOperator(invertedIndex.Object);
            Assert.Equal(expectedResult, testResult);
        }
        [Fact]
        public void GetNextOperatorTestSingleNot() {
            var invertedIndex = new Mock<InvertedIndex>();
            IOperator expectedResult = new Not("hi", invertedIndex.Object);
            var inputText = "-hi";
            var operatorExtractor = new OperatorExtractor(inputText);
            var testResult = operatorExtractor.GetNextOperator(invertedIndex.Object);
            Assert.Equal(expectedResult, testResult);
        }
        [Fact]
        public void GetAllOperatorTest() {
            var invertedIndex = new Mock<InvertedIndex>();
            var expectedResult = new List<IOperator> {
            new Or("hi",invertedIndex.Object),
            new And("hi",invertedIndex.Object),
            new Not("hi",invertedIndex.Object)
            };
            var text = "hi +hi -hi";
            var operatorExtractor = new OperatorExtractor(text);
            var testResult = OperatorExtractor.GetAllOperators(text, invertedIndex.Object);
            Assert.Equal(expectedResult, testResult);
        }
    }
}
