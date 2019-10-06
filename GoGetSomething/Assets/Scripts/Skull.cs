/**
 * Skull.cs
 * Created by Akeru on 06/10/2019
 */

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
    public void Damage(int dmg)
    {
        _health -= dmg;
        Debug.Log("Health: "+_health);
        if (_health <= 0) Die();
    }

    public void Die()
    {
        Debug.Log("Die");
    }
    #endregion
}
