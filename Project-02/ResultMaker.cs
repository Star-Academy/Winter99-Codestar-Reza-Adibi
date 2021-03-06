using System;
using System.Collections.Generic;
using System.Linq;

namespace Project_02 {
    public class ResultMaker {
        ProjectDataBase dataBase;
        public ResultMaker(ProjectDataBase dataBase) {
            this.dataBase = dataBase;
        }
        /// <summary>
        /// find top students using students and their scores.
        /// </summary>
        /// <param name="topStudentsCount">
        /// number of top strudents.
        /// </param>
        /// <returns>
        /// list of Tuple( "student first name", "student last name", "student average score" ).
        /// </returns>
        public List<Tuple<string, string, double>> GetTopStudents(int topStudentsCount = 3) {
            List<Student> studentsData = dataBase.GetStudents();
            List<StudentScore> scores = dataBase.GetStudentScores();
            var studentsAvrageScore =
                scores.GroupBy(studentScores => new {
                    groupStudentNumber = studentScores.StudentNumber
                }).
                Select(scoresGroup => new {
                    studentNumber = scoresGroup.Key.groupStudentNumber,
                    scoreAverage = scoresGroup.Average(studentScores => studentScores.Score)
                });
            var topStudents =
                studentsAvrageScore.Join(
                    studentsData,
                    student => student.studentNumber,
                    studentAvrageScore => studentAvrageScore.StudentNumber,
                    (stdAvrScr, std) => (
                        std.FirstName,
                        std.LastName,
                        stdAvrScr.scoreAverage
                    )).
                OrderByDescending(item => item.scoreAverage).
                Take(topStudentsCount);
            List<Tuple<string, string, double>> lt = new List<Tuple<string, string, double>>();
            foreach (var topStudent in topStudents)
                lt.Add(new Tuple<string, string, double>(
                    topStudent.FirstName,
                    topStudent.LastName,
                    topStudent.scoreAverage
                ));
            return lt;
        }
    }
}
