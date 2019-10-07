/**
 * UIController.cs
 * Created by Akeru on 05/10/2019
 */

using System.Collections.Generic;
using DG.Tweening;
using MEC;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    #region Fields

    [SerializeField] private CanvasGroup _topBarCg;

    [SerializeField] private CanvasGroup _timingCg;
    [SerializeField] private Image _timingBar;

    [SerializeField] private CanvasGroup _roundsCg;
    [SerializeField] private TextMeshProUGUI _currentRoundText, _totalRoundsText;

    [SerializeField] private CanvasGroup _skullCg;
    [SerializeField] private TextMeshProUGUI _skullsLeftText;

    private float _initTiming;
    private float _timingLeft;

    private bool _count;

    public float TimingLeft
    {
        get { return _timingLeft; }
        set
        {
            _timingLeft = value;
            if (_timingLeft <= 0) EndTiming();
            _timingBar.fillAmount = 1 / _initTiming * _timingLeft;
        }
    }

    #endregion

    #region MonoBehaviour Functions

    private void Start()
    {
        SetOffAll();
        _timingBar.gameObject.SetActive(false);
        _timingCg.DOFade(0, 0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            EventManager.OnStartSurvivalTiming(5);
        }
    }

    private void OnEnable()
    {
        EventManager.ZoneEntered += ZoneEntered;
        EventManager.StartSurvivalTiming += StartSurvivalTiming;

        EventManager.StartSkullZone += StartSkullZone;
        EventManager.SkullsUpdate += SkullsUpdate;

        EventManager.StartRoundZone += StartRoundZone;
        EventManager.RoundUpdate += RoundUpdate;

        EventManager.HideUIZone += HideUIZone;
    }

    private void OnDisable()
    {
        EventManager.ZoneEntered -= ZoneEntered;
        EventManager.StartSurvivalTiming -= StartSurvivalTiming;

        EventManager.StartSkullZone -= StartSkullZone;
        EventManager.SkullsUpdate -= SkullsUpdate;

        EventManager.StartRoundZone -= StartRoundZone;
        EventManager.RoundUpdate -= RoundUpdate;

        EventManager.HideUIZone -= HideUIZone;
    }

    #endregion

    #region Other Functions

    private Zone _currentZone;

    private void ZoneEntered(Zone zone)
    {
        _currentZone = zone;
    }

    private void StartSurvivalTiming(int value)
    {
        _timingBar.gameObject.SetActive(true);
        _timingCg.DOFade(1, 0.5f);

        _initTiming = value;
        _timingLeft = value;

        Timing.RunCoroutine(_InitCount());
    }

    private IEnumerator<float> _InitCount()
    {
        _timingCg.DOFade(1, 0.2f);
        _timingBar.fillAmount = 0;
        _timingBar.DOFillAmount(1, 0.5f).SetDelay(0.15f).SetEase(Ease.InOutSine);

        yield return Timing.WaitForSeconds(1);

        _count = true;
        Timing.RunCoroutine(_Count(), "_Count");
    }

    private IEnumerator<float> _Count()
    {
        yield return Timing.WaitForSeconds(Time.deltaTime);
        TimingLeft -= Time.deltaTime;

        if(_count) Timing.RunCoroutine(_Count(), "_Count");
    }

    private void EndTiming()
    {
        Debug.Log("END TIMING");
        _count = false;
        Timing.KillCoroutines("_Count");
        _timingCg.DOFade(0, 0.35f).SetDelay(1).SetEase(Ease.InOutSine);

        _currentZone.Completed();
    }

    private void RoundUpdate(int value, int value2)
    {
        _currentRoundText.text = "";
        for (int i = 0; i < value; i++) _currentRoundText.text += "/";
        for (int i = 0; i < value2; i++) _totalRoundsText.text += "o";
    }

    private void StartRoundZone()
    {
        _topBarCg.DOFade(1, 0.35f).SetEase(Ease.InOutSine);
        _roundsCg.DOFade(1, 0.35f).SetDelay(0.5f).SetEase(Ease.InOutSine);
    }

    private void SkullsUpdate(int value)
    {
        _skullsLeftText.text = "x" + value;
    }

    private void StartSkullZone()
    {
        _topBarCg.DOFade(1, 0.35f).SetEase(Ease.InOutSine);
        _skullCg.DOFade(1, 0.35f).SetDelay(0.5f).SetEase(Ease.InOutSine);
    }

    private void HideUIZone()
    {
        _topBarCg.DOFade(0, 0.35f).SetEase(Ease.InOutSine).OnComplete(SetOffAll);
    }

    private void SetOffAll()
    {
        _topBarCg.alpha = 0;
        _timingCg.alpha = 0;
        _roundsCg.alpha = 0;
        _skullCg.alpha = 0;
    }

    #endregion
}
