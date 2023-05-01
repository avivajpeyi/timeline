using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CursorManager : MonoBehaviour
{
    
    private static CursorManager _instance;
    public static CursorManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<CursorManager>();
            }

            return _instance;
        }
    }
    
    // hotspot
    private Vector2 defaultHotspot = Vector2.zero;
    private Vector2 handHotspot;

    private Texture2D defaultCursor;
    private Texture2D handOpen;
    private Texture2D handClosed;
    


    // Start is called before the first frame update
    void Start()
    {
        defaultCursor = Resources.Load("Cursors/hand-pointer") as Texture2D;
        handOpen = Resources.Load("Cursors/hand-open") as Texture2D;
        handClosed = Resources.Load("Cursors/hand-closed") as Texture2D;
        handHotspot = new Vector2(handOpen.width / 2, handOpen.height / 2);
        SetDefaultCursor();
    }


    public void SetHandCursor()
    {
        Cursor.SetCursor(handOpen, handHotspot, CursorMode.Auto);
    }

    public void SetDefaultCursor()
    {
        Cursor.SetCursor(defaultCursor, defaultHotspot, CursorMode.Auto);
    }

    public void SetClosedHandCursor()
    {
        Cursor.SetCursor(handClosed, handHotspot, CursorMode.Auto);
    }
    
    
    
    
    
}