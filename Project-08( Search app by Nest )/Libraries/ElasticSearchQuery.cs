using Nest;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Libraries {
    public abstract class ElasticSearchQuery {
        public HashSet<string> Filters { get; set; }

        protected ElasticSearchQuery() {
            Filters = new HashSet<string>();
        }

        public void AddFilter(string word) {
            Filters.Add(word);
        }

        public void AddFilters(IEnumerable<string> words) {
            Filters.UnionWith(words);
        }

        public void RemoveFilter(string word) {
            Filters.Remove(word);
        }

        public void RemoveFilter(IEnumerable<string> words) {
            Filters.RemoveWhere(word => words.Contains(word));
        }

        public bool ContainsFilter(string word) {
            return Filters.Contains(word);
        }

        public List<QueryContainer> GetQuery() {
            var containers = new List<QueryContainer>();
            foreach (var filter in Filters)
                containers.Add(new MatchQuery { Query = filter });
            return containers;
        }
    }
}
