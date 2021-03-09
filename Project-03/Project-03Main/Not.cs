using System;
using System.Collections.Generic;
using System.Linq;

namespace Project_03 {
    public class Not : IOperator {
        public static readonly int value = 3;
        public int Value { get { return value; } }
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
        public override bool Equals(object obj) {
            if (this.GetType() != obj.GetType())
                return false;
            Not not = (Not)obj;
            if (this.Token == not.Token && this.InvertedIndex == not.InvertedIndex)
                return true;
            return false;
        }
    }
}
