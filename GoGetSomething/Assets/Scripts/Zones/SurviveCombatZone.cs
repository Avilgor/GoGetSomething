/**
 * SurviveCombatZone.cs
 * Created by Akeru on 05/10/2019
 */

using System;
using System.Collections.Generic;
using MEC;
using Sirenix.OdinInspector;
using UnityEngine;

public class SurviveCombatZone : CombatZone
{
    #region Fields

    [TabGroup("Survive")] [SerializeField] private int _time = 30;
    [TabGroup("Survive")] [SerializeField] private EnemySpawners[] _enemySpawners;
//    [Space]

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

    #endregion

    #region MonoBehaviour Functions

    #endregion

    #region Other Functions

    protected override IEnumerator<float> _PrepareZone()
    {
        yield return Timing.WaitForSeconds(2);

        EventManager.OnStartSurvivalTiming(_time);

        yield return Timing.WaitForSeconds(1.5f);

        ZoneReady();
    }

    protected override void ZoneReady()
    {
        base.ZoneReady();
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < _enemySpawners.Length; i++)
        {
            _enemySpawners[i].Spawner.StartSpawn();
        }
    }

    public override void Completed()
    {
        base.Completed();
        EventManager.OnKillAllEnemies();
    }

    #endregion
}