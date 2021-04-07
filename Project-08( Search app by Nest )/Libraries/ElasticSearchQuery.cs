using Nest;
using System;
using System.Collections.Generic;

namespace Libraries {
    public abstract class ElasticSearchQuery {
        public abstract QueryType Type { get; }
        protected HashSet<string> Words { get; set; }
        public abstract Func<QueryContainerDescriptor<Dictionary<string, object>>, QueryContainer>[] Query { get; }

        public ElasticSearchQuery(QueryType type) {
            Words = new HashSet<string>();
        }

        public void AddWord(string word) {
            Words.Add(word);
        }

        public void AddWords(IEnumerable<string> words) {
            Words.UnionWith(words);
        }

        public override bool Equals(object obj) {
            return obj is ElasticSearchQuery query &&
                   Type == query.Type &&
                   EqualityComparer<HashSet<string>>.Default.Equals(Words, query.Words);
        }

        public override int GetHashCode() {
            return HashCode.Combine(Type);
        }
    }
}
