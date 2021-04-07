using Xunit;
using Libraries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Tests {
    public class SearchQueryShouldTests {
        private SearchQueryShould shouldQuery;

        public SearchQueryShouldTests() {
            shouldQuery = new SearchQueryShould();
        }

        [Fact()]
        public void EqualsTestShould() {
            var query = new SearchQueryShould();
            query.AddFilter("filter");
            shouldQuery.AddFilter("filter");
            Assert.True(shouldQuery.Equals(query));
        }

        [Fact()]
        public void EqualsTestShouldWithDiffrentFilters() {
            var query = new SearchQueryShould();
            query.AddFilter("filter");
            Assert.False(shouldQuery.Equals(query));
        }

        [Fact()]
        public void EqualsTestMust() {
            var query = new SearchQueryMust();
            Assert.False(shouldQuery.Equals(query));
        }

        [Fact()]
        public void EqualsTestMustNot() {
            var query = new SearchQueryMustNot();
            Assert.False(shouldQuery.Equals(query));
        }

        [Fact()]
        public void GetHashCodeTest() {
            var expectedResult = HashCode.Combine(typeof(SearchQueryShould));
            Assert.Equal(expectedResult, shouldQuery.GetHashCode());
        }
    }
}