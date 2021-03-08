using System;
using System.Collections;

namespace Project_02 {
    public class ConsoleUI : IDataWriter {
        public void DisplayObjects(IEnumerable objects) {
            if (objects == null)
                return;
            foreach (object ob in objects) {
                Console.WriteLine(ob.ToString());
            }
        }
    }
}
