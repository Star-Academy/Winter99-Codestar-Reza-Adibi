using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Project_05 {
    public class And : IOperator {
        public static readonly int priority = 2;
        public int Priority { get { return priority; } }
        public string Token { get; }

        public And(string token) {
            Token = token;
        }

        public List<string> Filter(List<string> inputList, IProgramDatabase database) {
            if (inputList == null || inputList.Count == 0)
                return new List<string>();
            if (database.TryGetTokenDocumentIDs(Token, out var tokenDocumentIDs))
                return inputList.Intersect(tokenDocumentIDs).ToList();
            return new List<string>();
        }

        public override bool Equals(object obj) {
            if (obj == null)
                return false;
            if (this.GetType() != obj.GetType())
                return false;
            And and = (And)obj;
            return this.Token == and.Token;
        }

        public override int GetHashCode() {
            return HashCode.Combine(Priority, Token);
        }
    }
}
