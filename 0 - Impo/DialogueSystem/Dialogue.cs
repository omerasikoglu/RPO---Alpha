using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenuAttribute(menuName = "Dialogue", fileName = "New Dialogue", order = 0)]
public class Dialogue : ScriptableObject
{
    [SerializeField] DialogueNode[] nodes;
}



