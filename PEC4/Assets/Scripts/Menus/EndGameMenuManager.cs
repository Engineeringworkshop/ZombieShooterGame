using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameMenuManager : MonoBehaviour
{
    [SerializeField] public AudioClip shootSound;
    [SerializeField] public AudioClip buttomSelectedSound;
    [SerializeField] public ParticleSystem shootEffect;

    [SerializeField] public Text scoreTotal;

    private AudioSource audioSource;

    private float timeBetweenShots = 0.3f;
    private float timeLastShoot;

    private float timeToLoad = 1f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        scoreTotal.text = PlayerPrefs.GetInt("score", 0).ToString();
    }

    private void Update()
    {
        // SI se ha pulsado
        if (Input.GetButtonDown("Fire1") && Time.time >= timeLastShoot + timeBetweenShots)
        {
            // Reproduce sonido
            audioSource.PlayOneShot(shootSound);

            // Instancia el efecto
            Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log("Cursor: " + cursorPos);
            var instance = Instantiate(shootEffect, new Vector3(cursorPos.x, cursorPos.y, 0), Quaternion.identity);
            instance.gameObject.layer = 5;

            // Reset de contador de tiempo
            timeLastShoot = Time.time;
        }
    }

    // Metodo para empezar nuevo juego
    public void StartNewGame()
    {
        StartCoroutine(StarNewGameDelayed(timeToLoad));
    }

    // Metodo para salir del juego
    public void ExitGame()
    {
        StartCoroutine(ExitGameDelayed(timeToLoad));
    }

    // Coroutine para retrasar la acción
    public IEnumerator StarNewGameDelayed(float time)
    {
        audioSource.PlayOneShot(buttomSelectedSound);

        yield return new WaitForSecondsRealtime(time);

        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(2);
    }

    // Coroutine para retrasar la acción
    public IEnumerator ExitGameDelayed(float time)
    {
        audioSource.PlayOneShot(buttomSelectedSound);

        yield return new WaitForSecondsRealtime(time);

        Debug.Log("Exit game");
        Application.Quit();
    }
}

