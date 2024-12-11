using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LoadingGame : MonoBehaviour
{

    string jsonURL = "https://drive.google.com/uc?export=download&id=1mdyo7xCXSB6VbK8xClgcIYqdcDUxVU1O";

    void Start()
    {
        StartCoroutine(GetData(jsonURL));
    }

    IEnumerator GetData(string url)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            // success...
            Data data = JsonUtility.FromJson<Data>(request.downloadHandler.text);
            switch (data.id)
            {
                case 1: SceneManager.LoadScene(1); break;
                case 2: SceneManager.LoadScene(3); break;
            }
        }

        // Clean up any resources it is using.
        request.Dispose();
    }
}
public struct Data
{
    public int id;
}