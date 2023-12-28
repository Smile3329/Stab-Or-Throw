using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWorldSpawner : MonoBehaviour
{
    [SerializeField] private Item.ItemType _itemType;

    private bool _isSpawned()
    {
        int childCount = transform.childCount;
        if (childCount >= 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Start()
    {
        StartCoroutine(SpawnItem());
    }

    IEnumerator SpawnItem()
    {
        if (!_isSpawned())
        {
            ItemWorld itemWorld = ItemWorld.SpawnItemWorld(this.transform.position, new Item { _itemType = _itemType });
            itemWorld.transform.parent = transform;
        }
        yield return new WaitForSeconds(5f);
        StartCoroutine(SpawnItem());
    }
}
