
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{
    private Invenetory _inventory;

    private Transform _itemSlotContainer;

    private Transform _itemSlotTemplate;

    private void Awake()
    {
        _itemSlotContainer = transform.Find("ItemSlotContainer");
        _itemSlotTemplate = _itemSlotContainer.Find("itemSlotTemplate");
    }
    public void SetInventory(Invenetory inventory)
    {
        _inventory = inventory;
        RefreshInventoryItems();
    }

    public void RefreshInventoryItems()
    {       
        foreach (Transform child in _itemSlotContainer) 
        {
            if (child == _itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }
        
        int x = 0;
        int y = 0;
        float itemSlotCellSize = 200f;

        foreach (Item item in _inventory.GetItemsList())
        {
            RectTransform itemSlotRectTransform = Instantiate(_itemSlotTemplate, _itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);

            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
            Image image = itemSlotRectTransform.Find("image").GetComponent<Image>();
            image.sprite = item.GetSprite();

            x++;
            if (x >= 3)
            {
                x = 0;
                y--;
            }
        }
    }
}
