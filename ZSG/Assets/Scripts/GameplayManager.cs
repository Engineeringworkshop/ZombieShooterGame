using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{
    // Variables estáticas

    public static bool gameIsPaused;

    [Header("Screen Frame controller")]

    [SerializeField] public ScreenFrameController screenFrameController;

    [Header("Sounds")]

    [SerializeField] public AudioClip victorySound;
    [SerializeField] public AudioClip defeatSound;

    // Atributos internos

    [HideInInspector] AudioSource audioSource;

    private void OnValidate()
    {
        if (screenFrameController == null)
        {
            screenFrameController = FindObjectOfType<ScreenFrameController>(includeInactive: true);
        }
    }

    void Start()
    {
        gameIsPaused = false;

        audioSource = GetComponent<AudioSource>();
    }

    // Metodo para gestionar el final del nivel
    public void EndLevel()
    {
        // Reproducir sonido de vitoria
        audioSource.PlayOneShot(victorySound);

        // Guarda la escena en los prefs para poder continuar
        //PlayerPrefs.SetInt("currentScene", SceneManager.GetActiveScene().buildIndex);

        // Inicia la coroutine de final de nivel
        StartCoroutine(EndLevelCoroutine());
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

    // Metodo para activar el dead frame
    public void ActivateDeadFrame()
    {
        screenFrameController.ToggleDeadFrame();
    }
    
    // Metodo para vovler al menú principal
    public void ExitMainMenu()
    {
        SceneManager.LoadScene(1);
    }

    // Metodo para cerrar el juego
    public void ExitGame()
    {
        Application.Quit();
    }

    // Metodo para pausar el juego
    public void PauseGame()
    {
        gameIsPaused = true;
        Time.timeScale = 0;
    }

    // metodo para reanudar el juego
    public void ResumeGame()
    {
        gameIsPaused = false;
        Time.timeScale = 1;
    }

    #region Coroutines

    // Metodo para cargar el siguiente nivel despues de un tiempo
    private IEnumerator EndLevelCoroutine()
    {
        yield return new WaitForSecondsRealtime(2.0f);
        SceneManager.LoadScene("Hideout");
    }

    // Metodo para reiniciar el nivel despues de un tiempo
    private IEnumerator ReloadLevel()
    {
        yield return new WaitForSecondsRealtime(5.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    #endregion
}
