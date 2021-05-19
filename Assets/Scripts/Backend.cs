using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backend : MonoBehaviour {
    private static string[] numbers = { "Cero", "Uno", "Dos", "Tres", "Cuatro", "Cinco", "Seis", "Siete", "Ocho", "Nueve", "Diez" };

    public static Question GetQuestion() {
        Question result = new Question();
        int i = Random.Range(0, numbers.Length);
        result.statement = numbers[i];
        
        //Posicion random de la respuesta correcta
        int j = Random.Range(0, result.responsesSize);
        result.responses[j] = i.ToString();
        result.correctAnswer = j;

        //Obtener respuesta erroneas
        for(int k = 0; k < result.responsesSize; ++k) {
            if(k != j) {
                result.responses[k] = GetRandomNumber(result.responses);
            }
        }

        return result;
    }

    //Devuelve una respuesta erronea no utilizada
    private static string GetRandomNumber(string[] usedNumbers) {
        int i = Random.Range(0, numbers.Length);
        
        while (IsContained(i.ToString(), usedNumbers)) {
            i++;
            if (i >= numbers.Length) i = 0;
        }
        
        return i.ToString();
    }

    //Devuelve si usedNumbers contiene el numero n
    private static bool IsContained(string n, string[] usedNumbers) {
        for(int i = 0; i< usedNumbers.Length; ++i) {
            if (usedNumbers[i] != null && usedNumbers[i].Equals(n)) return true;
        }

        return false;
    }
}
