/**
 * SettingsAssetCreator.cs
 * Created by Akeru on 21/03/2019
 * Copyright © iBoo Mobile. All rights reserved.
 */

using UnityEditor;

public class SettingsAssetCreator
{
    [MenuItem("Assets/Create/SettingsAsset Asset")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<SettingsAsset>();
    }
}
