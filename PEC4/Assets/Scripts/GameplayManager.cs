using UnityEngine;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{
    [Header("Score display")]

    [SerializeField] public Text number1;
    [SerializeField] public Text number2;
    [SerializeField] public Text number3;
    [SerializeField] public Text number4;
    [SerializeField] public Text number5;

    [Header("Weapon HUD")]

    [SerializeField] public Text currClipAmmo;
    [SerializeField] public Text currClipAmmount;

    [SerializeField] public HealthBar healthBarPlayer;

    [SerializeField] public Text aidKitAmount;

    [Header("Others")]

    [SerializeField] Player player;

    void Start()
    {
        SplitScoreNumbers();

        // Carga los datos del cargador y munición
        //currClipAmmo.text = player.WeaponComponent.currentBulletsInMagazine.ToString();
        //currClipAmmount.text = player.WeaponComponent.currentClipAmount.ToString();

        healthBarPlayer.SetMaxHealth(player.playerData.maxHealthBase);
    }

    private void Update()
    {
        SplitScoreNumbers();

        // Carga los datos del cargador y munición
        currClipAmmo.text = player.WeaponComponent.currentBulletsInMagazine.ToString();
        currClipAmmount.text = player.WeaponComponent.currentClipAmount.ToString();

        aidKitAmount.text = player.currAidKitAmount.ToString();

        healthBarPlayer.SetHealth(player.currHealth);
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
