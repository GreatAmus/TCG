using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CardDBInteractions : MonoBehaviour
{
    public InputField cardNameField;
    public InputField descriptionField;
    //public Text imagePath;
    public Toggle summon;
    public Toggle spell;
    public Dropdown elementDropdown;
    public Dropdown spellTypeDropdown;
    public InputField costField;
    public InputField lifeField;
    public InputField attackField;
    public InputField defenseField;
    public InputField cardMaxField;
    public InputField spellValueField;


    public void CallSaveCard() {
        StartCoroutine(SaveCard());
    }

    IEnumerator SaveCard () {
        Debug.Log("Creating form.");
        WWWForm form = new WWWForm();
        //form.AddField("deckID", null);
        form.AddField("cardName", cardNameField.text);
        form.AddField("description", descriptionField.text);
        //form.AddField("imagePath", );
        form.AddField("elementString", elementDropdown.options[elementDropdown.value].text);
        form.AddField("cost", costField.text);
        form.AddField("cardMax", cardMaxField.text);

        if (summon.isOn) {
            form.AddField("summon", 1);
            form.AddField("life", lifeField.text);
            form.AddField("attack", attackField.text);
            form.AddField("defense", defenseField.text);
        } else if (spell.isOn) {
            form.AddField("summon", 0);
            form.AddField("spellValue", spellValueField.text);
            form.AddField("spellType", spellTypeDropdown.value);
            Debug.Log(spellTypeDropdown.value);
        } else {
            Debug.Log("Error: no card type selected.");
        }

        //Send POST request
        using (UnityWebRequest www = UnityWebRequest.Post("https://southeja-unity-test.000webhostapp.com/SaveCard.php", form)) {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success) {
                Debug.Log("Webrequest result: " + www.error);
            } else {    //Request sent successfully
                var result = www.downloadHandler.text;
                //Display error
                if (result.Contains("Error:")) { //Check if card name already exists?
                    // errorText.text = result;
                    // errorText.gameObject.SetActive(true);
                } else {    //No error
                    Debug.Log(result);
                }
            }
        }

    }
}
