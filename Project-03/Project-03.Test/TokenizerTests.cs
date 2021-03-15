using Project_03;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Project_03Test {
    [ExcludeFromCodeCoverage]
    public class TokenizerTests {
        [Fact]
        public void GetNextTokenTestEmptyInput() {
            string expectedResult = null;
            string inputText = "";
            Tokenizer tokenizer = new Tokenizer(inputText);
            string testResult = tokenizer.GetNextToken();
            Assert.Equal(expectedResult, testResult);
        }
        [Fact]
        public void GetNextTokenTestSingleLowercaseWord() {
            string expectedResult = "string";
            string inputText = "string";
            Tokenizer tokenizer = new Tokenizer(inputText);
            string testResult = tokenizer.GetNextToken();
            Assert.Equal(expectedResult, testResult);
        }
        [Fact]
        public void GetNextTokenTestSingleUppercaseWord() {
            string expectedResult = "string";
            string inputText = "STRING";
            Tokenizer tokenizer = new Tokenizer(inputText);
            string testResult = tokenizer.GetNextToken();
            Assert.Equal(expectedResult, testResult);
        }
        [Fact]
        public void GetNextTokenTestSingleNumber() {
            string expectedResult = "0123";
            string inputText = "0123";
            Tokenizer tokenizer = new Tokenizer(inputText);
            string testResult = tokenizer.GetNextToken();
            Assert.Equal(expectedResult, testResult);
        }
        [Fact]
        public void GetNextTokenTestSingleUnderLineString() {
            string expectedResult = "____";
            string inputText = "____";
            Tokenizer tokenizer = new Tokenizer(inputText);
            string testResult = tokenizer.GetNextToken();
            Assert.Equal(expectedResult, testResult);
        }
        [Fact]
        public void GetAllTokensTestUniqueWords() {
            List<Tuple<string, string>> expectedResult = new List<Tuple<string, string>> {
                new Tuple<string, string>("id", "string"),
                new Tuple<string, string>("id", "___"),
                new Tuple<string, string>("id", "10254")
            };
            string inputText = "!@#$%^&*()+=-/?><|\"\\;:'][{}STRING ___ 10254";
            List<Tuple<string, string>> testResult = Tokenizer.GetAllTokens("id", inputText);
            Assert.Equal(expectedResult, testResult);
        }
        [Fact]
        public void GetAllTokensTestUnuniqueWords() {
            List<Tuple<string, string>> expectedResult = new List<Tuple<string, string>> {
                new Tuple<string, string>("id", "string"),
                new Tuple<string, string>("id", "string")
            };
            string inputText = "string string";
            List<Tuple<string, string>> testResult = Tokenizer.GetAllTokens("id", inputText);
            Assert.Equal(expectedResult, testResult);
        }
        [Fact]
        public void TokenizeTest() {
            string word = "STRING";
            string expectedResult = "string";
            string testResult = Tokenizer.Tokenize(word);
            Assert.Equal(expectedResult, testResult);
        }
    }
}
