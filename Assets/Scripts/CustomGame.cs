using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CustomGameData
{
    public int gameID;
    public string gameName;
    public string gameDescription;
    public string imagePath;
    public int startingHealth;
    public int gameTime;
}

public class CustomGame : MonoBehaviour
{
    public CustomGameData data;

    public void gameSetup (CustomGameData data) 
    {
        this.data = data;
    }
}
