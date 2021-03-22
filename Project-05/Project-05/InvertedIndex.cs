using System.Collections.Generic;

namespace Project_05 {
    public class InvertedIndex : ProgramDatabase {
        private readonly Dictionary<string, List<string>> tokenMap;
        public Dictionary<string, List<string>> TokenMap { get { return this.tokenMap; } }
        public InvertedIndex() {
            this.tokenMap = new Dictionary<string, List<string>>();
        }
        public override void InsertData(string token, string documentID) {
            List<string> documentIDs;
            if (tokenMap.TryGetValue(token, out documentIDs))
                documentIDs.Add(documentID);
            else {
                documentIDs = new List<string> { documentID };
                this.tokenMap.Add(token, documentIDs);
            }
        }
        public override bool TryGetTokenDocumentIDs(string token, out List<string> output) {
            return this.tokenMap.TryGetValue(token, out output);
        }
    }
}
