using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    private static bool quitapplication = false;
    private static object _lock = new object();
    public static T instance
    {
        get
        {
            if (quitapplication)
            {
                return null;
            }
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = (T)FindObjectOfType(typeof(T));

                    if (_instance == null)
                    {
                        GameObject singleton = new GameObject();
                        _instance = singleton.AddComponent<T>();
                        singleton.name = "(singleton) " + typeof(T).ToString();
                        DontDestroyOnLoad(singleton);
                        singleton.GetComponent<Singleton<T>>().Init();
                    }
                }
                return _instance;
            }
        }
    }
    public abstract void Init();
    private void OnApplicationQuit()
    {
        quitapplication = true;
    }
    private void OnDestroy()
    {
        quitapplication = true;
    }
}
