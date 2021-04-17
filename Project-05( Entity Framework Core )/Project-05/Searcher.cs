using System.Collections.Generic;
using System.Linq;

namespace Project_05 {
    public class Searcher {
        /// <summary>
        /// Run given operators and make result.
        /// </summary>
        /// <param name="operators"> List of operators orderd by priority. </param>
        /// <param name="database"> The database of pgogram. </param>
        /// <returns> List of document ids. </returns>
        public List<string> RunOperators(List<IOperator> operators, IProgramDatabase database) {
            var result = new List<string>();
            var firstOperatorIsAnd = operators.ElementAt(0) is And;
            for (int i = 0; i < operators.Count; i++) {
                IOperator op = operators.ElementAt(i);
                if (firstOperatorIsAnd) {
                    op = new Or(op.Token);
                    firstOperatorIsAnd = false;
                }
                result = op.Filter(result, database);
            }
            return result;
        }
    }
}
