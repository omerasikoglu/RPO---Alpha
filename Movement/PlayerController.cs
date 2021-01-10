using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, ICollider
{

    private Rigidbody2D rigidbody2d;

    //[SerializeField] private SpriteAnimator spriteAnimator;

    #region Animations

    [SerializeField] private Animator animator;


    #endregion

    #region Dash

    //variables
    private float dashTimeLeft;
    private float lastImageXpos;
    private float lastDash = -1f; //son dash attıgımızdan geçen süre

    //consts
    private float dashTime = .2f;
    private float dashSpeed = 30f;
    private float distanceBetweenImages = .1f;
    private float dashCooldown = 2.5f;

    #endregion

    #region Movement

    //vars
    private float x;   //girilen inputun yönünü belirlemek için
    private bool isWalking;
    private bool isFacingRight = true;
    private bool canFlip;

    //consts
    private const float movementSpeed = 10f;

    private float turnTimer;
    private float turnTimerSet = 1f;

    #endregion

    #region Jump

    //consts
    private const float jumpForce = 6f;
    private const float fallMultiplier = 5f;
    private const float lowJumpMultiplier = 6f;
    private const float wallJumpMultiplier = 6f;



    private int amountOfJumpsLeft;
    private int amountOfJumps = 2;

    private float jumpTimer;
    private float jumpTimerSet = 0.15f;

    private float wallJumpTimer;
    private float wallJumpTimerSet = 0.5f;
    private bool canJump;

    #endregion

    #region Wall-e

    //vars
    private bool canGrabWall = true;
    private bool wallGrabbing;
    private bool wallSliding;
    private bool canJumpFromRight;
    private bool canJumpFromLeft;

    //consts
    private float wallSlideSpeed = 2f;
    private float movementForceInAir = 50f;
    private float airDragMultiplier = .95f; //hava direnci
    private float variableJumpHeightMultiplier = .5f;

    public Vector2 wallHopDirection;
    public Vector2 wallJumpDirection;

    #endregion

    #region Ledge

    public Transform ledgePosBotTransform;

    private bool isClimbingLedge = false;

    private Vector2 ledgePosBot;
    private Vector2 ledgePos1;
    private Vector2 ledgePos2;


    private float wallCheckDistance = .3f; //rectangle collider'da da tanımlı

    private float ledgeClimbXOffset1 = 0.5f;
    private float ledgeClimbYOffset1 = 0f;

    private float ledgeClimbXOffset2 = 0.5f;
    private float ledgeClimbYOffset2 = 2f;

    #endregion

    #region Incoming 


    //eklenecekler
    private bool canMove = true;
    private bool wallJumping = false;
    private bool isDashing = false;
    private Vector2 velocity;

    #endregion

    #region Interface Stuffs

    //Interface objects
    private bool isGrounded;
    private bool onRightWall;
    private bool onLeftWall;
    private bool rightLedgeCheck;
    private bool leftLedgeCheck;

    //Interface Implements
    public void SetBoolIsGrounded(bool isGrounded)
    {
        this.isGrounded = isGrounded;
    }

    public void SetBoolOnRightWall(bool onRightWall)
    {
        this.onRightWall = onRightWall;
    }

    public void SetBoolOnLeftWall(bool onLeftWall)
    {
        this.onLeftWall = onLeftWall;
    }
    public void SetBoolOnRightLedgeCheck(bool rightLedgeCheck)
    {
        this.rightLedgeCheck = rightLedgeCheck;
    }
    public void SetBoolOnLeftLedgeCheck(bool leftLedgeCheck)
    {
        this.leftLedgeCheck = leftLedgeCheck;
    }
    #endregion

    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        amountOfJumpsLeft = amountOfJumps;
        wallHopDirection.Normalize();
        wallJumpDirection.Normalize();
        canMove = true; canFlip = true;



    }
    private void Update()
    {
        //Time.timeScale = .1f;
        ledgePosBot = ledgePosBotTransform.position;
        //wallSliding = false;
        //isWalking = false;
        //wallGrabbing = false;
        //ledgePosBot = transform.position + new Vector3(0f, .5f, 0f);
        x = Input.GetAxisRaw("Horizontal");    //yürüme

        UpdateAnimations();
        CheckInputs();
        CheckWallGrab();
        CheckMovementDirection();
        CheckDash();
        CheckLedgeClimb();

        if (turnTimer >= 0)
        {
            turnTimer -= Time.deltaTime;
            if (turnTimer <= 0)
            {
                canMove = true;
                canFlip = true;
                canJump = true;
            }
        }

    }
    private void FixedUpdate()
    {
        ApplyMovement();
        ApplyJumping();
    }
    private void UpdateAnimations()
    {
        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isGrounded", isGrounded);
        animator.SetFloat("yVelocity", rigidbody2d.velocity.y);
        animator.SetBool("isWallSliding", wallSliding);
        animator.SetBool("isGrabbing", wallGrabbing);
    }
    private void CheckInputs()
    {
        if (Input.GetKeyDown(KeyCode.X)) //dash atma
        {
            if (Time.time >= (lastDash + dashCooldown)) AttemptToDash();
        }

        if (isGrounded)
        {
            canJump = true;
            amountOfJumpsLeft = amountOfJumps;
            canJumpFromRight = true;
            canJumpFromLeft = true;
            wallSliding = false;



            if (x != 0) //yürüyor
            {
                isWalking = true;
            }

            else if (x == 0) //duruyor
            {
                isWalking = false;
            }

            if (canJump && Input.GetKeyDown(KeyCode.C)) //zıplıyor
            {
                rigidbody2d.velocity += jumpForce * (Vector2.up);
                amountOfJumpsLeft--;

            }

        }
        else if (!isGrounded)   //yere değmiyorsa
        {

            if (onRightWall)
            {
                if (Input.GetKey(KeyCode.Z) && canGrabWall)
                {
                    wallGrabbing = true;
                }
                else
                {
                    wallGrabbing = false;
                }

                if (x == 1 && rigidbody2d.velocity.y < -2f && !isClimbingLedge) //duvarda sağa tıklıyorsa
                {
                    wallSliding = true;
                }
                else
                {
                    wallSliding = false;
                }

                if (canJumpFromRight)
                {
                    if (Input.GetKeyDown(KeyCode.C)) //zıpladıysa
                    {
                        rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, wallJumpMultiplier);

                        canJumpFromRight = false;
                        canJumpFromLeft = true;
                    }
                    if (x < 0)
                    {
                        canMove = false;
                        canFlip = false;
                        canJump = false;
                        turnTimer = turnTimerSet;
                    }

                }
            }
            else if (onLeftWall)
            {
                if (Input.GetKey(KeyCode.Z)&&canGrabWall)
                {
                    wallGrabbing = true;
                }
                else
                {
                    wallGrabbing = false;
                }

                if (x == -1 && rigidbody2d.velocity.y < -2f && !isClimbingLedge) //duvarda sola tıklıyorsa
                {
                    wallSliding = true;
                }
                else
                {
                    wallSliding = false;
                }


                if (canJumpFromLeft)
                {
                    if (Input.GetKeyDown(KeyCode.C)) //zıpladıysa
                    {
                        rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, wallJumpMultiplier);
                        canJumpFromRight = true;
                        canJumpFromLeft = false;
                    }
                    if (x > 0)
                    {
                        canMove = false;
                        canFlip = false;
                        canJump = false;
                        turnTimer = turnTimerSet;
                    }
                }
            }
            else if (!CheckOnWall())
            {
                canJumpFromRight = true;
                canJumpFromLeft = true;
            }
        }
    }

    private void AttemptToDash()
    {
        isDashing = true;
        dashTimeLeft = dashTime;
        lastDash = Time.time;

        PlayerAfterImagePool.Instance.GetFromPool();
        lastImageXpos = transform.position.x;
    }
    private void CheckDash()
    {
        if (isDashing)
        {
            if (dashTimeLeft > 0)
            {
                canMove = false;
                canFlip = false;

                rigidbody2d.velocity = new Vector2(dashSpeed * x, 0);
                dashTimeLeft -= Time.deltaTime;

                if (Mathf.Abs(transform.position.x - lastImageXpos) > distanceBetweenImages)
                {
                    PlayerAfterImagePool.Instance.GetFromPool();
                    lastImageXpos = transform.position.x;
                }
            }
            if (dashTimeLeft <= 0 || CheckOnWall())
            {
                isDashing = false;
                canMove = true;
                canFlip = true;
            }

        }
    }
    private void ApplyJumping()
    {
        if (rigidbody2d.velocity.y < 0)      //düşüyorsa
        {
            if (!wallSliding)
            {
                rigidbody2d.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
            else if (wallSliding)
            {
                rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, -wallSlideSpeed);
            }
        }
        else if (rigidbody2d.velocity.y > 0 && !Input.GetKey(KeyCode.C))      //zıpladıysa
        {
            rigidbody2d.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    private void CheckWallGrab()
    {
        if (wallGrabbing)
        {
            rigidbody2d.gravityScale = 0;
            canMove = false;
            canFlip = false;
            rigidbody2d.velocity = new Vector2(0, 0);
        }
        else
        {
            canMove = true;
            canFlip = true;
            rigidbody2d.gravityScale = 1;
        }
    }
    private void CheckMovementDirection()
    {
        if (isFacingRight && x < 0 && canFlip)
        {
            Flip();
        }
        else if (!isFacingRight && x > 0 && canFlip)
        {
            Flip();
        }
    }
    public void Flip()
    {
        if (!wallSliding)
        {
            isFacingRight = !isFacingRight;
            transform.Rotate(0f, 180f, 0f);
        }

    }
    private void ApplyMovement()
    {
        if (canMove)
        {
            if (isGrounded) //yürüme hızı
            {
                rigidbody2d.velocity = new Vector2(movementSpeed * x, rigidbody2d.velocity.y);
            }
            else if (!isGrounded && !wallSliding && x != 0) //havada yürüme hızı
            {
                Vector2 forceToAdd = new Vector2(movementForceInAir * x, 0f);
                rigidbody2d.AddForce(forceToAdd);

                if (Mathf.Abs(rigidbody2d.velocity.x) > movementSpeed) //havada yürüme hızını sınırlama
                {
                    rigidbody2d.velocity = new Vector2(movementSpeed * x, rigidbody2d.velocity.y);
                }
            }
            else if (!isGrounded && !wallSliding && x == 0) //havada durdugunda yürüme hızını azaltma
            {
                rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x * airDragMultiplier, rigidbody2d.velocity.y);
            }
        }

        if (wallSliding)
        {
            if (rigidbody2d.velocity.y < -wallSlideSpeed) //kayma hızını sınırlama
            {
                rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, -wallSlideSpeed);
            }
        }
    }
    private bool CheckOnWall()
    {
        if (onRightWall || onLeftWall)
        {
            return true;
        }
        else return false;
    }

    private bool IsTouchingLedge()
    {
        if ((leftLedgeCheck || rightLedgeCheck)) { return true; }
        else return false;
    }
    private void CheckLedgeClimb()
    {

        if (IsTouchingLedge() && !isClimbingLedge)
        {
            if (rightLedgeCheck && isFacingRight)
            {
                isClimbingLedge = true;
                ledgePos1 = new Vector2(Mathf.Floor(ledgePosBot.x + wallCheckDistance) - ledgeClimbXOffset1, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset1);
                ledgePos2 = new Vector2(Mathf.Floor(ledgePosBot.x + wallCheckDistance) + ledgeClimbXOffset2, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset2);
            }
            else if (leftLedgeCheck && !isFacingRight)
            {
                isClimbingLedge = true;
                ledgePos1 = new Vector2(Mathf.Ceil(ledgePosBot.x - wallCheckDistance) + ledgeClimbXOffset1, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset1);
                ledgePos2 = new Vector2(Mathf.Ceil(ledgePosBot.x - wallCheckDistance) - ledgeClimbXOffset2, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset2);
            }
            canMove = false;
            canFlip = false;
            

            animator.SetBool("isClimbingLedge", isClimbingLedge);
        }
        if (isClimbingLedge)
        {
            transform.position = ledgePos1; //animasyon başlar transformun ilk framei
        }
    }
    public void FinishLedgeClimb()
    {
        isClimbingLedge = false;
        transform.position = ledgePos2;
        canMove = true;
        canFlip = true;
        animator.SetBool("isClimbingLedge", isClimbingLedge);
    }
}


//private void CheckIfCanJump()
//{
//    if (isGrounded && rigidbody2d.velocity.y <= 0)
//    {
//        canJump = true;
//    }
//    else
//    {
//        canJump = false;
//    }
//}