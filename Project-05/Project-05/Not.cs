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
        public List<string> Filter(List<string> inputList, ProgramDatabase database) {
            if (inputList == null || inputList.Count == 0)
                return new List<string>();
            var tokenDocumentIDs = new List<string>();
            database.TryGetTokenDocumentIDs(Token, out tokenDocumentIDs);
            inputList.RemoveAll(item => tokenDocumentIDs.Contains(item));
            return inputList;
        }
        public override bool Equals(object obj) {
            if (obj == null)
                return false;
            if (this.GetType() != obj.GetType())
                return false;
            Not not = (Not)obj;
            if (this.Token == not.Token)
                return true;
            return false;
        }
        [ExcludeFromCodeCoverage]
        public override int GetHashCode() {
            return HashCode.Combine(Priority, Token);
        }
    }
}
