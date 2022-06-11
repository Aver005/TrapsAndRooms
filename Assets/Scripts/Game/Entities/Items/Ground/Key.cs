using System;
using System.Collections;
using Redcode.Extensions;
using System.Collections.Generic;
using UnityEngine;

public class Key : Item, IInteractable
{
    /*
        Serialize Fields
    */
    [SerializeField] private int doorIndex;
    [SerializeField] private KeyCode useKey = KeyCode.E;

    /*
        Private Fields
    */

    /*
        Public Fields
    */
    public void Use(Player whoUse)
    {

    }

    /*
        Properties
    */

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
        
    }

    private void Update()
    {
        if (Input.GetKeyUp(useKey)) { Use(Room.Instance.Player); }
    }

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
