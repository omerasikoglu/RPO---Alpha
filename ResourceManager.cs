using System;                                                                      // 
using System.Collections;                                                          // 
using System.Collections.Generic;                                                  // 
using UnityEngine;                                                                 // 
                                                                                   // 
public class ResourceManager : MonoBehaviour                                       // 
{
    public static ResourceManager Instance { get; private set; }

    public enum ResourceType
    {
        None,
        //Global Resources
        Star,
        Gold,
        //In-game Resources
        GreenStar,
        BlueStar,
        RedStar,
        YellowStar,
    }
    private ResourceType resourceType;

    public event System.EventHandler OnResourceAmountChanged;

    public event EventHandler<OnStarAchievedEventArgs> OnWhichStarAchieved;
    public class OnStarAchievedEventArgs : EventArgs
    {
        public int achievedStarIndex;
    }

    private ResourceTypeListSO resourceTypeList;
    private LevelListSO levelList;
    private LevelSO currentLevel;

    private Dictionary<ResourceTypeSO, int> resourceAmountDictionary;

    [SerializeField] private List<ResourceData> startingResourceAmountList; //hile mode on

    private List<ResourceTypeSO> starTypesList; //3 yıldızın türleri
    private List<bool> achieveList; //yıldızların kazanımı

    private void Awake()
    {
        Instance = this;

        resourceAmountDictionary = new Dictionary<ResourceTypeSO, int>();

        resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);

        levelList = Resources.Load<LevelListSO>(typeof(LevelListSO).Name);

        

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
            AddResource(GetResourceTypeSO(ResourceType.Gold),3);
            Debug.Log(GetResourceAmount(GetResourceTypeSO(ResourceType.Gold)));
        }
       

    }
    public ResourceTypeSO GetResourceTypeSO(ResourceType resourceType)
    {
        switch (resourceType)
        {
            case ResourceType.Star:           return resourceTypeList.list[0];
            case ResourceType.Gold:           return resourceTypeList.list[1];
            case ResourceType.GreenStar:      return resourceTypeList.list[2];
            case ResourceType.BlueStar:       return resourceTypeList.list[3];
            case ResourceType.RedStar:        return resourceTypeList.list[4];
            case ResourceType.YellowStar:     return resourceTypeList.list[5];
            default:                          return resourceTypeList.list[0];
        }
    }
    public void SetStarAchieved(int achievedStarIndex, ResourceTypeSO resourceType) //0-1-2
    {
        if (!achieveList[achievedStarIndex]) //daha önceden yıldız alınmadıysa aktive olur
        {
            achieveList[achievedStarIndex] = true;
            ResourceManager.Instance.AddResource(resourceType, 1); //ResourceManager için
            OnWhichStarAchieved?.Invoke(this, new OnStarAchievedEventArgs { achievedStarIndex = achievedStarIndex }); //UI için
        }


    }
}
