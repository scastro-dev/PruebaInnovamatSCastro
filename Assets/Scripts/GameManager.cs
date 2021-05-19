using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    const int MAXFAILS = 2;
    public static GameManager instance;
    private StatementController statement;
    public AnswerController[] answers;
    private Question q;
    private int fails = 0;
    private int totalFails = 0;
    private int totalSucces = 0;
    public Text failsNumber;
    public Text succesNumber;

    private void Awake() {
        if (instance == null) instance = this;
        else Destroy(this);

        statement = FindObjectOfType<StatementController>();
    }

    void Start() {
        GetNewQuestion();
    }

    private void GetNewQuestion() { //Solicita una nueva pregunta con Enunciado, respuesta correcta e incorrectas
        q = Backend.GetQuestion();
        fails = 0;

        statement.SetNumber(q.statement); 

        for (int i = 0; i < q.responsesSize; ++i) {
            answers[i].SetNumber(q.responses[i]);
        }

        statement.JoinInScene(); //Inicia la animacion de entrada del enunciado
    }

    public void CheckAnswer(int answerID) { //Comprueba si answerID es la respuesta correcta
        if(answerID == q.correctAnswer) {
            answers[answerID].Mark(true);
            totalSucces++;
            succesNumber.text = totalSucces.ToString();
            for (int i = 0; i < answers.Length; ++i) answers[i].LeaveScene(); //Animaciones de salida de las respuestas
        }
        else {
            answers[answerID].Mark(false);
            fails++;
            if (fails >= MAXFAILS) {
                totalFails++;
                failsNumber.text = totalFails.ToString();
                answers[q.correctAnswer].Mark(true);
                for (int i = 0; i < answers.Length; ++i) answers[i].LeaveScene(); //Animaciones de salida de las respuestas
            }
            else answers[answerID].LeaveScene(); //Animacion de salida de la respuesta erronea
        }
    }

    public void EndQuestion() {
        GetNewQuestion();
    }

    public void StatementJoined() {
        StartCoroutine("StatementLeaveScene");       
    }

    private IEnumerator StatementLeaveScene() {
        yield return new WaitForSeconds(2); //Espera de 2 segundos antes de iniciar la animacion de salida
        statement.LeaveScene();
    }

    public void StatementLeaved() { //Tras la salida del enunciado se lanzan las animaciones de netrada de las respuestas
        for (int i = 0; i < answers.Length; ++i) answers[i].JoinInScene();
    }
}