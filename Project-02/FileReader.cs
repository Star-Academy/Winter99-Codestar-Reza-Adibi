using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Project_02 {
    public class FileReader : DataGetter {
        private string filePath;
        private string fileText;
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
        protected void ReadJsonFile() {
            string jsonTest = "";
            try {
                StreamReader streamReader = new StreamReader(filePath);
                jsonTest = streamReader.ReadToEnd();
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
            this.fileText = jsonTest;
        }
    }
}
