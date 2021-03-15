using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Project_03 {
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
        /// <param name="text">
        /// Input text.
        /// </param>
        /// <returns>
        /// List of strings( words ).
        /// </returns>
        private List<string> TextToWords(string text) {
            return Regex.Split(text, regex).ToList();
        }
        /// <summary>
        /// Get next token from your text.
        /// </summary>
        /// <returns>
        /// A token.
        /// </returns>
        public string GetNextToken() {
            if (pointer == words.Count)
                return null;
            string word = words.ElementAt(pointer++);
            if (word.Equals(""))
                return GetNextToken();
            string token = Tokenize(word);
            return token;
        }
        /// <summary>
        /// Convert word to token.
        /// </summary>
        /// <param name="word">
        /// A string of numbers, alphabet characters and underlines.  
        /// </param>
        /// <returns>
        /// A token created from input word.
        /// </returns>
        public static string Tokenize(string word) {
            string token = word.ToLower();
            return token;
        }
        /// <summary>
        /// Show is it end of text or not.
        /// </summary>
        /// <returns>
        /// "true" if pointer riched the end of text, otherwise "false".
        /// </returns>
        public bool EndOfText() {
            if (pointer >= words.Count)
                return true;
            else if (words.ElementAt(pointer) == "") {
                pointer++;
                return EndOfText();
            }
            return false;
        }
        /// <summary>
        /// Convert all of document text to tokens.
        /// </summary>
        /// <param name="documentID">ID of document.</param>
        /// <param name="documentText">The document's content.</param>
        /// <returns></returns>
        public static List<Tuple<string, string>> GetAllTokens(string documentID, string documentText) {
            List<Tuple<string, string>> documentIdTokenPairs = new List<Tuple<string, string>>();
            Tokenizer tokenizer = new Tokenizer(documentText);
            while (!tokenizer.EndOfText())
                documentIdTokenPairs.Add(new Tuple<string, string>(documentID, tokenizer.GetNextToken()));
            return documentIdTokenPairs;
        }
    }
}
