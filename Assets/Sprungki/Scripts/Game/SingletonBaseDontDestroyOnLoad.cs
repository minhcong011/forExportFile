using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonBaseDontDestroyOnLoad<T> : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = gameObject.GetComponent<T>();
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
}
