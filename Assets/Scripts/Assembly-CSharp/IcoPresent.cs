// ILSpyBased#2
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class IcoPresent : MonoBehaviour
{
    private void Start()
    {
        base.StartCoroutine(this.GetRequest("https://www.google.com/"));
    }

    public void ShowRewad()
    {
        VariblesGlobal.ShowADrewad = true;
    }

    private void Update()
    {
        if (VariblesGlobal.ADrewadShowed == 1)
        {
            UnityEngine.Object.DestroyImmediate(base.gameObject);
        }
    }

    private IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            yield return (object)webRequest.SendWebRequest();
            int num = uri.Split('/').Length;
            if (webRequest.isNetworkError)
            {
                UnityEngine.Object.DestroyImmediate(base.gameObject);
            }
        }
    }
}


