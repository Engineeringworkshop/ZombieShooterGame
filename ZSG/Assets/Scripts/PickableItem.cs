using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickableItem : MonoBehaviour
{
    [SerializeField] Item item;

    [SerializeField] SpriteRenderer spriteRenderer;

    private void OnValidate()
    {
        // Obtenemos el sprite render en OnValidate para poder asignar el sprite automaticamente
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        
        // Asingamos el icono al sprite
        spriteRenderer.sprite = item.Icon;
    }

    public Item GetPickableItem()
    {
        return item;
    }
}
