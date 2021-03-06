using System;
using System.IO;
using System.Text.Json;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Collections;

namespace Project_02 {
    class MainClass {
        private static DataSorce[] dataSorces = new DataSorce[]{
            new DataSorce(
                SorceType.File,
                "./../../../TestData/Students.json",
                typeof(Student)
            ),
            new DataSorce(
                SorceType.File,
                "./../../../TestData/Scores.json",
                typeof(StudentScore)
            )
        };

        static void Main(string[] args) {
            Dictionary<string, object> allDatas = new Dictionary<string, object>();
            foreach (DataSorce dataSorce in dataSorces) {
                DataReader dataGetter = null;
                if (dataSorce.SorceType == SorceType.File) {
                    dataGetter = new FileReader(dataSorce.SorceAccessInfo);
                }
                MethodInfo method = typeof(DataReader).GetMethod(nameof(DataReader.GetObjects));
                MethodInfo generic = method.MakeGenericMethod(dataSorce.DataType);
                allDatas.Add(dataSorce.DataType.ToString(), generic.Invoke(dataGetter, null));
            }
            try {
                ProjectDataBase dataBase = new LocalDataBase(allDatas);
                var bestStudents = (new ResultMaker(dataBase)).GetTopStudents();
                new ConsoleUI().DisplayObjects(bestStudents);
            }
            catch (Exception e) {
                Console.WriteLine(e.Message + "\n" + e.StackTrace);
            }
        }
    }
}
