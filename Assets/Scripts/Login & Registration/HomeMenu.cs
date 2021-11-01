using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeMenu : MonoBehaviour
{
    public Text usernameText;

    // Start is called before the first frame update
    void Start()
    {
        usernameText.text = "Welcome, " + UserManager.username + '!';
    }

    public void goToGameSelect() {
        SceneManager.LoadScene(3);
    }
}