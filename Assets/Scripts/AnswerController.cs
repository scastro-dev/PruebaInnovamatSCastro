using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerController : NumberController {
    private bool isCorrect = false;
    private Button button;

    public void Awake() {
        button = GetComponent<Button>();
    }

    public void Mark(bool correct) { //Cambia el color del texto para indicar si es correcto o no
        isCorrect = correct;
        if (correct) text.color = Color.green;
        else text.color = Color.red;
    }

    public virtual void LeaveScene() { //Inicia la animacion de salida
        if (text.color.a > 0) {
            t = 0;
            joining = false;
            animate = true;
            button.interactable = false; //Desactiva el boton durante las animaciones
        }
    }

    protected override void EndLeaveAnimation() {
        animate = false;
        //Solo la respuesta correcta avisa del final para que GameManager no reciba nunca más de un aviso
        if (isCorrect) GameManager.instance.EndQuestion();
    }
    
    protected override void EndJoinAnimation() {
        animate = false;
        button.interactable = true; //Activa el boton tras la animacion de entrada
    }
}
