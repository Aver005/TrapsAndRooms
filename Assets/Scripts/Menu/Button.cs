using System;
using System.Collections;
using Redcode.Extensions;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Button : MonoBehaviour
{
    /*
        Serialize Fields
    */
    [SerializeField] private string functionName;

    /*
        Private Fields
    */

    /*
        Public Fields
    */
    public void StartGame() { SceneManager.LoadScene(1); }
    public void GoMainMenu() { PopupWindow.Clear(); SceneManager.LoadScene(0); }
    public void OpenOrCloseInventory() { PopupWindow.Switch("Inventory"); }
    public void ShowTutorial() { PopupWindow.Switch("Tutorial"); }

    /*
        Properties
    */

    /*
        Public methods
    */

    /*
        Private methods
    */
    private void RoomChangedEvent(int newRoomIndex)
    {
        if (!functionName.Equals("ShowTutorial")) { return; }
        Room.Instance.roomChangedEvent -= RoomChangedEvent;
        PopupWindow.Hide("Tutorial");
        Destroy(gameObject);
    }

    /*
        Unity events
    */
    private void Start()
    {
        Room.Instance.roomChangedEvent += RoomChangedEvent;
    }

    private void OnMouseDown() { Invoke(functionName, 0.01f); }

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
