using Xunit;
using System.Collections.Generic;

namespace Libraries.Tests {
    public class ElasticSearchQueryTests {
        private ElasticSearchQuery elasticSearchQuery;

        public ElasticSearchQueryTests() {
            elasticSearchQuery = new SearchQueryMust();
        }

        [Fact()]
        public void AddFilterTest() {
            var expectedResult = new List<string> { "word1" };
            elasticSearchQuery.AddFilter("word1");
            Assert.Equal(expectedResult, elasticSearchQuery.Filters);
        }

        [Fact()]
        public void AddFilterTestMultiSameFilters() {
            var expectedResult = new List<string> { "word2" };
            elasticSearchQuery.AddFilter("word2");
            elasticSearchQuery.AddFilter("word2");
            Assert.Equal(expectedResult, elasticSearchQuery.Filters);
        }

        [Fact()]
        public void AddFiltersTest() {
            var expectedResult = new List<string> { "word3", "word4" };
            elasticSearchQuery.AddFilters(new List<string> { "word3", "word4" });
            Assert.Equal(expectedResult, elasticSearchQuery.Filters);
        }

        [Fact()]
        public void AddFiltersTestMultiSameFilters() {
            var expectedResult = new List<string> { "word3", "word4" };
            elasticSearchQuery.AddFilters(new List<string> { "word3", "word4", "word3", "word4" });
            Assert.Equal(expectedResult, elasticSearchQuery.Filters);
        }

        [Fact()]
        public void ContainsFilterTest() {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void GetQueryTest() {
            Assert.True(false, "This test needs an implementation");
        }
    }
}