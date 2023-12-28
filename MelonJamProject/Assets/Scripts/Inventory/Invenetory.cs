using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invenetory
{
    private List<Item> itemList;

    public Invenetory()
    {
        itemList = new List<Item>();
    }

    public void AddItem(Item item)
    {
        if (itemList.Count < 6)
        {
            itemList.Add(item);
        }
    }

    public List<Item> GetItemsList()
    {
        return itemList;
    }
}
