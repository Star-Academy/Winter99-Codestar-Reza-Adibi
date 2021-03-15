using System;
using System.Collections.Generic;

namespace Project_03 {
    class Program {
        private static string dataPath = "../../../../TestData/EnglishData";
        private static InvertedIndex invertedIndex;
        static void Main(string[] args) {
            invertedIndex = InitialDataBase();
            IUserInterface ui = new ConsoleUI();
            string userInputText = ui.UserInput;
            List<IOperator> operators = OperatorExtractor.GetAllOperators(userInputText, invertedIndex);
            List<string> result = new Searcher().RunOperators(operators);
            ui.ShowOutput(result);
        }
        private static InvertedIndex InitialDataBase() {
            FileReader fileReader = new FileReader(dataPath);
            InvertedIndex invertedIndex = new InvertedIndex();
            Dictionary<string, string> directoryData = fileReader.GetRawData();
            foreach (KeyValuePair<string, string> pair in directoryData) {
                List<Tuple<string, string>> documentIdTokenPairs = Tokenizer.GetAllTokens(pair.Key, pair.Value);
                invertedIndex.InsertDatas(documentIdTokenPairs);
            }
            return invertedIndex;
        }
    }
}
