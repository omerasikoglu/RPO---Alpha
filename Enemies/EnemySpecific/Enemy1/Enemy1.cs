using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Entity
{
    public E1_IdleState idleState { get; private set; }
    public E1_MoveState moveState { get; private set; }

    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_MoveState moveStateData;

    public override void Start()
    {
        base.Start();
        idleState = new E1_IdleState(this, stateMachine, "idle", idleStateData, this);
        moveState = new E1_MoveState(this, stateMachine, "move", moveStateData, this);

        stateMachine.Initialize(moveState);
    }


}
