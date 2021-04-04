using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Project_05 {
    public class Tokenizer {
        private readonly List<string> words;
        private int pointer;
        private static readonly string regex = @"\W+";

        public Tokenizer(string text) {
            words = TextToWords(text);
            pointer = 0;
        }

        /// <summary>
        /// Convert string to list of words.
        /// </summary>
        /// <param name="text"> Input text. </param>
        /// <returns> List of strings( words ). </returns>
        private List<string> TextToWords(string text) {
            return Regex.Split(text, regex).ToList();
        }

        /// <summary>
        /// Get next token from your text.
        /// </summary>
        /// <returns> A token. </returns>
        public string GetNextToken() {
            if (words.Count == 0)
                return null;
            var word = words.ElementAt(0);
            words.RemoveAt(0);
            if (string.IsNullOrWhiteSpace(word))
                return GetNextToken();
            var token = Tokenize(word);
            return token;
        }

        /// <summary>
        /// Convert word to token.
        /// </summary>
        /// <param name="word"> A string of numbers, alphabet characters and underlines. </param>
        /// <returns> A token created from input word. </returns>
        public static string Tokenize(string word) {
            var token = word.ToLower();
            return token;
        }

        /// <summary>
        /// Convert all of document text to tokens.
        /// </summary>
        /// <param name="documentID"> ID of document. </param>
        /// <param name="documentText"> The document's content. </param>
        /// <returns> All tokens of document. </returns>
        public static List<Tuple<string, string>> GetAllTokens(string documentID, string documentText) {
            var documentIdTokenPairs = new List<Tuple<string, string>>();
            var tokenizer = new Tokenizer(documentText);
            for (var token = tokenizer.GetNextToken(); token != null; token = tokenizer.GetNextToken())
                documentIdTokenPairs.Add(new Tuple<string, string>(documentID, token));
            return documentIdTokenPairs;
        }
    }
}
