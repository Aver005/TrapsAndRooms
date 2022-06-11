using System.Collections;
using System.Collections.Generic;
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

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        if (!canMove) { return; }
        Vector3 newPosition = transform.position;
        Vector3 newRotation = transform.rotation.eulerAngles;

        int right = Utils.BoolToInt(Input.GetKey(keyMoveRight));
        int left = Utils.BoolToInt(Input.GetKey(keyMoveLeft));
        int up = Utils.BoolToInt(Input.GetKey(keyMoveUp));
        int down = Utils.BoolToInt(Input.GetKey(keyMoveDown));

        if (Input.GetKey(keyMoveRight)) { newRotation.z = -90; }
        else if (Input.GetKey(keyMoveLeft)) { newRotation.z = 90; }
        if (Input.GetKey(keyMoveUp)) { newRotation.z = 0 + right * -45 + left * 45; }
        else if (Input.GetKey(keyMoveDown)) { newRotation.z = 180 + right * 45 + left * -45; }

        newPosition.x += moveSpeed * Time.deltaTime * (right - left);

        Collider2D collider = Physics2D.OverlapBox(newPosition, boxCollider.size, newRotation.z, Physics2D.AllLayers, (int)BlockLayer.WALL, (int)BlockLayer.WALL);
        if (collider == null) { transform.position = newPosition; }
        newPosition.y += moveSpeed * Time.deltaTime * (up - down);
        collider = Physics2D.OverlapBox(newPosition, boxCollider.size, newRotation.z, Physics2D.AllLayers, (int)BlockLayer.WALL, (int)BlockLayer.WALL);
        if (collider == null) { transform.position = newPosition; }
        transform.rotation = Quaternion.Euler(newRotation);
    }

    public void SwitchMove() { canMove = !canMove; }

    public bool CanMove
    {
        get { return canMove; }
        set { canMove = value; }
    }
}
