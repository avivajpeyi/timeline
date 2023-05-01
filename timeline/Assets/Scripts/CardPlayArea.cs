using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DanielLochner.Assets.SimpleScrollSnap;
using UnityEngine;

public class CardPlayArea : MonoBehaviour
{
    private CardDeck deck;
    private SimpleScrollSnap scrollSnap;
    public int cardsInPlayCount = 1;
    private AnswerEffects _answerEffects;
    private bool _answerEffectsInitialized = false;
    bool playFx = true;
    [SerializeField]private bool randomizedinit = true;
    
    

    void Start()
    {
        scrollSnap = FindObjectOfType<SimpleScrollSnap>();
        deck = FindObjectOfType<CardDeck>();
        if (deck == null)
        {
            deck = gameObject.AddComponent<CardDeck>();
        }
        scrollSnap.enabled = true;
        _answerEffects = AnswerEffects.Instance;
        if (_answerEffects != null)
        {
            _answerEffectsInitialized = true;
        }
        if (cardsInPlayCount > deck.Count) 
            cardsInPlayCount = deck.Count;
        DealCards();
    }


    public void AddCardLeft(Card newCard)
    {
        
        if (scrollSnap.NumberOfPanels == 0)
        {
            AddCard(newCard, 0);
        }
        else
        {
            AddCard(newCard, scrollSnap.CenteredPanel);    
        }
        
    }
    
    public void AddCardRight(Card newCard)
    {
        if (scrollSnap.NumberOfPanels == 0)
        {
            AddCard(newCard, 0);
        }
        else
        {
            AddCard(newCard, scrollSnap.CenteredPanel +1);    
        }
        
    }
    

    private void AddCard(Card newCard, int index)
    {
        Debug.Log($"Adding {newCard} to {index}");
        
        Card prevCard, nextCard;

        if (_answerEffectsInitialized)
            playFx = true;
        
        if (scrollSnap.NumberOfPanels == 0 || scrollSnap.Panels == null)
        {
            prevCard = null;
            nextCard = null;
            playFx = false;
        }
        else 
        {
            if (index == 0)
            {
                prevCard = null;
                nextCard = scrollSnap.Panels[index].GetComponent<Card>();
            }
            else if (index > scrollSnap.NumberOfPanels-1)
            {
                prevCard = scrollSnap.Panels[index - 1].GetComponent<Card>();
                nextCard = null;
            }
            else
            {
                prevCard = scrollSnap.Panels[index - 1].GetComponent<Card>();
                nextCard = scrollSnap.Panels[index].GetComponent<Card>();
            }
            
        }
        

        if (newCard.CheckCardsOrdered(prevCard, nextCard))
        {
            if (playFx)
                _answerEffects.PlayCorrectFx();
            scrollSnap.Add(newCard.gameObject, index);
            scrollSnap.Panels[index].GetComponent<Card>().SetData(newCard);
        }
        else
        {
            if (playFx)
            {
                _answerEffects.PlayIncorrectFx();
                _answerEffects.AddDiscardCard(newCard);
            }

        }
        
    }

    void DealCards()
    {

        List<Card> cards = deck.GetListOfCards(scrollSnap.transform.parent,cardsInPlayCount);
        
        for (int i = 0; i < cardsInPlayCount; i++)
        {
            AddCard(cards[i], i);
            Destroy(cards[i].gameObject);
        }
        
        if (scrollSnap.NumberOfPanels > 0)
        {
            StartCoroutine(SetPanelToMid());
        }
    }

    IEnumerator SetPanelToMid()
    {
        yield return new WaitForSeconds(0.05f);
        scrollSnap.GoToPanel(cardsInPlayCount/2);
    }
    
}