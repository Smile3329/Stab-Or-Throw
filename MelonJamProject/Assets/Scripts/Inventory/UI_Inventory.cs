
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class UI_Inventory : MonoBehaviour
{
    [SerializeField] ThrowPotion _throwPotion;

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
        float itemSlotCellSize = 200f;

        foreach (Item item in _inventory.GetItemsList())
        {
            RectTransform itemSlotRectTransform = Instantiate(_itemSlotTemplate, _itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);

            itemSlotRectTransform.GetComponent<Button>().onClick.AddListener(delegate () {
                _throwPotion.PotionAim(item, itemSlotRectTransform.transform.Find("image").GetComponent<Image>());
                _inventory.RemoveItem(item);
                //_inventory.UseItem(item);
                RefreshInventoryItems();
            });

            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
            Image image = itemSlotRectTransform.transform.Find("image").GetComponent<Image>();
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
