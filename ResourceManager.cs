using System;                                                                      // 
using System.Collections;                                                          // ! total star bölüm sonunda eklenmez  
using System.Collections.Generic;                                                  // 
using UnityEngine;                                                                 // 
                                                                                   // 
public class ResourceManager : MonoBehaviour                                       // 
{                                                                                  // 
    public static ResourceManager Instance { get; private set; }

    public event System.EventHandler OnResourceAmountChanged;

    public Dictionary<ResourceTypeSO, int> resourceAmountDictionary; //hangi kaynaktan ne kadar var (total yıldız ve total altın)

    private LevelListSO levelList;

    [SerializeField] private List<ResourceData> startingResourceAmountList;
    public ResourceData resourceAmount;


    private void Awake()
    {
       
        resourceAmountDictionary = new Dictionary<ResourceTypeSO, int>();
        ResourceTypeListSO resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);
        levelList = Resources.Load<LevelListSO>(typeof(LevelListSO).Name);

        Instance = this;

        foreach (ResourceTypeSO resourceType in resourceTypeList.list)
        {
            resourceAmountDictionary[resourceType] = 0;
        }
        foreach (ResourceData resourceAmount in startingResourceAmountList)
        {
            AddResource(resourceAmount.resourceType, resourceAmount.amount);
        }
    }

    public void SpendResources(ResourceTypeSO resourceType, int resourceAmount)
    {
        resourceAmountDictionary[resourceType] -= resourceAmount;
        OnResourceAmountChanged?.Invoke(this, EventArgs.Empty);
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
            AddResource(resourceAmount.resourceType,5);
            Debug.Log(GetResourceAmount(resourceAmount.resourceType));
        }
       

    }
    private void TestAddGold()
    {
        AddResource(resourceAmount.resourceType, 1);
        
       

    }

}
