using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWorld : MonoBehaviour
{
    public static ItemWorld SpawnItemWorld(Vector3 pos, Item item)
    {
        Transform transform = Instantiate(ItemAssets.Instance.pfItemWorld, pos, Quaternion.identity);

        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);

        return itemWorld;
    }

    [SerializeField] private Item.ItemType _itemType;

    public Item _item;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _item = new Item();
        _item._itemType = _itemType;
        SetItem(_item);
    }

    public void SetItem(Item item)
    {
        _item = item; 
        _spriteRenderer.sprite = item.GetSprite();
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
