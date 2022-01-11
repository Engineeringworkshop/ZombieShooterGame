using UnityEngine;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] public Text number1;
    [SerializeField] public Text number2;
    [SerializeField] public Text number3;
    [SerializeField] public Text number4;
    [SerializeField] public Text number5;

    [SerializeField] readonly Player player;

    void Start()
    {
        SplitScoreNumbers();
    }

    private void Update()
    {
        SplitScoreNumbers();
    }

    // Metodo para dividir el score en digitos para mostrar en el display
    public void SplitScoreNumbers()
    {
        int temp = player.score;
        number1.text = (temp % 10).ToString();
        temp /= 10;
        number2.text = (temp % 10).ToString();
        temp /= 10;
        number3.text = (temp % 10).ToString();
        temp /= 10;
        number4.text = (temp % 10).ToString();
        temp /= 10;
        number5.text = (temp % 10).ToString();
    }
}
