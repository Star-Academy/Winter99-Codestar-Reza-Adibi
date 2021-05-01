using System;

namespace Project_05 {
    public class DocToken {
        public string Token { set; get; }
        public string DocumentID { set; get; }

        public override bool Equals(object obj) {
            return obj is DocToken token &&
                   Token == token.Token &&
                   DocumentID == token.DocumentID;
        }

        public override int GetHashCode() {
            return HashCode.Combine(Token, DocumentID);
        }
    }
}
