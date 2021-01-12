using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    [SerializeField] private LayerMask whatIsDamageable;

    private Transform attack1HitBoxPos;
    
    private bool combatEnabled;
    private bool gotInput;
    private bool isAttacking;
    private bool isFirstAttack;

    private float lastInputTime;
    private float inputTimer;

    private Animator animator;

    private void Awake()
    {
        attack1HitBoxPos = transform.Find("attack1HitBox");
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("canAttack", combatEnabled);
    }
    private void Update()
    {
        CheckCombatInput();
        CheckAttacks();
    }
    private void CheckCombatInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (combatEnabled)
            {
                gotInput = true;
                lastInputTime = Time.time;
            }
        }
    }
    private void CheckAttacks()
    {
        if (gotInput) //attack1 ile saldır
        {
            if (!isAttacking)
            {
                gotInput = false;
                isAttacking = true;
                isFirstAttack = !isFirstAttack;
                animator.SetBool("attack1", true);
                animator.SetBool("firstAttack", isFirstAttack);
                animator.SetBool("isAttacking", isAttacking);
            }
        }
        if (Time.time >= lastInputTime + inputTimer)
        {
            gotInput = false;
        }
    }

    private void CheckAttackHitBox()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(Vector2.zero, 1f);
    }










}
