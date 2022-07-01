using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MapPanelController : MonoBehaviour
{
    [Header("Description panel components")]
    [SerializeField] private GameObject levelDescriptionPanel;
    [SerializeField] private TMP_Text levelName;
    [SerializeField] private TMP_Text levelDescription;

    [Header("Confirmation Panel Components")]
    [SerializeField] private GameObject travelConfirmationPanel;
    [SerializeField] private TMP_Text travelConfirmationPanelTitle;

    private LevelData locationData;

    private void OnEnable()
    {
        MapLocationController.OnMouseEnterOnLocation += ShowLocationInfo;
        MapLocationController.OnMouseExitOnLocation += HideLocationInfo;

        MapLocationController.OnLocationSelected += ShowConfirmationTravelPanel;

        print("Enable");
    }

    // Eliminamos la suscripción
    private void OnDisable()
    {
        MapLocationController.OnMouseEnterOnLocation -= ShowLocationInfo;
        MapLocationController.OnMouseExitOnLocation -= HideLocationInfo;

        MapLocationController.OnLocationSelected -= ShowConfirmationTravelPanel;

        print("Disable");
    }

    private void Awake()
    {
        // El panel de descripción empieza desactivado
        levelDescriptionPanel.SetActive(false);
        travelConfirmationPanel.SetActive(false);
    }

    // Método para activar el panel cuando se seleccione una localización
    private void ShowLocationInfo(LevelData locationData)
    {
        print("Mouse enter");

        // Activa el panel
        levelDescriptionPanel.SetActive(true);

        // Guarda los datos
        this.locationData = locationData;

        // Muestra los datos
        levelName.text = locationData.levelName;
        levelDescription.text = locationData.levelDescritpion;
    }

    // Método para desactivar el panel cuando se saque el mouse de una localización
    private void HideLocationInfo(LevelData locationData)
    {
        // Desactiva el panel
        levelDescriptionPanel.SetActive(false);
    }

    // Método para activar el panel de confirmación de viaje
    private void ShowConfirmationTravelPanel(LevelData levelData)
    {
        travelConfirmationPanel.SetActive(true);
        travelConfirmationPanelTitle.text = "Travel to " + levelData.levelName + " location?";
    }

    // Método para cargar la escena cuando se pulse el botón
    public void LoadLevel()
    {
        print("Start game");

        if (locationData != null)
        {
            SceneManager.LoadScene(locationData.levelScene);
        }
        else
        {
            Debug.LogError("Can't start game, scene don't selected");
        }
    }

    public void HideConfirmationTravelPanel()
    {
        travelConfirmationPanel.SetActive(false);
    }
}
