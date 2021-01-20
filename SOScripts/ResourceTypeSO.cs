using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/RecourceType")]
public class ResourceTypeSO : ScriptableObject
{
    public string nameString;
    public Sprite sprite;
    public string colorHex;
    public Color color;
}
