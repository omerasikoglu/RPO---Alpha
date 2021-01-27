using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newMeleeAttackStateData", menuName = "Data/State Data/Melee Attack State")]

public class D_MeleeAttackState : ScriptableObject
{
    public LayerMask whatIsPlayer;

    public float attackRadius = .5f;
    public int attackDamage=1;
}
