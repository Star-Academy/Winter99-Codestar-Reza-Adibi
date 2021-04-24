using Libraries;
using Microsoft.Extensions.Configuration;
using Project_9.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_9.Services {
    public class SearchService : ISearchService {
        public readonly ElasticIndex index;

        public SearchService(ElasticIndex index) {
            this.index = index;
        }

        public void AddToIndex(IEnumerable<Document> items) {
            var textDocuments = ConvertToTextDocument(items);
            index.AddToIndex(textDocuments);
        }

        private IEnumerable<TextDocument> ConvertToTextDocument(IEnumerable<Document> items) {
            return items.Select(item => item.ConvertToTextDocument()).ToList();
        }

        public IEnumerable<Document> Search(string stringQuery) {
            var queryExtractore = new BasicBoolQueryExtractor(stringQuery, "docText");
            var query = queryExtractore.ExtractQuery();
            var searchResult = index.RunSearchQuery(query);
            var resultDocuments = ConvertToDocument((IEnumerable<TextDocument>)searchResult);
            return resultDocuments;
        }

        private IEnumerable<Document> ConvertToDocument(IEnumerable<TextDocument> items) {
            return items.Select(item => new Document(item)).ToList();
        }
    }
}
