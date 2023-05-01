using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnlargeOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public Vector3 enlargeScale = new Vector3(1.5f, 1.5f, 1.5f); 
    Vector3 cachedScale;
 
    void Start() {
 
        cachedScale = transform.localScale;
    }
 
 
    public void OnPointerEnter(PointerEventData eventData) {
        Debug.Log($"OnPointerEnter -- {gameObject.name}");
        transform.localScale = enlargeScale;
    }
 
    public void OnPointerExit(PointerEventData eventData) {
 
        transform.localScale = cachedScale;
    }
}
