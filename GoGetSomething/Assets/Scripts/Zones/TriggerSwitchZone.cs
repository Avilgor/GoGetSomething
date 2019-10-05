/**
 * NewBehaviourScript.cs
 * Created by Akeru on 05/10/2019
 */

using UnityEngine;

public class TriggerSwitchZone : MonoBehaviour
{
    #region Fields

    [SerializeField] private SwitchZone _switch;
    [SerializeField] private Zone _toZone;
    [SerializeField] private bool _isRed;

    #endregion

    #region MonoBehaviour Functions

    #endregion

    #region Other Functions

    public void SwitchZoneTriggerEntered(Zone currentZone, PlayerController player)
    {
        if (_toZone == currentZone) return;
        
        currentZone.HideZone();
        _toZone.ShowZone();

        _switch.Switch(player, _isRed);
    }
    #endregion
}
