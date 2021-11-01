// Class used to switch images depending on the type of element used by the card

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ElementSwitch : MonoBehaviour
{
    public Dropdown element;    // The type of element selected in the dropdown of the card creator
    public Image logo;          // The logo displayed on the card
    public Image border;        // Card border
    
    public void ChangeImage()
    {
        switch (element.value)
        {
            case (int)Definitions.ElementSet.Air:
                logo.sprite = Definitions.airLogo;
                border.sprite = Definitions.airBorder;
                break;
            case (int)Definitions.ElementSet.Fire:
                logo.sprite = Definitions.fireLogo;
                border.sprite = Definitions.fireBorder;
                break;
            case (int)Definitions.ElementSet.Earth:
                logo.sprite = Definitions.earthLogo;
                border.sprite = Definitions.earthBorder;
                break;
            case (int)Definitions.ElementSet.Water:
                logo.sprite = Definitions.waterLogo;
                border.sprite = Definitions.waterBorder;
                break;
        }
    }

    // Start is called before the first frame update. Loads the images associated with each element
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
