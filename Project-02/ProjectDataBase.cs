using System;
using System.Collections.Generic;

namespace Project_02 {
    public interface ProjectDataBase {
        public List<Student> GetStudents();
        public List<StudentScore> GetStudentScores();
    }
    public struct DataSorce {
        public DataSorce(SorceType sorceType, string sorceAccessInfo, Type dataType) : this() {
            this.sorceType = sorceType;
            this.sorceAccessInfo = sorceAccessInfo;
            this.dataType = dataType;
        }
        public SorceType sorceType { get; }
        public string sorceAccessInfo { get; }
        public Type dataType { get; }
    }
    public enum SorceType { File }
}
