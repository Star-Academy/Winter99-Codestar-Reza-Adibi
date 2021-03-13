namespace Project_02 {
    public class Student {
        public int StudentNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Student(int studentNumber, string firstName, string lastName) {
            this.StudentNumber = studentNumber;
            this.FirstName = firstName;
            this.LastName = lastName;
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