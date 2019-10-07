/**
 * RoundsCombatZone.cs
 * Created by Akeru on 05/10/2019
 */

using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class RoundsCombatZone : CombatZone
{
    #region Fields

    [Serializable]
    public struct RoundData
    {
        public EnemySpawners[] Spawners;
    }

    [Serializable]
    public struct SimpleRoundData
    {
        public EnemySpawn[] Spawns;
    }

    [TabGroup("Rounds")] [SerializeField] private RoundData[] _rounds;
    [OnValueChanged("SetSpawnsToRounds")] [TabGroup("Rounds")] [SerializeField] private SimpleRoundData[] _roundsSpawns;

    private int _roundCount;
    private int _enemiesDied;

    #endregion

    #region MonoBehaviour Functions

    #endregion

    #region Other Functions

    protected override void SpawnEnemies()
    {
    }

    [Button("Create Rounds")]
    private void SetSpawnsToRounds()
    {
        _rounds = new RoundData[_roundsSpawns.Length];
        for (int i = 0; i < _roundsSpawns.Length; i++)
        {
            _rounds[i].Spawners = new EnemySpawners[_roundsSpawns[i].Spawns.Length];
        }
    }

    [Button("Assign")]
    private void Assign()
    {
        for (int i = 0; i < _roundsSpawns.Length; i++)
        {
            for (int j = 0; j < _roundsSpawns[i].Spawns.Length; j++)
            {
                _rounds[i].Spawners[j].Spawner = _roundsSpawns[i].Spawns[j];
            }
        }
    }

    protected override void ZoneReady()
    {
        base.ZoneReady();
        StartRound();
    }

    private void StartRound()
    {
        EventManager.OnStartRoundZone();
        Debug.Log("Start Round");

        var round = _rounds[_roundCount];
        for (int i = 0; i < round.Spawners.Length; i++)
        {
            round.Spawners[i].Spawner.StartSpawn(this);
        }

        EventManager.OnRoundUpdate(_roundCount, _rounds.Length);
    }

    public override void EnemyKilled(Enemy enemy)
    {
        _enemiesDied++;
        if (_enemiesDied >= _rounds[_roundCount].Spawners.Length)
        {
            _roundCount++;
            if (_roundCount >= _rounds.Length - 1)
            {
                Completed();
            }
            else
            {
                StartRound();
            }
        }
    }

    #endregion
}
