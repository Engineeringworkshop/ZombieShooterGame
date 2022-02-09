using UnityEngine;

// esta clase es para gestionar los botones del game menu dentro del juego, hace de enlace entre el UI y el Gameplay manager. 
// Con esta clase no perdemos las referencias cuando creamos escenas nuevas con el prefab UI.
public class GameMenuController : MonoBehaviour
{
    GameplayManager gameplayManager;
    ScreenFrameController screenFrameController;

    private void OnValidate()
    {
        if (gameplayManager == null)
        {
            gameplayManager = FindObjectOfType<GameplayManager>();
        }

        if (screenFrameController == null)
        {
            screenFrameController = FindObjectOfType<ScreenFrameController>(includeInactive: true);
        }
    }

    // Metodo para gestionar la pulsacion del boton del mismo nombre
    public void RestartLevel()
    {
        gameplayManager.RestartLevel();
    }

    // Metodo para gestionar la pulsacion del boton del mismo nombre
    public void ContinuePlaying()
    {
        screenFrameController.DisableGameMenu();
    }

    // Metodo para gestionar la pulsacion del boton del mismo nombre
    public void ExitGameMenu()
    {
        gameplayManager.ExitMainMenu();
    }

    // Metodo para gestionar la pulsacion del boton del mismo nombre
    public void ExitGame()
    {
        gameplayManager.ExitGame();
    }


}
