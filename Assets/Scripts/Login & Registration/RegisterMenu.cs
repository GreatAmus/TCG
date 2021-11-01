using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RegisterMenu : MonoBehaviour
{
    public static int USERNAME_LENGTH = 3;
    public static int PASSWORD_LENGTH = 5;

    public InputField usernameField;
    public InputField passwordField;
    public InputField confirmPasswordField;

    public Button registerButton;

    public Text errorText;

    public void CallCreateUser() {
        StartCoroutine(CreateUser());
    }

    IEnumerator CreateUser() {
        //Create form inputs for POST request
        WWWForm form = new WWWForm();
        form.AddField("username", usernameField.text);
        form.AddField("password", passwordField.text);

        //Send POST request
        using (UnityWebRequest www = UnityWebRequest.Post("https://southeja-unity-test.000webhostapp.com/RegisterUser.php", form)) {
            yield return www.SendWebRequest();
            
            if (www.result != UnityWebRequest.Result.Success) {
                Debug.Log(www.error);
            } else {    //Request sent successfully
                var result = www.downloadHandler.text;

                //User registered to database, navigate to main menu
                if (result == "Success.") {
                    Debug.Log("Registration form upload complete.");
                    SceneManager.LoadScene(0);
                } else {  //Username is taken, database has not been updated
                    errorText.text = result;
                    errorText.gameObject.SetActive(true);
                }
            }
        }
    }

    public void ValidateInputs() {
        errorText.text = "Passwords must match!";
        errorText.gameObject.SetActive(passwordField.text != confirmPasswordField.text);

        registerButton.interactable = (
            usernameField.text.Length >= USERNAME_LENGTH
            && passwordField.text.Length >= PASSWORD_LENGTH
            && passwordField.text == confirmPasswordField.text);
    }

    public void GoToLoginScene() {
        SceneManager.LoadScene(0);
    }

    
}
