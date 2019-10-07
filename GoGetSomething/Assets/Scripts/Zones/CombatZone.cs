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

    [TabGroup("Combat")] [OnValueChanged("SetName")] [SerializeField] protected Type CombatType;
    [TabGroup("Combat")] [SerializeField] protected WeaponSpawn[] WeaponsSpawns;
    [TabGroup("Combat")] [SerializeField] protected EnemySpawners[] EnemySpawners;
    [OnValueChanged("SetSpawnsToRounds")] [TabGroup("Combat")] [SerializeField] private EnemySpawn[] _roundsSpawns;

    #endregion

    #region MonoBehaviour Functions

    private void Reset()
    {
        ZoneType = ZoneType.Combat;
    }

    #endregion

    #region Other Functions


    [Button("Create Rounds")]
    private void SetSpawnsToRounds()
    {
        EnemySpawners = new EnemySpawners[_roundsSpawns.Length];
    }

    [Button("Assign")]
    private void Assign()
    {
        for (int i = 0; i < _roundsSpawns.Length; i++)
        {
            EnemySpawners[i].Spawner = _roundsSpawns[i];
        }
    }

    protected override void ZoneReady()
    {
        base.ZoneReady();
        SpawnEnemies();
    }

    protected virtual void SpawnEnemies()
    {
        for (int i = 0; i < WeaponsSpawns.Length; i++)
        {
            WeaponsSpawns[i].StartSpawn();
        }

        for (int i = 0; i < EnemySpawners.Length; i++)
        {
            if(EnemySpawners[i].Spawner != null) EnemySpawners[i].Spawner.StartSpawn();
        }
    }

    protected virtual void Failed()
    {
        PlayerController.I.Die();
        //TODO Reset to last saved point
    }

    public virtual void EnemyKilled(Enemy enemy)
    {
        
    }

    public void SetName()
    {
        gameObject.name = "[" + ID + "] - " + CombatType + " Zone";
    }
    #endregion
}
