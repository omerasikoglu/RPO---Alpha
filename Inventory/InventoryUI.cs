using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class InventoryUI : MonoBehaviour
{
    private PlayerController player;
    private Inventory inventory;

    private Transform itemSlot; //inventory sayfa 1

    private float itemSpacing;
    private float itemCellSizeX;
    private float borderCellSizeY = 88f; // TODO: daha sonra starttan ön tanımlı yapabilirsin, hard code alert
    private float bgQuadCellSizeY = 74f;

    private Transform background;
    private Transform borderShadow;
    private Transform border;
    private Transform bgQuad;


    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;



    private void Awake()
    {
        itemSlot = transform.Find("itemSlot");

        background = itemSlot.Find("background");
        borderShadow = background.Find("borderShadow");
        border = background.Find("border");
        bgQuad = background.Find("bgQuad");

        itemSlotContainer = itemSlot.Find("itemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");

        itemCellSizeX = itemSlotContainer.GetComponent<GridLayoutGroup>().cellSize.x;
        itemSpacing = itemSlotContainer.GetComponent<GridLayoutGroup>().spacing.x;


    }
    private void Start()
    {
        UpdateSlotSizes();

    }
    public void SetPlayer(PlayerController player)
    {
        this.player = player;
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;

        inventory.OnItemListChanged += Inventory_OnItemListChanged;

        RefreshInventoryItems();
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems()
    {
        foreach (Transform child in itemSlotContainer)
        {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }

        //int x = 0;
        //int y = 0;
        //float itemSlotCellSize = 75f;
        foreach (Item item in inventory.GetItemList())
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);

            itemSlotRectTransform.transform.GetComponent<Button>().onClick.AddListener(() => { inventory.UseItem(item); });
            itemSlotRectTransform.transform.GetComponent<MouseEnterExitEvents>().OnRightClickEnter += (object senter, System.EventArgs e) =>
              {
                  Item duplicateItem = new Item { itemType = item.itemType, amount = item.amount };
                  inventory.RemoveItem(item);
                  ItemWorld.DropItem(player.GetPosition(), duplicateItem);
                  TooltipUI.Instance.Hide();
              };
            itemSlotRectTransform.transform.GetComponent<MouseEnterExitEvents>().OnMouseEnter += (object sender, System.EventArgs e) => { TooltipUI.Instance.Show($"<b><color=#FFFFFF>{item.itemType}</b></color>"); };
            itemSlotRectTransform.transform.GetComponent<MouseEnterExitEvents>().OnMouseExit += (object sender, System.EventArgs e) => { TooltipUI.Instance.Hide(); };

            //itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, -y * itemSlotCellSize);
            Image image = itemSlotRectTransform.Find("image").GetComponent<Image>();
            image.sprite = item.GetSprite();

            TextMeshProUGUI uiText = itemSlotRectTransform.Find("amountText").GetComponent<TextMeshProUGUI>();
            if (item.amount > 1)
            {
                uiText.SetText(item.amount.ToString());
            }
            else
            {
                uiText.SetText("");
            }

            //x++;
            //if (x >= 4)
            //{
            //    x = 0;
            //    y++;
            //}
        }
        UpdateSlotSizes();
    }

    private void UpdateSlotSizes()
    {
        UpdateBorderShadowSize();
        UpdateBGQuadSize();
        UpdateBorderSize();
    }
    private void UpdateBorderShadowSize()
    {
        int itemCount = inventory.GetItemAmount();
        if (itemCount == 0) borderShadow.gameObject.SetActive(false);
        else
        {
            borderShadow.transform.GetComponent<RectTransform>().sizeDelta = new Vector2
            (itemCount * itemCellSizeX + (itemCount + 1) * itemSpacing, borderCellSizeY);
            borderShadow.gameObject.SetActive(true);
        }
    }
    private void UpdateBGQuadSize()
    {
        int itemCount = inventory.GetItemAmount();
        if (itemCount == 0) bgQuad.gameObject.SetActive(false);
        else
        {
            bgQuad.transform.GetComponent<RectTransform>().sizeDelta = new Vector2
            (itemCount * itemCellSizeX + (itemCount) * itemSpacing, bgQuadCellSizeY);
            bgQuad.gameObject.SetActive(true);
        }


    }
    private void UpdateBorderSize()
    {
        int itemCount = inventory.GetItemAmount();
        if (itemCount == 0) border.gameObject.SetActive(false);
        else
        {
            border.transform.GetComponent<RectTransform>().sizeDelta = new Vector2
            (itemCount * itemCellSizeX + (itemCount + 1) * itemSpacing, borderCellSizeY);
            border.gameObject.SetActive(true);
        }
    }
}
