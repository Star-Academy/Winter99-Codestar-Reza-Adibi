using System.Collections.Generic;

namespace Libraries {
    public interface IModel {
        public IModel GetFomeFile(string filePath);

        public IEnumerable<IModel> GetFomeDirectory(string directoryPath);
    }
}