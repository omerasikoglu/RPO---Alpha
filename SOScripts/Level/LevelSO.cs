using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelSO", menuName = "LevelDesign/Level")]

public class LevelSO : ScriptableObject
{
    [Header("[Level Details]")]
    public int sceneNumber;
    public List<StarTypeSO> starTypesList;
}
