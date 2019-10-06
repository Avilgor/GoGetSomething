using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventsHandler : MonoBehaviour
{
    #region Player events

    [SerializeField] public GameObject player;

    public void onAttackFinish()
    {
        player.GetComponent<PlayerController>()._attacking = false;
    }

    #endregion

    #region Enemy events

    [SerializeField] public GameObject enemy;

    public void onAttackDone()
    {
        enemy.GetComponent<Enemy>()._attacking = false;
    }

    #endregion
}
