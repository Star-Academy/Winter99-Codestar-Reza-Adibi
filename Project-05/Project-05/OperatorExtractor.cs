using System.Collections.Generic;
using System.Linq;

namespace Project_05 {
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
        /// <param name="text"> User input filters. </param>
        /// <returns> List of tokenized filters( operator tokens ). </returns>
        private List<string> TextToTokens(string text) {
            var words = text.Split(separatorsRegex);
            var tokens = new List<string>();
            foreach (string word in words)
                if (!string.IsNullOrWhiteSpace(word))
                    tokens.Add(Tokenizer.Tokenize(word));
            return tokens;
        }
        /// <summary>
        /// Get next operator from user input text.
        /// </summary>
        /// <returns> Next operator from user Input text. </returns>
        public IOperator GetNextOperator() {
            var operatorToken = tokens[pointer++];
            switch (operatorToken[0]) {
                case '+': return new Or(operatorToken.Substring(1));
                case '-': return new Not(operatorToken.Substring(1));
                default: return new And(operatorToken);
            }
        }
        /// <summary>
        /// Check if the pointer reached to  the end of user input text. 
        /// </summary>
        /// <returns> If pointer reached to end of text "True", otherwise "False". </returns>
        public bool EndOfText() {
            return pointer >= tokens.Count;

        }
        /// <summary>
        /// Get all operators from user input text.
        /// </summary>
        /// <param name="userInputText"> User input text. </param>
        /// <returns> List of all operators order by priority. </returns>
        public static List<IOperator> GetAllOperators(string userInputText) {
            var operatorExtractor = new OperatorExtractor(userInputText);
            var operators = new List<IOperator>();
            while (!operatorExtractor.EndOfText())
                operators.Add(operatorExtractor.GetNextOperator());
            return operators.OrderBy(op => op.Priority).ToList();
        }
    }
}
