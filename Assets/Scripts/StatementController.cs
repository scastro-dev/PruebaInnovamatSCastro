using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatementController : NumberController {
    protected override void EndJoinAnimation() {
        animate = false;
        //Comunica a GameManager que ha terminado la animacion de entrada
        GameManager.instance.StatementJoined();
    }
    protected override void EndLeaveAnimation() {
        animate = false;
        //Comunica a GameManager que ha terminado la animacion de salida
        GameManager.instance.StatementLeaved();
    }
}
