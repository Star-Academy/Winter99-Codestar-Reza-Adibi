using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Libraries.Tests {
    class GeneralFunctions {
        public static void CreateFile(string path) {
            if (!File.Exists(path))
                File.Create(path).Close();
        }
        public static void CreateFile(string path, string content) {
            if (!File.Exists(path))
                File.WriteAllText(path, content);
        }
    }
}
