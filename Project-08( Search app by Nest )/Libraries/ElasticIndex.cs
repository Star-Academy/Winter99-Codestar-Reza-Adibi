using Nest;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Libraries {
    public abstract class ElasticIndex {
        public IElasticClient elasticClient;
        List<char> invalidChars = new List<char> { '\\', '/', '*', '?', '"', '<', '>', '|', ' ', ',' };
        public string IndexName { get; }


        public ElasticIndex(string indexName, string connectionAddress) {
            ValidateIndexName(indexName);
            IndexName = indexName;
            var serverUri = new Uri(connectionAddress);
            var connectionSettings = new ConnectionSettings(serverUri);
            elasticClient = new ElasticClient(connectionSettings);
            Setup();
        }

        public ElasticIndex(string indexName, IElasticClient elasticClient) {
            ValidateIndexName(indexName);
            IndexName = indexName;
            this.elasticClient = elasticClient;
            Setup();
        }

        /// <summary>
        /// Validate index name and if index name was invalid throw exeption.
        /// </summary>
        /// <param name="indexName">Name of your index.</param>
        private void ValidateIndexName(string indexName) {
            if (invalidChars.Any(c => indexName.Contains(c)))
                throw new Exception("Invalid index name!");

        }

        /// <summary>
        /// Check connection an make sure of index existence.
        /// </summary>
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
            var response = elasticClient.Ping().Validate();
        }

        private bool IndexExists() {
            try {
                var response = elasticClient.Indices.Exists(IndexName).Validate();
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
            ).Validate();
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
        public void AddToIndex(IModel item) {
            BulkDescriptor bulkDescriptor = GetBulkDescriptor(new List<IModel> { item });
            var response = elasticClient.Bulk(bulkDescriptor).Validate();
            RefreshIndex();
        }

        /// <summary>
        /// Add documents to index using Bulk.<br/>
        /// Throws exception if inserting failed.
        /// </summary>
        /// <param name="items">An enumerable of IndexItems. </param>
        public void AddToIndex(IEnumerable<IModel> items) {
            BulkDescriptor bulkDescriptor = GetBulkDescriptor(items);
            var response = elasticClient.Bulk(bulkDescriptor).Validate();
            RefreshIndex();
        }

        /// <summary>
        /// Create BulkDescriptor to add given items to index.
        /// </summary>
        /// <param name="items">List of IIndexItems.</param>
        /// <returns>BulkDescriptor created to add given items to index</returns>
        public BulkDescriptor GetBulkDescriptor(IEnumerable<IModel> items) {
            var bulkDescriptor = new BulkDescriptor();
            foreach (var item in items) {
                bulkDescriptor.Index<IModel>(x => x
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
            var response = elasticClient.Indices.Refresh(IndexName).Validate();
        }

        /// <summary>
        /// Run given query on this index.
        /// </summary>
        /// <param name="query">A search query.</param>
        /// <returns>Query result.</returns>
        public abstract IEnumerable<IModel> RunSearchQuery(QueryContainer query);
    }
}