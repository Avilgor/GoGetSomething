/**
 * DestroyCombatZone.cs
 * Created by Akeru on 05/10/2019
 */

using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class DestroyCombatZone : CombatZone
{
    #region Fields

    [TabGroup("Destroy")] [OnValueChanged("SetZonesToSkull")] [SerializeField] private Skull[] _skullsToDestroy;

    private List<Skull> _skulls = new List<Skull>();

    #endregion

    #region MonoBehaviour Functions

    private void Start()
    {
        _skulls = _skullsToDestroy.ToList();
        for (int i = 0; i < _skulls.Count; i++) _skulls[i].gameObject.SetActive(false);
    }
    #endregion

    #region Other Functions

    private void SetZonesToSkull()
    {
        for (int i = 0; i < _skullsToDestroy.Length; i++)
        {
            _skullsToDestroy[i].SetDestroyZone(this);
        }
    }

    protected override void ZoneReady()
    {
        base.ZoneReady();
        for (int i = 0; i < _skulls.Count; i++) _skulls[i].gameObject.SetActive(true);
        EventManager.OnStartSkullZone();
        EventManager.OnSkullsUpdate(_skulls.Count);
    }

    public void SkullDestroyed(Skull skull)
    {
        _skulls.Remove(skull);
        EventManager.OnSkullsUpdate(_skulls.Count);
        if (_skulls.Count <= 0) Completed();
    }

    public override void Completed()
    {
        base.Completed();
        EventManager.OnKillAllEnemies();
    }

    #endregion
}