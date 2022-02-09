using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    private CinemachineVirtualCamera mainCamera;

    public float scrollSpeed = 1.0f;

    public float minSizeValue = 4.0f;
    public float maxSizeValue = 20.0f;

    void Awake()
    {
        mainCamera = GetComponent<CinemachineVirtualCamera>();
    }

    // Metodo para leer el imput de la rueda del ratón
    public void OnCameraZoom(InputAction.CallbackContext context)
    {
        Debug.Log("Camerazoom");
        float mouseScrollY = context.ReadValue<float>() / 120f;
        SetCameraZoom(mouseScrollY);
    }

    // metodo para mover el zoom de la cámara según el valor dado
    private void SetCameraZoom(float zoomValue)
    {
        if (mainCamera.m_Lens.Orthographic)
        {
            mainCamera.m_Lens.OrthographicSize = Mathf.Clamp(mainCamera.m_Lens.OrthographicSize - zoomValue * scrollSpeed, minSizeValue, maxSizeValue);
        }
        else
        {
            mainCamera.m_Lens.FieldOfView -= zoomValue * scrollSpeed;
        }
    }
}
