using System;
using System.Collections;
using Redcode.Extensions;
using System.Collections.Generic;
using UnityEngine;

public class PopupWindow : MonoBehaviour
{
    /*
        Serialize Fields
    */
    [SerializeField] protected string nameID = "";
    [SerializeField] protected Vector2 showPosition;
    [SerializeField] protected Vector2 hiddenPosition;
    [SerializeField] protected bool isShown = false;
    [SerializeField] protected string text = "";
    [SerializeField] private TMPro.TextMeshPro textMesh = null;

    /*
        Private Fields
    */
    private Vector2 nextPosition;

    /*
        Public Fields
    */

    /*
        Properties
    */
    public bool IsShown
    {
        get { return isShown; }
        set
        {
            if (value) { Show(); } else { Hide(); }
            isShown = value;
        }
    }

    /*
        Public methods
    */
    public void Switch() { IsShown = !isShown; }

    /*
        Private methods
    */
    protected void Show()
    {
        if (isShown) { return; }
        nextPosition = showPosition;
    }

    protected void Hide()
    {
        if (!isShown) { return; }
        nextPosition = hiddenPosition;
    }

    protected void UpdateText() { textMesh.text = text; }

    /*
        Unity events
    */
    protected void Start()
    {
        if (textMesh == null) { textMesh = GetComponentInChildren<TMPro.TextMeshPro>(); }
        nextPosition = isShown ? showPosition : hiddenPosition;

        windows.Add(nameID, this);
    }

    protected void Update()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        transform.SetPositionXY(x + (nextPosition.x - x) / 20, y + (nextPosition.y - y) / 20);
    }

    /*
        Static private fields
    */
    private static Dictionary<string, PopupWindow> windows = new Dictionary<string, PopupWindow>();

    /*
        Static public fields
    */


    /*
        Static private methods
    */

    /*
        Static public methods
    */
    public static void Switch(string name)
    {
        if (!windows.ContainsKey(name)) { return; }
        windows[name].Switch();
    }

    public static void Hide(string name)
    {
        if (!windows.ContainsKey(name)) { return; }
        if (windows[name].isShown) { windows[name].Switch(); }
    }

    public static void Clear() { windows.Clear(); }
}
