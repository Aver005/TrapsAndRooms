using System;
using System.Collections;
using Redcode.Extensions;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : PopupWindow
{
    /*
        Serialize Fields
    */

    /*
        Private Fields
    */
    private Player player;
    private List<SpriteRenderer> slots = new List<SpriteRenderer>();

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
    private new void Start()
    {
        base.Start();
        player = Room.Instance.Player;
        player.healthChangedEvent += PlayerHealthChangedEvent;
        player.hungerChangedEvent += PlayerHungerChangedEvent;
        player.thirstChangedEvent += PlayerThirstChangedEvent;
        player.frightChangedEvent += PlayerFrightChangedEvent;

        player.Inventory.inventoryNewItemEvent += InventoryNewItemEvent;
    }

    /*
        Custom events
    */
    private void InventoryNewItemEvent(Inventory inv, Item item, int slotID, bool isNewSlot)
    {
        GameObject slot;

        if (isNewSlot)
        {
            GameObject slotPrefab = Resources.Load<GameObject>("Prefabs/InventorySlot");
            slot = Instantiate(slotPrefab, gameObject.transform);
            SpriteRenderer newSlotRender = slot.GetComponent<SpriteRenderer>();
            Sprite sprite = item.GetComponent<SpriteRenderer>().sprite;
            Vector3 newPos = new Vector3(-1.74f + (1.14f * slots.Count), -1.24f, -1);

            slot.transform.localPosition = newPos;
            newSlotRender.sprite = sprite;

            slots.Add(newSlotRender);
        }
        else
        {
            slot = slots[slotID].gameObject;
        }

        slot.transform.GetChild(0).GetComponent<TMPro.TextMeshPro>().text = item.Name;
        slot.transform.GetChild(1).GetComponent<TMPro.TextMeshPro>().text = inv.Get(slotID).Count + " шт.";
    }

    private void PlayerHealthChangedEvent(float oldHealth, float newHealth)
    {
        text = text.Replace("Здоровье: " + oldHealth, "Здоровье: " + newHealth);
        UpdateText();
    }

    private void PlayerHungerChangedEvent(float oldHunger, float newHunger)
    {
        text = text.Replace("Голод: " + oldHunger, "Голод: " + newHunger);
        UpdateText();
    }

    private void PlayerThirstChangedEvent(float oldThirst, float newThirst)
    {
        text = text.Replace("Жажда: " + oldThirst, "Жажда: " + newThirst);
        UpdateText();
    }

    private void PlayerFrightChangedEvent(float oldFright, float newFright)
    {
        text = text.Replace("Страх: " + oldFright, "Страх: " + newFright);
        UpdateText();
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
