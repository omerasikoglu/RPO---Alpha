using System.Collections;                 // TODO: DICLERI YOK ET RESOURCE MANAGERDAN AYARLANIR YAP!
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
                                                        //Toplam yıldız ve altın sayın
public class ResourcesUI : MonoBehaviour
{
    private TextMeshProUGUI resourceAmountText;

    private Dictionary<ResourceTypeSO, Transform> mainResourcesTransformDictionary;
    private ResourceTypeListSO mainResourcesList;

    private void Awake()
    {
        mainResourcesList = Resources.Load<ResourceTypeListSO>("MainResourcesListSO");
        mainResourcesTransformDictionary = new Dictionary<ResourceTypeSO, Transform>();

        Transform resourceTemplate = transform.Find("ResourceTemplate");
        resourceTemplate.gameObject.SetActive(false);

        int index = 0;
        foreach (ResourceTypeSO resourceType in mainResourcesList.list) //kaynak miktarlarını 0'lama
        {
            Transform resourceTranform = Instantiate(resourceTemplate, transform);
            resourceTranform.gameObject.SetActive(true);

            resourceTranform.GetComponent<Image>().color = resourceType.GetColor();
            resourceTranform.Find("image").GetComponent<Image>().sprite = resourceType.sprite;
            resourceTranform.Find("text").GetComponent<TextMeshProUGUI>().text = ResourceManager.Instance.GetResourceAmount(resourceType).ToString();

            mainResourcesTransformDictionary[resourceType] = resourceTranform;

            MouseEnterExitEvents mouseEnterExitEvents = resourceTranform.GetComponent<MouseEnterExitEvents>();
            mouseEnterExitEvents.OnMouseEnter += (object sender, System.EventArgs e) => { TooltipUI.Instance.Show(resourceType.tooltipString); };
            mouseEnterExitEvents.OnMouseExit += (object sender, System.EventArgs e) => { TooltipUI.Instance.Hide(); };

            index++;
        }

        resourceAmountText = transform.Find("ResourceTemplate").Find("text").GetComponent<TextMeshProUGUI>();

    }
    private void Start()
    {
        ResourceManager.Instance.OnResourceAmountChanged += ResourceManager_OnResourceAmountChanged;
        UpdateResources();
    }

    private void ResourceManager_OnResourceAmountChanged(object sender, System.EventArgs e)
    {
        UpdateResources();
    }

    private void SetStarAmount(int starAmount)
    {
        resourceAmountText.text = "";
    }
    private int GetMaxStarAmount(int maxStarAmount)
    {
        //this.maxStarAmount = maxStarAmount;
        return maxStarAmount;
    }

    private void UpdateResources()
    {
        foreach (ResourceTypeSO resourceType in mainResourcesList.list)
        {
            Transform resourceTransform = mainResourcesTransformDictionary[resourceType];

            int resourceAmount = ResourceManager.Instance.GetResourceAmount(resourceType);
            resourceTransform.Find("text").GetComponent<TextMeshProUGUI>().SetText(resourceAmount.ToString());

        }

    }
}
