
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{
    [SerializeField] ThrowPotion _throwPotion;
    [SerializeField] float maxX = 3;

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
        
        foreach (Transform child in _itemSlotContainer) { 
            if (child == _itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }
        

        int x = 0;
        int y = 0;
        float itemSlotCellSize = _itemSlotTemplate.GetComponent<RectTransform>().rect.height+20;

        foreach (Item item in _inventory.GetItemsList())
        {
            RectTransform itemSlotRectTransform = Instantiate(_itemSlotTemplate, _itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);

            itemSlotRectTransform.GetComponent<Button>().onClick.AddListener(delegate () {
                if (_throwPotion._canThrowPotion) {
                    _throwPotion.PotionAim(item, itemSlotRectTransform.transform.Find("image").GetComponent<Image>());
                    _inventory.RemoveItem(item);
                    RefreshInventoryItems();
                }
            });

            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
            Image image = itemSlotRectTransform.transform.Find("image").GetComponent<Image>();
            image.sprite = item.GetSprite();          

            x++;
            if (x >= maxX)
            {
                x = 0;
                y--;
            }
        }
    }
}
