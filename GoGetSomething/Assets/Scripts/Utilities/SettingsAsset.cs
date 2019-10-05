/**
 * SettingsAsset.cs
 * Created by Akeru on 21/03/2019
 * Copyright © iBoo Mobile. All rights reserved.
 */

using UnityEngine;

public class SettingsAsset : ScriptableObject
{
    public int Version = 0;
    public bool DebugMode = false;
    public string InitSceneName;
}