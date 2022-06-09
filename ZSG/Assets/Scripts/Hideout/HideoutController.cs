using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideoutController : MonoBehaviour
{
    [SerializeField] ItemStash itemStash;
    [SerializeField] List<GameObject> hideoutElements;

    // TODO autorellenar la lista con todos los objetos interactuables del hideout

    public void InteractionKeyIsPushed()
    {
        foreach (var item in hideoutElements)
        {
            // Obtengo el interface por comodidad
            IInteractable var = item.GetComponent<IInteractable>();

            // Si esta activado (tiene el ratón por encima) lo abriremos.
            if (var.IsTriggered)
            {
                var.InteractionMethod();
            }
        }
    }
}
