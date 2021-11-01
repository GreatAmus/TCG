using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.Networking;

// stackoverflow.com/questions/31765518/how-to-load-an-image-from-url-with-unity

public class AddImage : MonoBehaviour
{
    public Image portrait;
	private string path;

    public void GetImage()
    {

		Debug.Log("The path is: " + path);
//        Sprite newImage = LoadImage(path);
//		Debug.Log(newImage.name.ToString());
 //       if (newImage != null)
  //          portrait.sprite = newImage;
    }


	private Sprite LoadImage(string path)
    {
        if (path != null)
        {
            byte[] data = File.ReadAllBytes(path);
            Texture2D texture = new Texture2D(64, 64, TextureFormat.ARGB32, false);
            texture.LoadImage(data);
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(texture.width / 2, texture.height / 2));
            texture.name = Path.GetFileNameWithoutExtension(path);
            return sprite;
        }
        return null;
    }

    private IEnumerator getWebImage()
    {
        string path = "";
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(path);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.ConnectionError)
            Debug.Log(request.error);
        else
        {
            Texture2D tex = ((DownloadHandlerTexture)request.downloadHandler).texture;
            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(tex.width / 2, tex.height / 2));
            portrait.sprite = sprite;
        }
    }


}
