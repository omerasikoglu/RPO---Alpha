using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    [SerializeField] private LayerMask whatIsDamageable;


    [SerializeField] private float attack1Radius;
    private Transform attack1HitBoxPos;
    [SerializeField] private float attack1Damage;
    [SerializeField] private float inputTimer;

    private bool combatEnabled = true;
    private bool gotInput;
    private bool isAttacking;
    private bool isFirstAttack;


    private float lastInputTime = Mathf.NegativeInfinity;


    private Animator animator;

    private void Awake()
    {
        attack1HitBoxPos = transform.Find("Attack1HitBoxPos");
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

    private void CheckAttackHitBox() //animatorden ulaşılıyo
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attack1HitBoxPos.position, attack1Radius, whatIsDamageable);

        foreach (Collider2D collider2d in detectedObjects)
        {
          
            HealthSystem healthSystem = collider2d.transform.parent.transform.GetComponent<HealthSystem>();
            if (healthSystem != null)
            {
                healthSystem.Damage(1);
            }
        }
    }
    private void FinishAttack1() //animatorden ulaşılıyo
    {
        isAttacking = false;
        animator.SetBool("isAttacking", isAttacking);
        animator.SetBool("attack1", false);
    }
}

//collider2d.transform.parent.GetComponent<HealthSystem>().SendMessage("Metodd");