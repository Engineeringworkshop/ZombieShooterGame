using UnityEngine;

// En esta clase declararemos todas los atributos que el Player tiene: Ej velocidad de movimiento, resistencia...
// Esta clase heredará de ScriptableObject para poder crear asset del script, nos permitirá crear los objetos de "base de datos"

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data")]

public class PlayerData : ScriptableObject
{
    [Header("Move State")]
    public float movementVelocity = 10f;
}
