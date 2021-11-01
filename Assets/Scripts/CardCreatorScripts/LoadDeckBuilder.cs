using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadDeckBuilder : MonoBehaviour
{
    public void LoadDeckCreator()
    {
        SceneManager.LoadScene("DeckCreator");
    }
}
