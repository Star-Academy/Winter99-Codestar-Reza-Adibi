using System;
using System.Collections.Generic;
using System.Linq;

namespace Project_03 {
    public class Not : IOperator {
        public InvertedIndex InvertedIndex { get; }
        public string Token { get; }

        public Not(string token, InvertedIndex invertedIndex) {
            Token = token;
            InvertedIndex = invertedIndex;
        }
        public List<string> Filter(List<string> inputList) {
            if (inputList == null || inputList.Count == 0)
                return new List<string>();
            List<string> tokenDocumentIDs = new List<string>();
            InvertedIndex.TryGetTokenDocumentIDs(Token, out tokenDocumentIDs);
            inputList.RemoveAll(item => tokenDocumentIDs.Contains(item));
            return inputList;
        }
    }
}
