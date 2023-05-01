using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CardHand : MonoBehaviour
{
    public Transform slot;
    private Card cardHeld;
    private CardDeck deck;
    private CardPlayArea playArea;
    private AnswerEffects _answerEffects;
    private bool _answerEffectsInitialized = false;
    private bool slotFilled = false;

    void Awake()
    {
        SetInitialReferences();
    }


    void SetInitialReferences()
    {
        // find deck
        deck = FindObjectOfType<CardDeck>();
        if (deck == null)
        {
            deck = gameObject.AddComponent<CardDeck>();
        }

        playArea = FindObjectOfType<CardPlayArea>();
        _answerEffects = AnswerEffects.Instance;
        if (_answerEffects != null)
        {
            _answerEffectsInitialized = true;
        }
    }

    private void Update()
    {

        if (!slotFilled && !deck.IsEmpty)
        {
            DrawCard();
        }
        
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PlayCardLeft();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            PlayCardRight();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }

    

    /// <summary>
    /// Draw a card from the timeline data and add it to the hand
    ///
    /// Conditions:
    /// 1. The card must not already be in play (inHand/onBoard)
    /// 2. The hand card slots must not be full
    /// </summary>
    void DrawCard()
    {
        if (slotFilled)
        {
            return;
        }
        slotFilled = true;
        Debug.Log("Hand requesting card from deck");
        cardHeld = deck.GetNewCard(parent: slot);
        cardHeld.PrepForHand();
        deck.PrintSize();
        _answerEffects.UpdateDeckCount(deck.Count);
    }


    public void PlayCard(int side)
    {
        if (slotFilled)
        {
            cardHeld.PrepForBoard();
            if (side < 0)
            {
                playArea.AddCardLeft(cardHeld);    
            }
            else
            {
                playArea.AddCardRight(cardHeld);
            }
                
            
            Destroy(cardHeld.gameObject);
            cardHeld = null;
            slotFilled = false;
            
        }
    }

    public void PlayCardRight()
    {
        PlayCard(1);
    }
    public void PlayCardLeft()
    {
        PlayCard(-1);
    }
    
    
    
}