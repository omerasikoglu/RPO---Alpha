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
        WhiteStar,
    }
  
    #region Events
    private ResourceType resourceType;

    public event System.EventHandler OnResourceAmountChanged;

    public event EventHandler<OnWhichStarAchievedEventArgs> OnWhichStarAchieved;
    public class OnWhichStarAchievedEventArgs : EventArgs
    {
        public ResourceTypeSO resourceType;
    }
    #endregion

    //private LevelListSO levelList;
    //private LevelSO currentLevel;

    //private Dictionary<ResourceTypeSO, int> resourceAmountDictionary;

    [SerializeField] private List<ResourceData> startingResourceAmountList; //hile mode on

    private ResourceTypeListSO resourceTypeList; //her kaynağın (yıldız,altın,yeşil yıldız) toplam sayısı
    private ResourceTypeListSO starTypeList;
    [SerializeField] private LevelSO currentLevel;

    private Dictionary<ResourceTypeSO, bool> starAchievementsDictionary; //leveldaki 3 yıldızın kaçı alınmış
    //private List<ResourceTypeSO> starTypesList; //3 yıldızın türleri
    //private List<bool> achieveList; //yıldızların kazanımı
    private int total;

    private void Awake()
    {
        Instance = this;


        resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);
        //starTypeList = Resources.Load<ResourceTypeListSO>("StarTypeListSO");
        starAchievementsDictionary = new Dictionary<ResourceTypeSO, bool>(); //sadece 3 çeşit yıldız tutan dic
        // currentLevel = LevelManager.Instance.GetCurrentLevel();

        LoadStarAchievementsToDictionary();
        //SetRemainedResourceAmountToMaxAmount();
        HileModu();




        //resourceAmountDictionary = new Dictionary<ResourceTypeSO, int>();
        //levelList = Resources.Load<LevelListSO>(typeof(LevelListSO).Name);

        //foreach (ResourceTypeSO resourceType in resourceTypeList.list) //kaynak miktarlarını 0'lama
        //{
        //    resourceAmountDictionary[resourceType] = 0;
        //}

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
    public void AddResource(ResourceType resourceType, int resourceAmount)
    {
        //resourceAmountDictionary[resourceType] += resourceAmount;
        GetResourceTypeSO(resourceType).totalAmount += resourceAmount;
        if (IsStar(resourceType))
        {
            resourceTypeList.list[0].remainedAmount += 1; 
            resourceTypeList.list[0].totalAmount += 1; //total yıldız da 1 artar
        }
            OnResourceAmountChanged?.Invoke(this, EventArgs.Empty);

    }
    public void SpendResources(ResourceTypeSO resourceType, int resourceAmount) //TODO: belki birden fazla yıldızla açılabilen yer ekleyebilin
    {
        //resourceAmountDictionary[resourceType] -= resourceAmount;
        resourceType.totalAmount -= resourceAmount;
        OnResourceAmountChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetResourceAmount(ResourceTypeSO resourceType)
    {
        //return resourceAmountDictionary[resourceType];
        return resourceType.totalAmount;
    }
    public int GetRemainedResourceAmount(ResourceType resourceType)
    {
        return GetResourceTypeSO(resourceType).remainedAmount;
    }
    public void SetRemainedResourceAmount(ResourceType resourceType, int amount)
    {
        GetResourceTypeSO(resourceType).remainedAmount -= amount;
    }
    public ResourceTypeSO GetResourceTypeSO(ResourceType resourceType)
    {
        switch (resourceType)
        {
            case ResourceType.Star: return resourceTypeList.list[0];
            case ResourceType.Gold: return resourceTypeList.list[1];
            case ResourceType.GreenStar: return resourceTypeList.list[2];
            case ResourceType.BlueStar: return resourceTypeList.list[3];
            case ResourceType.RedStar: return resourceTypeList.list[4];
            case ResourceType.YellowStar: return resourceTypeList.list[5];
            case ResourceType.WhiteStar: return resourceTypeList.list[6];
            default: return resourceTypeList.list[0];
        }
    }

    /******* ONLY STAR STUFF *************/

    //public int GetRemainedStarPoint()
    //{
    //    return GetResourceTypeSO(ResourceType.Star).remainedAmount;
    //}
    public int GetTotalAchievedStarAmount()
    {
        return GetResourceAmount(GetResourceTypeSO(ResourceManager.ResourceType.Star));

        //int index = 0;
        //foreach (LevelSO level in levelList.list)
        //{
        //    foreach (bool achievedStars in level.achieveList)
        //    {
        //        if (achievedStars == true)
        //        {
        //            index++;
        //        }
        //    }
        //}
        //return index;
    }


    public void SetStarAchieved(ResourceType resourceType) //0-1-2
    {
        if (starAchievementsDictionary[GetResourceTypeSO(resourceType)] == false)// yıldız daha önceden alınmadıysa aktive olur
        {
            starAchievementsDictionary[GetResourceTypeSO(resourceType)] = true;
            if (starAchievementsDictionary.ContainsKey(GetResourceTypeSO(resourceType)))
            {
                //int index = LevelManager.Instance.GetIndexFromStar(GetResourceTypeSO(resourceType));
                //currentLevel.achieveList[index] = true;

                int index = 0;
                foreach (ResourceTypeSO st in currentLevel.starTypesList)
                {
                    if (GetResourceTypeSO(resourceType)  == st)
                    {
                        currentLevel.achieveList[index] = true;
                    }
                    index++;
                }
            }
            AddResource(resourceType, 1); //total yıldız için
            OnWhichStarAchieved?.Invoke(this, new OnWhichStarAchievedEventArgs { resourceType = GetResourceTypeSO(resourceType) }); //UI için
        }


    }

    public void SetCurrentLevel(LevelSO currentLevel)
    {
        this.currentLevel = currentLevel;
    }
    private void LoadStarAchievementsToDictionary()
    {
        int index = 0;
        foreach (ResourceTypeSO resourceType in currentLevel.starTypesList)
        {
            starAchievementsDictionary[resourceType] = currentLevel.achieveList[index];
            index++;
        }
    }
    private void SetRemainedResourceAmountToMaxAmount()
    {
        foreach (ResourceTypeSO resourceType in resourceTypeList.list)
        {
            //resourceType.remainedAmount = resourceType.totalAmount;
            resourceType.remainedAmount = resourceType.totalAmount - total;
        }

    }
    public void SetTotalUnlockedSkillPoint(int total)
    {
        this.total = total;
    }

    /******* TEST STUFF *************/
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.M))
        {
            //AddResource(ResourceType.Star, 1);
            foreach (var item in starAchievementsDictionary.Values)
            {
                Debug.Log(item);
            }
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            Debug.Log(GetResourceAmount(GetResourceTypeSO(ResourceType.Star)));
        }


    }
    private void HileModu()
    {
        foreach (ResourceData resourceAmount in startingResourceAmountList) //başlangıç fazla kaynak hile modu
        {
            AddResource(resourceAmount.resourceType, resourceAmount.amount);
        }
    }
    public bool IsStar(ResourceType resourceType)
    {
        switch (resourceType)
        {
            case ResourceType.Gold: return false;
            case ResourceType.GreenStar: return true;
            case ResourceType.BlueStar: return true;
            case ResourceType.RedStar: return true;
            case ResourceType.YellowStar: return true;
            case ResourceType.WhiteStar: return true;
            default: return false;
        }
    }
}
