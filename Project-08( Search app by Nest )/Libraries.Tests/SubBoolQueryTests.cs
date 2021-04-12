using Elasticsearch.Net;
using Nest;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Libraries.Tests {
    [ExcludeFromCodeCoverage]
    public class SubBoolQueryTests {
        private SubBoolQuery subBoolQuery;

        public SubBoolQueryTests() {
            subBoolQuery = new SubBoolQuery();
        }

        [Fact()]
        public void AddFilterTest() {
            var expectedResult = new List<string> { "word1" };
            subBoolQuery.AddFilter("word1");
            Assert.Equal(expectedResult, subBoolQuery.Filters);
        }

        [Fact()]
        public void AddFilterTestMultiSameFilters() {
            var expectedResult = new List<string> { "word2" };
            subBoolQuery.AddFilter("word2");
            subBoolQuery.AddFilter("word2");
            Assert.Equal(expectedResult, subBoolQuery.Filters);
        }

        [Fact()]
        public void AddFiltersTest() {
            var expectedResult = new List<string> { "word3", "word4" };
            subBoolQuery.AddFilters(new List<string> { "word3", "word4" });
            Assert.Equal(expectedResult, subBoolQuery.Filters);
        }

        [Fact()]
        public void AddFiltersTestMultiSameFilters() {
            var expectedResult = new List<string> { "word3", "word4" };
            subBoolQuery.AddFilters(new List<string> { "word3", "word4", "word3", "word4" });
            Assert.Equal(expectedResult, subBoolQuery.Filters);
        }

        [Fact()]
        public void ContainsFilterTest() {
            var word = "word5";
            subBoolQuery.Filters.Add(word);
            Assert.True(subBoolQuery.ContainsFilter(word));
        }

        [Fact()]
        public void ContainsFilterTestDoesNotContainsWord() {
            Assert.False(subBoolQuery.ContainsFilter("word6"));
        }

        [Fact()]
        public void RemoveFilterTest() {
            subBoolQuery.Filters.Add("word7");
            subBoolQuery.RemoveFilter("word7");
            Assert.Equal(new HashSet<string>(), subBoolQuery.Filters);
        }

        [Fact()]
        public void RemoveFiltersTest() {
            subBoolQuery.Filters.Add("word8");
            subBoolQuery.Filters.Add("word9");
            subBoolQuery.RemoveFilters(new string[] { "word8", "word9" });
            Assert.Equal(new HashSet<string>(), subBoolQuery.Filters);
        }

        [Fact()]
        public void GetQueryTestSingleFilter() {
            var expectedResult = "{\"match\":{\"test_field\":{\"query\":\"word10\"}}}";
            subBoolQuery.AddFilters(new List<string> { "word10" });
            var query = subBoolQuery.GetQuery("test_field");
            var client = new ElasticClient();
            var testResult = "";
            foreach (var item in query)
                testResult += client.RequestResponseSerializer.SerializeToString<QueryContainer>(item);
            Assert.Equal(expectedResult, testResult);
        }

        [Fact()]
        public void GetQueryTestMultiFilter() {
            var expectedResult =
                "{\"match\":{\"test_field\":{\"query\":\"word11\"}}}" +
                "{\"match\":{\"test_field\":{\"query\":\"word12\"}}}";
            subBoolQuery.AddFilters(new List<string> { "word11", "word12" });
            var query = subBoolQuery.GetQuery("test_field");
            var client = new ElasticClient();
            var testResult = "";
            foreach (var item in query)
                testResult += client.RequestResponseSerializer.SerializeToString<QueryContainer>(item);
            Assert.Equal(expectedResult, testResult);
        }
    }
}