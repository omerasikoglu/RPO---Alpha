using System;                                                                      // 
using System.Collections;                                                          // 
using System.Collections.Generic;                                                  // 
using UnityEngine;                                                                 // 
                                                                                   // 
public class ResourceManager : MonoBehaviour                                       // 
{
    public ResourceData resourceAmount;

    public static ResourceManager Instance { get; private set; }

    public event System.EventHandler OnResourceAmountChanged;

    public Dictionary<ResourceTypeSO, int> resourceAmountDictionary; //hangi kaynaktan ne kadar var (her renk yıldızdan ne kadar ve altın)

    private LevelListSO levelList;

    [SerializeField] private List<ResourceData> startingResourceAmountList;
   

    private void Awake()
    {
       
        resourceAmountDictionary = new Dictionary<ResourceTypeSO, int>();

        ResourceTypeListSO resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);
        levelList = Resources.Load<LevelListSO>(typeof(LevelListSO).Name);

        Instance = this;

        foreach (ResourceTypeSO resourceType in resourceTypeList.list) //kaynak miktarlarını 0'lama
        {
            resourceAmountDictionary[resourceType] = 0;
        }
        foreach (ResourceData resourceAmount in startingResourceAmountList) //başlangıç fazla kaynak hile modu
        {
            AddResource(resourceAmount.resourceType, resourceAmount.amount);
        }
    }

    public bool HaveEnoughResource(ResourceTypeSO resourceType, int needingResourceAmount) //gereken kaynak sayısı
    {
        if (GetResourceAmount(resourceType) >= needingResourceAmount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void AddResource(ResourceTypeSO resourceType, int resourceAmount)
    {
        resourceAmountDictionary[resourceType] += resourceAmount;
        OnResourceAmountChanged?.Invoke(this, EventArgs.Empty);
    }
    public void SpendResources(ResourceTypeSO resourceType, int resourceAmount) //TODO: belki birden fazla yıldızla açılabilen yer ekleyebilin
    {
        resourceAmountDictionary[resourceType] -= resourceAmount;
        OnResourceAmountChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetResourceAmount(ResourceTypeSO resourceType)
    {
        return resourceAmountDictionary[resourceType];
    }
    public int GetTotalAchievedStarAmount()
    {
        int index = 0;
        foreach (LevelSO level in levelList.list)
        {
            foreach (bool achievedStars in level.achieveList)
            {
                if (achievedStars == true)
                {
                    index++;
                }
            }
        }
        return index;
    }
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.M))
        {
            AddResource(resourceAmount.resourceType,3);
            Debug.Log(GetResourceAmount(resourceAmount.resourceType));
        }
       

    }
    

}
