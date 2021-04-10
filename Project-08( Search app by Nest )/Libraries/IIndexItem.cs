using System.Collections.Generic;

namespace Libraries {
    public interface IIndexItem {
        /// <summary>
        /// Read File and extract index item from it.
        /// </summary>
        /// <param name="filePath">Path tp file.</param>
        /// <returns>Extracted index item.</returns>
        public IIndexItem GetFomeFile(string filePath);

        /// <summary>
        /// Read all Files in path and extract index items from it.
        /// </summary>
        /// <param name="directoryPath">Path to directory containing datafiles.</param>
        /// <returns>List of extracted index items.</returns>
        public IEnumerable<IIndexItem> GetFomeDirectory(string directoryPath);
    }
}