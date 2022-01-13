using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuGameplayManager : MonoBehaviour
{
    // Metodo para continuar juego anterior
    public void ContinueGame()
    {
        // Carga la escena anterior, si no encuentra la variable, empieza un nuevo juego
        if (PlayerPrefs.HasKey("currentScene"))
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("currentScene"));
        }
        else
        {
            StartNewGame();
        }
        
    }

    // Metodo para empezar nuevo juego
    public void StartNewGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Metodo para salir del juego
    public void ExitGame()
    {
        Debug.Log("Exit game");
        Application.Quit();
    }
}
