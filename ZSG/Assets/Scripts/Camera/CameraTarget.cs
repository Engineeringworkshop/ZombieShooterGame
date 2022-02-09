using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    [SerializeField] public Camera cam;
    [SerializeField] public Transform playerTransform;
    [SerializeField] public float threshold;

    private void OnValidate()
    {
        if (playerTransform == null)
        {
            var player = FindObjectOfType<Player>(includeInactive: true);
            playerTransform = player.transform;
        }
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 targetPos = (playerTransform.position + mousePos) / 2f;

        // Restringimos la coordenada x e y a los limites que queremos
        targetPos.x = Mathf.Clamp(targetPos.x, -threshold + playerTransform.position.x, threshold + playerTransform.position.x);
        targetPos.y = Mathf.Clamp(targetPos.y, -threshold + playerTransform.position.y, threshold + playerTransform.position.y);

        transform.position = targetPos;
    }
}
