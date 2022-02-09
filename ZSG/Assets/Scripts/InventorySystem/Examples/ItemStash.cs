using UnityEngine;

public class ItemStash : ItemContainer, IInteractable
{
	[SerializeField] Transform itemsParent;
	[SerializeField] SpriteRenderer spriteRenderer;
	//[SerializeField] KeyCode openKeyCode = KeyCode.E;

	[SerializeField] ScreenFrameController screenFrameController;

	[SerializeField] RectTransform stashCanvas;
	[SerializeField] Transform interactionIcon;

	// La detección del jugador se ha movido a otro scrip para no influir en las funciones OnMouse... , así puedo detectar por separado al jugador, la colisión física de la caja, y la derección del mouse con 3 colldiers separados.
	[SerializeField] ProximityDetector proximityDetector;

	private bool isOpen;

    private Character character;

    public bool IsTriggered { get; private set; }

    protected override void OnValidate()
	{
		if (itemsParent != null)
		{
			itemsParent.GetComponentsInChildren(includeInactive: true, result: ItemSlots);
		}

		if (spriteRenderer == null)
		{
			spriteRenderer = GetComponentInChildren<SpriteRenderer>(includeInactive: true);
		}

        if (screenFrameController == null)
        {
			screenFrameController = FindObjectOfType<ScreenFrameController>(includeInactive: true);
        }

        if (proximityDetector == null)
        {
			proximityDetector = GetComponentInChildren<ProximityDetector>(includeInactive: true);
        }

        if (character == null)
        {
			character = FindObjectOfType<Character>(includeInactive: true);
        }
	}

	protected override void Awake()
	{
		base.Awake();
		
		//stashCanvas.gameObject.SetActive(false);
		interactionIcon.gameObject.SetActive(false);
	}

	private void Update()
	{
		/*if (proximityDetector.IsInRange && Input.GetKeyDown(openKeyCode))
		{
			isOpen = !isOpen;
			
			stashCanvas.gameObject.SetActive(isOpen);

			if (isOpen)
				character.OpenItemContainer(this);
			else
				character.CloseItemContainer(this);
		}*/
	}

    #region Mouse managment

    private void OnMouseEnter()
    {
		Debug.Log("on mouse enter");

		if (proximityDetector.IsInRange)
		{
			interactionIcon.gameObject.SetActive(true);
			IsTriggered = true;
		}
	}

    private void OnMouseOver()
    {
        if (proximityDetector.IsInRange)
        {
            if (!interactionIcon.gameObject.activeSelf)
            {
				interactionIcon.gameObject.SetActive(true);
				IsTriggered = true;
			}

			interactionIcon.transform.position = Input.mousePosition + new Vector3(50f, 50f, 0f);
		}
		// por si nos movemos fuera del rango
        else
        {
			interactionIcon.gameObject.SetActive(false);
			IsTriggered = false;
		}
	}

    private void OnMouseExit()
    {
		interactionIcon.gameObject.SetActive(false);
		IsTriggered = false;
	}

    public void InteractionMethod()
    {
        Debug.Log("Se ha activado el elemento del hideout");

		// Añadimos el panel al screenFrameController
		screenFrameController.SetCurrentPanel(stashCanvas);

		// Activamos el panel
		screenFrameController.EnableCurrentPanel();

		// Como es un almacen de objetos, activamos tambien el panel de inventario y el panel de equipamiento y stats
		screenFrameController.EnableInventoryPanel();
		screenFrameController.EnableEquipmentAndStatsPanel();

		// Llamamos al metodo de character que añade los listeners para los eventos del ratón
		character.OpenItemContainer(this);
	}

    #endregion

}
