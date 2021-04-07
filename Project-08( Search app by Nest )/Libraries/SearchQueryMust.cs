using Nest;
using System;
using System.Collections.Generic;

namespace Libraries {
    public class SearchQueryMust : ElasticSearchQuery {
        public override bool Equals(object obj) {
            return
                obj is SearchQueryMust must &&
                   HashSet<string>.CreateSetComparer().Equals(Filters, must.Filters);
        }

        public override int GetHashCode() {
            return HashCode.Combine(this.GetType());
        }
    }
}
