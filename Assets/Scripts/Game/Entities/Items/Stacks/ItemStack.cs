using System.Collections;
using Redcode.Extensions;
using System.Collections.Generic;
using UnityEngine;

public class ItemStack : IInteractable
{
    /*
        Serialize Fields
    */

    /*
        Private Fields
    */
    private string name = string.Empty;
    private int count = 1;
    private int maxStackSize = 5;

    /*
        Public Fields
    */
    public delegate void ItemUsedHandler(ItemStack item);
    public ItemUsedHandler itemUsedEvent;

    /*
        Properties
    */
    public int Count
    {
        get { return count; }
        set { count = value; }
    }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public int MaxStackSize { get { return maxStackSize; } }

    /*
        Public methods
    */
    public ItemStack(Item item)
    {
        Name = item.Name;
        Count = item.Count;
        maxStackSize = item.MaxStackSize;
    }

    public virtual void Use(Player whoUse) { }

    /*
        Private methods
    */

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
