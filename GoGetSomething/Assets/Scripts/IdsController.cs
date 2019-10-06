/**
 * IdsController.cs
 * Created by Akeru on 06/10/2019
 */

using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class IdsController : MonoBehaviour
{
    #region Fields

    [TabGroup("CombatZones")] [SerializeField] private CombatZones[] _combatZones;
    [TabGroup("SafeZones")] [SerializeField] private SafeZones[] _safeZones;

    [Serializable]
    public class CombatZones
    {
        public CombatZone CombatZone;
        [ShowIf("IsNotNull")] [OnValueChanged("ChangeID")] public int ID;

        public bool IsNotNull()
        {
            return CombatZone != null;
        }

        public void ChangeID()
        {
            CombatZone.IID = ID;
            CombatZone.ZoneType = ZoneType.Combat;
            CombatZone.SetName();
        }
    }

    [Serializable]
    public class SafeZones
    {
        public SafeZone SafeZone;
        [ShowIf("IsNotNull")] [OnValueChanged("ChangeID")] public int ID;

        public bool IsNotNull()
        {
            return SafeZone != null;
        }

        public void ChangeID()
        {
            SafeZone.IID = ID;
            SafeZone.ZoneType = ZoneType.Safe;
            SafeZone.SetBonfireId();
            SafeZone.SetName();
        }
    }

    #endregion

    #region MonoBehaviour Functions

    #endregion

    #region Other Functions

    #endregion
}
