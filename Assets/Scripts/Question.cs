public class Question {
    public string statement;
    public string[] responses;
    public int correctAnswer;
    public int responsesSize = 3;

    public Question() {
        responses = new string[responsesSize];
    }
}
