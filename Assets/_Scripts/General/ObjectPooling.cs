using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPooling : SingletonBase<ObjectPooling>
{
    [SerializeField] private GameObject holder;
    [SerializeField] private GameObject uiHolder;
    private List<GameObject> objectInPool = new();
    // Start is called before the first frame update
    public GameObject CreateObject(GameObject objToCreate)
    {
        GameObject newObj = Create(objToCreate);
        newObj.transform.SetParent(holder.transform, false);
        return newObj;
    }
    public GameObject CreateUIObject(GameObject objToCreate)
    {
        GameObject newObj = Create(objToCreate);
        newObj.transform.SetParent(uiHolder.transform, false);
        return newObj;
    }
    public GameObject Create(GameObject objToCreate)
    {
        foreach (GameObject obj in objectInPool)
        {
            if (!obj.activeSelf && $"{objToCreate.name}(Clone)" == obj.name)
            {
                obj.SetActive(true);
                return obj;
            }
        }
        GameObject newObj = Instantiate(objToCreate);
        objectInPool.Add(newObj);
        return newObj;
    }
}
