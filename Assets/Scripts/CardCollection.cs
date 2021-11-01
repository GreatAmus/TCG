// https://stackoverflow.com/questions/57641745/how-can-i-save-the-data-of-some-variables-across-scenes
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class builds a collection of cards as a deck of card or as a sorted array.
// The class makes it easy to manage teh various cards being used

public class CardCollection : SingletonClass<CardCollection>
{
    // Stack for a deck of cards
    public Stack<CardObject> deck = new Stack<CardObject>();

    // Sorted list for deck creation
    public List<CardObject> sortedDeck = new List<CardObject>();
    public int currentCard;
        
    // This is a stub to create a new Deck. We'll want to delete this once we have real data
    public void createDeck(CardObjectData data, GameObject canvas)
    {
        float scale = 33.0f;
        for (int i = 0; i < 14; i++)
        {
            if (i % 2 == 0)
                Instance.deck.Push(addCard(scale, true, data, canvas));
            else
                Instance.deck.Push(addCard(scale, false, data, canvas));
        }
    }

    public void deleteCards()
    {
        foreach (CardObject c in sortedDeck)
        {
            Destroy(c.CardDisplay);
            c.CardDisplay = null;
        }
    }

    public CardObject updateDisplay(float scale, int index, GameObject canvas)
    {
        sortedDeck[index].createCard();
        scaleCard(sortedDeck[index].CardDisplay, scale, canvas);
        return sortedDeck[index];

    }
  
    public CardObject addCard(float scale, bool summon, CardObjectData data, GameObject canvas)
    {
        CardObject newCard;
        if (summon)
        {
            newCard = gameObject.AddComponent<CardObject>() as CardObject;
            newCard.summonSetup(data);
            newCard.createCard();
        }
        else
        {
            newCard = gameObject.AddComponent<CardObject>();
            newCard.spellSetup(data);
            newCard.createCard();

        }
        scaleCard(newCard.CardDisplay, scale, canvas);
        sortedDeck.Add(newCard);
        return newCard;
    }


    // Scale the image of the card
    // Useful if the scenes need different card sizes
    public void scaleCard(GameObject sizedObject, float scale, GameObject canvas)
    {
        scale = scale * canvas.GetComponent<RectTransform>().rect.width/1000;
//        scale = scale / 0.85f;
        float currentX = sizedObject.transform.localScale.x;
        float currentY = sizedObject.transform.localScale.y;
        sizedObject.transform.localScale = new Vector2(currentX * scale / 100, currentY * scale / 100);
    }
}
