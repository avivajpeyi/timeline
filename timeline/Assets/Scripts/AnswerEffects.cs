using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerEffects : MonoBehaviour
{

    [SerializeField]private Sprite cross;
    [SerializeField]private Sprite check;
    [SerializeField] UnityEngine.UI.Image image;
    [SerializeField] GameObject checkParticlePrefeb;
    [SerializeField] GameObject crossParticlePrefeb;
    [SerializeField] TMPro.TextMeshProUGUI deckCountTxt;
    [SerializeField] TMPro.TextMeshProUGUI discardCountTxt;
    [SerializeField] Transform discardPile;
    [SerializeField] private GameObject restartText;
    
    
    private int discardCount = 0;
    
    

    private void Awake()
    {
        image.enabled = false;
    }

    private void PlayFx(GameObject fxPrefab, Sprite s)
    {
        GameObject go = Instantiate(fxPrefab, transform.position, Quaternion.identity);
        Destroy(go, 1);
        StartCoroutine(FadeOut(s));
    }

    public void PlayIncorrectFx()
    {
        PlayFx(crossParticlePrefeb, cross);
    }
    
    public void PlayCorrectFx()
    {
        PlayFx(checkParticlePrefeb, check);
    }

    // coroutine to fade out image
    IEnumerator FadeOut(Sprite s)
    {
        image.enabled = true;
        image.sprite = s;
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime;
            image.color = new Color(1, 1, 1, 1 - t);
            yield return null;
        }
        image.enabled = false;
    }

    private static AnswerEffects _instance;
    public static AnswerEffects Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<AnswerEffects>();
            }

            return _instance;
        }
    }
    
    public void UpdateDeckCount(int count)
    {
        deckCountTxt.text = $"Deck: {count}";
    }
    
    public void AddDiscardCard(Card discardCard)
    {
        discardCount += 1;
        discardCountTxt.text = $"Discarded: {discardCount}";
        Card c = Instantiate(discardCard.gameObject, discardPile).GetComponent<Card>();
        c.PrepForBoard();
        c.FadeAndDelete();
    }


    public void EnableRestartText()
    {
        restartText.SetActive(true);
    }

}
