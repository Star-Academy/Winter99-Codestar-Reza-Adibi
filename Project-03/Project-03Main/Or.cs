using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Project_03 {
    public class Or : IOperator {
        public InvertedIndex InvertedIndex { get; }
        public string Token { get; }

        public Or(string token, InvertedIndex invertedIndex) {
            Token = token;
            InvertedIndex = invertedIndex;
        }
        public List<string> Filter(List<string> inputList) {
            if (inputList == null)
                inputList = new List<string>();
            List<string> tokenDocuments;
            if (InvertedIndex.TryGetTokenDocumentIDs(Token, out tokenDocuments))
                inputList = inputList.Union(tokenDocuments).ToList();
            return inputList;
        }
    }
}
