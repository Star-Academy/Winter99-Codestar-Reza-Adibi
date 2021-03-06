using System;
using System.Collections.Generic;

namespace Project_02 {
    public interface ProjectDataBase {
        public List<Student> GetStudents();
        public List<StudentScore> GetStudentScores();
    }
    public struct DataSorce {
        public DataSorce(SorceType sorceType, string sorceAccessInfo, Type dataType) : this() {
            SorceType = sorceType;
            SorceAccessInfo = sorceAccessInfo;
            DataType = dataType;
        }
        public SorceType SorceType { get; }
        public string SorceAccessInfo { get; }
        public Type DataType { get; }
    }
    public enum SorceType { File }
}
