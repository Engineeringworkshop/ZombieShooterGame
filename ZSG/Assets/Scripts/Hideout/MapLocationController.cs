using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MapLocationController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("Components")]
    [SerializeField] GameObject highlightedFrame;
    [SerializeField] GameObject selectedFrame;

    [Header("Location Data")]
    [SerializeField] private LevelData levelData;

    public event Action<MapLocationController> OnPointerEnterEvent;
    public event Action<MapLocationController> OnPointerExitEvent;
    public event Action<MapLocationController> OnLeftClickEvent;

    public delegate void MouseEnterOnLocation(LevelData levelData);
    public static event MouseEnterOnLocation OnMouseEnterOnLocation;

    public delegate void MouseExitOnLocation(LevelData levelData);
    public static event MouseExitOnLocation OnMouseExitOnLocation;

    public delegate void LocationSelected(LevelData levelData);
    public static event LocationSelected OnLocationSelected;

    protected bool isPointerOver;
    protected bool isSelected;

    private void Awake()
    {
        highlightedFrame.SetActive(false);
        selectedFrame.SetActive(false);

        isPointerOver = false;
        isSelected = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isPointerOver = true;

        if (!isSelected)
        {
            highlightedFrame.SetActive(true);
        }

        //Debug.Log("Mouse enter location");

        if (OnPointerEnterEvent != null)
        {
            print("Mouse enter");
            OnPointerEnterEvent(this);
        }

        if (OnMouseEnterOnLocation != null)
        {
            OnMouseEnterOnLocation(levelData);
        }
            
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerOver = false;

        if (!isSelected)
        {
            highlightedFrame.SetActive(false);
        }

        //Debug.Log("Mouse exit location");

        if (OnPointerExitEvent != null)
        {
            OnPointerExitEvent(this);
        }

        if (OnMouseExitOnLocation != null)
        {
            OnMouseExitOnLocation(levelData);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        isSelected = true;

        if (eventData != null && eventData.button == PointerEventData.InputButton.Left)
        {
            highlightedFrame.SetActive(false);
            selectedFrame.SetActive(true);

            if (OnLeftClickEvent != null)
                OnLeftClickEvent(this);
        }

        // Activa el evento
        if (OnLocationSelected != null)
        {
            OnLocationSelected(levelData);
        }
    }
}
