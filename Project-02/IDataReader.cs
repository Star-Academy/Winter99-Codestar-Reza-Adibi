using System.Collections.Generic;

namespace Project_02 {
    interface IDataReader {
        /// <summary>
        /// get all T data from DataSorce as IEnumerable of T objects. 
        /// </summary>
        /// <typeparam name="T">
        /// The dataType of data that you want from DataSorce.
        /// </typeparam>
        /// <returns>
        /// IEnumerable of T objects containing data from DataSorce.
        /// </returns>
        public IEnumerable<T> GetObjects<T>();
    }
}
