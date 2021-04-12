using System;
using System.Collections.Generic;
using System.IO;

namespace Libraries {
    public class FileReader {
        /// <summary>
        /// Read all files in given directory.
        /// </summary>
        /// <param name="directoryPath">Path to the drectory containing your files.</param>
        /// <returns> Pairs of "fileID, text" in given directory or "null" if path is empty or doesn't exists. </returns>
        public static Dictionary<string, string> ReadFromDirectory(string directoryPath) {
            var stringData = new Dictionary<string, string>();
            try {
                var pathes = Directory.GetFiles(directoryPath);
                foreach (string path in pathes) {
                    stringData.Add(path.Replace("\\", "/"), File.ReadAllText(path));
                }
            }
            catch (Exception exception) {
                Console.WriteLine(exception.Message + '\n' + exception.StackTrace);
            }
            return stringData;
        }

        /// <summary>
        /// Read file from given file path.
        /// </summary>
        /// <param name="filePath">Path to your file.</param>
        /// <returns> Pair of "fileID, text" of your file or "null" if file doesn't exists. </returns>
        public static Tuple<string, string> ReadFromFile(string filePath) {
            Tuple<string, string> result = null;
            try {
                var text = File.ReadAllText(filePath);
                var id = filePath.Replace("\\", "/");
                result = new Tuple<string, string>(id, text);
            }
            catch (Exception exception) {
                Console.WriteLine(exception.Message + '\n' + exception.StackTrace);
            }
            return result;
        }
    }
}
