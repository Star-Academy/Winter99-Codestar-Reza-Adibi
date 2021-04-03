using System;
using System.Threading.Tasks;

namespace Libraries {
    class Program {
        private static readonly string link = "http://localhost:9200/";
        static void Main(string[] args) {
            var indexName = "test1";
            var elasticsearch = new ElasticsearchREST(link);
            var createTask = Task.Run(() => elasticsearch.CreateIndex(indexName));
            Console.WriteLine("createTask.Result" + createTask.Result.ToString());
            var existsTask = Task.Run(() => elasticsearch.IndexExists(indexName));
            Console.WriteLine("existsTask.Result" + existsTask.Result.ToString());
            var person =
                @"{ 
                ""age"": 26,    
                ""eyeColor"": ""blue"",
                ""name"": ""Sykes Rivera"", 
                ""gender"": true,
                ""company"": ""NETPLAX"",    
                ""email"": ""sykesrivera@netplax.com"",
                ""phone"": ""+1 (946) 596-2890"",
                ""address"": ""486 Apollo Street, Blodgett, Northern Mariana Islands, 1875"",
                ""about"": ""In laboris in anim eiusmod consectetur elit. Ut incididunt eiusmod incididunt anim incididunt laborum minim qui consequat incididunt ut nisi fugiat. Fugiat ipsum consectetur adipisicing enim amet.\r\n"",   
                ""registration_date"": ""2015/02/16 11:11:31"",    
                ""location"": ""-21.459832,-148.208613""
                }";
            var addTask = Task.Run(() => elasticsearch.AddToIndex(indexName, person));
            Console.WriteLine("addTask.Result" + addTask.Result.ToString());
        }
    }
}
