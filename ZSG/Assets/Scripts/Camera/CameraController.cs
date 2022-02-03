using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public PlayerInput playerInput;

    private CinemachineVirtualCamera mainCamera;

    public float scrollSpeed = 1.0f;

    public float minSizeValue = 4.0f;
    public float maxSizeValue = 20.0f;

    void Awake()
    {
        mainCamera = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        // Obtenemos el movimiento de los controles
        //mouseScrollY = 0f; //playerInput.Gameplay.CameraZoom.ReadValue<float>() / 120f;
        //TODO arreglar zoom raton


    }

    void OnCameraZoom(InputValue value)
    {
        Debug.Log("Camerazoom");
        float mouseScrollY = value.Get<float>() / 120f;
        SetCameraZoom(mouseScrollY);
    }

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
