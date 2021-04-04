using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries {
    class ElasticIndex {
        private ElasticClient elasticClient;
        public string IndexName { get; }

        public ElasticIndex(string indexName, string connectionAddress) {
            IndexName = indexName;
            var serverUri = new Uri(connectionAddress);
            var connectionSettings = new ConnectionSettings(serverUri);
            elasticClient = new ElasticClient(connectionSettings);
            var response = elasticClient.Ping();
        }

        public ElasticIndex(string indexName, ElasticClient elasticClient) {
            IndexName = indexName;
            this.elasticClient = elasticClient;
            var response = elasticClient.Ping();
        }


    }
}
