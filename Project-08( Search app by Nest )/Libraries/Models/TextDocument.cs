﻿using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Libraries {
    public class TextDocument : IIndexItem {
        [JsonPropertyName("path")]
        public string Path { set; get; }
        [JsonPropertyName("doc_text")]
        public string DocText { set; get; }

        IIndexItem IIndexItem.GetFomeFile(string filePath) {
            return TextDocument.GetFomeFile(filePath);
        }

        /// <summary>
        /// Read File and extract index item from it.
        /// </summary>
        /// <param name="filePath">Path tp file.</param>
        /// <returns>Extracted index item.</returns>
        public static TextDocument GetFomeFile(string filePath) {
            var data = FileReader.ReadFromFile(filePath);
            return data == null ? null : new TextDocument { Path = data.Item1, DocText = data.Item2 };
        }

        IEnumerable<IIndexItem> IIndexItem.GetFomeDirectory(string directoryPath) {
            return TextDocument.GetFomeDirectory(directoryPath);
        }

        /// <summary>
        /// Read all Files in path and extract index items from it.
        /// </summary>
        /// <param name="directoryPath">Path to directory containing datafiles.</param>
        /// <returns>List of extracted index items.</returns>
        public static IEnumerable<TextDocument> GetFomeDirectory(string directoryPath) {
            var datas = FileReader.ReadFromDirectory(directoryPath);
            var textDocuments = new List<TextDocument>();
            foreach (var data in datas) {
                textDocuments.Add(new TextDocument { Path = data.Key, DocText = data.Value });
            }
            return textDocuments;
        }

        public override bool Equals(object obj) {
            return obj is TextDocument document &&
                   Path == document.Path &&
                   DocText == document.DocText;
        }

        public override int GetHashCode() {
            return System.HashCode.Combine(Path, DocText);
        }

        public override string ToString() {
            return
                "Path: " + Path +
                "\nText: " + DocText;
        }
    }
}
