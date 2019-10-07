/**
 * WeaponList.cs
 * Created by Akeru on 06/10/2019
 */

using System;
using UnityEngine;

public class WeaponList : Singleton<WeaponList>
{
    #region Fields
    [SerializeField] private WeaponPrefabs[] _weaponPrefabs;

    [Serializable]
    public struct WeaponPrefabs
    {
        public WeaponType Type;
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

    public GameObject GetWeaponPrefab(WeaponType type)
    {
        for (int i = 0; i < _weaponPrefabs.Length; i++)
        {
            if (_weaponPrefabs[i].Type == type) return _weaponPrefabs[i].Prefab;
        }

        return _weaponPrefabs[0].Prefab;
    }
    #endregion
}
