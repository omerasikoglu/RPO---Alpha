using System;                                               //yıldızlar vb.. toplanınca kaynak artırdıgı için Res Gen koydum
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarWorld : MonoBehaviour
{
    public static StarWorld SpawnStarWorld(Vector2 position, ResourceTypeSO starType)
    {
        Transform transform = Instantiate(WorldAssets.Instance.pfStarWorld, position, Quaternion.identity);

        StarWorld starWorld = transform.GetComponent<StarWorld>();

        starWorld.SetStarType(starType);

        return starWorld;
    }

    private ResourceTypeSO starType;
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetStarType(ResourceTypeSO starType)
    {
        this.starType = starType;
        spriteRenderer.sprite = starType.sprite;
        spriteRenderer.color = starType.GetColor();
    }
    public ResourceTypeSO GetStarType()
    {
        return starType;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    //public event System.EventHandler OnStarAchieved; //level içi

    //[SerializeField] ResourceData resourceData;
    ////[SerializeField] bool isAchieved = false;
    //private SpriteRenderer spriteRenderer;
    //[SerializeField] private LevelSO currentLevel;


    //private void Awake()
    //{

    //    spriteRenderer = GetComponent<SpriteRenderer>();
    //    spriteRenderer.sprite = resourceData.resourceType.sprite;

    //}
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.transform.CompareTag("Player"))
    //    {
    //        int index = 0;
    //        foreach (ResourceTypeSO starType in currentLevel.starTypesList)
    //        {
    //            if (starType == resourceData.resourceType)
    //            {
    //                StarManager.Instance.SetStarAchieved(index, resourceData.resourceType, resourceData.amount);
    //            }
    //            index++;
    //        }
    //        Destroy(gameObject);
    //    }
    //}


}
