using Libraries;
using System;

namespace ConsoleApp {
    class ConsoleAppMain {
        public static void Main(string[] args) {
            var testFilePath = @"E:\Programing\CodeStarWinter99\Project-08( Search app by Nest )\TestData\data\sample";
            try {
                var t = TextDocument.GetFomeFile(testFilePath);
                var index = new ElasticIndexTextDocumentIndex("test2", "http://127.0.0.1:9200/");
                //index.AddToIndex(t);

            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }
    }
}
