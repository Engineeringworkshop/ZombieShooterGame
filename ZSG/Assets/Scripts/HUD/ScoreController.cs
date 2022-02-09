using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public List<Text> scoreDigits;

    private void OnValidate()
    {
        GetComponentsInChildren(includeInactive: true, result: scoreDigits);
    }

    // Metodo para dividir el score en digitos para mostrar en el display
    public void UpdateScoreDisplay(int score)
    {
        int temp = score;
        foreach (Text item in scoreDigits)
        {
            item.text = (temp % 10).ToString();
            temp /= 10;
        }
    }
}