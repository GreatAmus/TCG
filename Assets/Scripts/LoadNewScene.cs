using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewScene : MonoBehaviour

{

    public void LoadCardCreator()
    {
        CardCollection.Instance.currentCard = -1;
        SceneManager.LoadScene("CardCreator");
    }
    public void LoadDeckBuilder()
    {
        SceneManager.LoadScene("DeckCreator");
    }
    public void LoadHome()
    {
        Debug.Log("Loading Home");
        SceneManager.LoadScene("Home");
    }
    public void LoadGame()
    {
        Debug.Log("Loading Game");
        SceneManager.LoadScene("GameBoard");
    }
}
