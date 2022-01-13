using System.Collections;
using UnityEngine;

public class PickableController : MonoBehaviour
{
    protected AudioSource audioSource;
    protected SpriteRenderer spriteRenderer;
    protected Collider2D colider;

    public int scorePoints;
    public AudioClip pickupSound;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        colider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Activamos el solido, desactivamos la visualización y la interactibilidad, y programamos la destrucción
            collision.GetComponent<Player>().IncreseScore(scorePoints);
            audioSource.PlayOneShot(pickupSound);
            spriteRenderer.enabled = false;
            colider.enabled = false;
            StartCoroutine(DestroyGameObject(pickupSound.length));
        }
    }

    public IEnumerator DestroyGameObject(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        Destroy(gameObject);
    }
}
