using System;                                               //yıldızlar vb.. toplanınca kaynak artırdıgı için Res Gen koydum
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarWorld : MonoBehaviour
{
    public event System.EventHandler OnStarAchieved; //level içi

    [SerializeField] ResourceData resourceData;
    //[SerializeField] bool isAchieved = false;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private LevelSO currentLevel;


    private void Awake()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = resourceData.resourceType.sprite;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            int index = 0;
            foreach (ResourceTypeSO starType in currentLevel.starTypesList)
            {
                if (starType == resourceData.resourceType)
                {
                    StarManager.Instance.SetStarAchieved(index, resourceData.resourceType, resourceData.amount);
                }
                index++;
            }
            Destroy(gameObject);
        }
    }

}
