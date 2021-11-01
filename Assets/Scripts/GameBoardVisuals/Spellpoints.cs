using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class Spellpoints : MonoBehaviour
{

    public Text SpellpointsText;
    public int testTotalSpellpoints;
    public int testAvailableSpellpoints;
    public Image[] SpellpointsImageArray;
    public bool[] isEnabled;

    public void newTurn()
    {
        testTotalSpellpoints += 1;
    }

    public void setTotalSpellpoints()
    {

        if (testTotalSpellpoints > SpellpointsImageArray.Length ||
            testTotalSpellpoints < 0)
            testTotalSpellpoints = SpellpointsImageArray.Length;

        for(int i = 0; i <SpellpointsImageArray.Length; i++)
        {
            if (i < testTotalSpellpoints)
                isEnabled[i] = true;
            else
                isEnabled[i] = false;
        }

        for (int i = 0; i < SpellpointsImageArray.Length; i++)
        {
            SpellpointsImageArray[i].enabled = isEnabled[i];
        }
    }

    public void setAvailableSpellpoints()
    {
        if (testAvailableSpellpoints > testTotalSpellpoints ||
            testAvailableSpellpoints < 0)
            testAvailableSpellpoints = testTotalSpellpoints;

        for(int i = 0; i < testTotalSpellpoints; i++)
        {
            if (i < testAvailableSpellpoints)
                SpellpointsImageArray[i].color = Color.white;
            else
                SpellpointsImageArray[i].color = Color.grey;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < SpellpointsImageArray.Length; i++)
        {
            isEnabled[i] = false;
        }

        setTotalSpellpoints();
        setAvailableSpellpoints();
        SpellpointsText.text = string.Format("{0} / {1}", 
            testAvailableSpellpoints.ToString(), testTotalSpellpoints.ToString());

    }
}
