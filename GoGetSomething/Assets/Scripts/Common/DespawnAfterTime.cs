using System.Collections.Generic;
using MEC;
using UnityEngine;

public class DespawnAfterTime : MonoBehaviour
{
    [SerializeField] private float _time = 1;

    private void OnEnable()
    {
        Timing.RunCoroutine(_Despawn());
    }

    private IEnumerator<float> _Despawn()
    {
        yield return Timing.WaitForSeconds(_time);

        SimplePool.Despawn(gameObject);
    } 

}