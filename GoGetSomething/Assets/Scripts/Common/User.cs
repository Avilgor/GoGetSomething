/**
 * User.cs
 * Created by Akeru on 13/03/2019
 * Copyright © iBoo Mobile. All rights reserved.
 */

using System.Linq;
using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;

public class User : MonoBehaviour
{
    public static string Uuid
    {
        get { return ObscuredPrefs.GetString("Uuid"); }
        set { ObscuredPrefs.SetString("Uuid", value); }
    }

    public static void SetZoneCompletedQueue(string zoneId)
    {
        var list = ObscuredPrefsX.GetStringArray("ZonesCompletedQueue").ToList();
        if (!list.Contains(zoneId))
        {
            list.Add(zoneId);
            Debug.Log("Zone Completed Added in Queue: " + zoneId);
        }
        ObscuredPrefsX.SetStringArray("ZonesCompletedQueue", list.ToArray());
    }

    public static void SaveProcess()
    {
        var savedList = ObscuredPrefsX.GetStringArray("ZonesCompletedSaved").ToList();
        var queueList = ObscuredPrefsX.GetStringArray("ZonesCompletedQueue").ToList();

        for (int i = 0; i < savedList.Count; i++)
        {
            for (int j = 0; j < queueList.Count; j++)
            {
                if (!savedList[i].Contains(queueList[j]))
                {
                    savedList.Add(queueList[j]);
                    Debug.Log("Zone Completed Added in Saved: " + queueList[j]);
                }
            }
        }
        ObscuredPrefsX.SetStringArray("ZonesCompletedSaved", savedList.ToArray());
        Debug.Log("Process Saved");
    }
}