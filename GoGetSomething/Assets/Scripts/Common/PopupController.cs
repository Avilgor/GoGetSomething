/**
 * PopupController.cs
 * Created by Akeru on 22/03/2019
 * Copyright © iBoo Mobile. All rights reserved.
 */

using System;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public enum PopupID
{
    Null = -1,
    TrySave
}

public class PopupController : Singleton<PopupController>
{
    #region Fields

    [Title("Setup")]
    [TabGroup("General")] public float BoxEffectTime = 0.2f;
    [TabGroup("General")] public Ease BoxEase = Ease.InOutSine;

    [TabGroup("General")]public float InsideElementsEffectTime = 0.2f;
    [TabGroup("General")]public float InsideElementsDelayTime = 0.2f;
    [TabGroup("General")]public Ease InsideElementsEase = Ease.InOutSine;

    [Title("References")]
    [TabGroup("General")][SerializeField] private RectTransform _popupBG;
    [TabGroup("General")][SerializeField] private CanvasGroup _popupBGCG;

    [TabGroup("General")] [Header("List")] [TableList] public List<PopupRef> Popups = new List<PopupRef>();

    [TabGroup("Exceptions")] [SerializeField] private TryToSavePopup _tryToSavePopup;

    private PopupID _lastId = PopupID.Null;

    [Serializable] public class PopupRef
    {
        [OnValueChanged("SetPopupType")] public PopupID ID;
        [OnValueChanged("SetPopupType")] public Popup Popup;

        private void SetPopupType()
        {
            Popup.ID = ID;
        }
    }

    private bool _showing;
    private bool _popupActive;
    private bool _closing;

    #endregion

    #region MonoBehaviour Functions

    private void Start()
    {
        for (int i = 0; i < Popups.Count; i++)
        {
            Popups[i].Popup.gameObject.SetActive(true);
        }

        _popupBGCG.DOFade(0, 0);
        HideBG();
    }

    #endregion

    #region Other Functions
    public void Show(PopupID id, bool closeLastBefore = true)
    {
        EventManager.OnPopupOpened();

        if (closeLastBefore && _lastId != PopupID.Null) Hide(_lastId);
        _lastId = id;

        _closing = false;

        if (!_showing)
        {
            _showing = true;

            DOTween.Kill("HidePopup");
        }

        var p = GetPopupById(id);
        p.gameObject.SetActive(true);
        p.Show();

        _popupActive = true;
    }

    public void Hide(PopupID id)
    {
        if (id == PopupID.Null) return;
        GetPopupById(id).Hide();
    }

    public void HideAll(bool forceAll = false)
    {
        EventManager.OnPopupsClosed();

        if(forceAll) for (int i = 0; i < Popups.Count; i++) Popups[i].Popup.Hide();
        else Hide(_lastId);

        HideBG();
    }

    private void HideBG()
    {
        if (_closing) return;
        _closing = true;

        DOTween.Kill("ShowPopup");

        _showing = false;
        _popupBGCG.DOFade(0, BoxEffectTime).SetDelay(InsideElementsDelayTime + 0.2f).SetEase(BoxEase).SetId("HidePopup")
            .OnComplete(EndClose);
    }

    private void EndClose()
    {
        _closing = false;
        _popupActive = false;
        _popupBG.gameObject.SetActive(false);
    }

    private Popup GetPopupById(PopupID id)
    {
        for (int i = 0; i < Popups.Count; i++)
        {
            if (Popups[i].ID == id) return Popups[i].Popup;
        }

        return null;
    }
    #endregion

    #region Exceptions

    public void ShowTryToSavePopup(Bonfire bonfire)
    {
        Debug.Log("?");
        Debug.Log("Bonfire: "+ bonfire);
        Show(PopupID.TrySave);
//        _tryToSavePopup.BonfireTarget = bonfire;
    }
    #endregion
}
