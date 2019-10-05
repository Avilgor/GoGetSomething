/**
 * UIController.cs
 * Created by Akeru on 05/10/2019
 */

using System.Collections.Generic;
using DG.Tweening;
using MEC;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    #region Fields

    [SerializeField] private Image _timingBar;
    [SerializeField] private CanvasGroup _timingCg;

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
        EventManager.StartSurvivalTiming += StartSurvivalTiming;
    }

    private void OnDisable()
    {
        EventManager.StartSurvivalTiming -= StartSurvivalTiming;
    }

    #endregion

    #region Other Functions

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
    }

    #endregion
}
