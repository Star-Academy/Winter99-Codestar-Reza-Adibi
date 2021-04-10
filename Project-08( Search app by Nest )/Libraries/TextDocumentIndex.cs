﻿using Nest;
using System.Collections.Generic;

namespace Libraries {
    public class TextDocumentIndex : ElasticIndex {
        public TextDocumentIndex(string indexName, string connectionAddress) : base(indexName, connectionAddress) { }

        public TextDocumentIndex(string indexName, IElasticClient elasticClient) : base(indexName, elasticClient) { }

        protected override ITypeMapping GetTypeMappingDescriptor() {
            return new TypeMappingDescriptor<TextDocument>()
                .Properties(properties => properties
                    .Keyword(keyword => keyword.Name(textDocument => textDocument.Path))
                    .Text(text => text.Name(textDocument => textDocument.Text))
                );
        }

        public override IEnumerable<IIndexItem> RunSearchQuery(QueryContainer query) {
            var response = elasticClient.Search<TextDocument>(s => s
                  .Index(IndexName)
                  .Query(q => query)
            );
            return response.Documents;
        }
    }
}
