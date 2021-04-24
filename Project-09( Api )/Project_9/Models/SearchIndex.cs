using Libraries;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_9.Models {
    public class SearchIndex : ElasticIndexTextDocumentIndex {
        private readonly IConfiguration configuration;

        public SearchIndex(IConfiguration configuration) : base(configuration["ElasticSearch:IndexName"], configuration["ElasticSearch:ServerUri"]) {
            this.configuration = configuration;
        }
    }
}
