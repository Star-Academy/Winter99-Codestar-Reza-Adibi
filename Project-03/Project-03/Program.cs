using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Project_03 {
    [ExcludeFromCodeCoverage]
    class Program {
        private static string dataPath = "../../../../TestData/EnglishData";
        static void Main(string[] args) {
            FileReader fileReader = new FileReader(dataPath);
            InvertedIndex invertedIndex = new InvertedIndex();
            Dictionary<string, string> directoryData = fileReader.GetRawData();
            foreach (KeyValuePair<string, string> pair in directoryData) {
                invertedIndex.InsertDatas(Tokenizer.GetAllTokens(pair.Key, pair.Value));
            }
            IUserInterface ui = new ConsoleUI();
            string userInputText = ui.UserInput;
            List<IOperator> operators = OperatorExtractor.GetAllOperators(userInputText, invertedIndex);
            List<string> result = new Searcher().RunOperators(operators);
            ui.ShowOutput(result);
        }
    }
}
