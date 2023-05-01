using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeCurosrOnDrag : MonoBehaviour
{
    [SerializeField] private bool isInDraggingArea = false;
    CursorManager cursorManager;
    
    void OnMouseOver()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        Debug.Log("Mouse is over GameObject.");
    }
    
    void OnMouseExit()
    {
        //The mouse is no longer hovering over the GameObject so output this message each frame
        Debug.Log("Mouse is no longer on GameObject.");
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        cursorManager = CursorManager.Instance;
    }
    
    
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown from ChangeCurosrOnDrag.cs");
        if (isInDraggingArea)
        {
            cursorManager.SetClosedHandCursor();
        }
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        if (isInDraggingArea)
        {
            cursorManager.SetHandCursor();
        }
        else
        {
            cursorManager.SetHandCursor();
        }
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        isInDraggingArea = true;
        cursorManager.SetHandCursor();
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        isInDraggingArea = false;
        cursorManager.SetDefaultCursor();
    }
}