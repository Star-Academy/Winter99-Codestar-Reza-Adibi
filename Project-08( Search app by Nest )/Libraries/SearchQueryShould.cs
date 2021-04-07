using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries {
    public class SearchQueryShould : ElasticSearchQuery {
        public override bool Equals(object obj) {
            return obj is SearchQueryShould should &&
                   HashSet<string>.CreateSetComparer().Equals(Filters, should.Filters);
        }

        public override int GetHashCode() {
            return HashCode.Combine(this.GetType());
        }
    }
}
