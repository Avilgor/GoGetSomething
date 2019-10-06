/**
 * Skull.cs
 * Created by Akeru on 06/10/2019
 */

using System.Collections.Generic;
using MEC;
using Sirenix.OdinInspector;
using UnityEngine;

public class Skull : MonoBehaviour, IDamagable
{
    #region Fields

    [ReadOnly] [SerializeField] private DestroyCombatZone _destroyCombatZone;
    [SerializeField] private float _health = 100;

    #endregion

    #region MonoBehaviour Functions

    #endregion

    #region Other Functions

    public void SetDestroyZone(DestroyCombatZone zone)
    {
        _destroyCombatZone = zone;
    }

    public void Damage(int dmg)
    {
        if (_health < 0) return;
        _health -= dmg;
        Debug.Log("Health: "+_health);
        if (_health <= 0) Die();
    }

    public void Die()
    {
        Debug.Log("Die");
        _destroyCombatZone.SkullDestroyed(this);
        Timing.RunCoroutine(_Destroy());
    }

    private IEnumerator<float> _Destroy()
    {
        yield return Timing.WaitForSeconds(1);
        Destroy(gameObject);
    }
    #endregion
}
