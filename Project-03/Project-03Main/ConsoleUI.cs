using System;
using System.Collections.Generic;
using System.Text;

namespace Project_03 {
    class ConsoleUI : IUserInterface {
        public string UserInput { get { return GetUserInput(); } }
        public ConsoleUI() {
            Console.WriteLine("Hello dear user!");
        }
        private string GetUserInput() {
            string userInput;
            do {
                Console.WriteLine("Write your filters and use \" \"( spase ) as seprator:");
                userInput = Console.ReadLine();
            } while (userInput == "");
            return userInput;
        }

        public void ShowOutput(string text) {
            Console.WriteLine(text);
        }
    }
}
