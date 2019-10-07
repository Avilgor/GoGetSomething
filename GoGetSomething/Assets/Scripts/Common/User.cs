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
    public static int Firedust
    {
        get { return ObscuredPrefs.GetInt("Firedust", 3); }
        set
        {
            if (value < 0) value = 0;
            ObscuredPrefs.SetInt("Firedust", value);
            Debug.Log("Total Firedusts: "+Firedust);
        }
    }

    public static void SetBonfireFired(int bonfireId, Vector2 playerPosition)
    {
        var realId = "Bonfire"+bonfireId;

        var list = ObscuredPrefsX.GetStringArray("BonfireFired").ToList();
        if (!list.Contains(realId))
        {
            list.Add(realId);
            Debug.Log("Bonfire Fired: " + realId);
        }
        ObscuredPrefsX.SetStringArray("BonfireFired", list.ToArray());

        ObscuredPrefsX.SetVector2("PlayerPositionSaved", playerPosition);
    }

    public static Vector2 LastSavedPlayerPosition()
    {
        return ObscuredPrefsX.GetVector2("PlayerPositionSaved", Vector2.zero);
    }

    public static bool IsBonfireFired(int bonfireId)
    {
        var realId = "Bonfire"+bonfireId;
        var list = ObscuredPrefsX.GetStringArray("BonfireFired").ToList();

        Debug.Log("Bonfire: " + bonfireId);
        Debug.Log("list.Contains(realId): " + list.Contains(realId));

        return list.Contains(realId);
    }

    public static void ClearZonesQueued()
    {
        ObscuredPrefsX.SetStringArray("ZonesCompletedQueue", new string[0]);
        Debug.Log("Zones Queued Cleared");
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

    public static bool IsZoneCompleted(string zoneId)
    {
        var list = ObscuredPrefsX.GetStringArray("ZonesCompletedSaved").ToList();
        if (list.Contains(zoneId)) return true;

        var queueList = ObscuredPrefsX.GetStringArray("ZonesCompletedQueue").ToList();
        return queueList.Contains(zoneId);
    }

    public static void SaveProcess()
    {
        var savedList = ObscuredPrefsX.GetStringArray("ZonesCompletedSaved").ToList();
        var queueList = ObscuredPrefsX.GetStringArray("ZonesCompletedQueue");

        for (int j = 0; j < queueList.Length; j++)
        {
            if (!savedList.Contains(queueList[j]))
            {
                savedList.Add(queueList[j]);
                Debug.Log("Zone Completed Added in Saved: " + queueList[j]);
            }
        }

        ObscuredPrefsX.SetStringArray("ZonesCompletedSaved", savedList.ToArray());
        Debug.Log("Process Saved");
    }
}