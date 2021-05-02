using Nest;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Libraries {
    public class MatchSubBoolQuery : IQueryBuilder {
        public HashSet<string> Filters { get; set; }

        public MatchSubBoolQuery() {
            Filters = new HashSet<string>();
        }

        public void AddFilter(string filter) {
            Filters.Add(filter);
        }

        public void AddFilters(IEnumerable<string> filters) {
            Filters.UnionWith(filters);
        }

        public void RemoveFilter(string filter) {
            Filters.Remove(filter);
        }

        public void RemoveFilters(IEnumerable<string> filters) {
            Filters.RemoveWhere(word => filters.Contains(word));
        }

        public bool ContainsFilter(string filter) {
            return Filters.Contains(filter);
        }

        public IEnumerable<QueryContainer> GetQuery(string fieldName) {
            var query = Filters.Select<string, QueryContainer>(
                filter =>
                new MatchQuery
                {
                    Field = fieldName,
                    Query = filter
                }
            );
            return query;
        }
    }
}
