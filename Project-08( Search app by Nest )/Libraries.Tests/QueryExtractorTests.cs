using Elasticsearch.Net;
using Nest;
using Xunit;

namespace Libraries.Tests {
    public class QueryExtractorTests {
        private readonly static string fieldName = "test_field";

        [Fact()]
        public void ExtractBoolQueryTestSingleMust() {
            var expectedResult =
                "{\"bool\":" +
                "{\"must\":[" +
                "{\"match\":{\"" + fieldName + "\":{\"query\":\"test\"}}}" +
                "]}" +
                "}";
            var query = QueryExtractor.ExtractBoolQuery("test", fieldName);
            var client = new ElasticClient();
            var testResult = client.RequestResponseSerializer.SerializeToString<QueryContainer>(query);
            Assert.Equal(expectedResult, testResult);
        }

        [Fact()]
        public void ExtractBoolQueryTestMultiMust() {
            var expectedResult =
                "{\"bool\":" +
                "{\"must\":[" +
                "{\"match\":{\"" + fieldName + "\":{\"query\":\"test\"}}}," +
                "{\"match\":{\"" + fieldName + "\":{\"query\":\"test2\"}}}" +
                "]}" +
                "}";
            var query = QueryExtractor.ExtractBoolQuery("test test2", fieldName);
            var client = new ElasticClient();
            var testResult = client.RequestResponseSerializer.SerializeToString<QueryContainer>(query);
            Assert.Equal(expectedResult, testResult);
        }

        [Fact()]
        public void ExtractBoolQueryTestSingleMustNot() {
            var expectedResult =
                "{\"bool\":" +
                "{\"must_not\":[" +
                "{\"match\":{\"" + fieldName + "\":{\"query\":\"test\"}}}" +
                "]}" +
                "}";
            var query = QueryExtractor.ExtractBoolQuery("-test", fieldName);
            var client = new ElasticClient();
            var testResult = client.RequestResponseSerializer.SerializeToString<QueryContainer>(query);
            Assert.Equal(expectedResult, testResult);
        }

        [Fact()]
        public void ExtractBoolQueryTestMultiMustNot() {
            var expectedResult =
                "{\"bool\":" +
                "{\"must_not\":[" +
                "{\"match\":{\"" + fieldName + "\":{\"query\":\"test\"}}}," +
                "{\"match\":{\"" + fieldName + "\":{\"query\":\"test2\"}}}" +
                "]}" +
                "}";
            var query = QueryExtractor.ExtractBoolQuery("-test -test2", fieldName);
            var client = new ElasticClient();
            var testResult = client.RequestResponseSerializer.SerializeToString<QueryContainer>(query);
            Assert.Equal(expectedResult, testResult);
        }

        [Fact()]
        public void ExtractBoolQueryTestSingleShould() {
            var expectedResult =
                "{\"bool\":" +
                "{\"should\":[" +
                "{\"match\":{\"" + fieldName + "\":{\"query\":\"test\"}}}" +
                "]}" +
                "}";
            var query = QueryExtractor.ExtractBoolQuery("+test", fieldName);
            var client = new ElasticClient();
            var testResult = client.RequestResponseSerializer.SerializeToString<QueryContainer>(query);
            Assert.Equal(expectedResult, testResult);
        }

        [Fact()]
        public void ExtractBoolQueryTestMultiShould() {
            var expectedResult =
                "{\"bool\":" +
                "{\"should\":[" +
                "{\"match\":{\"" + fieldName + "\":{\"query\":\"test\"}}}," +
                "{\"match\":{\"" + fieldName + "\":{\"query\":\"test2\"}}}" +
                "]}" +
                "}";
            var query = QueryExtractor.ExtractBoolQuery("+test +test2", fieldName);
            var client = new ElasticClient();
            var testResult = client.RequestResponseSerializer.SerializeToString<QueryContainer>(query);
            Assert.Equal(expectedResult, testResult);
        }

        [Fact()]
        public void ExtractBoolQueryTestMultiTypes() {
            var expectedResult =
                "{\"bool\":" +
                "{\"must\":[" +
                "{\"match\":{\"" + fieldName + "\":{\"query\":\"test\"}}}" +
                "]," +
                "\"must_not\":[" +
                "{\"match\":{\"" + fieldName + "\":{\"query\":\"test3\"}}}" +
                "]," +
                "\"should\":[" +
                "{\"match\":{\"" + fieldName + "\":{\"query\":\"test2\"}}}" +
                "]" +
                "}}";
            var query = QueryExtractor.ExtractBoolQuery("test +test2 -test3", fieldName);
            var client = new ElasticClient();
            var testResult = client.RequestResponseSerializer.SerializeToString<QueryContainer>(query);
            Assert.Equal(expectedResult, testResult);
        }
    }
}