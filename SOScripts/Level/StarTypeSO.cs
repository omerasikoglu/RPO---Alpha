using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StarTypeSO", menuName = "LevelDesign/StarType")]

public class StarTypeSO : ScriptableObject
{
    public string nameString;
    public Sprite notAchievedSprite;
    public Sprite achievedSprite;
    public string tooltipString;
    public bool isAchieved=false;
}
