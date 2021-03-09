using Project_03;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project_03Test {
    public class GeneralFunctions {
        public static InvertedIndex InitialInvertedIndex() {
            InvertedIndex invertedIndex = new InvertedIndex();
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
