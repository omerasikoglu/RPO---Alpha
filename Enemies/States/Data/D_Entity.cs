﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/Entity Data/Base Data")]
public class D_Entity : ScriptableObject
{
    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;

    public float wallCheckDistance = .2f;
    public float ledgeCheckDistance = .2f;

    public float minAgroRange = 3f;
    public float maxAgroRange = 4f;

    public float closeAttackActionDistance = 2f;

    public int healthAmountMax = 3;
    public float damageHopSpeed = 2f; //düşmanın havaya zıplaması
}
