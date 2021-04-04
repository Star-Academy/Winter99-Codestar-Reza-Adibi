using System;
using System.Collections.Generic;

namespace Project_05 {
    public class ConsoleUI : IUserInterface {
        public ConsoleUI() {
            Console.WriteLine("Hello dear user!");
        }

        public string UserInput {
            get {
                return ReadFromConsole("Write your filters and use \" \"( spase ) as seprator:");
            }
        }

        public string UserDataPath {
            get {
                return ReadFromConsole("Write the path to your data directory( folder ):");
            }
        }

        /// <summary>
        /// Read data from console.
        /// </summary>
        /// <param name="message"> The message that we show to user on console. </param>
        /// <returns> User input. </returns>
        private static string ReadFromConsole(string message) {
            string userInput;
            do {
                Console.WriteLine(message);
                userInput = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(@userInput));
            return userInput;
        }

        public void ShowOutput(List<string> listOfStrings) {
            if (listOfStrings.Count == 0)
                ShowOutput("no Result!");
            ShowOutput(String.Join("\n", listOfStrings));
        }

        public void ShowOutput(string text) {
            Console.WriteLine(text);
        }
    }
}
