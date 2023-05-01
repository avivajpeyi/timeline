using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragableUiItem : MonoBehaviour, IDragHandler
{
    Canvas canvas;

    private void Start()
    {
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.localPosition += (Vector3)eventData.delta / canvas.scaleFactor;
    }
}
