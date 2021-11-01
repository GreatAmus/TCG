using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeckBuilder : MonoBehaviour
{
   
    private CardCollection cards;
    private int currentPage = 0;
    private int itemsPerPage = 10;
    private float scale = 63.0f;
    private GameObject[] panels = new GameObject[10];

    IEnumerator Start()
    {
        cards = CardCollection.Instance;
        for (int i = 0; i < 10; i++) 
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag("CardCount" + i.ToString());
            panels[i] = objects[0];
        }
        if (cards.sortedDeck.Count == 0)
        {
            yield return StartCoroutine(stubCreateCards());
        }
        else
        {
            cards.deleteCards();
            CardObject updatedCard;
            for (int i = 0; i < cards.sortedDeck.Count; i++)
            {
                updatedCard = cards.updateDisplay(scale, i, gameObject);
            }
        }
        displayCards();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void previous()
    {
        if (currentPage != 0)
        {
            currentPage -= 1;
            displayCards();
        }
    }

    public void next()
    {
        if ((currentPage+1) * itemsPerPage < cards.sortedDeck.Count)
        {
            currentPage += 1;
            displayCards();
        }
    }

    IEnumerator stubCreateCards()
    {
        string cardsJson = "";
        yield return StartCoroutine(getCardsFromDB(s => cardsJson = s));
        CardObjectData[] allCardData = JsonHelper.FromJson<CardObjectData>(cardsJson);
        CardObject newcard;
        for (int i = 0; i < allCardData.Length; i++)
        {
            newcard = cards.addCard(scale, allCardData[i].summon, allCardData[i], gameObject);
        }
    }

    public void displayCards()
    {
        int startRange = currentPage * itemsPerPage;
        int endRange = currentPage * itemsPerPage + itemsPerPage;
        int rows = itemsPerPage / 2;
        int totalWidth = 362;
        int spacing = 180;
        int y1 = 120;
        int y2 = -90;

        for (int i = 0 ; i < cards.sortedDeck.Count; i++) {
            if (i >= startRange && i < endRange) {
                int x1 = (i - startRange) * spacing - totalWidth;
                int x2 = ((i - startRange) - rows) * spacing - totalWidth;

                if (i - startRange < rows)
                    cards.sortedDeck[i].CardDisplay.transform.localPosition = new Vector2(x1, y1);
                else
                    cards.sortedDeck[i].CardDisplay.transform.localPosition = new Vector2(x2, y2);
                cards.sortedDeck[i].CardDisplay.SetActive(true);
            }
            else
            {
                cards.sortedDeck[i].CardDisplay.SetActive(false);
            }
        }

        UpdateCardCount();
    }

    public void UpdateCardCount()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("CardCount");
        int index = currentPage * itemsPerPage; 
        foreach (GameObject p in panels)
        {
            if (index < cards.sortedDeck.Count)
            {
                Text count = p.transform.Find("Edit/txtNumberofCards").GetComponent<Text>();
                count.text = cards.sortedDeck[index].data.cardCount.ToString() + "/" + cards.sortedDeck[index].data.cardMax.ToString();
                index++;
                p.SetActive(true);
            }
            else
            {
                p.SetActive(false);
            }
        }
        updateTotal();
    }

    public void IncrementCount(int index)
    {
        int arrayIndex = currentPage * itemsPerPage + index;
        if (cards.sortedDeck[arrayIndex].data.cardCount < cards.sortedDeck[arrayIndex].data.cardMax)
            updateCountDisplay(index, 1);
    }

    public void DecrementCount(int index)
    {
        int arrayIndex = currentPage * itemsPerPage + index;
        if (cards.sortedDeck[arrayIndex].data.cardCount > 0)
            updateCountDisplay(index, -1);
    }

    private void updateCountDisplay(int index, int change)
    {
        int actualIndex = currentPage * itemsPerPage + index;
        cards.sortedDeck[actualIndex].data.cardCount += change;
        Text count = panels[index].transform.Find("Edit/txtNumberofCards").GetComponent<Text>();
        count.text = cards.sortedDeck[actualIndex].data.cardCount.ToString() + "/" + cards.sortedDeck[actualIndex].data.cardMax.ToString();
        updateTotal();
    }

    private void updateTotal()
    {
        int total = 0;
        foreach (CardObject c in cards.sortedDeck) {
            total += c.data.cardCount;
        }
        Text size = GameObject.FindGameObjectsWithTag("DeckSize")[0].GetComponent<Text>();
        size.text = "Card Count: " + total.ToString();

    }

    IEnumerator getCardsFromDB(System.Action<string> json) {
        WWWForm form = new WWWForm();
        form.AddField("userID", UserManager.userID); //TODO: SENDING USERID DOES NOTHING YET
        
        using (UnityWebRequest www = UnityWebRequest.Post("https://southeja-unity-test.000webhostapp.com/GetCards.php", form)) {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success) {
                Debug.Log(www.error);
            } else {    //Request sent successfully
                json(www.downloadHandler.text);
            }
        }
    }

    public void LoadEditCard(int index)
    {
        CardCollection.Instance.currentCard = currentPage * itemsPerPage + index; ;
        SceneManager.LoadScene("CardCreator");
    }

}
