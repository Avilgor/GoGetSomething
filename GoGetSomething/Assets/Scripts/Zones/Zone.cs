using UnityEngine;

public enum ZoneType
{
    Null = -1,
    Safe,
    Combat
}

public class Zone : MonoBehaviour
{
    [SerializeField] protected ZoneType ZoneType = ZoneType.Null;
    [SerializeField] protected int IID;

    public virtual string ID => ZoneType + "-" + IID;

    protected virtual void Entered()
    {
        EventManager.OnZoneEntered(this);
    }

    protected virtual void Exit()
    {
        EventManager.OnZoneExit(this);
    }

    public void ShowZone()
    {
    }

    public void HideZone()
    {
    }
}