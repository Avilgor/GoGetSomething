/**
 * Utils.cs
 * Created by Akeru on 21/03/2019
 * Copyright © iBoo Mobile. All rights reserved.
 */

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class Find
{
    public static List<T> FindAssetsByType<T>() where T : Object
    {
        List<T> assets = new List<T>();
        string[] guids = AssetDatabase.FindAssets(string.Format("t:{0}", typeof(T)));
        for (int i = 0; i < guids.Length; i++)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
            T asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);
            if (asset != null)
            {
                assets.Add(asset);
            }
        }
        return assets;
    }

    public static T FindAssetByType<T>() where T : Object
    {
        List<T> assets = new List<T>();
        string[] guids = AssetDatabase.FindAssets(string.Format("t:{0}", typeof(T)));
        for (int i = 0; i < guids.Length; i++)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
            T asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);
            if (asset != null)
            {
                return asset;
            }
        }
        return null;
    }

    public static SettingsAsset Settings => FindAssetByType<SettingsAsset>();
    public static EditorHelperAsset EditorHelper => FindAssetByType<EditorHelperAsset>();
}
