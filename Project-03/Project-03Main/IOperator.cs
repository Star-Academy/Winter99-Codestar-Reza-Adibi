using System.Collections.Generic;

namespace Project_03 {
    public interface IOperator {
        public int Priority { get; }
        public InvertedIndex InvertedIndex { get; }
        public string Token { get; }
        /// <summary>
        /// Use operator on input list and Token's DocumentIds list.
        /// </summary>
        /// <param name="inputList">
        /// A starting List of DocumentIDs.
        /// </param>
        /// <returns>
        /// Result of using operator on operands( inputList and Token's DocumentIDs ).
        /// </returns>
        public List<string> Filter(List<string> inputList);
    }
}
