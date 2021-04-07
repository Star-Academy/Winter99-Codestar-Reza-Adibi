using Xunit;
using Libraries;
using System;
using System.Collections.Generic;
using System.Text;
using Nest;

namespace Libraries.Tests {
    public class SearchQueryMustTests {
        private SearchQueryMust mustQuery;

        public SearchQueryMustTests() {
            mustQuery = new SearchQueryMust();
        }

        [Fact()]
        public void EqualsTestMust() {
            var query = new SearchQueryMust();
            query.AddFilter("filter");
            mustQuery.AddFilter("filter");
            Assert.True(mustQuery.Equals(query));
        }

        [Fact()]
        public void EqualsTestMustWithDiffrentFilters() {
            var query = new SearchQueryMust();
            query.AddFilter("filter");
            Assert.False(mustQuery.Equals(query));
        }

        [Fact()]
        public void EqualsTestMustNot() {
            var query = new SearchQueryMustNot();
            Assert.False(mustQuery.Equals(query));
        }

        [Fact()]
        public void EqualsTestShould() {
            var query = new SearchQueryShould();
            Assert.False(mustQuery.Equals(query));
        }

        [Fact()]
        public void GetHashCodeTest() {
            var expectedResult = HashCode.Combine(typeof(SearchQueryMust));
            Assert.Equal(expectedResult, mustQuery.GetHashCode());
        }
    }
}