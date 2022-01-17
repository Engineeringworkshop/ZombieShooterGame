using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    [Header("Sounds")]

    [SerializeField] public AudioClip victorySound;
    [SerializeField] public AudioClip defeatSound;

    [Header("Others")]

    [SerializeField] Player player;

    [HideInInspector] AudioSource audioSource;

    void Start()
    {
        SplitScoreNumbers();

        audioSource = GetComponent<AudioSource>();

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

    // Metodo para gestionar el final del nivel
    public void EndLevel()
    {
        // reproducir sonido de vitoria
        audioSource.PlayOneShot(victorySound);

        // Guarda la escena en los prefs para poder continuar
        PlayerPrefs.SetInt("currentScene", SceneManager.GetActiveScene().buildIndex);

        // cargar el siguiente nivel despues de 5 segundos.
        StartCoroutine(LoadNextLevel());
    }

    // Metodo para reiniciar el nivel en caso de muerte
    public void RestartLevel()
    {
        // reproducir sonido de derrota
        audioSource.PlayOneShot(defeatSound);

        // cargar overlay de sangre


        // reinciar el nivel despues de 5 segundos
        StartCoroutine(ReloadLevel());
    }

    // Metodo para cargar el siguiente nivel despues de un tiempo
    public IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(5.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Metodo para reiniciar el nivel despues de un tiempo
    public IEnumerator ReloadLevel()
    {
        yield return new WaitForSecondsRealtime(5.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
