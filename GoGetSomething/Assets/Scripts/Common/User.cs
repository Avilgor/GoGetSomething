/**
 * User.cs
 * Created by Akeru on 13/03/2019
 * Copyright © iBoo Mobile. All rights reserved.
 */

using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;

public class User : MonoBehaviour
{
    public static string Uuid
    {
        get { return ObscuredPrefs.GetString("Uuid"); }
        set { ObscuredPrefs.SetString("Uuid", value); }
    }
}
