using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Project_05 {
    public class Or : IOperator {
        public static readonly int priority = 1;
        public int Priority { get { return priority; } }
        public string Token { get; }

        public Or(string token) {
            Token = token;
        }
        public List<string> Filter(List<string> inputList, IProgramDatabase database) {
            if (inputList == null)
                inputList = new List<string>();
            List<string> tokenDocuments;
            if (database.TryGetTokenDocumentIDs(Token, out tokenDocuments))
                inputList = inputList.Union(tokenDocuments).ToList();
            return inputList;
        }
        public override bool Equals(object obj) {
            if (obj == null)
                return false;
            if (this.GetType() != obj.GetType())
                return false;
            Or or = (Or)obj;
            return this.Token == or.Token;
        }
        public override int GetHashCode() {
            return HashCode.Combine(Priority, Token);
        }
    }
}
