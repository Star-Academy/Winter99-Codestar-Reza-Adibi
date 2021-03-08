
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
        if (obj.GetType() != this.GetType())
            return false;
        Student student = (Student)obj;
        if (student.FirstName == this.FirstName && student.LastName == this.LastName && student.StudentNumber == this.StudentNumber)
            return true;
        return false;
    }
}
