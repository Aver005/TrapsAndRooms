using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Redcode.Extensions;

public class Block : Entity
{
    /*
        Private fields
    */
    [SerializeField] protected BlockLayer layer = BlockLayer.WALL;
    [SerializeField] protected BlockFacing facing = BlockFacing.WEST;
    protected GameObject blocksParent = null;

    /*
        Properties
    */
    public BlockLayer Layer { get { return layer; } }
    public BlockFacing Facing { get { return facing; } }

    /*
        Public methods
    */
    public Block Create(Vector3 position, Quaternion rotation)
    {
        if (blocksParent == null) 
        {
            blocksParent = GameObject.Find("Blocks");
            if (blocksParent == null) { blocksParent = new GameObject(); }
        }

        position.z = (float) layer;
        GameObject obj = Instantiate(gameObject, position, rotation, blocksParent.transform);
        obj.name = Name + "-" + count;
        count++;
        return obj.GetComponent<Block>();
    }

    public void Rotate(BlockFacing facing)
    {
        this.facing = facing;
        transform.SetEulerAnglesZ((float)facing);
    }

    public void Rotate(float angle)
    {
        transform.SetEulerAnglesZ(RepairAngle(transform.rotation.z + angle));
    }

    public void setRotation(float angle)
    {
        transform.SetEulerAnglesZ(RepairAngle(angle));
    }

    /*
        Private methods
    */
    private float RepairAngle(float angle)
    {
        while (angle < 0) { angle += 360; }
        while (angle > 360) { angle -= 360; }
        if (angle < 90 || angle == 360) { this.facing = BlockFacing.NORTH; }
        else if (angle < 180) { this.facing = BlockFacing.WEST; }
        else if (angle < 270) { this.facing = BlockFacing.SOUTH; }
        else if (angle < 360) { this.facing = BlockFacing.EAST; }
        return angle;
    }

    /*
        Unity events
    */
    private void Awake() 
    {
        blocksParent = GameObject.Find("Blocks");
        if (blocksParent == null) { blocksParent = new GameObject(); }
    }

    /*
        Static fields
    */
    private static int count = 0;

    /*
        Static methods
    */

    public static bool Is(GameObject obj) { return obj != null && obj.GetComponent<Block>() != null; }
}
