using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPanelInput : MonoBehaviour
{
    [SerializeField] GameObject CharacterPanelGameObject;

    void OnInventory()
    {
        CharacterPanelGameObject.SetActive(!CharacterPanelGameObject.activeSelf);
    }
}
