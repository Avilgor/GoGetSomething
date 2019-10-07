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
        if(currentZone != null) Debug.Log("Switch Current Zone ["+currentZone.ID+"] to ["+_toZone.ID+"]");

        if(_toZone == currentZone) return;
        
        if(currentZone.ID != null) currentZone.Exit();
        if (User.IsZoneCompleted(_toZone.ID))
        {
            Debug.Log("Zone ["+_toZone.ID+"] Completed");
            return;
        }

        _toZone.Enter();
        _switch.Switch(player, _isRed);
    }
    #endregion
}
