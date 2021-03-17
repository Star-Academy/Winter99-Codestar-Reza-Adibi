using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Project_03 {
    [ExcludeFromCodeCoverage]
    class Program {
        static void Main(string[] args) {
            var dataPath = "../../../../TestData/EnglishData";
            var fileReader = new FileReader(dataPath);
            var invertedIndex = new InvertedIndex();
            var directoryData = fileReader.GetRawData();
            foreach (KeyValuePair<string, string> pair in directoryData) {
                invertedIndex.InsertDatas(Tokenizer.GetAllTokens(pair.Key, pair.Value));
            }
            IUserInterface ui = new ConsoleUI();
            var userInputText = ui.UserInput;
            var operators = OperatorExtractor.GetAllOperators(userInputText, invertedIndex);
            var result = new Searcher().RunOperators(operators);
            ui.ShowOutput(result);
        }
    }
}
