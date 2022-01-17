using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public PlayerInput playerInput;

    private CinemachineVirtualCamera mainCamera;

    public float scrollSpeed = 1.0f;

    public float minSizeValue = 4.0f;
    public float maxSizeValue = 20.0f;

    #region Enable/Disable
    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    #endregion

    // Start is called before the first frame update
    void Awake()
    {
        // Creamos el mapa de acciones (controles)
        playerInput = new PlayerInput();

        mainCamera = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        // Obtenemos el movimiento de los controles
        float mouseScrollY = playerInput.Gameplay.CameraZoom.ReadValue<float>() / 120f;

        if (mainCamera.m_Lens.Orthographic)
        {
            mainCamera.m_Lens.OrthographicSize = Mathf.Clamp(mainCamera.m_Lens.OrthographicSize - mouseScrollY * scrollSpeed, minSizeValue, maxSizeValue); 
        }
        else
        {
            mainCamera.m_Lens.FieldOfView -= mouseScrollY * scrollSpeed;
        }
    }
}
