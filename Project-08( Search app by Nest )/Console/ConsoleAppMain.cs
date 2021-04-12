using Libraries;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp {
    class ConsoleAppMain {
        public readonly static string defaultServerUri = "http://127.0.0.1:9200/";
        public ElasticIndex index;
        public static void Main(string[] args) {
            try {
                Console.WriteLine("Hello dear user!");
                var app = new ConsoleAppMain();
                var indexName = app.GetIndexName();
                var serverUri = app.GetServerUri();
                app.SetIndex(indexName, serverUri);
                app.AddingToIndexRoutine();
                app.SearchingRoutine();
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }

        private string GetServerUri() {
            var input = "";
            do {
                Console.WriteLine("Write your server uri or 'd' to use default:");
                input = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(input));
            if (input == "d")
                return defaultServerUri;
            return @input;
        }

        private string GetIndexName() {
            var invalidChars = new List<char> { '\\', '/', '*', '?', '"', '<', '>', '|', ' ', ',' };
            var input = "";
            do {
                Console.WriteLine("Write your index name:");
                input = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(input) || invalidChars.Any(c => input.Contains(c)));
            return input;
        }

        private void SetIndex(string indexName, string serverUri) {
            index = new ElasticIndexTextDocumentIndex(indexName, serverUri);
        }

        private void AddingToIndexRoutine() {
            var doNotExit = "";
            do {
                var userChoise = "";
                do {
                    Console.WriteLine("Write 'f' to add single file, 'd' to add folder of files to index or 's' to skip addeing:");
                    userChoise = Console.ReadLine();
                } while (userChoise != "f" && userChoise != "d" && userChoise != "s");
                if (userChoise == "s")
                    return;
                string path = GetPath(userChoise);
                switch (userChoise) {
                    case "f": {
                            var item = TextDocument.GetFomeFile(path);
                            index.AddToIndex(item);
                            break;
                        }
                    case "d": {
                            var items = TextDocument.GetFomeDirectory(path);
                            index.AddToIndex(items);
                            break;
                        }
                }
                Console.WriteLine("Do you like to add more data?( y / anything else ):");
                doNotExit = Console.ReadLine();
            } while (doNotExit == "y");
        }

        private static string GetPath(string userChoise) {
            var path = "";
            do {
                Console.WriteLine("Write path to " + (userChoise == "f" ? "file" : "folder") + ":");
                path = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(path));
            return path;
        }

        private void SearchingRoutine() {
            var doNotExit = "";
            do {
                var userQuery = "";
                do {
                    Console.WriteLine("Write your search query( '+' before filter means 'or', '-' before filter means 'not' and none of this signs before filter means 'and' ) or 'e' to exit:");
                    userQuery = Console.ReadLine();
                } while (string.IsNullOrWhiteSpace(userQuery));
                if (userQuery == "e")
                    return;
                var query = QueryExtractor.ExtractBoolQuery(userQuery, "docText");
                var searchResult = index.RunSearchQuery(query);
                ShowSearchResult(searchResult);
                Console.WriteLine("Do you like to continue searching?( y / anything else ):");
                doNotExit = Console.ReadLine();
            } while (doNotExit == "y");
        }

        private void ShowSearchResult(IEnumerable<IIndexItem> searchResult) {
            if (searchResult == null || searchResult.Count() == 0) {
                Console.WriteLine("No result!");
                return;
            }
            foreach (var item in searchResult) {
                Console.WriteLine(item.ToString());
            }
        }
    }
}
