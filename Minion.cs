//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Minion : MonoBehaviour, IHandleTargeting
//{

//    private enum State
//    {
//        Walking,
//        Knockback,
//        Dead
//    }

//    #region Dış Minnaklar
//    public MinionTypeSO minionType;
//    private HealthSystem healthSystem;
//    #endregion

//    #region İç Minnaklar
//    private Rigidbody2D rigidbody2d;
//    private Transform targetTransform;
//    #endregion

//    #region Interface Implements
//    public void HandleTargeting(Transform targetTransform)
//    {
//        this.targetTransform = targetTransform;
//    } 
//    #endregion
//    private void Start()
//    {
//        rigidbody2d = GetComponent<Rigidbody2D>();
//        healthSystem = GetComponent<HealthSystem>();

//        healthSystem.OnDied += HealthSystem_OnDied;
//    }
//    private void HealthSystem_OnDied(object sender, System.EventArgs e)
//    {
//        Destroy(gameObject);
//    }
//    private void Update()
//    {
//        HandleMovement();

//    }
//    private void HandleMovement()
//    {
//        if (targetTransform != null)
//        {


//            if (isMate)
//            {
//                rigidbody2d.velocity = Vector2.right * movementSpeed;
//            }
//            else if (!isMate)
//            {
//                rigidbody2d.velocity = Vector2.left * movementSpeed;
//            }
//            //DYNAMIC TARGETING
//            //Vector3 moveDir = (targetTransform.position - transform.position).normalized;
//            //rigidbody2d.velocity = moveDir * movementSpeed;
//        }
//        else //hedefi ölünce bu minyonun durması
//        {
//            Vector3 moveDir = new Vector3(0, 0, 0);

//            if (isMate)
//            {
//                if (BuildingManager.Instance.GetEnemyHQBuilding() != null)
//                {
//                    moveDir = ((BuildingManager.Instance.GetEnemyHQBuilding().transform.position) - transform.position).normalized;
//                }

//                else Destroy(gameObject);
//            }
//            else
//            {
//                if (BuildingManager.Instance.GetYourHQBuilding() != null)
//                {
//                    moveDir = ((BuildingManager.Instance.GetYourHQBuilding().transform.position) - transform.position).normalized;
//                }

//                else Destroy(gameObject);
//            }
//            rigidbody2d.velocity = moveDir * movementSpeed;
//            //rigidbody2d.velocity = Vector2.zero;
//        }
//    }

   

//}