using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Block, Interactive
{
    [SerializeField] private bool locked = false;
    [SerializeField] private bool freeze = false;
    public bool isLocked { get { return locked; } }
    public bool isFrozen { get { return freeze; } }

    public void SwitchFreeze() {freeze = !freeze;}

    public void Use()
    {
        if (!CanBeUsed()) { return; }
        Room.Instance.Regenerate();
    }

    public void OnMouseDown()
    {
        float distance = Vector2.Distance(Room.Instance.Player.transform.position, transform.position);
        if (distance > 0.9f) { return; }
        Use();
    }

    public bool CanBeUsed() { return !locked && !freeze; }
}
