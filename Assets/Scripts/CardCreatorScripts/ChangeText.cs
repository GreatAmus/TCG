using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeText : MonoBehaviour
{
    public InputField card;
    public Text lbl;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void ValueChange()
    {
        if (lbl != null && card != null)
            lbl.text = card.text.ToString();
    }

    // Update is called once per frame
    void Update()         
    {

    }

}
