using Libraries;

namespace Project_9.Models {
    public class Document {
        public string Id { get; set; }
        public string Text { get; set; }

        public Document() { }

        public Document(TextDocument textDocument) {
            Id = textDocument.Path;
            Text = textDocument.DocText;
        }

        /// <summary>
        /// Convert this object to elastic model TextDocument.
        /// </summary>
        /// <returns>New TextDocument{ DocText = Text, Path = Id }.</returns>
        public TextDocument ConvertToTextDocument() {
            return new TextDocument
            {
                DocText = Text,
                Path = Id
            };
        }
    }
}
