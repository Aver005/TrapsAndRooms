using System.Collections;
using Redcode.Extensions;
using System.Collections.Generic;
using UnityEngine;

public class FoodStack : ItemStack
{
    /*
        Serialize Fields
    */

    /*
        Private Fields
    */
    private float saturation, quenching = 0;

    /*
        Public Fields
    */

    /*
        Properties
    */

    /*
        Public methods
    */
    public FoodStack(Food item) : base(item)
    {
        saturation = Random.Range(item.MinSaturation, item.MaxSaturation);
        quenching = Random.Range(item.MinQuenching, item.MaxQuenching);
    }

    public override void Use(Player whoUse) 
    {
        whoUse.Hunger += saturation;
        whoUse.Thirst += quenching;
        itemUsedEvent(this);
    }

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
