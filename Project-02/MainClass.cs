using System;
using System.IO;
using System.Text.Json;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Collections;

namespace Project_02 {
    struct DataSorce {
        public DataSorce(SorceType sorceType, string sorceAccessInfo, Type dataType) : this() {
            this.sorceType = sorceType;
            this.sorceAccessInfo = sorceAccessInfo;
            this.dataType = dataType;
        }
        public SorceType sorceType { get; }
        public string sorceAccessInfo { get; }
        public Type dataType { get; }
    }
    enum SorceType { File }
    class MainClass {
        private static DataSorce[] dataSorces = new DataSorce[]{
            new DataSorce(
                SorceType.File,
                "E:/Programing/CodeStarWinter99/Project-02/Project-02/TestData/Students.json",
                typeof(Student)
            ),
            new DataSorce(
                SorceType.File,
                "E:/Programing/CodeStarWinter99/Project-02/Project-02/TestData/Scores.json",
                typeof(StudentScore)
            )
        };

        static void Main(string[] args) {
            Dictionary<string, object> allDatas = new Dictionary<string, object>();
            foreach (DataSorce dataSorce in Project_02.MainClass.dataSorces) {
                DataGetter dataGetter = null;
                if (dataSorce.sorceType == SorceType.File) {
                    dataGetter = new FileReader(dataSorce.sorceAccessInfo);
                }
                MethodInfo method = typeof(DataGetter).GetMethod(nameof(DataGetter.GetObjects));
                MethodInfo generic = method.MakeGenericMethod(dataSorce.dataType);
                allDatas.Add(dataSorce.dataType.ToString(), generic.Invoke(dataGetter, null));
            }
            (new ResultMaker(allDatas)).GetBestStrudents();

        }
    }
}
