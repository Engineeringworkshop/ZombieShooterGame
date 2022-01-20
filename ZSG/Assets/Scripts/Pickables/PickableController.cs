using System.Collections;
using UnityEngine;

public class PickableController : MonoBehaviour
{
    protected AudioSource audioSource;
    protected SpriteRenderer spriteRenderer;
    protected Collider2D pickableCollider;

    public int scorePoints;
    public AudioClip pickupSound;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        pickableCollider = GetComponent<Collider2D>();
    }

    public IEnumerator DestroyGameObject(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        Destroy(gameObject);
    }

    // Metodo para recoger un elemento
    public void PickupItem(Player player)
    {
        // Activamos el solido, desactivamos la visualización y la interactibilidad, y programamos la destrucción
        player.IncreseScore(scorePoints);
        audioSource.PlayOneShot(pickupSound);
        spriteRenderer.enabled = false;
        pickableCollider.enabled = false;
        StartCoroutine(DestroyGameObject(pickupSound.length));
    }
}
