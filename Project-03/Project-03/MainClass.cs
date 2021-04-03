using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Project_03 {
    [ExcludeFromCodeCoverage]
    class MainClass {
        static void Main(string[] args) {
            IUserInterface ui = new ConsoleUI();
            var dataPath = ui.UserDataPath;
            var fileReader = new FileReader(dataPath);
            var directoryData = fileReader.GetRawData();
            var invertedIndex = new InvertedIndex();
            foreach (KeyValuePair<string, string> pair in directoryData) {
                invertedIndex.InsertDatas(Tokenizer.GetAllTokens(pair.Key, pair.Value));
            }
            var userInputText = ui.UserInput;
            var operators = OperatorExtractor.GetAllOperators(userInputText, invertedIndex);
            var result = new Searcher().RunOperators(operators);
            ui.ShowOutput(result);
        }
    }
}