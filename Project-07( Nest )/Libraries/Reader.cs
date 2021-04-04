using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Libraries {
    public class Reader {
        private readonly string path;

        public Reader(string filePath) {
            if (File.Exists(filePath))
                this.path = filePath;
            else
                throw new Exception("You gave wrong path to reader!");
        }

        public IEnumerable<Person> People {
            get {
                return GetObjects<Person>();
            }
        }

        private IEnumerable<T> GetObjects<T>() {
            IEnumerable<T> objects = new List<T>();
            try {
                var text = ReadJsonFile();
                objects = JsonSerializer.Deserialize<IEnumerable<T>>(text);
            }
            catch (Exception exception) {
                Console.WriteLine(exception.Message);
            }
            return objects;
        }

        private string ReadJsonFile() {
            return File.ReadAllText(this.path);
        }
    }
}
