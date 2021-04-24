using Libraries;
using Project_9.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_9.Services {
    public interface ISearchService {

        /// <summary>
        /// Run search query and return result.
        /// </summary>
        /// <param name="stringQuery">
        /// String querysearch query( 
        ///     '+' before filter means 'or', 
        ///     '-' before filter means 'not' and
        ///     none of this signs before filter means 'and' 
        /// ).
        /// </param>
        /// <returns>List of documents.</returns>
        public IEnumerable<Document> Search(string stringQuery);

        /// <summary>
        /// Add list of documents to index.
        /// </summary>
        /// <param name="items">List of documents.</param>
        public void AddToIndex(IEnumerable<Document> items);
    }
}
