using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Project_02 {
    public class FileReader : IDataReader {
        protected string filePath;
        protected string fileText;
        public FileReader(string filePath) {
            this.filePath = filePath;
            fileText = null;
        }
        public IEnumerable<T> GetObjects<T>() {
            IEnumerable<T> result = null;
            try {
                ReadJsonFile();
                result = JsonSerializer.Deserialize<IEnumerable<T>>(fileText);
            }
            catch (Exception exception) {
                Console.WriteLine(exception.Message);
            }
            return result;
        }
        /// <summary>
        ///     get path to json file and read it's text.
        /// </summary>
        /// <param name="path">
        ///     full path to file.
        /// </param>
        /// <returns>
        ///     json files text as string.
        /// </returns>
        public void ReadJsonFile() {
            string jsonTest = "";
            try {
                jsonTest = File.ReadAllText(filePath);
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
            this.fileText = jsonTest;
        }
    }
}
