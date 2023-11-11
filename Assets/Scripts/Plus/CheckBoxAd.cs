using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBoxAd : MonoBehaviour
{
    public GameObject adController;
    public GameObject checkBoxAdContainer;
    private float timeToHide;
    public CheckBoxAd[] arrCheckBoxAd = new CheckBoxAd[100];
    private void Start()
    {
        adController = GameObject.Find("AdmodController");
        checkBoxAdContainer = GameObject.Find("CheckBoxAdContainer");
        //gameObject.transform.SetParent(checkBoxAdContainer.transform);
        arrCheckBoxAd = FindObjectsOfType<CheckBoxAd>();
        timeToHide = adController.GetComponent<adcontroller>().timeToHideAd;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PlayerHead"))
        {
            Debug.Log("Show Ad with Tag");
            adController.GetComponent<adcontroller>().RequestInterstitial();
            HideAllCheckBoxAd();
            Invoke(nameof(ShowAllCheckBoxAd), timeToHide);
        }      
    }   
    public void HideAllCheckBoxAd()
    {
        foreach (var item in arrCheckBoxAd)
        {
            item.GetComponent<BoxCollider>().enabled = false;
        }
    }
    public void ShowAllCheckBoxAd()
    {
        foreach (var item in arrCheckBoxAd)
        {
            item.GetComponent<BoxCollider>().enabled = true;
        }
    }    
}
