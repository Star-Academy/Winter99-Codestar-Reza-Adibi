using Nest;
using System;

namespace Libraries {
    public class TextDocumentIndex : ElasticIndex {
        public TextDocumentIndex(string indexName, string connectionAddress) : base(indexName, connectionAddress) { }

        public TextDocumentIndex(string indexName, IElasticClient elasticClient) : base(indexName, elasticClient) { }

        public override void CreateIndex() {
            var response = elasticClient.Indices.Create(
                    IndexName,
                    i => i.
                    Map<TextDocument>(map => map.Properties(properties => properties.
                          Keyword(keyword => keyword.Name(textDocument => textDocument.Path)).
                          Text(text => text.Name(textDocument => textDocument.Text))
                    ))
                );
            var validator = new ElasticResponseValidator(response);
            if (!validator.IsValid) {
                throw new Exception("Create index failed: \n" + validator.DebugInformation);
            }
        }
    }
}
