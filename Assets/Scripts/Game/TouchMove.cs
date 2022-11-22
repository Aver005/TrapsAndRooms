using System;
using System.Collections;
using Redcode.Extensions;
using System.Collections.Generic;
using UnityEngine;

public class TouchMove : MonoBehaviour
{
    /*
        Serialize Fields
    */

    /*
        Private Fields
    */

    /*
        Public Fields
    */

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
        
    }

    private void OnMouseDown()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Room.Instance.Player.NextPosition = mousePos;
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
