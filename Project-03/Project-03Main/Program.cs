using System;
using System.Collections.Generic;
using System.Linq;

namespace Project_03 {
    class Program {
        private static string dataPath = @"..\..\..\..\TestData\EnglishData";
        private static InvertedIndex invertedIndex;
        static void Main(string[] args) {
            invertedIndex = InitialDataBase();
            IUserInterface ui = new ConsoleUI();
            List<IOperator> operators = GetOperatorsFromUser(ui);
            List<string> result = FindResult(operators);
            ui.ShowOutput(ResultToString(result));
        }

        private static string ResultToString(List<string> listOfStrings) {
            if (listOfStrings.Count == 0)
                return "no Result!";
            return String.Join("\n", listOfStrings);
        }

        private static List<string> FindResult(List<IOperator> operators) {
            List<string> result = new List<string>();
            bool firstOperatorIsAnd = operators.ElementAt(0).GetType() == typeof(And);
            for (int i = 0; i < operators.Count; i++) {
                IOperator op = operators.ElementAt(i);
                if (firstOperatorIsAnd)
                    op = new Or(op.Token, op.InvertedIndex);
                result = op.Filter(result);
            }
            return result;
        }

        private static List<IOperator> GetOperatorsFromUser(IUserInterface ui) {
            string userInputText = ui.UserInput;
            OperatorExtractor operatorExtractor = new OperatorExtractor(userInputText);
            List<IOperator> operators = new List<IOperator>();
            while (!operatorExtractor.EndOfText())
                operators.Add(operatorExtractor.GetNextOperator(invertedIndex));
            return operators.OrderBy(op => op.Value).ToList();
        }

        static InvertedIndex InitialDataBase() {
            FileReader fileReader = new FileReader(dataPath);
            InvertedIndex invertedIndex = new InvertedIndex();
            Dictionary<string, string> directoryData = fileReader.GetRawData();
            foreach (KeyValuePair<string, string> pair in directoryData) {
                List<Tuple<string, string>> documentIdTokenPairs = GetDocumentTokens(pair.Key, pair.Value);
                invertedIndex.InsertDatas(documentIdTokenPairs);
            }
            return invertedIndex;
        }
        static List<Tuple<string, string>> GetDocumentTokens(string documentID, string documentText) {
            List<Tuple<string, string>> documentIdTokenPairs = new List<Tuple<string, string>>();
            Tokenizer tokenizer = new Tokenizer(documentText);
            while (!tokenizer.EndOfText())
                documentIdTokenPairs.Add(new Tuple<string, string>(documentID, tokenizer.GetNextToken()));
            return documentIdTokenPairs;
        }

    }
}
