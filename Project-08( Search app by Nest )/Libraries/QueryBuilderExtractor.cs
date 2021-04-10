using Nest;

namespace Libraries {
    public class QueryBuilderExtractor {
        private static readonly string separatorsRegex = " ";

        /// <summary>
        /// Extract a bool query from given query text on given field.
        /// </summary>
        /// <param name="queryText">A text containing or( + ), not( - ) & and( ony word ) filters.</param>
        /// <param name="fieldName">The index field that to run queries on it.</param>
        /// <returns>An elasticSearch bool query.</returns>
        public static QueryContainer ExtractBoolQuery(string queryText, string fieldName) {
            var must = new SubBoolQuery();
            var mustNot = new SubBoolQuery();
            var should = new SubBoolQuery();
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
            return new BoolQuery {
                Must = must.GetQuery(fieldName),
                MustNot = mustNot.GetQuery(fieldName),
                Should = should.GetQuery(fieldName)
            };
        }
    }
}
