using System.Collections.Generic;

namespace Libraries {
    public class TextDocument : IIndexItem {
        public string Path { set; get; }
        public string Text { set; get; }

        IIndexItem IIndexItem.GetFomeFile(string filePath) {
            return TextDocument.GetFomeFile(filePath);
        }

        public static TextDocument GetFomeFile(string filePath) {
            var data = FileReader.ReadFromFile(filePath);
            return data == null ? null : new TextDocument { Path = data.Item1, Text = data.Item2 };
        }

        IEnumerable<IIndexItem> IIndexItem.GetFomeDirectory(string directoryPath) {
            return TextDocument.GetFomeDirectory(directoryPath);
        }

        public static IEnumerable<TextDocument> GetFomeDirectory(string directoryPath) {
            var datas = FileReader.ReadFromDirectory(directoryPath);
            var textDocuments = new List<TextDocument>();
            foreach (var data in datas) {
                textDocuments.Add(new TextDocument { Path = data.Key, Text = data.Value });
            }
            return textDocuments.Count == 0 ? null : textDocuments;
        }
    }
}
