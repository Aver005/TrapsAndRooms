using System.Collections;
using Redcode.Extensions;
using System.Collections.Generic;
using UnityEngine;

public class Food : Item
{
    /*
        Serialize Fields
    */
    [SerializeField] private float minSaturation, maxSaturation = 0;
    [SerializeField] private float minQuenching, maxQuenching = 0;

    /*
        Private Fields
    */

    /*
        Public Fields
    */

    /*
        Properties
    */
    public float MinQuenching
    {
        get { return minQuenching; }
        set { minQuenching = value; }
    }

    public float MaxQuenching
    {
        get { return maxQuenching; }
        set { maxQuenching = value; }
    }

    public float MinSaturation
    {
        get { return minSaturation; }
        set { minSaturation = value; }
    }

    public float MaxSaturation
    {
        get { return maxSaturation; }
        set { maxSaturation = value; }
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
