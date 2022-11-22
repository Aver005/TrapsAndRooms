using System.Collections;
using System.Collections.Generic;
using Redcode.Extensions;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private KeyCode keyMoveUp = KeyCode.W;
    [SerializeField] private KeyCode keyMoveDown = KeyCode.S;
    [SerializeField] private KeyCode keyMoveRight = KeyCode.D;
    [SerializeField] private KeyCode keyMoveLeft = KeyCode.A;

    [SerializeField] private float moveSpeed = 0.5f;
    [SerializeField] private bool canMove = true;

    private BoxCollider2D boxCollider;
    private Vector3 nextPosition;
    private bool follow = false;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        if (!canMove) { return; }
        float speed = moveSpeed * Time.deltaTime;
        Vector3 newPosition = transform.position;
        Vector3 newRotation = transform.rotation.eulerAngles;

        int right = Utils.BoolToInt(Input.GetKey(keyMoveRight));
        int left = Utils.BoolToInt(Input.GetKey(keyMoveLeft));
        int up = Utils.BoolToInt(Input.GetKey(keyMoveUp));
        int down = Utils.BoolToInt(Input.GetKey(keyMoveDown));
        if (follow) { follow = right + left + up + down == 0; }

        if (Input.GetKey(keyMoveRight)) { newRotation.z = -90; }
        else if (Input.GetKey(keyMoveLeft)) { newRotation.z = 90; }
        if (Input.GetKey(keyMoveUp)) { newRotation.z = 0 + right * -45 + left * 45; }
        else if (Input.GetKey(keyMoveDown)) { newRotation.z = 180 + right * 45 + left * -45; }

        newPosition.x += speed * (right - left);
        if (follow)
        {
            float xOffset = Mathf.Abs(transform.position.x - NextPosition.x);
            if (xOffset > speed) 
            {
                newRotation.z = nextPosition.x > transform.position.x ? 270 : 0;
                newPosition.x += speed * (nextPosition.x > transform.position.x ? 1 : -1); 
            }
        }

        Collider2D collider = Physics2D.OverlapBox(newPosition, boxCollider.size, newRotation.z, Physics2D.AllLayers, (int)BlockLayer.WALL, (int)BlockLayer.WALL);
        if (collider == null) { transform.position = newPosition; }

        newPosition.y += speed * (up - down);
        if (follow) 
        {
            float yOffset = Mathf.Abs(transform.position.y - NextPosition.y);
            if (yOffset > speed)
            {
                newRotation.z = nextPosition.y > transform.position.y ? 0 : 180;
                newPosition.y += speed * (nextPosition.y > transform.position.y ? 1 : -1); 
            }
        }

        collider = Physics2D.OverlapBox(newPosition, boxCollider.size, newRotation.z, Physics2D.AllLayers, (int)BlockLayer.WALL, (int)BlockLayer.WALL);
        if (collider == null) { transform.position = newPosition; }
        transform.rotation = Quaternion.Euler(newRotation);

        if (follow && Vector2.Distance(NextPosition, transform.position) <= speed)
        {
            nextPosition = transform.position;
            follow = false;
        }
    }

    public void SwitchMove() { canMove = !canMove; }

    public bool CanMove
    {
        get { return canMove; }
        set { canMove = value; }
    }

    public float MoveSpeed
    {
        get { return moveSpeed; }
    }

    public Vector3 NextPosition
    {
        get { return nextPosition; }

        set
        {
            follow = true;
            nextPosition = value;
        }
    }
}
