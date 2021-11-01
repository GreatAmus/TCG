// Change the type of card from summon to spell and vice versa

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeCardType : MonoBehaviour
{
    public Toggle summonButtonSelected;
    public Image cardTypeDisplay;
    public GameObject SummonEnterPanel;
    public GameObject SummonPanel;
    public GameObject SpellEnterPanel;
    public GameObject SpellPanel;


    public void ChangeType()
    {

    bool summonDisplayed;
        if (summonButtonSelected.isOn)
        {
            summonDisplayed = true;
            cardTypeDisplay.sprite = Definitions.summonImage;
        }
        else
        {
            summonDisplayed = false;
            cardTypeDisplay.sprite = Definitions.spellImage;
        }
        SummonEnterPanel.SetActive(summonDisplayed);
        SummonPanel.SetActive(summonDisplayed);
        SpellEnterPanel.SetActive(!summonDisplayed);
        SpellPanel.SetActive(!summonDisplayed);
    }

    void changeDisplay(GameObject img, GameObject info, bool display)
    {
        img.SetActive(display);
        info.SetActive(display);
    }

    // Start is called before the first frame update. Load the two card types
    void Start()
    {

        ChangeType();
    }

}
