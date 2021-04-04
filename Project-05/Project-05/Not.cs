using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Project_05 {
    public class Not : IOperator {
        public static readonly int priority = 3;
        public int Priority { get { return priority; } }
        public string Token { get; }

        public Not(string token) {
            Token = token;
        }

        public List<string> Filter(List<string> inputList, IProgramDatabase database) {
            if (inputList == null || inputList.Count == 0)
                return new List<string>();
            database.TryGetTokenDocumentIDs(Token, out var tokenDocumentIDs);
            inputList.RemoveAll(item => tokenDocumentIDs.Contains(item));
            return inputList;
        }

        public override bool Equals(object obj) {
            if (obj == null)
                return false;
            if (this.GetType() != obj.GetType())
                return false;
            Not not = (Not)obj;
            return this.Token == not.Token;
        }

        public override int GetHashCode() {
            return HashCode.Combine(Priority, Token);
        }
    }
}
