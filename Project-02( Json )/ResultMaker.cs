using System;
using System.Collections.Generic;
using System.Linq;

namespace Project_02 {
    public class ResultMaker {
        IProgrammeDataBase dataBase;

        public ResultMaker(IProgrammeDataBase dataBase) {
            this.dataBase = dataBase;
        }
        /// <summary>
        /// Find top students using students and their scores.
        /// </summary>
        /// <param name="topStudentsCount">
        /// Number of top strudents.
        /// </param>
        /// <returns>
        /// IEnumerable of StudentGrades.
        /// </returns>
        public IEnumerable<StudentGrade> GetTopStudents(int topStudentsCount = 3) {
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
                        std.StudentNumber,
                        std.FirstName,
                        std.LastName,
                        stdAvrScr.scoreAverage
                    )
                ).
                OrderByDescending(item => item.scoreAverage).
                Take(topStudentsCount);
            foreach (var topStudent in topStudents)
                yield return new StudentGrade(
                    topStudent.StudentNumber,
                    topStudent.FirstName,
                    topStudent.LastName,
                    topStudent.scoreAverage
                );
        }
    }
}
