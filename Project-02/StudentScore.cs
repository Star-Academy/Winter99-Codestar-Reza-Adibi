public class StudentScore {
    public StudentScore(int StudentNumber, string Lesson, double Score) {
        this.StudentNumber = StudentNumber;
        this.Lesson = Lesson;
        this.Score = Score;
    }

    public int StudentNumber { get; set; }
    public string Lesson { get; set; }
    public double Score { get; set; }
}
