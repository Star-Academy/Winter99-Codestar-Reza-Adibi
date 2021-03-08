namespace Project_02 {
    public class Student {
        public int StudentNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Student(int StudentNumber, string FirstName, string LastName) {
            this.StudentNumber = StudentNumber;
            this.FirstName = FirstName;
            this.LastName = LastName;
        }
        public override bool Equals(object obj) {
            if (obj.GetType() != GetType())
                return false;
            Student student = (Student)obj;
            if (student.FirstName == FirstName && student.LastName == LastName && student.StudentNumber == StudentNumber)
                return true;
            return false;
        }
    }
}