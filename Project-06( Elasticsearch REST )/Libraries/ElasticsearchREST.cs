using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Libraries {
    public class ElasticsearchREST {
        private readonly HttpClient httpClient;
        private readonly string elasticsearchAddress;
        private readonly static string indexSettingsAndMapping = @"
                    {  
                        ""settings"": {
                            ""analysis"": {
                                ""analyzer"": {
                                    ""phone_number"": {
                                        ""char_filter"": ""digits_only"", ""tokenizer"": ""keyword"", ""filter"": [ ""ten_digits_min"" ]
                                    }
                                },
                                ""char_filter"": {
                                    ""digits_only"": {
                                        ""type"": ""pattern_replace"", ""pattern"": ""[^\\d]""
                                    }      
                                },
                                ""filter"": { 
                                    ""ten_digits_min"": { 
                                        ""type"": ""length"", ""min"": 10  
                                    }
                                }
                            }
                        },
                        ""mappings"": {
                            ""properties"": {  
                                ""age"": { ""type"": ""integer"" },
                                ""eyeColor"": { ""type"": ""text"" },  
                                ""name"": { ""type"": ""text"", ""fields"": { ""keyword"": { ""type"": ""keyword"", ""ignore_above"": 128 } } },
                                ""gender"": { ""type"": ""boolean"" }, 
                                ""company"": { ""type"": ""text"", ""fields"": { ""keyword"": { ""type"": ""keyword"", ""ignore_above"": 128 } } },
                                ""emaii"": { ""type"": ""keyword"" },
                                ""phone"": { ""type"": ""text"", ""analyzer"": ""phone_number"" },     
                                ""address"": { ""type"": ""text"" },   
                                ""about"": { ""type"": ""text"" }, 
                                ""registration_date"": { ""type"": ""date"", ""format"": ""yyyy/MM/dd HH:mm:ss||yyyy/MM/dd||epoch_millis"" },
                                ""location"": { ""type"": ""geo_point"" }
                                }
                            }
                    }";
        public ElasticsearchREST(string elasticsearchAddress) {
            httpClient = new HttpClient();
            this.elasticsearchAddress = elasticsearchAddress;
        }
        public async Task<bool> CreateIndex(string indexName) {
            var t = Task.Run(() => IndexExists(indexName));
            if (!t.Result) {
                var link = new Uri(elasticsearchAddress + indexName);
                try {
                    var response = await httpClient.PutAsync(
                        link,
                        new StringContent(indexSettingsAndMapping, Encoding.UTF8, "application/json")
                    );
                    response.EnsureSuccessStatusCode();
                    return true;
                }
                catch (HttpRequestException) {
                    return false;
                }
            }
            return true;
        }
        public async Task<bool> IndexExists(string indexName) {
            var link = new Uri(elasticsearchAddress + indexName);
            try {
                var response = await httpClient.GetAsync(link);
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (HttpRequestException) {
                return false;
            }
        }
        public async Task<bool> AddToIndex(string indexName, string content) {
            try {
                await httpClient.PostAsync(
                    elasticsearchAddress + indexName + "/_doc",
                    new StringContent(content, Encoding.UTF8, "application/json")
                );
                return true;
            }
            catch (HttpRequestException) {
                return false;
            }
        }
    }
}
