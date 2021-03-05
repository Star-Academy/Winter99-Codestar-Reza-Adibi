using System;
using System.Collections;

namespace Project_02 {
    class ConsoleUI : DataWriter {
        public void DisplayObjects(IEnumerable objects) {
            foreach (object ob in objects) {
                Console.WriteLine(ob.ToString());
            }
        }
    }
}
