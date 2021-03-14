using Project_03;
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
        public void GetNextTokenTestMultiUniqueWords() {
            HashSet<string> expectedResult = new HashSet<string> { "string", "___", "10254" };
            string inputText = "!@#$%^&*()+=-/?><|\"\\;:'][{}STRING ___ 10254";
            Tokenizer tokenizer = new Tokenizer(inputText);
            HashSet<string> testResult = new HashSet<string>();
            string token;
            while (!tokenizer.EndOfText()) {
                token = tokenizer.GetNextToken();
                testResult.Add(token);
            }
            Assert.Equal(expectedResult, testResult);
        }
        [Fact]
        public void GetNextTokenTestMultiUnuniqueWords() {
            List<string> expectedResult = new List<string> { "string", "string" };
            string inputText = "string string";
            Tokenizer tokenizer = new Tokenizer(inputText);
            List<string> testResult = new List<string>();
            string token;
            while (!tokenizer.EndOfText()) {
                token = tokenizer.GetNextToken();
                testResult.Add(token);
            }
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
