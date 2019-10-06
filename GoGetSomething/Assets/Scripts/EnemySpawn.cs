/**
 * EnemySpawn.cs
 * Created by Akeru on 06/10/2019
 */

using System;
using System.Collections.Generic;
using MEC;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawn : MonoBehaviour
{
    #region Fields

    [SerializeField] public Vector2 SpawnTimeRate;
    [SerializeField] public EnemyType[] PossibleEnemies;

    #endregion

    #region MonoBehaviour Functions

    #endregion

    #region Other Functions

    public void StartSpawn()
    {
        Timing.RunCoroutine(_Spawn());
    }

    private IEnumerator<float> _Spawn()
    {
        yield return Timing.WaitForSeconds(Random.Range(SpawnTimeRate.x, SpawnTimeRate.y));

        Spawn();
    }

    private void Spawn()
    {
        var type = PossibleEnemies[Random.Range(0, PossibleEnemies.Length)];

        var enemy = SimplePool.Spawn(EnemyList.I.GetEnemyPrefab(type), transform.position, Quaternion.identity).transform;
        enemy.SetParent(transform);
    }
    #endregion
}
