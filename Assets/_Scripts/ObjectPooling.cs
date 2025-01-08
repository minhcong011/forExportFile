using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPooling : SingletonBase<ObjectPooling>
{
    [SerializeField] public GameObject holder;
    [SerializeField] private GameObject uiHolder;
    private List<GameObject> objectInPool = new();
    public List<GameObject> meetTrash = new();
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
    public void CleanHolder()
    {
        GameObject[] objToClean = meetTrash.ToArray();
        for(int i = 0; i < objToClean.Length; i++)
        {
            Destroy(objToClean[i]);
        }
        meetTrash.Clear();
    }
}
