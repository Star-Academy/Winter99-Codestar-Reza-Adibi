using System.Collections.Generic;

namespace Libraries {
    public interface IIndexItem {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public IIndexItem GetFomeFile(string filePath);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <returns></returns>
        public IEnumerable<IIndexItem> GetFomeDirectory(string directoryPath);
    }
}