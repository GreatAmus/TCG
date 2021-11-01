using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class GameSettings
{
    public string name;
    public int elementFactor;
}


public class GameEngine : MonoBehaviour
{
    GameSettings settings = new GameSettings();

    // Use one card to attack another. If the card is defeated, return 1 otherwise return 0
    public int attack(CardObject attacker, CardObject defender) 
    {
        bool weak = false;
        bool strong = false;
        if (attacker.data.element == Definitions.ElementSet.Air)
        {
            if (defender.data.element == Definitions.ElementSet.Fire)
                weak = true;
            else if (defender.data.element == Definitions.ElementSet.Earth)
                strong = true;
        }
        if (attacker.data.element == Definitions.ElementSet.Fire)
        {
            if (defender.data.element == Definitions.ElementSet.Water)
                weak = true;
            else if (defender.data.element == Definitions.ElementSet.Air)
                strong = true;
        }
        if (attacker.data.element == Definitions.ElementSet.Water)
        {
            if (defender.data.element == Definitions.ElementSet.Earth)
                weak = true;
            else if (defender.data.element == Definitions.ElementSet.Fire)
                strong = true;
        }
        if (attacker.data.element == Definitions.ElementSet.Earth)
        {
            if (defender.data.element == Definitions.ElementSet.Air)
                weak = true;
            else if (defender.data.element == Definitions.ElementSet.Water)
                strong = true;
        }
        int damage = attacker.data.attack - defender.data.defense;
        if (strong)
            damage += settings.elementFactor;
        else if (weak)
            damage -= settings.elementFactor;

        if (damage > 0)
            defender.data.life -= defender.data.life;

        if (defender.data.life <= 0)
            return 1;

        return 0;
    }
        // Start is called before the first frame update
        void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
