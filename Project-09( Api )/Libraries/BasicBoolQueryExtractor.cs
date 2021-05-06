using Nest;

namespace Libraries {
    public class BasicBoolQueryExtractor : IQueryExtractor {
        private static readonly string separatorsRegex = " ";
        private readonly string queryText;
        private readonly string fieldName;

        /// <param name="queryText">A text containing or( + ), not( - ) & and( ony word ) filters.</param>
        /// <param name="fieldName">The index field that to run queries on it.</param>
        public BasicBoolQueryExtractor(string queryText, string fieldName) {
            this.queryText = queryText;
            this.fieldName = fieldName;
        }

        public QueryContainer ExtractQuery() {
            var must = new MatchSubBoolQuery();
            var mustNot = new MatchSubBoolQuery();
            var should = new MatchSubBoolQuery();
            var words = queryText.Split(separatorsRegex);
            foreach (var word in words) {
                if (!string.IsNullOrWhiteSpace(word)) {
                    switch (word[0]) {
                        case '+': should.AddFilter(word[1..]); break;
                        case '-': mustNot.AddFilter(word[1..]); break;
                        default: must.AddFilter(word); break;
                    }
                }
            }
            return new BoolQuery
            {
                Must = must.GetQuery(fieldName),
                MustNot = mustNot.GetQuery(fieldName),
                Should = should.GetQuery(fieldName)
            };
        }
    }
}
