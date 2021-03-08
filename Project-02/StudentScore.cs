namespace Project_02 {
    public class StudentScore {
        public int StudentNumber { get; set; }
        public string Lesson { get; set; }
        public double Score { get; set; }
        public StudentScore(int StudentNumber, string Lesson, double Score) {
            this.StudentNumber = StudentNumber;
            this.Lesson = Lesson;
            this.Score = Score;
        }
        public override bool Equals(object obj) {
            if (obj.GetType() != GetType())
                return false;
            StudentScore studentScore = (StudentScore)obj;
            if (studentScore.Lesson == Lesson && studentScore.Score == Score && studentScore.StudentNumber == StudentNumber)
                return true;
            return false;
        }
    }
}