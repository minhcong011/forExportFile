using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LoadingGame : MonoBehaviour
{
    string jsonURL = "https://drive.google.com/uc?export=download&id=1pzLYbjQS8h2p08uRlt4JaqeAjXbag2rH";

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
            Debug.Log(request.error);
            SceneManager.LoadScene(1);
        }
        else
        {
            Data data = JsonUtility.FromJson<Data>(request.downloadHandler.text);
            switch (data.id)
            {
                case 1: SceneManager.LoadScene(1); break;
                case 2: SceneManager.LoadScene(3); break;
            }
        }

        request.Dispose();
    }
}
public struct Data
{
    public int id;
}
