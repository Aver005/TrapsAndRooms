using System;
using System.Collections;
using Redcode.Extensions;
using System.Collections.Generic;
using UnityEngine;

public class Activist : MonoBehaviour
{
    /*
        Serialize Fields
    */
    [SerializeField] private KeyCode interactKey = KeyCode.E;

    /*
        Private Fields
    */
    private Player player;
    private List<Item> itemsOnGround = new List<Item>();

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
    private void CheckInteract()
    {
        if (!Input.GetKeyUp(interactKey)) { return; }

        if (itemsOnGround.Count > 0)
        {
            Item item = itemsOnGround[0];
            bool pickedUp = player.PickUp(item);

            if (pickedUp) 
            {
                Destroy(item.gameObject);
                itemsOnGround.Remove(item);
            }
        }

        RaycastHit2D hit = Physics2D.Linecast(transform.position, transform.position + transform.up, Physics2D.AllLayers, (int)BlockLayer.WALL, (int)BlockLayer.WALL); ;
        if (hit.collider == null) { return; }
        Interactive interactive = hit.collider.gameObject.GetComponent<Interactive>();
        if (interactive == null) { return; }
        interactive.Use();
    }

    /*
        Unity events
    */
    private void Start() { player = Room.Instance.Player; }
    private void Update() { CheckInteract(); }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider == null) { return; }
        if (!Item.Is(collision.gameObject)) { return; }
        Item item = collision.gameObject.GetComponent<Item>();
        if (itemsOnGround.Contains(item)) { return; }
        itemsOnGround.Add(item);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider == null) { return; }
        if (!Item.Is(collision.gameObject)) { return; }
        Item item = collision.gameObject.GetComponent<Item>();
        if (!itemsOnGround.Contains(item)) { return; }
        itemsOnGround.Remove(item);
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
