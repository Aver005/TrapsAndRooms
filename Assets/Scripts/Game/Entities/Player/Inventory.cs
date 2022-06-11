using System;
using System.Collections;
using Redcode.Extensions;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    /*
        Serialize Fields
    */

    /*
        Private Fields
    */
    private int size = 4;
    private List<ItemStack> items = new List<ItemStack>();

    /*
        Public Fields
    */
    public delegate void InventoryNewItemHandler(Inventory inv, Item item, int slotID, bool isNewSlot);
    public InventoryNewItemHandler inventoryNewItemEvent;

    /*
        Properties
    */

    /*
        Public methods
    */
    public int GetFreeSlot(Item newItem)
    {
        int slotIndex = -1;
        foreach (ItemStack item in items)
        {
            slotIndex++;
            if (!item.Name.Equals(newItem.Name)) { continue; }
            if (item.Count + newItem.Count > item.MaxStackSize) { continue; }
            return slotIndex;
        }

        if (size == items.Count) { return -1; }
        return items.Count;
    }

    public void AddItem(Item newItem)
    {
        int slot = GetFreeSlot(newItem);
        if (slot == -1) { return; }

        ItemStack itemStack;
        int count = items.Count;
        if (slot == count) 
        {
            itemStack = new ItemStack(newItem);
            items.Add(itemStack); 
        }
        else
        {
            itemStack = items[slot];
            itemStack.Count += newItem.Count;
        }

        inventoryNewItemEvent(this, newItem, slot, items.Count > count);
        itemStack.itemUsedEvent += ItemUsedEvent;
    }

    public ItemStack Get(int index) { return items[index]; }

    /*
        Private methods
    */
    private void ItemUsedEvent(ItemStack item)
    {
        if (item.Count > 0) { return; }
        item.itemUsedEvent -= ItemUsedEvent;
        items.Remove(item);
    }

    /*
        Unity events
    */

    /*
        Static private fields
    */

    /*
        Static public fields
    */

    /*
        Static private methods
    */

    /*
        Static public methods
    */
}
