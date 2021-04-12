using Nest;
using System;
using System.Collections.Generic;

namespace Libraries {
    public abstract class ElasticIndex {
        public IElasticClient elasticClient;
        public string IndexName { get; }

        public ElasticIndex(string indexName, string connectionAddress) {
            IndexName = indexName;
            var serverUri = new Uri(connectionAddress);
            var connectionSettings = new ConnectionSettings(serverUri);
            elasticClient = new ElasticClient(connectionSettings);
            Setup();
        }

        public ElasticIndex(string indexName, IElasticClient elasticClient) {
            IndexName = indexName;
            this.elasticClient = elasticClient;
            Setup();
        }

        private void Setup() {
            CheckConnection();
            if (!IndexExists())
                CreateIndex();
        }

        /// <summary>
        /// Ping server.<br/>
        /// Throws exeption if ping failed.
        /// </summary>
        private void CheckConnection() {
            var response = elasticClient.Ping();
            ElasticResponseValidator.Validate(response);
        }

        private bool IndexExists() {
            var response = elasticClient.Indices.Exists(IndexName);
            try {
                ElasticResponseValidator.Validate(response);
                return true;
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        /// <summary>
        /// Create index using index mapping and settings.<br/>
        /// Throws exception if creating index failed.
        /// </summary>
        private void CreateIndex() {
            var response = elasticClient.Indices.Create(
                GetCreateIndexDescriptor()
            );
            ElasticResponseValidator.Validate(response);
        }

        /// <summary>
        /// Get create index discriptor to create this index.
        /// </summary>
        /// <returns>The CreateIndexDescriptor needed to create this index.</returns>
        public CreateIndexDescriptor GetCreateIndexDescriptor() {
            return new CreateIndexDescriptor(IndexName)
                .Map(map => GetTypeMappingDescriptor());
        }

        /// <summary>
        /// Get mappings of this index.
        /// </summary>
        /// <returns>Mappings of this elastic index for creating index.</returns>
        protected abstract ITypeMapping GetTypeMappingDescriptor();

        /// <summary>
        /// Add document to index using Bulk.<br/>
        /// Throws exception if inserting failed.
        /// </summary>
        /// <param name="items">An instance of IIndexItems. </param>
        public void AddToIndex(IIndexItem item) {
            BulkDescriptor bulkDescriptor = GetBulkDescriptor(new List<IIndexItem> { item });
            var response = elasticClient.Bulk(bulkDescriptor);
            ElasticResponseValidator.Validate(response);
            RefreshIndex();
        }

        /// <summary>
        /// Add documents to index using Bulk.<br/>
        /// Throws exception if inserting failed.
        /// </summary>
        /// <param name="items">An enumerable of IndexItems. </param>
        public void AddToIndex(IEnumerable<IIndexItem> items) {
            BulkDescriptor bulkDescriptor = GetBulkDescriptor(items);
            var response = elasticClient.Bulk(bulkDescriptor);
            ElasticResponseValidator.Validate(response);
            RefreshIndex();
        }

        /// <summary>
        /// Create BulkDescriptor to add given items to index.
        /// </summary>
        /// <param name="items">List of IIndexItems.</param>
        /// <returns>BulkDescriptor created to add given items to index</returns>
        public BulkDescriptor GetBulkDescriptor(IEnumerable<IIndexItem> items) {
            var bulkDescriptor = new BulkDescriptor();
            foreach (var item in items) {
                bulkDescriptor.Index<IIndexItem>(x => x
                    .Index(IndexName)
                    .Document(item)
                );
            }
            return bulkDescriptor;
        }

        /// <summary>
        /// Refresh this index shardes.
        /// </summary>
        protected void RefreshIndex() {
            var response = elasticClient.Indices.Refresh(IndexName);
            ElasticResponseValidator.Validate(response);
        }

        /// <summary>
        /// Run given query on this index.
        /// </summary>
        /// <param name="query">A search query.</param>
        /// <returns>Query result.</returns>
        public abstract IEnumerable<IIndexItem> RunSearchQuery(QueryContainer query);
    }
}