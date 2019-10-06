using System.Collections.Generic;
using DG.Tweening;
using MEC;
using Sirenix.OdinInspector;
using UnityEngine;

public class Popup : MonoBehaviour
{
    [SerializeField] [OnValueChanged("GetCanvasGroup")] protected RectTransform BoxBG;
    [SerializeField] [OnValueChanged("GetCanvasGroup")] protected RectTransform[] InsideBoxElements;
    protected Vector3[] InitScale;

    [SerializeField] [ReadOnly] protected CanvasGroup BoxBGCG;
    [SerializeField] [ReadOnly] protected CanvasGroup[] InsideBoxElementsCG = new CanvasGroup[0];

    [ReadOnly]
    public PopupID ID;

    protected bool Enabled;



    protected virtual void Start()
    {
        BoxBG.gameObject.SetActive(false);
        BoxBG.localScale = Vector3.zero;
        BoxBGCG.alpha = 0;

        InitScale = new Vector3[InsideBoxElements.Length];

        for (int i = 0; i < InsideBoxElements.Length; i++)
        {
            InitScale[i] = InsideBoxElements[i].localScale;

            InsideBoxElements[i].gameObject.SetActive(false);
            InsideBoxElements[i].localScale = Vector3.zero;
            InsideBoxElementsCG[i].alpha = 0;
        }
    }

    protected virtual void GetCanvasGroup()
    {
        if (BoxBG.GetComponent<CanvasGroup>() != null)
        {
            BoxBGCG = BoxBG.GetComponent<CanvasGroup>();
        }
        else
        {
            BoxBGCG = BoxBG.gameObject.AddComponent<CanvasGroup>();
        }

        InsideBoxElementsCG = new CanvasGroup[InsideBoxElements.Length];

        for (int i = 0; i < InsideBoxElements.Length; i++)
        {
            if (InsideBoxElements[i].GetComponent<CanvasGroup>() != null)
            {
                InsideBoxElementsCG[i] = InsideBoxElements[i].GetComponent<CanvasGroup>();
            }
            else
            {
                InsideBoxElementsCG[i] = InsideBoxElements[i].gameObject.AddComponent<CanvasGroup>();
            }
        }
    }

    public virtual void Show()
    {
        Debug.Log("[UNITY LOG] - Show [" + ID+"] Popup");

        Timing.KillCoroutines("Disable" + ID);
        DOTween.Kill("Hide" + ID);

        Enabled = true;

        var p = PopupController.I;

        BoxBG.gameObject.SetActive(true);
        BoxBG.DOScale(Vector3.one, p.BoxEffectTime).SetEase(p.BoxEase).SetId("Show" + ID);
        BoxBGCG.DOFade(1, p.BoxEffectTime).SetEase(p.BoxEase).SetId("Show" + ID);

        for (int i = 0; i < InsideBoxElements.Length; i++)
        {
//            Debug.Log("[UNITY LOG] - ["+ID+"] Popup Item [" + i + "] Name: "+ InsideBoxElements[i].gameObject.name);

            if (InsideBoxElements[i].gameObject == null)
            {
                Debug.LogError("InsideBoxElements["+i+"] of Popup "+ID+" NULL!");
                continue;
            }

            InsideBoxElements[i].gameObject.SetActive(true);

            InsideBoxElements[i].DOScale(InitScale[i], p.BoxEffectTime).SetDelay(p.InsideElementsDelayTime * (i + 1))
                .SetEase(p.BoxEase).SetId("Show" + ID);
            InsideBoxElementsCG[i].DOFade(1, p.BoxEffectTime).SetDelay(p.InsideElementsDelayTime * (i + 1))
                .SetEase(Ease.InOutSine).SetId("Show" + ID);
        }
    }

    public virtual void Hide()
    {
        if (!Enabled) return;

        Debug.Log("[UNITY LOG] - Hide [" + ID + "] Popup");

        Timing.KillCoroutines("Disable" + ID);
        DOTween.Kill("Show" + ID);

        var p = PopupController.I;

        for (int i = 0; i < InsideBoxElements.Length; i++)
        {
            InsideBoxElements[i].DOScale(Vector3.zero, p.BoxEffectTime * 0.8f).SetDelay(p.InsideElementsDelayTime * (i + 1) * 0.8f)
                .SetEase(p.BoxEase).SetId("Hide" + ID);
            InsideBoxElementsCG[i].DOFade(0, p.BoxEffectTime * 0.6f).SetDelay(p.InsideElementsDelayTime * (i + 1) * 0.8f)
                .SetEase(Ease.InOutSine).SetId("Hide" + ID);
        }

        Timing.RunCoroutine(_Disable(), "Disable" + ID);
    }

    protected virtual IEnumerator<float> _Disable()
    {
        var p = PopupController.I;

        yield return Timing.WaitForSeconds((InsideBoxElements.Length + 1) * p.InsideElementsDelayTime * 0.8f);

        BoxBG.DOScale(Vector3.zero, p.BoxEffectTime * 0.8f).SetEase(p.BoxEase).SetId("Hide" + ID);
        BoxBGCG.DOFade(0, p.BoxEffectTime * 0.8f).SetEase(p.BoxEase).SetId("Hide" + ID);

        yield return Timing.WaitForSeconds(p.BoxEffectTime * 0.8f);
        BoxBG.gameObject.SetActive(false);

        Enabled = false;
    }
}