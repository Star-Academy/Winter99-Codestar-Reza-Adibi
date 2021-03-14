using System.Collections.Generic;

namespace Project_03 {
    public class OperatorExtractor {
        private readonly List<string> tokens;
        private int pointer;
        private static readonly string separatorsRegex = " ";

        public OperatorExtractor(string text) {
            tokens = TextToTokens(text);
            pointer = 0;
        }
        private List<string> TextToTokens(string text) {
            var words = text.Split(separatorsRegex);
            var tokens = new List<string>();
            foreach (string word in words)
                tokens.Add(Tokenizer.Tokenize(word));
            return tokens;
        }
        public IOperator GetNextOperator(InvertedIndex invertedIndex) {
            string operatorToken = tokens[pointer++];
            switch (operatorToken[0]) {
                case '+': return new Or(operatorToken.Substring(1), invertedIndex);
                case '-': return new Not(operatorToken.Substring(1), invertedIndex);
                default: return new And(operatorToken, invertedIndex);
            }
        }
        public bool EndOfText() {
            if (pointer >= tokens.Count)
                return true;
            return false;
        }
    }
}
