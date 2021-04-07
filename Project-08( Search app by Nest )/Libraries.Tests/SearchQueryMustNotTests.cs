using Xunit;
using System;

namespace Libraries.Tests {
    public class SearchQueryMustNotTests {
        private SearchQueryMustNot mustNotQuery;

        public SearchQueryMustNotTests() {
            mustNotQuery = new SearchQueryMustNot();
        }

        [Fact()]
        public void EqualsTestNotMust() {
            var query = new SearchQueryMustNot();
            query.AddFilter("filter");
            mustNotQuery.AddFilter("filter");
            Assert.True(mustNotQuery.Equals(query));
        }

        [Fact()]
        public void EqualsTestMustNotWithDiffrentFilters() {
            var query = new SearchQueryMustNot();
            query.AddFilter("filter");
            Assert.False(mustNotQuery.Equals(query));
        }

        [Fact()]
        public void EqualsTestMust() {
            var query = new SearchQueryMust();
            Assert.False(mustNotQuery.Equals(query));
        }

        [Fact()]
        public void EqualsTestShould() {
            var query = new SearchQueryShould();
            Assert.False(mustNotQuery.Equals(query));
        }

        [Fact()]
        public void GetHashCodeTest() {
            var expectedResult = HashCode.Combine(typeof(SearchQueryMustNot));
            Assert.Equal(expectedResult, mustNotQuery.GetHashCode());
        }
    }
}