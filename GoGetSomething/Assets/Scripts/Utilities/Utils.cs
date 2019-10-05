/**
 * Utils.cs
 * Created by Akeru on 21/03/2019
 * Copyright © iBoo Mobile. All rights reserved.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public static class Utils
{

    public static void SetLayer(this Transform trans, int layer, bool childIncluded = true)
    {
        trans.gameObject.layer = layer;
        if(childIncluded)
            foreach (Transform child in trans)
            child.SetLayer(layer);
    }

    private static Random _rng = new Random();
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public static IEnumerator CheckInternetConnection(Action<bool> action)
    {
        WWW www = new WWW("http://google.com");
        yield return www;
        if (www.error != null)
        {
            action(false);
        }
        else
        {
            action(true);
        }
    }


}
