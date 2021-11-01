using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DisplayHand : MonoBehaviour
{

    public GameObject[] hand = new GameObject[2];
    private CardCollection cards;
    public GameObject firstSlot;
    public GameObject lastSlot;
    public float scale = 33.0f;

    // Start is called before the first frame update
    // Spaces cards evenly between the first card and last card
    // Works for any number of cards, but requires first and last card to be 
    // placed on the board.
    IEnumerator Start()
    {

        firstSlot = GameObject.Find("FirstSlot");
        lastSlot = GameObject.Find("LastSlot");
        cards = gameObject.AddComponent<CardCollection>() as CardCollection;
        yield return StartCoroutine(stubCreateCards());
        displayHand();
    }

    IEnumerator stubCreateCards()
    {
        string cardsJson = "";
        yield return StartCoroutine(getCardsFromDB(s => cardsJson = s));
        CardObjectData[] allCardData = JsonHelper.FromJson<CardObjectData>(cardsJson);
        // Debug.Log(cardsJson);

        CardObject newcard;
        for (int i = 0; i < allCardData.Length; i++)
        {
            // Debug.Log(allCardData[i].cardName);
            newcard = cards.addCard(scale, allCardData[i].summon, allCardData[i], gameObject);
            // Debug.Log(newcard.data.cardName);
        }
    }

    public void displayHand()
    {
        Vector3 firstSlotPos = firstSlot.transform.position;
        Vector3 lastSlotPos = lastSlot.transform.position;

        float xSpacing = (lastSlotPos.x - firstSlotPos.x) / (float)(hand.Length - 1);
        float ySpacing = 0;
        float zSpacing = (lastSlotPos.z - firstSlotPos.z) / (float)(hand.Length - 1);

        Vector3 spacing = new Vector3(xSpacing, ySpacing, zSpacing);

        // TODO change index
        for (int i = 0; i < 6; i++)
        {
            // Debug.Log("test");
            // Debug.Log(cards.sortedDeck[i].data.cardName);
            cards.sortedDeck[i].CardDisplay.SetActive(true);
            hand[i] = cards.sortedDeck[i].CardDisplay;
            hand[i].transform.position = firstSlotPos + (spacing * i);
        }
    }

    IEnumerator getCardsFromDB(System.Action<string> json)
    {
        WWWForm form = new WWWForm();
        form.AddField("userID", UserManager.userID); //TODO: SENDING USERID DOES NOTHING YET

        using (UnityWebRequest www = UnityWebRequest.Post("https://southeja-unity-test.000webhostapp.com/GetCards.php", form))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {    //Request sent successfully
                json(www.downloadHandler.text);
            }
        }
    }
    // Update is called once per frame

    void Update()
    {

    }

}
