using System;
using System.Collections.Generic;

namespace Project_02 {
    public interface IProgrammeDataBase {
        /// <summary>
        /// Get students data from DataBase.
        /// </summary>
        /// <returns>
        /// List of Students.
        /// </returns>
        public List<Student> GetStudents();
        /// <summary>
        /// Get StudentScores data from DataBase.
        /// </summary>
        /// <returns>
        /// List of StudentScores.
        /// </returns>
        public List<StudentScore> GetStudentScores();
    }

    public struct Datasource {
        public Datasource(SourceType sorceType, string sorceAccessInfo, Type dataType) : this() {
            SorceType = sorceType;
            SorceAccessInfo = sorceAccessInfo;
            DataType = dataType;
        }
        public SourceType SorceType { get; }
        public string SorceAccessInfo { get; }
        public Type DataType { get; }
    }

    public enum SourceType { File }
}
