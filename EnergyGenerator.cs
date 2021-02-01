//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class EnergyGenerator : MonoBehaviour
//{
//    private ResourceGeneratorData resourceGeneratorData;
//    private float timer;
//    private float timerMax;

//    private void Awake()
//    {
//        resourceGeneratorData = GetComponent<CommanderTypeHolder>().commanderType.resourceGeneratorData;
//        timerMax = resourceGeneratorData.timerMax;
//        timer = timerMax;
//    }

//    private void Update()
//    {
//        timer -= Time.deltaTime;
//        if (timer <= 0)
//        {
//            timer += timerMax;
//            ResourceManagerManaEnergy.Instance.AddResource(resourceGeneratorData.resourceType, 1);    //Her timerMax snde 1 mana üretiyor
//        }
//    }
//}
