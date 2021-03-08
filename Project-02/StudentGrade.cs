using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_02 {
    public class StudentGrade {
        int StudentID { set; get; }
        string StudentFirstName { set; get; }
        string StudentLastName { set; get; }
        double StudentAverageGrade { set; get; }
        public StudentGrade(int studentID, string studentFirstName, string studentLastName, double studentAverageGrade) {
            StudentID = studentID;
            StudentFirstName = studentFirstName;
            StudentLastName = studentLastName;
            StudentAverageGrade = studentAverageGrade;
        }
        public override string ToString() {
            return "ID: " + StudentID.ToString() +
                ", FirstName: " + StudentFirstName +
                ", LastName: " + StudentLastName +
                ", AverageGrade: " + StudentAverageGrade.ToString();
        }
    }
}
