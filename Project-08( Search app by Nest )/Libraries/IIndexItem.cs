using System.Collections.Generic;

namespace Libraries {
    public interface IIndexItem {
        public IIndexItem GetFomeFile(string filePath);

        public IEnumerable<IIndexItem> GetFomeDirectory(string directoryPath);
    }
}