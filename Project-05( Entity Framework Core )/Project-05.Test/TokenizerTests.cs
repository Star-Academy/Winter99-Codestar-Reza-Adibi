using Project_05;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Project_05Test {
    [ExcludeFromCodeCoverage]
    public class TokenizerTests {
        [Fact]
        public void GetNextTokenTestEmptyInput() {
            string expectedResult = null;
            var inputText = "";
            var tokenizer = new Tokenizer(inputText);
            var testResult = tokenizer.GetNextToken();
            Assert.Equal(expectedResult, testResult);
        }

        [Fact]
        public void GetNextTokenTestSingleLowercaseWord() {
            var expectedResult = "string";
            var inputText = "string";
            var tokenizer = new Tokenizer(inputText);
            var testResult = tokenizer.GetNextToken();
            Assert.Equal(expectedResult, testResult);
        }

        [Fact]
        public void GetNextTokenTestSingleUppercaseWord() {
            var expectedResult = "string";
            var inputText = "STRING";
            var tokenizer = new Tokenizer(inputText);
            var testResult = tokenizer.GetNextToken();
            Assert.Equal(expectedResult, testResult);
        }

        [Fact]
        public void GetNextTokenTestSingleNumber() {
            var expectedResult = "0123";
            var inputText = "0123";
            var tokenizer = new Tokenizer(inputText);
            var testResult = tokenizer.GetNextToken();
            Assert.Equal(expectedResult, testResult);
        }

        [Fact]
        public void GetNextTokenTestSingleUnderLineString() {
            var expectedResult = "____";
            var inputText = "____";
            var tokenizer = new Tokenizer(inputText);
            var testResult = tokenizer.GetNextToken();
            Assert.Equal(expectedResult, testResult);
        }

        [Fact]
        public void GetAllTokensTestUniqueWords() {
            var expectedResult = new List<DocToken> {
                new DocToken { DocumentID = "id", Token = "string" },
                new DocToken { DocumentID = "id", Token = "___" },
                new DocToken { DocumentID = "id", Token = "10254" }
            };
            var inputText = "!@#$%^&*()+=-/?><|\"\\;:'][{}STRING ___ 10254";
            var testResult = Tokenizer.GetAllTokens("id", inputText);
            Assert.Equal(expectedResult, testResult);
        }

        [Fact]
        public void GetAllTokensTestUnuniqueWords() {
            var expectedResult = new List<DocToken> {
                new DocToken { DocumentID = "id", Token = "string" },
                new DocToken { DocumentID = "id", Token = "string" }
            };
            var inputText = "string string";
            var testResult = Tokenizer.GetAllTokens("id", inputText);
            Assert.Equal(expectedResult, testResult);
        }

        [Fact]
        public void TokenizeTest() {
            var word = "STRING";
            var expectedResult = "string";
            var testResult = Tokenizer.Tokenize(word);
            Assert.Equal(expectedResult, testResult);
        }
    }
}
