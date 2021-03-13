using System.Collections.Generic;
using System.Linq;

namespace Project_03 {
    public class Or : IOperator {
        public static readonly int priority = 1;
        public int Priority { get { return priority; } }
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
        public override bool Equals(object obj) {
            if (this.GetType() != obj.GetType())
                return false;
            Or or = (Or)obj;
            if (this.Token == or.Token && this.InvertedIndex == or.InvertedIndex)
                return true;
            return false;
        }
    }
}
