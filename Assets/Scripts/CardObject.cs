using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

// This is the card object that is used during the game
// The card object holds the data for the cards during the game and for the deck creator. 
// Using this limits the number of database requests we need to make
[System.Serializable]
public class CardObjectData {
    public Definitions.ElementSet element;
    public string elementString;
    public string cardName;
    public string description;
    public bool summon = true;
    public int attack = 30;
    public int defense = 20;
    public int life = 15;
    public int cost = 50;
    public int spellValue = 4;
    public int spellType = 0;
    public Sprite portrait;
    public int cardCount = 0;
    public int cardMax = 5;

    //public GameObject CardDisplay;
}

public class CardObject : MonoBehaviour
{
    public CardObjectData data;

    public GameObject CardDisplay;



    public void summonSetup(CardObjectData data)
    {
        this.data = data;
        this.data.element = setElement(this.data.elementString);

    }

    public void spellSetup(CardObjectData data)
    {
        this.data = data;
        this.data.element = setElement(this.data.elementString);
    }

    public Definitions.ElementSet setElement(string elementString) {
        switch (elementString)
            {
                case "Air":
                    return Definitions.ElementSet.Air;
                case "Fire":
                    return Definitions.ElementSet.Fire;
                case "Earth":
                    return Definitions.ElementSet.Earth;
                case "Water":
                    return Definitions.ElementSet.Water;
            }
        return Definitions.ElementSet.Air; //If incompatible choice, default to Air.
    }

    public void createCard()
    {
        // Create a new CardDisplay object, which is a visual version of the card
        // Put it on the canvase of whatever scene is in view
        Transform top = GameObject.Find("Canvas").transform;
        CardDisplay = Instantiate(Resources.Load("CardDisplay")) as GameObject;
        CardDisplay.transform.SetParent(top);

        // Set the card's name
        Text foundName = CardDisplay.transform.Find("lblName").GetComponent<Text>();
        foundName.text = data.cardName;

        // Set the card description
        Text foundDescription = CardDisplay.transform.Find("lblDescription").GetComponent<Text>();
        foundDescription.text = data.description;

        // Set the card's element, changing the logo and border as appropriate 
        Image cardImageElement = CardDisplay.transform.Find("imgElement").GetComponent<Image>();
        Image cardImageBorder = CardDisplay.transform.Find("imgFrame").GetComponent<Image>();

        // Set the card cost
        Text foundCost = CardDisplay.transform.Find("lblCost").GetComponent<Text>();
        foundCost.text = data.cost.ToString();

        switch (data.element)
        {
            case Definitions.ElementSet.Air:
                cardImageElement.sprite = Definitions.airLogo;
                cardImageBorder.sprite = Definitions.airBorder;
                break;
            case Definitions.ElementSet.Fire:
                cardImageElement.sprite = Definitions.fireLogo;
                cardImageBorder.sprite = Definitions.fireBorder;
                break;
            case Definitions.ElementSet.Earth:
                cardImageElement.sprite = Definitions.earthLogo;
                cardImageBorder.sprite = Definitions.earthBorder;
                break;
            case Definitions.ElementSet.Water:
                cardImageElement.sprite = Definitions.waterLogo;
                cardImageBorder.sprite = Definitions.waterBorder;
                break;
        }

        // Display spell or summon specific info
        if (data.summon)
            summonDisplay();
        else
            spellDisplay();

    }

    private void summonDisplay()
    {
        Image cardImageType = CardDisplay.transform.Find("imgCardType").GetComponent<Image>();
        cardImageType.sprite = Definitions.summonImage;

        // Set the card attack
        Text foundAttack = CardDisplay.transform.Find("SummonPanel/lblAttack").GetComponent<Text>();
        foundAttack.text = data.attack.ToString();

        // Set the card life
        Text foundLife = CardDisplay.transform.Find("SummonPanel/lblLife").GetComponent<Text>();
        foundLife.text = data.life.ToString();

        // Set the card defense
        Text foundDefense = CardDisplay.transform.Find("SummonPanel/lblDefense").GetComponent<Text>();
        foundDefense.text = data.defense.ToString();

        //  Hide the spell panel for summons
        Image panel = CardDisplay.transform.Find("SpellPanel").GetComponent<Image>();
        panel.gameObject.SetActive(false);
    }

    private void spellDisplay()
    {

        // Show the spell icon
        Image cardImageType = CardDisplay.transform.Find("imgCardType").GetComponent<Image>();
        cardImageType.sprite = Definitions.spellImage;

        // Set the card's power
        Text foundSpell = CardDisplay.transform.Find("SpellPanel/lblSpell").GetComponent<Text>();
        foundSpell.text = data.spellValue.ToString();

        // Hide the summon panel for spells
        Image panel = CardDisplay.transform.Find("SummonPanel").GetComponent<Image>();
        panel.gameObject.SetActive(false);

    }
}
