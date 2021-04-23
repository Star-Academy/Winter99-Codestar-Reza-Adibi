using Nest;
using System.Collections.Generic;

namespace Libraries {
    public interface IQueryBuilder {

        /// <summary>
        /// Add given filter to list of filters.
        /// </summary>
        /// <param name="filter">A filter to be added to filters list.</param>
        public void AddFilter(string filter);

        /// <summary>
        /// Add given filters to list of filters.
        /// </summary>
        /// <param name="filters">List of filters to be added to filters list.</param>
        public void AddFilters(IEnumerable<string> filters);

        /// <summary>
        /// Remove given filter from list of filters.
        /// </summary>
        /// <param name="filter">A filter to be removed from filters list.</param>
        public void RemoveFilter(string filter);

        /// <summary>
        /// Remove given filters from list of filters.
        /// </summary>
        /// <param name="filters">List of filters to be removed from filters list.</param>
        public void RemoveFilters(IEnumerable<string> filters);

        /// <summary>
        /// Check if filters list contans given filter. 
        /// </summary>
        /// <param name="filter">The filter that we want to know if filters list contans it.</param>
        /// <returns>If filters list contain given filter "true", otherwise "false".</returns>
        public bool ContainsFilter(string filter);

        /// <summary>
        /// Generate list of NEST.QueryContainers using filters of this class.
        /// </summary>
        /// <returns>List of NEST.QueryContainers to use in nest bool query.</returns>
        public IEnumerable<QueryContainer> GetQuery(string fieldName);
    }
}