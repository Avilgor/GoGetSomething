/**
 * SurviveCombatZone.cs
 * Created by Akeru on 05/10/2019
 */

using System.Collections.Generic;
using MEC;
using UnityEngine;

public class SurviveCombatZone : CombatZone
{
    #region Fields
    [SerializeField] private int _time = 30;
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

    public override void Completed()
    {
        base.Completed();
        EventManager.OnKillAllEnemies();
    }

    #endregion
}
