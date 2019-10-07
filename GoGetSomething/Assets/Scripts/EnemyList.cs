/**
 * EnemyList.cs
 * Created by Akeru on 06/10/2019
 */

using System;
using UnityEngine;

public class EnemyList : Singleton<EnemyList>
{
    #region Fields

    [SerializeField] private EnemyPrefabs[] _enemyPrefabs;

    [Serializable]
    public struct EnemyPrefabs
    {
        public EnemyType Type;
        public GameObject Prefab;
    }
    #endregion

    #region MonoBehaviour Functions

    private void OnEnable()
    {
        EventManager.ResetAll += DestroyThis;
    }

    private void OnDisable()
    {
        EventManager.ResetAll -= DestroyThis;
    }

    private void DestroyThis()
    {
        I = null;
        Destroy(gameObject);
    }
    #endregion

    #region Other Functions
    public GameObject GetEnemyPrefab(EnemyType type)
    {
        for (int i = 0; i < _enemyPrefabs.Length; i++)
        {
            if (_enemyPrefabs[i].Type == type) return _enemyPrefabs[i].Prefab;
        }

        return _enemyPrefabs[0].Prefab;
    }
    #endregion
}
