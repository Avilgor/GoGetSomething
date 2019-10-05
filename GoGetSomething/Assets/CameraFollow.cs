/**
 * CameraFollow.cs
 * Created by Akeru on 05/10/2019
 */

using DG.Tweening;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    #region Fields

    [SerializeField] private Transform _target;
    [SerializeField] private float _distance = -15;
    [SerializeField] private float _smooth = 2;

    #endregion

    #region MonoBehaviour Functions

    private void Update()
    {
        transform.DOMove(_target.position + Vector3.forward * _distance, _smooth).SetEase(Ease.InOutSine);
    }
    #endregion

    #region Other Functions

    #endregion
}
