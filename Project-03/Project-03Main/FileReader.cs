using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Project_03 {
    public class FileReader {
        private readonly string directoryPath;
        public FileReader(string directoryPath) {
            this.directoryPath = directoryPath;
        }
        public string GetRawData() {
            var stringData = "";
            var pathes = Directory.GetFiles(directoryPath);
            foreach (string path in pathes)
                stringData += File.ReadAllText(path) + '\n';
            stringData = stringData.Remove(stringData.Length - 1);
            return stringData;
        }
    }
}
