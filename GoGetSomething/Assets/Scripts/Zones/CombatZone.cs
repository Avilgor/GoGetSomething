/**
 * CombatZone.cs
 * Created by Akeru on 05/10/2019
 */

using UnityEngine;

public class CombatZone : Zone
{
    #region Fields

    public enum Type
    {
        Null = -1,
        Survive, Destroy, Rounds
    }

    [SerializeField] protected Type CombatType;

    #endregion

    #region MonoBehaviour Functions

    private void Reset()
    {
        ZoneType = ZoneType.Combat;
    }

    #endregion

    #region Other Functions
    protected virtual void Completed()
    {
        EventManager.OnZoneCompleted(this);
        User.SetZoneCompletedQueue(ID);
    }

    protected virtual void Failed()
    {
        //TODO Reset to last saved point
    }
    #endregion
}