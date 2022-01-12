using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuGameplayManager : MonoBehaviour
{
    // Metodo para continuar juego anterior
    public void ContinueGame()
    {
        // Carga la escena anterior, si no encuentra la variable, carga la primera escena
        SceneManager.LoadScene(PlayerPrefs.GetInt("currentScene", 2));
    }

    // Metodo para empezar nuevo juego
    public void StartNewGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(2);
    }

    // Metodo para salir del juego
    public void ExitGame()
    {
        Debug.Log("Exit game");
        Application.Quit();
    }
}
