using System.Collections.Generic;
using System.Linq;

namespace Project_03 {
    public class OperatorExtractor {
        private readonly List<string> tokens;
        private int pointer;
        private static readonly string separatorsRegex = " ";

        public OperatorExtractor(string text) {
            tokens = TextToTokens(text);
            pointer = 0;
        }
        /// <summary>
        /// Convert user input text to list of operator tokens.
        /// </summary>
        /// <param name="text">User input filters.</param>
        /// <returns>List of tokenized filters( operator tokens ).</returns>
        private List<string> TextToTokens(string text) {
            var words = text.Split(separatorsRegex);
            var tokens = new List<string>();
            foreach (string word in words)
                tokens.Add(Tokenizer.Tokenize(word));
            return tokens;
        }
        /// <summary>
        /// Get next operator from user input text.
        /// </summary>
        /// <param name="invertedIndex">The inverted index that we are going to run operators on it.</param>
        /// <returns>Next operator from user Input text.</returns>
        public IOperator GetNextOperator(InvertedIndex invertedIndex) {
            string operatorToken = tokens[pointer++];
            switch (operatorToken[0]) {
                case '+': return new Or(operatorToken.Substring(1), invertedIndex);
                case '-': return new Not(operatorToken.Substring(1), invertedIndex);
                default: return new And(operatorToken, invertedIndex);
            }
        }
        /// <summary>
        /// Check if the pointer reached to  the end of user input text. 
        /// </summary>
        /// <returns>If pointer reached to end of text "True", otherwise "False".</returns>
        public bool EndOfText() {
            if (pointer >= tokens.Count)
                return true;
            return false;
        }
        /// <summary>
        /// Get all operators from user input text.
        /// </summary>
        /// <param name="userInputText">User input text.</param>
        /// <param name="invertedIndex">The inverted index that we are going to run operators on it.</param>
        /// <returns>List of all operators order by priority.</returns>
        public static List<IOperator> GetAllOperators(string userInputText, InvertedIndex invertedIndex) {
            OperatorExtractor operatorExtractor = new OperatorExtractor(userInputText);
            List<IOperator> operators = new List<IOperator>();
            while (!operatorExtractor.EndOfText())
                operators.Add(operatorExtractor.GetNextOperator(invertedIndex));
            return operators.OrderBy(op => op.Priority).ToList();
        }
    }
}
