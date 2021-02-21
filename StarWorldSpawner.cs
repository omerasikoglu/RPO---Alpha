using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarWorldSpawner : MonoBehaviour
{
    public ResourceTypeSO starType;

    private void Awake()
    {
        StarWorld.SpawnStarWorld(transform.position, starType);
        Destroy(gameObject);
    }
}
