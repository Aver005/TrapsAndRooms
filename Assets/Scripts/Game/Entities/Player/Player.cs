using System.Collections;
using Redcode.Extensions;
using System.Collections.Generic;
using UnityEngine;

public class Player : LivingEnity
{
    /*
        Serialize Fields
    */
    [SerializeField] private float hunger, thirst, fright = 0;

    /*
        Private Fields
    */
    private Movement movement;

    /*
        Public Fields
    */
    public delegate void HungerChangedHandler(float oldHunger, float newHunger);
    public delegate void ThirstChangedHandler(float oldThirst, float newThirst);
    public delegate void FrightChangedHandler(float oldFright, float newFright);
    public event HungerChangedHandler hungerChangedEvent;
    public event ThirstChangedHandler thirstChangedEvent;
    public event FrightChangedHandler frightChangedEvent;

    /*
        Properties
    */
    public float Hunger
    {
        get { return hunger; }
        set 
        { 
            if (hunger == value) { return; }
            hungerChangedEvent(hunger, value);
            hunger = value;
        }
    }

    public float Thirst
    {
        get { return thirst; }
        set
        {
            if (thirst == value) { return; }
            thirstChangedEvent(thirst, value);
            thirst = value;
        }
    }

    public float Fright
    {
        get { return fright; }
        set
        {
            if (fright == value) { return; }
            frightChangedEvent(thirst, value);
            fright = value;
        }
    }

    public Vector3 NextPosition
    {
        get { return movement.NextPosition; }
        set { movement.NextPosition = value; }
    }

    /*
        Public methods
    */

    /*
        Private methods
    */
    private void ChangeHunger() 
    { 
        Hunger += Random.Range(0.5f, 5f);
        Invoke("ChangeHunger", Random.Range(6f, 51f));
    }

    private void ChangeThirst() 
    {
        Thirst = Random.Range(0.5f, 5f);
        Invoke("ChangeThirst", Random.Range(4f, 38f));
    }

    private void ChangeFright() 
    { 
        Fright = Random.Range(0.5f, 5f);
        Invoke("ChangeFright", Random.Range(8f, 42f));
    }

    /*
        Unity events
    */
    private void Start()
    {
        movement = GetComponent<Movement>();
        Invoke("ChangeHunger", Random.Range(0.01f, 2f));
        Invoke("ChangeThirst", Random.Range(0.01f, 2f));
        Invoke("ChangeFright", Random.Range(0.01f, 2f));
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
