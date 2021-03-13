using System;
using System.Collections.Generic;
using System.Reflection;

namespace Project_02 {
    class MainClass {
        private static readonly Datasource[] datasources = new Datasource[]{
            new Datasource(
                SourceType.File,
                "./../../../TestData/Students.json",
                typeof(Student)
            ),
            new Datasource(
                SourceType.File,
                "./../../../TestData/Scores.json",
                typeof(StudentScore)
            )
        };

        static void Main(string[] args) {
            try {
                IProgrammeDataBase dataBase = InitialDataBase();
                ResultMaker resultMaker = new ResultMaker(dataBase);
                var bestStudents = resultMaker.GetTopStudents();
                IDataWriter ui = new ConsoleUI();
                ui.DisplayObjects(bestStudents);
            }
            catch (Exception e) {
                Console.WriteLine(e.Message + "\n" + e.StackTrace);
            }
        }
        /// <summary>
        /// Create database using datasources.
        /// </summary>
        /// <returns>
        /// Programme's database.
        /// </returns>
        private static IProgrammeDataBase InitialDataBase() {
            Dictionary<string, object> allDatas = new Dictionary<string, object>();
            foreach (Datasource dataSorce in datasources) {
                IDataReader dataGetter = null;
                if (dataSorce.SorceType == SourceType.File) {
                    dataGetter = new FileReader(dataSorce.SorceAccessInfo);
                }
                MethodInfo method = typeof(IDataReader).GetMethod(nameof(IDataReader.GetObjects));
                MethodInfo generic = method.MakeGenericMethod(dataSorce.DataType);
                allDatas.Add(dataSorce.DataType.ToString(), generic.Invoke(dataGetter, null));
            }
            return new LocalDataBase(allDatas);
        }
    }
}
