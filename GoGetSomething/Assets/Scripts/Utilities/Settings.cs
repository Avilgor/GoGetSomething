/**
 * Settings.cs
 * Created by Akeru on 21/03/2019
 * Copyright © iBoo Mobile. All rights reserved.
 */

using UnityEngine;

public class Settings : Singleton<Settings>
{
    [SerializeField] private SettingsAsset _settings;

    public SettingsAsset Get => _settings;
}
