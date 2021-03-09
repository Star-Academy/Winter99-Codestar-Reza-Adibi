using System;
using System.Collections.Generic;
using System.Text;

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
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="documentID"></param>
        private void InsertData(string token, string documentID) {
            List<string> documentIDs;
            if (tokenMap.TryGetValue(token, out documentIDs))
                documentIDs.Add(documentID);
            else {
                documentIDs = new List<string> { documentID };
                tokenMap.Add(token, documentIDs);
            }
        }
    }
}
