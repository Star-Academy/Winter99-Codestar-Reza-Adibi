using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries {
    public class SearchQueryMustNot : ElasticSearchQuery {
        public override bool Equals(object obj) {
            return obj is SearchQueryMustNot not &&
                HashSet<string>.CreateSetComparer().Equals(Filters, not.Filters);
        }

        public override int GetHashCode() {
            return HashCode.Combine(this.GetType());
        }
    }
}
