using System.Collections.Generic;
using DG.Tweening;
using MEC;
using UnityEngine;
using UnityEngine.Tilemaps;
using Sirenix.OdinInspector;

public enum ZoneType
{
    Null = -1,
    Safe,
    Combat
}

public class Zone : MonoBehaviour
{
    [ReadOnly] [Tooltip("Change it in 'IdsController' Script")] public ZoneType ZoneType = ZoneType.Null;
    [ReadOnly] [Tooltip("Change it in 'IdsController' Script")] public int IID;
    [SerializeField] private bool _isInitZone;
    [SerializeField] private Tilemap[] _tilemapsToShowHide;
    [SerializeField] private SwitchZone[] _switches;

    public virtual string ID => ZoneType + "-" + IID;

    public void EnterInitZone()
    {
        OpenSwitches();
        ShowZone();
    }

//    protected virtual void Start()
//    {
//        if(!_isInitZone) HideZone();
//    }

    public virtual void Enter()
    {
        Debug.Log("enter");

        EventManager.OnCleanPlayer();
        EventManager.OnZoneEntered(this);
        if (ZoneType == ZoneType.Combat) CloseSwitches();
        ShowZone();

        Debug.Log("?");

        Timing.RunCoroutine(_PrepareZone());
        Debug.Log("2?");
    }

    public virtual void Exit()
    {
        EventManager.OnZoneExit(this);
        HideZone();
    }

    public void ShowZone()
    {
        //Fade(1);
    }

    public void HideZone()
    {
        //Fade(0);
    }

    private void CloseSwitches()
    {
        for (int i = 0; i < _switches.Length; i++) _switches[i].Close();
    }

    private void OpenSwitches()
    {
        for (int i = 0; i < _switches.Length; i++) _switches[i].Open();
    }

    private void Fade(float alpha)
    {
        Debug.Log("Fade - "+ID + "- alpha: "+ alpha);

        for (int i = 0; i < _tilemapsToShowHide.Length; i++)
        {
            var i1 = i;
            var a = _tilemapsToShowHide[i1].color.a;
            var c = _tilemapsToShowHide[i1].color;

            DOTween.To(() => a, v => a = v, alpha, 0.35f).OnUpdate(()=> SetColor(a, _tilemapsToShowHide[i1]))
                .SetEase(Ease.InOutSine).SetId("Color" + _tilemapsToShowHide[i1].GetInstanceID());
        }
    }

    private void SetColor(float a, Tilemap tilemap)
    {
        tilemap.color = new Color(tilemap.color.r, tilemap.color.g, tilemap.color.b, a);
    }

    protected virtual IEnumerator<float> _PrepareZone()
    {
        yield return Timing.WaitForSeconds(2);

        ZoneReady();
    }

    protected virtual void ZoneReady()
    {
        EventManager.OnZoneReady();
    }

    public virtual void Completed()
    {
        EventManager.OnZoneCompleted(this);
        EventManager.OnHideUIZone();
        User.SetZoneCompletedQueue(ID);

        OpenSwitches();
    }
}