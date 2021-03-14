using System;
using System.Collections.Generic;
using System.IO;

namespace Project_03 {
    public class FileReader {
        private readonly string directoryPath;
        public FileReader(string directoryPath) {
            this.directoryPath = directoryPath;
        }
        /// <summary>
        /// Read and concat text files in given directory.
        /// </summary>
        /// <returns>
        /// Pairs of "fileID, texts" in given directory.
        /// </returns>
        public Dictionary<string, string> GetRawData() {
            var stringData = new Dictionary<string, string>();
            try {
                var pathes = Directory.GetFiles(directoryPath);
                foreach (string path in pathes)
                    stringData.Add(path, File.ReadAllText(path));
            }
            catch (Exception exception) {
                Console.WriteLine(exception.Message + '\n' + exception.StackTrace);
            }
            return stringData;
        }
    }
}
