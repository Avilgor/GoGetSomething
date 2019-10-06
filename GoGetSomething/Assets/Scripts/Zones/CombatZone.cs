/**
 * SafeZone.cs
 * Created by Akeru on 05/10/2019
 */

using System;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public class EnemySpawners
{
    public EnemySpawn Spawner;
    [OnValueChanged("UpdateSpawner")] public Vector2 SpawnTimeRate;
    [OnValueChanged("UpdateSpawner")] public EnemyType[] PossibleEnemies;

    public void UpdateSpawner()
    {
        if (Spawner != null)
        {
            Spawner.SpawnTimeRate = SpawnTimeRate;
            Spawner.PossibleEnemies = PossibleEnemies;
        }
    }
}

public class CombatZone : Zone
{
    #region Fields

    public enum Type
    {
        Null = -1,
        Survive, Destroy, Rounds
    }

    [SerializeField] protected Type CombatType;
//    [SerializeField] private Transform[] _weaponSpawns;
//    [SerializeField] private Transform[] _enemiesSpawns;

    #endregion

    #region MonoBehaviour Functions

    private void Reset()
    {
        ZoneType = ZoneType.Combat;
    }

    #endregion

    #region Other Functions

    protected virtual void Failed()
    {
        PlayerController.I.Die();
        //TODO Reset to last saved point
    }
    #endregion
}
