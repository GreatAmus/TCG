using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class GameDisplay : MonoBehaviour
{
    //public Game[] gameInstance;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetGames());
    }

    IEnumerator GetGames() {
        WWWForm form = new WWWForm();
        form.AddField("userID", UserManager.userID);
        
        using (UnityWebRequest www = UnityWebRequest.Post("https://southeja-unity-test.000webhostapp.com/GetGames.php", form)) {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success) {
                Debug.Log(www.error);
            } else {    //Request sent successfully
                var result = www.downloadHandler.text;
                //Display error
                if (result.Contains("Error:")) {

                } else {    //No error
                    Debug.Log(result);
                    CustomGameData[] gamesData = JsonHelper.FromJson<CustomGameData>(result);
                    CustomGame newGame = gameObject.AddComponent<CustomGame>() as CustomGame;
                    if (gamesData.Length > 0)
                    {
                        newGame.gameSetup(gamesData[0]);
                        Debug.Log("GameObjectGameName:" + newGame.data.gameName);
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
