using Nest;
using System;
using System.Collections.Generic;

namespace Libraries {
    public abstract class ElasticIndex {
        protected IElasticClient elasticClient;
        public string IndexName { get; }

        public ElasticIndex(string indexName, string connectionAddress) {
            IndexName = indexName;
            var serverUri = new Uri(connectionAddress);
            var connectionSettings = new ConnectionSettings(serverUri);
            elasticClient = new ElasticClient(connectionSettings);
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
            var validator = new ElasticResponseValidator(response);
            if (!validator.IsValid) {
                throw new Exception("Ping failed:\n" + validator.DebugInformation);
            }
        }

        /// <summary>
        /// Create index using index mapping and settings.<br/>
        /// Throws exception if creating index failed.
        /// </summary>
        public void CreateIndex() {
            var response = elasticClient.Indices.Create(
                GetCreateIndexDescriptor()
            );
            var validator = new ElasticResponseValidator(response);
            if (!validator.IsValid) {
                throw new Exception("Create index failed: \n" + validator.DebugInformation);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private CreateIndexDescriptor GetCreateIndexDescriptor() {
            return new CreateIndexDescriptor(IndexName)
                .Map(map => GetTypeMappingDescriptor())
                .Settings(GetIndexSettingsDescriptor);
        }

        /// <summary>
        /// Get settings of this index.
        /// </summary>
        /// <returns>Settings of this elastic index for creating index.</returns>
        protected IPromise<IIndexSettings> GetIndexSettingsDescriptor(IndexSettingsDescriptor settingsDescriptor) {
            return settingsDescriptor;
        }

        /// <summary>
        /// Get mappings of this index.
        /// </summary>
        /// <returns>Mappings of this elastic index for creating index.</returns>
        protected abstract ITypeMapping GetTypeMappingDescriptor();

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
            var validator = new ElasticResponseValidator(response);
            if (!validator.IsValid) {
                throw new Exception("Add failed:\n" + validator.DebugInformation);
            }
        }

        public abstract IEnumerable<IIndexItem> RunSearchQuery(QueryContainer query);

    }
}
