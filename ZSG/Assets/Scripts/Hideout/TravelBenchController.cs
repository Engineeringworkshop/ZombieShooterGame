using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelBenchController : MonoBehaviour, IInteractable
{
    [SerializeField] ScreenFrameController screenFrameController;
    [SerializeField] SpriteRenderer spriteRenderer;

    [SerializeField] RectTransform travelMapCanvas;
    [SerializeField] InteractionIconController interactionIconController;

    // La detección del jugador se ha movido a otro scrip para no influir en las funciones OnMouse... , así puedo detectar por separado al jugador, la colisión física de la caja, y la derección del mouse con 3 colldiers separados.
    [SerializeField] ProximityDetector proximityDetector;

    public bool IsTriggered { get; private set; }

    protected void OnValidate()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>(includeInactive: true);
        }

        if (screenFrameController == null)
        {
            screenFrameController = FindObjectOfType<ScreenFrameController>(includeInactive: true);
        }

        if (interactionIconController == null)
        {
            interactionIconController = FindObjectOfType<InteractionIconController>(includeInactive: true);
        }

        if (proximityDetector == null)
        {
            proximityDetector = GetComponentInChildren<ProximityDetector>(includeInactive: true);
        }
    }

    private void Awake()
    {
        interactionIconController.DisableInteractionIcon();
    }

    #region Mouse managment

    private void OnMouseEnter()
    {
        //Debug.Log("on mouse enter");

        if (proximityDetector.IsInRange)
        {
            interactionIconController.LoadInteractionIcon("E");
            IsTriggered = true;
        }
    }

    private void OnMouseOver()
    {
        if (proximityDetector.IsInRange)
        {
            if (!interactionIconController.ImageIsActive())
            {
                interactionIconController.LoadInteractionIcon("E");
                IsTriggered = true;
            }

            interactionIconController.UpdateInteractionIconPosition();
        }
        // por si nos movemos fuera del rango
        else
        {
            interactionIconController.DisableInteractionIcon();
            IsTriggered = false;
        }
    }

    private void OnMouseExit()
    {
        interactionIconController.DisableInteractionIcon();
        IsTriggered = false;
    }

    public virtual void InteractionMethod()
    {
        // Añadimos el panel al screenFrameController
        screenFrameController.SetCurrentPanel(travelMapCanvas);

        // Activamos el panel
        screenFrameController.EnableCurrentPanel();
    }

    #endregion
}
