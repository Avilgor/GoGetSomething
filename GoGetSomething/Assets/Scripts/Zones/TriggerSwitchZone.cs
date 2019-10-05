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
        Debug.Log("Switch Current Zone ["+currentZone.ID+"] to ["+_toZone.ID+"]");

        if (_toZone == currentZone) return;
        
        if(currentZone.ID != null) currentZone.Exit();
        _toZone.Enter();

        player.ChangeZone(_toZone);
        _switch.Switch(player, _isRed);
    }
    #endregion
}
