/**
 * SafeZone.cs
 * Created by Akeru on 05/10/2019
 */

using System.Collections.Generic;
using MEC;
using Sirenix.OdinInspector;
using UnityEngine;

public class SafeZone : Zone
{
    #region Fields

    [OnValueChanged("SetBonfireId")] [SerializeField] private Bonfire _bonfire;

    #endregion

    #region MonoBehaviour Functions

    private void Reset()
    {
        ZoneType = ZoneType.Safe;
    }

    #endregion

    #region Other Functions

    public override void Enter()
    {
    }

    public void SetBonfireId()
    {
        _bonfire.ID = IID;
    }

    #endregion
}
