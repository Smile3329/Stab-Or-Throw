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

    private Item _item;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetItem(Item item)
    {
        this._item = item; 
        _spriteRenderer.sprite = item.GetSprite();
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
