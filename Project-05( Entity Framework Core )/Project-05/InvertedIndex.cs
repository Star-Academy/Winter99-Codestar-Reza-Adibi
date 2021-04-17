using System;
using System.Collections.Generic;

namespace Project_05 {
    public class InvertedIndex : IProgramDatabase {
        private readonly Dictionary<string, List<string>> tokenMap;
        public Dictionary<string, List<string>> TokenMap { get { return this.tokenMap; } }
        public InvertedIndex() {
            this.tokenMap = new Dictionary<string, List<string>>();
        }

        public void InsertData(string token, string documentID) {
            List<string> documentIDs;
            if (tokenMap.TryGetValue(token, out documentIDs))
                documentIDs.Add(documentID);
            else {
                documentIDs = new List<string> { documentID };
                this.tokenMap.Add(token, documentIDs);
            }
        }

        public bool TryGetTokenDocumentIDs(string token, out List<string> output) {
            return this.tokenMap.TryGetValue(token, out output);
        }

        public void InsertDataList(List<DocToken> data) {
            foreach (var pair in data)
                InsertData(pair.Token, pair.DocumentID);
        }
    }
}
