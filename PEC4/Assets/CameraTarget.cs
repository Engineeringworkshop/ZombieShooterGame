using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    [SerializeField] public Camera cam;
    [SerializeField] public Transform player;
    [SerializeField] public float threshold;

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 targetPos = (player.position + mousePos) / 2f;

        // Restringimos la coordenada x e y a los limites que queremos
        targetPos.x = Mathf.Clamp(targetPos.x, -threshold + player.position.x, threshold + player.position.x);
        targetPos.y = Mathf.Clamp(targetPos.y, -threshold + player.position.y, threshold + player.position.y);

        transform.position = targetPos;
    }
}
