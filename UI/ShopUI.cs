using System;                                               //
using System.Collections;                                   // bunun item infolarını çekmesi lazım
using System.Collections.Generic;                           //
using UnityEngine;                                          //
using UnityEngine.UI;
using TMPro;

public class ShopUI : MonoBehaviour
{
    private Transform shopItemTemplate;

    private SaveSO saveFile;
    MouseEnterExitEvents mouseEnterExitEvents;

    //Dependencies
    private PlayerController player;

    private void Awake()
    {
        saveFile = Resources.Load<SaveSO>(typeof(SaveSO).Name);

        shopItemTemplate = transform.Find("ShopItemTemplate");
        shopItemTemplate.gameObject.SetActive(false);
    }
    private void Start()
    {
        foreach (Item item in saveFile.fullItemList)
        {
            Transform shopItemTransform = Instantiate(shopItemTemplate, transform);

            shopItemTransform.Find("button").GetComponent<Image>().sprite = item.GetShopButtonBG();
            shopItemTransform.Find("button").Find("itemText").GetComponent<TextMeshProUGUI>().SetText(item.GetString());
            shopItemTransform.Find("button").Find("priceText").GetComponent<TextMeshProUGUI>().SetText(item.GetPrice().ToString()+" Gold");
            shopItemTransform.Find("button").Find("itemBG").GetComponent<Image>().sprite = item.GetShopButtonBG();
            shopItemTransform.Find("button").Find("itemBG").Find("itemImage").GetComponent<Image>().sprite = item.GetSprite();
            shopItemTransform.gameObject.SetActive(true);

            shopItemTransform.Find("button").GetComponent<Button>().onClick.AddListener(() =>
            {
                player.GetInventory().AddItem(new Item { itemType=item.itemType,amount=1 });
              
                //saveFile.gottenItemList.Add(item);
            });

            mouseEnterExitEvents = shopItemTransform.Find("button").GetComponent<MouseEnterExitEvents>();
            mouseEnterExitEvents.OnMouseEnter += (object sender, System.EventArgs e) =>
            {
                Tooltip_ItemInfo.Instance.Show(item.GetString(), item.GetFeatureText(), item.GetDescription(), item.GetShopButtonBG());
                shopItemTransform.Find("button").GetComponent<Image>().color = Color.magenta;
            };
            mouseEnterExitEvents.OnMouseExit += (object sender, System.EventArgs e) =>
            {
                Tooltip_ItemInfo.Instance.Hide();
                shopItemTransform.Find("button").GetComponent<Image>().color = Color.white;
            };

        }

    }

    internal void SetPlayer(PlayerController player)
    {
        this.player = player;
    }
}

//Tooltip_ItemStatsUI.Instance.Show("hasan", "asdasd", "5", "4", "3", "2", "1", "2", "3");
//Tooltip_ItemStatsUI.Instance.Hide();

//public event System.EventHandler<OnItemSoldedEventArgs> OnItemSolded;
//public class OnItemSoldedEventArgs
//{
//    public Item item;
//}
//OnItemSolded?.Invoke(this, new OnItemSoldedEventArgs { item = item }); ;