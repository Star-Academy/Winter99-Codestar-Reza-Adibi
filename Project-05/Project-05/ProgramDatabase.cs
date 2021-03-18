using System;
using System.Collections.Generic;

namespace Project_05 {
    public abstract class ProgramDatabase {
        /// <summary>
        /// Insert list of Tuple(documentID, token) to database.
        /// </summary>
        /// <param name="data"> List of Tuple\<string, string\>( documentID, token ). </param>
        public void InsertDataList(List<Tuple<string, string>> data) {
            foreach (Tuple<string, string> pair in data)
                InsertData(pair.Item2, pair.Item1);
        }
        /// <summary>
        /// Insert documentID to token's documentIDs List. 
        /// </summary>
        /// <param name="token"> Your token. </param>
        /// <param name="documentID"> Token's documentID. </param>
        public abstract void InsertData(string token, string documentID);
        /// <summary>
        /// Try to get token's documentIDs from InvertedIndex. 
        /// </summary>
        /// <param name="token"> The token that you want its List of documentIDs. </param>
        /// <param name="output"> Output List of documentIDs. </param>
        /// <returns> If invertedIndex contains token "true", otherwise "false". </returns>
        public abstract bool TryGetTokenDocumentIDs(string token, out List<string> output);
    }
}
