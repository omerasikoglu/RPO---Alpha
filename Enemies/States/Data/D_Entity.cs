using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/Entity Data/Base Data")]
public class D_Entity : ScriptableObject
{
    public float wallCheckDistance = .2f;
    public float ledgeCheckDistance = .2f;

    public LayerMask whatIsGround;
}
