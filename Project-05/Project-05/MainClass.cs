using System.Diagnostics.CodeAnalysis;

namespace Project_05 {
    [ExcludeFromCodeCoverage]
    class MainClass {
        private static readonly string sqliteFilePath =
            "E:\\Programing\\CodeStarWinter99\\Project-05\\TestData\\sqlitedb.db";
        private static readonly string sqlSqeverServerName = "localhost";
        private static readonly string sqlSqeverDatabaseName = "Codestar_Project05";
        static void Main(string[] args) {
            IUserInterface ui = new ConsoleUI();
            var dataPath = ui.UserDataPath;
            var fileReader = new FileReader(dataPath);
            var directoryData = fileReader.GetRawData();
            var database = new SqlDatabaseSqlServer(sqlSqeverServerName, sqlSqeverDatabaseName);
            foreach (var pair in directoryData) {
                database.InsertDataList(Tokenizer.GetAllTokens(pair.Key, pair.Value));
            }
            var userInputText = ui.UserInput;
            var operators = OperatorExtractor.GetAllOperators(userInputText);
            var result = new Searcher().RunOperators(operators, database);
            ui.ShowOutput(result);
        }
    }
}