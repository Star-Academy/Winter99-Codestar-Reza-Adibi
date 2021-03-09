using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Project_03 {
    public class And : IOperator {
        public InvertedIndex InvertedIndex { get; }
        public string Token { get; }

        public And(string token, InvertedIndex invertedIndex) {
            Token = token;
            InvertedIndex = invertedIndex;
        }
        public List<string> Filter(List<string> inputList) {
            if (inputList == null || inputList.Count == 0)
                return new List<string>();
            List<string> tokenDocumentIDs;
            if (InvertedIndex.TryGetTokenDocumentIDs(Token, out tokenDocumentIDs))
                return inputList.Intersect(tokenDocumentIDs).ToList();
            return new List<string>();
        }
    }
}
