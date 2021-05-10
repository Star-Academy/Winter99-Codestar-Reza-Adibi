using Libraries;
using Microsoft.Extensions.Configuration;

namespace Project_9.Models {
    public class SearchIndex : ElasticIndexTextDocumentIndex {
        private readonly IConfiguration configuration;

        public SearchIndex(IConfiguration configuration) : base(configuration["ElasticSearch:IndexName"], configuration["ElasticSearch:ServerUri"]) {
            this.configuration = configuration;
        }
    }
}
