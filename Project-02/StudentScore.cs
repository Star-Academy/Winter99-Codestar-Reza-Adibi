public class StudentScore {
    public StudentScore(int StudentNumber, string Lesson, double Score) {
        this.StudentNumber = StudentNumber;
        this.Lesson = Lesson;
        this.Score = Score;
    }

    public int StudentNumber { get; set; }
    public string Lesson { get; set; }
    public double Score { get; set; }
    public override bool Equals(object obj) {
        if (obj.GetType() != this.GetType())
            return false;
        StudentScore studentScore = (StudentScore)obj;
        if (studentScore.Lesson == this.Lesson && studentScore.Score == this.Score && studentScore.StudentNumber == this.StudentNumber)
            return true;
        return false;
    }
}
