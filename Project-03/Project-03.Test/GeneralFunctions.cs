using Project_03;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Project_03Test {
    [ExcludeFromCodeCoverage]
    public class GeneralFunctions {
        /// <summary>
        /// Initial test InvertedIndex with default values. 
        /// </summary>
        /// <returns>
        /// An InvertedIndex with default values.
        /// </returns>
        public static InvertedIndex InitialInvertedIndex() {
            var invertedIndex = new InvertedIndex();
            invertedIndex.InsertDatas(new List<Tuple<string, string>> {
                new Tuple<string, string>("file1","test"),
                new Tuple<string, string>("file2","test"),
                new Tuple<string, string>("file1","test2"),
                new Tuple<string, string>("file3","test2"),
            });
            return invertedIndex;
        }
    }
}
