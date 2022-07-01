using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "newLevelData", menuName = "Data/Level Data")]
public class LevelData : ScriptableObject
{
    [Header("Location configuration")]
    public string levelName;
    public string levelDescritpion;
    public SceneField levelScene; 
}
