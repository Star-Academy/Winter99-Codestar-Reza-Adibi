using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_02 {
    class LocalDataBase : ProjectDataBase {
        public Dictionary<string, object> datas;
        public LocalDataBase(Dictionary<string, object> datas = null) {
            this.datas = datas;
        }
        public List<StudentScore> GetStudentScores() {
            object scoresObject = null;
            if (!datas.TryGetValue(typeof(StudentScore).ToString(), out scoresObject) || scoresObject == null)
                throw new Exception("score list is empty");
            IEnumerable<StudentScore> scores = (IEnumerable<StudentScore>)scoresObject;
            return (List<StudentScore>)scores;
        }

        public List<Student> GetStudents() {
            object studentsObject = null;
            if (!datas.TryGetValue(typeof(Student).ToString(), out studentsObject) || studentsObject == null)
                throw new Exception("student list is empty");
            IEnumerable<Student> studentsData = (IEnumerable<Student>)studentsObject;
            return (List<Student>)studentsData;
        }
    }
}
