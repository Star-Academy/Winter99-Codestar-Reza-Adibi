﻿using System;
using System.Collections.Generic;

namespace Project_03 {
    public class ConsoleUI : IUserInterface {
        public ConsoleUI() {
            Console.WriteLine("Hello dear user!");
        }
        public string UserInput {
            get {
                string userInput;
                do {
                    Console.WriteLine("Write your filters and use \" \"( spase ) as seprator:");
                    userInput = Console.ReadLine();
                } while (string.IsNullOrWhiteSpace(userInput));
                return userInput;
            }
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
