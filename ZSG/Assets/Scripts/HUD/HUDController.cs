using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [SerializeField] public Text currClipAmmo;
    [SerializeField] public Text currClipAmmount;

    [SerializeField] public Text aidKitAmount;

    [SerializeField] public PlayerHealthBar playerHealthBarPlayer;

    private void OnValidate()
    {
        if (playerHealthBarPlayer == null)
        {
            playerHealthBarPlayer = FindObjectOfType<PlayerHealthBar>();
        }
    }

    // Metodo para cambiar las balas del cargador actual
    public void SetCurrentClipAmmo(int value)
    {
        currClipAmmo.text = value.ToString();
    }

    // Metodo para cambiar la cantidad de cargadores actual
    public void SetCurrentClipAmmount(int value)
    {
        currClipAmmount.text = value.ToString();
    }

    // Metodo para cambiar la cantidad de kits médicos actual
    public void SetAidKitAmount(int value)
    {
        aidKitAmount.text = value.ToString();
    }

    // Metodo para establecer la vida máxima de la barra de vida del jugador
    public void SetMaxHealthOnHealthBar(float value)
    {
        playerHealthBarPlayer.SetMaxHealth(value);
    }

    // Metodo para establecer la vida actual en la barra de vida del jugador
    public void SetCurrentHealthOnHealthBar(float value)
    {
        playerHealthBarPlayer.SetHealth(value);
    }
}
