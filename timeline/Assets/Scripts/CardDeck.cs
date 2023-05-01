using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardDeck : MonoBehaviour
{
    private GameObject cardPrefab;
    private TimelineData TimelineData;
    private AnswerEffects _answerEffects;
    
    void Awake()
    {
        cardPrefab = Resources.Load<GameObject>("Card");
        TimelineData = Resources.Load<TimelineData>("TimelineData");
        TimelineData.SetInitialReferences();
        _answerEffects = AnswerEffects.Instance;
    }

    public void PrintSize()
    {
        Debug.Log($"Deck size: {TimelineData.AvailableItemCount}/{TimelineData.ItemCount}");
    }

    public bool IsEmpty
    {
        get => TimelineData.AvailableItemCount == 0;
    }
    
    public int Count
    {
        get => TimelineData.AvailableItemCount;
    }
    
    /// <summary>
    /// Gets a new card that is not already in play
    /// </summary>
    /// <returns></returns>
    public Card GetNewCard(Transform parent,  bool randomize = true)
    {
        Debug.Log("Deck drawing card (randomize: " + randomize + ") -- parent: " + parent.name + "");
        
        if (IsEmpty) // Deck is now empty 
        {
            Debug.Log("Deck is empty");
            if (_answerEffects != null)
            {
                _answerEffects.EnableRestartText();
            }
            return null;
        }

        // Get a random item from the timeline data that is not 'inPlay'
        TimelineData.TimelineItem item = TimelineData.GetItem(randomize);
        GameObject cardGo = Instantiate(cardPrefab);
        Card card = cardGo.GetComponent<Card>();
        card.SetData(item);

        Transform cardT = cardGo.transform;
        cardT.SetParent(parent, false);

        return card;
    }
    
    public List<Card> GetListOfCards(Transform parent, int count)
    {
        List<Card> cards = new List<Card>(count);
        for (int i = 0; i < count; i++)
        {
            cards.Add(GetNewCard(parent, randomize:true));
        }
        // sort cards by year
        cards.Sort((a, b) => a.yearValue.CompareTo(b.yearValue));
        
        return cards;
    }
    

    

}