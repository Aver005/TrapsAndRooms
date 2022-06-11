using System;
using System.Collections;
using Redcode.Extensions;
using System.Collections.Generic;
using UnityEngine;

public class Item : Entity
{
    /*
        Serialize Fields
    */
    [SerializeField] private int itemID;
    [SerializeField] private string description;
    [SerializeField] private int maxStackSize;

    /*
        Private Fields
    */
    private string displayName;
    private int count = 1;

    /*
        Public Fields
    */

    /*
        Properties
    */
    public int ID { get { return itemID; } }
    public string DisplayName { get { return displayName; } set { displayName = value; } }
    public string Description { get { return description; } set { description = value; } }
    public int MaxStackSize { get { return maxStackSize; } set { maxStackSize = value; } }
    public int Count
    {
        get { return count; }
        set { count = value < 0 ? 0 : value; }
    }

    /*
        Public methods
    */

    /*
        Private methods
    */

    /*
        Unity events
    */
    private void Start()
    {
        displayName = name;
        if (!itemByID.ContainsKey(ID)) { itemByID.Add(ID, this); }
    }

    /*
        Static private fields
    */
    private static Dictionary<int, Item> itemByID = new Dictionary<int, Item>();

    /*
        Static public fields
    */
    public static Item getByID(int ID) 
    {
        if (itemByID.ContainsKey(ID)) { return itemByID[ID]; }
        return null;
    }

    public static bool Is(GameObject obj) { return obj != null && obj.GetComponent<Item>() != null; }

    /*
        Static private methods
    */

    /*
        Static public methods
    */
}
