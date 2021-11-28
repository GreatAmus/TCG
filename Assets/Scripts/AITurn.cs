using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITurn : MonoBehaviour
{
    public static AITurn AITurnEngine;
    // Start is called before the first frame update
    void Start()
    {
        AITurnEngine = this;
    }

    public IEnumerator doAITurn()
    {
        Debug.Log(Time.time);
        yield return new WaitForSeconds(3.0f);
        while (playCards() || makeAttacks())
        {
            Debug.Log("doing stuff");
            yield return new WaitForSeconds(1.0f);
        }
        yield return new WaitForSeconds(2.0f);
        GameSettings.Instance.engine.EndTurn();
        Debug.Log(Time.time);
    }

    private IEnumerator Delay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    // Play Cards From Hand at random -> 
    private bool playCards()
    {
        Deck player = GameSettings.Instance.player2;
        GameObject area = GameObject.Find("EnemyArea/EnemySummons");
        foreach (CardObject card in GameSettings.Instance.player2.hand)
        {
            if (card.data.summon)
            {
                if (card.data.cost <= player.spellPoints 
                    && player.table.Count < GameSettings.Instance.maxSummons)
                {
                    // Play the card
                    Debug.Log("Playing a summon");
                    player.spellPoints -= card.data.cost;
                    player.table.Add(card);
                    player.hand.Remove(card);
                    card.CardDisplay.GetComponent<RectTransform>().SetParent(area.transform, true);
                    return true;
                }
            }
            
        }
        Debug.Log(Time.time);
        Debug.Log("No summons");
        return false;
    }

    
    private bool makeAttacks()
    {
        return false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
