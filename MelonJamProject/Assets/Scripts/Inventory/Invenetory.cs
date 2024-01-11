using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invenetory
{
    private List<Item> itemList;
    private Action<Item> useItemAction;
    private float maxItems;

    public Invenetory(Action<Item> useItemAction, float maxItems)
    {
        this.useItemAction = useItemAction;
        this.maxItems = maxItems;
        itemList = new List<Item>();
    }

    public bool AddItem(Item item)
    {
        if (itemList.Count < maxItems)
        {
            itemList.Add(item);
            return true;
        }
        return false;
    }

    public void RemoveItem(Item item)
    {
        itemList.Remove(item);
    }

    public List<Item> GetItemsList()
    {
        return itemList;
    }

    public void UseItem(Item item)
    {
        useItemAction(item);
    }
}
