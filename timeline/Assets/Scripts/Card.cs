using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;



public class Card : MonoBehaviour
{
    private TimelineData.TimelineItem timelineItem;

    public TMPro.TextMeshProUGUI title;
    public TMPro.TextMeshProUGUI details;
    public TMPro.TextMeshProUGUI year;
    public UnityEngine.UI.Image image;
    public TMPro.TextMeshProUGUI source;
    private UnityEngine.UI.Image backgroundImage;
    
    public int yearValue => timelineItem.year;

    private float doubleClickTime = .2f;
    private float lastClickTime = 0;


    public override string ToString()
    {
        return $"{timelineItem.title}({timelineItem.year})]";
    }
    
    private void Awake()
    {
        backgroundImage = GetComponent<UnityEngine.UI.Image>();
    }

    public void SetData(TimelineData.TimelineItem item)
    {
        timelineItem = item;
        title.text = item.title;
        details.text = item.details;
        year.text = item.year.ToString();
        source.text = item.source;
        // StartCoroutine(LoadImage(item.image));
    }
    
    public void PrepForHand()
    {
        year.gameObject.SetActive(false);
    }
    
    public void PrepForBoard()
    {
        year.gameObject.SetActive(true);
    }
    
    public void SetData(Card c)
    {
        timelineItem = c.timelineItem;
        title.text = timelineItem.title;
        details.text = timelineItem.details;
        year.text = timelineItem.year.ToString();
        source.text = timelineItem.source;
        // StartCoroutine(LoadImage(timelineItem.image));
    }

    // /// <summary>
    // /// Load image from url and assign to image component.
    // /// If download fails, default image is used.
    // /// </summary>
    // /// <param name="url"></param>
    // /// <returns></returns>
    // IEnumerator LoadImage(string url)
    // {
    //     // Start a download of the given URL
    //     UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
    //     // Wait for download to complete
    //     yield return request.SendWebRequest();
    //     if (request.isNetworkError || request.isHttpError)
    //     {
    //         // assign default image
    //         // image.sprite = Resources.Load<Sprite>("default");
    //         yield break;
    //     }
    //
    //     // assign texture
    //     Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
    //     image.sprite = Sprite.Create(texture,
    //         new Rect(0, 0, texture.width, texture.height), Vector2.zero);
    // }
    

    public bool CheckCardsOrdered(Card past, Card future)
    {
        bool checkPassed = true;
        if (past != null && past.timelineItem.year > this.timelineItem.year)
        {
            checkPassed = false;
        }
        if (future != null && future.timelineItem.year < this.timelineItem.year)
        {
            checkPassed = false;
        }

        string col = "red";
        if (checkPassed)
            col= "green";
        
        Debug.Log(
            $"<color={col}>" +
            $"{past} < {this} < {future} = {checkPassed} " +
            "</color>"
        );

        return checkPassed;
    }
    
    public void FadeAndDelete()
    {
        Debug.Log("Fading out card (parent: " + transform.parent.name);
        StartCoroutine(FadeOutCoroutine());
    }
    
    IEnumerator FadeOutCoroutine()
    {
        
        float fadeTime = 10f;
        float elapsedTime = 0f;
        Color txtColor = title.color;
        Color backColor = backgroundImage.color;
        Color startColor = image.color;
        Color endColor = new Color(0, 0, 0, 0);
        while (elapsedTime < fadeTime)
        {
            backgroundImage.color = Color.Lerp(backColor, endColor, (elapsedTime / fadeTime)); 
            image.color = Color.Lerp(startColor, endColor, (elapsedTime / fadeTime));
            title.color = Color.Lerp(txtColor, endColor, (elapsedTime / fadeTime));
            year.color = Color.Lerp(txtColor, endColor, (elapsedTime / fadeTime));
            details.color = Color.Lerp(txtColor, endColor, (elapsedTime / fadeTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
    
    
}