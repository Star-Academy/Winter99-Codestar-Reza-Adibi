using System;
using System.Collections.Generic;
using System.Linq;

namespace Project_02 {
    public class ResultMaker {
        protected Dictionary<string, object> datas;
        public ResultMaker(Dictionary<string, object> datas) {
            this.datas = datas;
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
        public List<Tuple<string, string, double>> GetTopStrudents(int topStudentsCount = 3) {
            object studentsObject = null;
            if (!datas.TryGetValue(typeof(Student).ToString(), out studentsObject) || studentsObject == null)
                throw new Exception("student list is empty");
            IEnumerable<Student> studentsData = (IEnumerable<Student>)studentsObject;
            object scoresObject = null;
            if (!datas.TryGetValue(typeof(StudentScore).ToString(), out scoresObject) || scoresObject == null)
                throw new Exception("score list is empty");
            IEnumerable<StudentScore> scores = (IEnumerable<StudentScore>)scoresObject;
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
                    (stdAvrScr, std) => new {
                        std.FirstName,
                        std.LastName,
                        stdAvrScr.scoreAverage
                    }).
                OrderByDescending(item => item.scoreAverage).
                Take(topStudentsCount);
            List<Tuple<string, string, double>> lt = new List<Tuple<string, string, double>>();
            foreach (var topStudent in topStudents) {
                lt.Add(new(topStudent.FirstName, topStudent.LastName, topStudent.scoreAverage));
            }
            return lt;
        }
    }
}
