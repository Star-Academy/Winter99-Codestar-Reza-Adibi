﻿using System.Collections.Generic;
using System.Linq;

namespace Project_03 {
    public class And : IOperator {
        public static readonly int priority = 2;
        public int Priority { get { return priority; } }
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
        public override bool Equals(object obj) {
            if (obj == null)
                return false;
            if (this.GetType() != obj.GetType())
                return false;
            And and = (And)obj;
            if (this.Token == and.Token && this.InvertedIndex == and.InvertedIndex)
                return true;
            return false;
        }
    }
}
