using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEnity : Entity
{
    /*
       Serialize Fields
    */

    [SerializeField] private float maxHP = 100f;
    [SerializeField] private float hp;

    /*
        Private Fields
    */
    private Inventory inventory = new Inventory();

    /*
        Public methods
    */
    public delegate void HealthChangedHandler(float oldHP, float newHP);
    public HealthChangedHandler healthChangedEvent;

    public virtual bool PickUp(Item item)
    {
        if (inventory.GetFreeSlot(item) == -1) { return false; }
        inventory.AddItem(item);
        return true;
    }

    /*
       Properties
    */

    public float MaxHP { get { return maxHP; } set { maxHP = value > 0 ? value : 0; } }
    public float HP 
    { 
        get { return hp; } 
        set
        {
            if (hp == value) { return; }
            float newHP = value > 0 ? value : 0;
            healthChangedEvent(hp, newHP);
            hp = newHP; 
        } 
    }
    public Inventory Inventory { get { return inventory; } }

    /*
        Static
    */

    public static bool Is(GameObject obj) { return obj != null && obj.GetComponent<LivingEnity>() != null; }
}
