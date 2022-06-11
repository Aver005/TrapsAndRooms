using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Extensions;

public class Entity : MonoBehaviour
{
    /*
        Private fields
    */
    [SerializeField] protected string _Name = "";

    /*
        Properties
    */
    public Vector3 Location { get { return transform.position; } set { transform.position = value; } }
    public Quaternion Rotation { get { return transform.rotation; } set { transform.rotation = value; } }
    public float Angle
    {
        get { return Mathf.Abs(transform.eulerAngles.z); }
        set 
        { 
            if (value < 0) { value = 360 + value; }
            transform.SetEulerAnglesZ(value); 
        }
    }

    public string Name
    {
        get { return _Name; }
    }
}
