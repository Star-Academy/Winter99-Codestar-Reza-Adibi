﻿using Nest;

namespace Libraries {
    public interface IQueryExtractor {
        /// <summary>
        /// Extract a query from class query text on class field.
        /// </summary>
        /// <returns>An elasticSearch query.</returns>
        public QueryContainer ExtractQuery();
    }
}
