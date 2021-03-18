using Moq;
using Project_05;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Project_05Test {
    [ExcludeFromCodeCoverage]
    public class OperatorExtractorTests {
        [Fact]
        public void GetNextOperatorTestSingleOr() {
            IOperator expectedResult = new Or("hi");
            var inputText = "+hi";
            var operatorExtractor = new OperatorExtractor(inputText);
            var testResult = operatorExtractor.GetNextOperator();
            Assert.Equal(expectedResult, testResult);
        }
        [Fact]
        public void GetNextOperatorTestSingleAnd() {
            IOperator expectedResult = new And("hi");
            var inputText = "hi";
            var operatorExtractor = new OperatorExtractor(inputText);
            var testResult = operatorExtractor.GetNextOperator();
            Assert.Equal(expectedResult, testResult);
        }
        [Fact]
        public void GetNextOperatorTestSingleNot() {
            IOperator expectedResult = new Not("hi");
            var inputText = "-hi";
            var operatorExtractor = new OperatorExtractor(inputText);
            var testResult = operatorExtractor.GetNextOperator();
            Assert.Equal(expectedResult, testResult);
        }
        [Fact]
        public void GetAllOperatorTest() {
            var expectedResult = new List<IOperator> {
            new Or("hi"),
            new And("hi"),
            new Not("hi")
            };
            var text = "hi +hi -hi";
            var operatorExtractor = new OperatorExtractor(text);
            var testResult = OperatorExtractor.GetAllOperators(text);
            Assert.Equal(expectedResult, testResult);
        }
    }
}
