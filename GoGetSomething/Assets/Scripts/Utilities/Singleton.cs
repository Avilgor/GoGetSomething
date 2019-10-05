/**
 * Singleton.cs
 * Created by Akeru on 22/03/2019
 * Copyright © iBoo Mobile. All rights reserved.
 */

using UnityEngine;

/// <summary>
/// Inherit from this base class to create a singleton.
/// e.g. public class MyClassName : Singleton<MyClassName> {}
/// </summary>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    /// <summary>
    /// Access singleton instance through this propriety.
    /// </summary>
    public static T I
    {
        get;
        set;
    }

    protected virtual void Awake()
    {
        if (I == null)
        {
            I = (T)FindObjectOfType(typeof(T));
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public void Delete()
    {
        
    }
}
 