using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public D_Entity entityData;
    public FiniteStateMachine stateMachine;

    public int facingDirection { get; private set; }

    public Rigidbody2D rigidbody2d { get; private set; }
    public Animator animator { get; private set; }
    public GameObject aliveGO { get; private set; }
    public BoxCollider2D boxCollider2d { get; private set; }

    private Vector2 velocityWorkspace;


    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform ledgeCheck;

    public virtual void Start()
    {
        facingDirection = 1;

        aliveGO = transform.Find("Alive").gameObject;
        rigidbody2d = aliveGO.GetComponent<Rigidbody2D>();
        animator = aliveGO.GetComponent<Animator>();
        boxCollider2d = aliveGO.GetComponent<BoxCollider2D>();

        stateMachine = new FiniteStateMachine();
    }
    public virtual void Update()
    {
        stateMachine.currentState.LogicUpdate();
    }
    public virtual void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();

        Debug.Log("Ledge" + CheckLedge()+ "Wall" + CheckWall());
    }
    public virtual void SetVelocity(float velocity)
    {
        velocityWorkspace.Set(facingDirection * velocity, rigidbody2d.velocity.y);
        rigidbody2d.velocity = velocityWorkspace;
    }
    public virtual bool CheckWall()
    {
        //RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, facingDirection * Vector2.right, entityData.wallCheckDistance, entityData.whatIsGround);
        //return raycastHit.collider != null;
       
        return Physics2D.Raycast(wallCheck.position, aliveGO.transform.right, entityData.wallCheckDistance, entityData.whatIsGround);
    }
    public virtual bool CheckLedge()
    {
        //RaycastHit2D raycastHit = Physics2D.BoxCast(new Vector2(facingDirection*(boxCollider2d.bounds.max.x + boxCollider2d.bounds.extents.x / 2), boxCollider2d.bounds.min.y + .1f), new Vector2(.1f, 0f), 0f, Vector2.down, entityData.ledgeCheckDistance, entityData.whatIsGround);
        //RaycastHit2D raycastHit = Physics2D.BoxCast( new Vector2(facingDirection * boxCollider2d.bounds.extents.x / 2 * 3, boxCollider2d.bounds.center.y),Vector2.one, 0f, Vector2.down, entityData.ledgeCheckDistance, entityData.whatIsGround);
        //return raycastHit.collider!=null;
        return Physics2D.Raycast(ledgeCheck.position, Vector2.down, entityData.ledgeCheckDistance, entityData.whatIsGround);
    }
    public virtual void Flip()
    {
        facingDirection *= -1;
        aliveGO.transform.Rotate(0f, 180f, 0f);
        
    }
    public virtual void OnDrawGizmos()
    {
      
    }






}

//Color rayColor;
//if (raycastHit.collider != null) rayColor = Color.cyan;
//else rayColor = Color.red;
//Debug.DrawRay(boxCollider2d.bounds.center + new Vector3(0, boxCollider2d.bounds.extents.y), facingDirection * Vector2.right * (boxCollider2d.bounds.extents.x + entityData.wallCheckDistance), rayColor);
//Debug.DrawRay(boxCollider2d.bounds.center - new Vector3(0, (boxCollider2d.bounds.extents.y) / 2), facingDirection * Vector2.right * (boxCollider2d.bounds.extents.x + entityData.wallCheckDistance), rayColor);
//Debug.DrawRay(boxCollider2d.bounds.center + new Vector3((facingDirection * (boxCollider2d.bounds.extents.x + entityData.wallCheckDistance)), boxCollider2d.bounds.extents.y), Vector2.down * ((3 * boxCollider2d.bounds.extents.y) / 2), rayColor);




//RaycastHit2D raycastHitUp = Physics2D.BoxCast(boxCollider2d.bounds.center + new Vector3(0, (boxCollider2d.bounds.extents.y) / 4 * 3, 0), new Vector3(facingDirection * boxCollider2d.bounds.extents.x, .1f, 0), 0f, facingDirection * Vector2.right, entityData.wallCheckDistance, entityData.whatIsGround);
//RaycastHit2D raycastHitDown = Physics2D.BoxCast(boxCollider2d.bounds.center + new Vector3(0, (boxCollider2d.bounds.extents.y) / 4 * 2, 0), new Vector3(facingDirection * boxCollider2d.bounds.extents.x, .1f, 0), 0f, facingDirection * Vector2.right, entityData.wallCheckDistance, entityData.whatIsGround);

//if (raycastHitDown.collider != null && raycastHitUp.collider == null) //kafası collidera değmiyor, vücudu duvarda ama
//{
//    return true;
//}
//else return false;