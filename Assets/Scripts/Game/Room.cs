using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Extensions;

public class Room : MonoBehaviour
{
    /*
       SerializeFields
    */
    [SerializeField] private int sizeWidth = 10;
    [SerializeField] private int sizeHeight = 10;
    [SerializeField] private List<Block> blockPrefabsList = new List<Block>();
    [SerializeField] private List<Block> floorPrefabsList = new List<Block>();
    [SerializeField] private List<Item> itemsPrefabsList = new List<Item>();
    [SerializeField] private Player player = null;
    [SerializeField] private Door doorPrefab;

    /*
       Private fields
    */
    private List<Block> blocks = new List<Block>();
    private List<Item> items = new List<Item>();
    private Door door = null;
    private int roomIndex = 0;

    /*
       Public fields
    */
    public delegate void RoomChangedHandler(int newRoomIndex);
    public event RoomChangedHandler roomChangedEvent;

    /*
       Properties
    */
    public Player Player { get { return player; } }
    public GameObject PlayerObject { get { return player.gameObject; } }
    public int RoomIndex { get { return roomIndex; } }

    /*
        Unity events
    */
    void Awake() { instance = this; }

    void Start() { Generate(); }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) { RespawnDoor(); }
    }

    /*
        Public methods
    */
    public void RespawnItems()
    {
        foreach(Item item in items) 
        {
            if (item == null) { continue; }
            Destroy(item.gameObject); 
        }

        items.Clear();

        for (int i = 0; i < Random.Range(0, 5); i++)
        {
            int index = Random.Range(0, itemsPrefabsList.Count);
            Item item = itemsPrefabsList[index];
            Vector3 pos = new Vector3(Random.Range(-4f, 4f), Random.Range(-4f, 4f));
            Item newItem = item.Create(pos);
            newItem.Count = Random.Range(1, item.MaxStackSize);
            items.Add(newItem);
        }
    }
    public void RespawnDoor()
    {
        // Door creation
        Vector3 doorRotation = new Vector3(0, 0, 0);
        int lineX = Random.Range(0, 2) * 9;
        int lineY = Random.Range(0, 2) * 9;
        if (Random.Range(0, 2) == 0)
        {
            lineY = Random.Range(1, 9);
            doorRotation.z = lineX == 0 ? 270 : 90;
        }
        else
        {
            lineX = Random.Range(1, 9);
            doorRotation.z = lineY == 0 ? 0 : 180;
        }

        Vector3 doorPosition = new Vector3(lineX - 4.5f, lineY - 4.5f, (float) BlockLayer.WALL);
        Vector3 size = doorPrefab.GetComponent<BoxCollider2D>().size;
        RaycastHit2D[] hits = Physics2D.BoxCastAll(doorPosition, size, doorRotation.z, doorRotation);
        bool hasFloor = false;
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider == null) { continue; }
            if (hit.collider.gameObject == null) { continue; }
            if (door != null && hit.collider.name.StartsWith(door.Name)) { continue; }
            if (hit.collider.name.StartsWith(player.Name)) { continue; }
            if (hit.collider.name.StartsWith("Floor")) { hasFloor = true; continue; }
            Destroy(hit.collider.gameObject);
        }

        if (!hasFloor) 
        {
            int index = Random.Range(0, floorPrefabsList.Count);
            floorPrefabsList[index].Create(doorPosition, Quaternion.Euler(doorRotation));
        }

        if (door != null)
        {
            transform.SetPositionX(1f);
            door.transform.position = doorPosition;
            door.transform.rotation = Quaternion.Euler(doorRotation);
            door.setRotation(doorRotation.z);
            return;
        }

        door = (Door) doorPrefab.Create(doorPosition, Quaternion.Euler(doorRotation));
        door.setRotation(doorRotation.z);
    }

    public void Generate()
    {
        Vector3 position = new Vector3(sizeWidth/2 - 0.5f, sizeHeight/2 - 0.5f, 0);

        for (int y = 0; y < sizeHeight; y++)
        {
            for (int x = 0; x < sizeWidth; x++)
            {
                List<Block> blocks = 
                    (x % (sizeWidth - 1) == 0 || y % (sizeHeight - 1) == 0) 
                    ? blockPrefabsList : floorPrefabsList;
                int rotate = Random.Range(0, 4) * 90;
                int index = Random.Range(0, blocks.Count);
                blocks[index].Create(position, new Quaternion(0, 0, rotate, 0));
                position.x -= 1;
            }

            position.x = sizeWidth / 2 - 0.5f;
            position.y -= 1;
        }

        RespawnDoor();
        RespawnItems();
    }

    public void Regenerate()
    {
        player.GetComponent<Movement>().CanMove = false;
        int index = Random.Range(0, blockPrefabsList.Count);
        blockPrefabsList[index].Create(door.Location, door.Rotation);
        RespawnDoor();
        RespawnItems();

        Vector3 offset = new Vector3();
        if (door.Facing.Equals(BlockFacing.WEST)) {offset = new Vector3(-1.25f, 0, 0);}
        if (door.Facing.Equals(BlockFacing.EAST)) {offset = new Vector3(1.25f, 0, 0);}
        if (door.Facing.Equals(BlockFacing.SOUTH)) {offset = new Vector3(0, -1.25f, 0);}
        if (door.Facing.Equals(BlockFacing.NORTH)) {offset = new Vector3(0, 1.25f, 0);}
        player.Location = door.Location + offset;
        player.transform.SetPositionZ((int) BlockLayer.OBJECT);
        player.Rotation = door.Rotation;
        player.GetComponent<Movement>().Invoke("SwitchMove", 0.2f);
        player.NextPosition = player.transform.position;

        door.SwitchFreeze();
        door.Invoke("SwitchFreeze", 1f);

        roomIndex++;
        roomChangedEvent(roomIndex);
    }

    public void RemoveItem(Item item)
    {
        if (!items.Contains(item)) { return; }
        items.Remove(item);
        Destroy(item.gameObject);
    }

    /*
        Static
    */
    private static Room instance;
    public static Room Instance { get { return instance; } }
}
