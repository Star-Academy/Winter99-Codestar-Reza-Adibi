using Nest;
using System;

namespace Libraries {
    class Program {
        static void Main(string[] args) {
            //var index = new PeopleIndex("people-simple2", "http://127.0.0.1:9200/");
            //index.search();
            var serverUri = new Uri("http://127.0.0.1:9200/");
            var connectionSettings = new ConnectionSettings(serverUri);
            var elasticClient = new ElasticClient(connectionSettings);

            //var response = elasticClient.Cluster.Health();
            //var response = elasticClient.Cat.Nodes();
            var response = elasticClient.Cat.Indices();
            return;
        }
    }
}
