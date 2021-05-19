using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberController : MonoBehaviour {
    protected Text text;
    protected bool animate = false;
    protected bool joining = true;
    protected float t = 0;
    
    float finalT = 2;
    

    void Start() {
        text = GetComponent<Text>();
    }

    void Update() {
        if (animate) {
            t += Time.deltaTime;
            Color c = text.color;

            if (joining) { //Animacion de entrada FadeIn
                c.a = t / finalT;
                text.color = c;
                if (t >= 2) {
                    c.a = 1;
                    text.color = c;
                    EndJoinAnimation();
                }
            }
            else { //Animacion de salida FadeOut
                c.a = 1 - (t / finalT);
                text.color = c;
                if (t >= finalT) {
                    c = Color.white;
                    c.a = 0;
                    text.color = c;
                    EndLeaveAnimation();
                }
            }
        }
    }

    public void JoinInScene() { //Inicia la animacion de entrada
        if (text.color.a < 1) { 
            t = 0;
            joining = true;
            animate = true;
        }
    }

    public virtual void LeaveScene() { //Inicia la animacion de salida
        if (text.color.a > 0) {
            t = 0;
            joining = false;
            animate = true;
        }
    }

    public void SetNumber(string number) {
        text.text = number;
    }

    protected virtual void EndJoinAnimation() {}
    protected virtual void EndLeaveAnimation() {}
}
