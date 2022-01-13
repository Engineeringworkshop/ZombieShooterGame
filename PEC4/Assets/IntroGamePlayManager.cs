using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroGamePlayManager : MonoBehaviour
{
    public PlayerInput introInput;

    private void Awake()
    {
        // Creamos el mapa de acciones (controles)
        introInput = new PlayerInput();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            Debug.Log("any key pushed");

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void OnEnable()
    {
        // Activamos el mapa de acciones
        introInput.Gameplay.Enable();
    }

    private void OnDisable()
    {
        // Desactivamos el mapa de acciones
        introInput.Gameplay.Disable();
    }
}
