using System;
using System.Collections.Generic;

namespace Project_05 {
    public interface IProgramDatabase {
        /// <summary>
        /// Insert list of Tuple(documentID, token) to database.
        /// </summary>
        /// <param name="data"> List of Tuple\<string, string\>( documentID, token ). </param>
        public void InsertDataList(List<Tuple<string, string>> data);
        /// <summary>
        /// Insert documentID to token's documentIDs List. 
        /// </summary>
        /// <param name="token"> Your token. </param>
        /// <param name="documentID"> Token's documentID. </param>
        public void InsertData(string token, string documentID);
        /// <summary>
        /// Try to get token's documentIDs from InvertedIndex. 
        /// </summary>
        /// <param name="token"> The token that you want its List of documentIDs. </param>
        /// <param name="output"> Output List of documentIDs. </param>
        /// <returns> If invertedIndex contains token "true", otherwise "false". </returns>
        public bool TryGetTokenDocumentIDs(string token, out List<string> output);
    }
    public enum DbmsName { SqlServer, Sqlite, Memory }
}
