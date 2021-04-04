using System.Collections.Generic;

namespace Project_05 {
    public interface IOperator {
        int Priority { get; }
        string Token { get; }
     
        /// <summary>
        /// Use operator on input list and Token's DocumentIds list.
        /// </summary>
        /// <param name="inputList"> A starting List of DocumentIDs. </param>
        /// <param name="database"> The data base of program. </param>
        /// <returns>
        /// Result of using operator on operands( inputList and Token's DocumentIDs ).
        /// </returns>
        List<string> Filter(List<string> inputList, IProgramDatabase database);
    }
}
