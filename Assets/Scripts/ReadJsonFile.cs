using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class ReadJsonFile : MonoBehaviour
{
    public struct Data
    {
        public string idGame;
    }
    string jsonURL = "https://drive.google.com/uc?export=download&id=1ZCfTcOCOrYl3z7V8FrPq_93yOihrCXVl";

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

        }
        else
        {
            Data data = JsonUtility.FromJson<Data>(request.downloadHandler.text);
            if (data.idGame == "1") SceneManager.LoadScene("Game1");
            if (data.idGame == "2") SceneManager.LoadScene("main");
        }

        // Clean up any resources it is using.
        request.Dispose();
    }
}
