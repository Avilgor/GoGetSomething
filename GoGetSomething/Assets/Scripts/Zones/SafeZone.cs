/**
 * SafeZone.cs
 * Created by Akeru on 05/10/2019
 */

using System.Collections.Generic;
using MEC;
using UnityEngine;

public class SafeZone : Zone
{
    #region Fields

    #endregion

    #region MonoBehaviour Functions

    private void OnEnable()
    {
        EventManager.SaveProcess += SaveProcess;
    }

    private void OnDisable()
    {
        EventManager.SaveProcess -= SaveProcess;
    }

    private void Reset()
    {
        ZoneType = ZoneType.Safe;
    }

    #endregion

    #region Other Functions

    public override void Enter()
    {
    }

    //TODO Maybe this shouldn't be here
    private void SaveProcess()
    {
        User.SaveProcess();
    }
    #endregion
}
