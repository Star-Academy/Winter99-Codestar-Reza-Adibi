using System;
using System.Collections.Generic;

namespace Project_03 {
    public class InvertedIndex {
        private readonly Dictionary<string, List<string>> tokenMap;
        public Dictionary<string, List<string>> TokenMap { get { return this.tokenMap; } }
        public InvertedIndex() {
            tokenMap = new Dictionary<string, List<string>>();
        }
        /// <summary>
        /// Insert list of Tuple(documentID, token) to invertedIndex.
        /// </summary>
        /// <param name="data">
        /// List of Tuple\<string, string\>( documentID, token ). 
        /// </param>
        public void InsertDatas(List<Tuple<string, string>> data) {
            foreach (Tuple<string, string> pair in data)
                InsertData(pair.Item2, pair.Item1);
            Console.WriteLine(tokenMap.ToString());
        }
        /// <summary>
        /// Insert documentID to token's documentIDs List. 
        /// </summary>
        /// <param name="token">
        /// Your token
        /// </param>
        /// <param name="documentID">
        /// Token's documentID.
        /// </param>
        private void InsertData(string token, string documentID) {
            List<string> documentIDs;
            if (tokenMap.TryGetValue(token, out documentIDs))
                documentIDs.Add(documentID);
            else {
                documentIDs = new List<string> { documentID };
                tokenMap.Add(token, documentIDs);
            }
        }
        /// <summary>
        /// Try to get token's documentIDs from InvertedIndex. 
        /// </summary>
        /// <param name="token">
        /// The token that you want its List of documentIDs.
        /// </param>
        /// <param name="output">
        /// Output List of documentIDs.
        /// </param>
        /// <returns>
        /// If invertedIndex contains token "true", otherwise "false".
        /// </returns>
        public bool TryGetTokenDocumentIDs(string token, out List<string> output) {
            if (tokenMap.TryGetValue(token, out output))
                return true;
            return false;
        }
    }
}
