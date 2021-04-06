using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries {
    public abstract class ElasticIndex {
        protected IElasticClient elasticClient;
        public string IndexName { get; }

        public ElasticIndex(string indexName, string connectionAddress) {
            IndexName = indexName;
            var serverUri = new Uri(connectionAddress);
            var connectionSettings = new ConnectionSettings(serverUri);
            elasticClient = new ElasticClient(connectionSettings);
            CheckConnection();
        }

        public ElasticIndex(string indexName, IElasticClient elasticClient) {
            IndexName = indexName;
            this.elasticClient = elasticClient;
            CheckConnection();
        }

        /// <summary>
        /// Ping server.<br/>
        /// Throws exeption if ping failed.
        /// </summary>
        protected void CheckConnection() {
            var response = elasticClient.Ping();
            if (!response.IsValid) {
                throw new Exception("Ping failed:\n" + response.DebugInformation);
            }
        }

        /// <summary>
        /// Create index using index mapping and settings.<br/>
        /// Throws exception if creating index failed.
        /// </summary>
        public abstract void CreateIndex();

        /// <summary>
        /// Add documents to index using Bulk.<br/>
        /// Throws exception if inserting failed.
        /// </summary>
        /// <param name="items">An enumerable of IndexItems. </param>
        public void AddToIndex(IEnumerable<IIndexItem> items) {
            var bulkDescriptor = new BulkDescriptor();
            foreach (var item in items) {
                bulkDescriptor.Index<IIndexItem>(x => x.
                    Index(this.IndexName).
                    Document(item)
                );
            }
            var response = elasticClient.Bulk(bulkDescriptor);
            if (!response.IsValid) {
                throw new Exception("Add failed:\n" + response.DebugInformation);
            }
        }
    }
}
