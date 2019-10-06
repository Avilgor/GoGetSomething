/**
 * WeaponSpawn.cs
 * Created by Akeru on 06/10/2019
 */

using System.Collections.Generic;
using MEC;
using Sirenix.OdinInspector;
using UnityEngine;

public class WeaponSpawn : MonoBehaviour
{
    #region Fields

    [SerializeField] private Vector2 _spawnTimeRate;
    [SerializeField] private WeaponType[] _possibleWeaponTypes;

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
        yield return Timing.WaitForSeconds(Random.Range(_spawnTimeRate.x, _spawnTimeRate.y));

        Spawn();
    }

    private void Spawn()
    {
        var type = _possibleWeaponTypes[Random.Range(0, _possibleWeaponTypes.Length)];

        SimplePool.Spawn(WeaponList.I.GetWeaponPrefab(type), transform.position, Quaternion.identity);
    }

    #endregion
}
