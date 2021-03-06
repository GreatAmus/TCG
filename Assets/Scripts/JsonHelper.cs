//Wrapper class to enable unity's JsonUtility to deserialize a json array.
//This code used from https://stackoverflow.com/questions/36239705/serialize-and-deserialize-json-and-json-array-in-unity

using UnityEngine;

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        json = fixJson(json);
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }

    public static string fixJson(string value)
    {
    value = "{\"Items\":" + value + "}";
    return value;
    }
}