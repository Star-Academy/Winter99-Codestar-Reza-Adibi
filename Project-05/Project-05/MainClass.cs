using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Project_05 {
    [ExcludeFromCodeCoverage]
    class MainClass {
        static void Main(string[] args) {
            IUserInterface ui = new ConsoleUI();
            var dataPath = ui.UserDataPath;
            var fileReader = new FileReader(dataPath);
            var directoryData = fileReader.GetRawData();
            var database = new SqlDatabase(DbmsName.SqlServer);
            foreach (KeyValuePair<string, string> pair in directoryData) {
                database.InsertDataList(Tokenizer.GetAllTokens(pair.Key, pair.Value));
            }
            var userInputText = ui.UserInput;
            var operators = OperatorExtractor.GetAllOperators(userInputText);
            var result = new Searcher().RunOperators(operators, database);
            ui.ShowOutput(result);
        }
    }
}