using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventsHandler : MonoBehaviour
{
    #region _player events

    [SerializeField] public GameObject _player;
    [SerializeField] public GameObject[] _boneAreas;
    [SerializeField] public GameObject []_porraAreas;

    public void onAttackFinish()
    {
        _player.GetComponent<PlayerController>()._attacking = false;
        for(int i=0;i<4;i++)
        {
            _boneAreas[(int)_player.GetComponent<PlayerController>()._aimDirection].SetActive(false);
            _porraAreas[(int)_player.GetComponent<PlayerController>()._aimDirection].SetActive(false);
        }
    }

    public void boneAttack()
    {
        _boneAreas[(int)_player.GetComponent<PlayerController>()._aimDirection].SetActive(true);
    }

    public void porraAttack()
    {
        _porraAreas[(int)_player.GetComponent<PlayerController>()._aimDirection].SetActive(true);
    }

    #endregion

    #region _enemy events

    [SerializeField] public GameObject _enemy,_attackArea;

    public void onAttackDone()
    {
        _enemy.GetComponent<Enemy>()._attacking = false;
        _attackArea.SetActive(false);
    }
    public void _enemyAttack()
    {
        _attackArea.SetActive(true);
    }


    #endregion
}
