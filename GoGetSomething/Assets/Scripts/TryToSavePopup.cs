/**
 * TryToSavePopup.cs
 * Created by Akeru on 06/10/2019
 */

using DG.Tweening;
using TMPro;
using UnityEngine;

public class TryToSavePopup : MonoBehaviour
{
    #region Fields

    [SerializeField] private GameObject _fireDust, _noFireDust;
    [SerializeField] private TextMeshProUGUI _fireDustLeftText;
    [SerializeField] private float _yDisplacement = 0.02f;
    [SerializeField] private CanvasGroup _cg;

    [HideInInspector] public Bonfire Bonfire;

    private float _initYPos;

    #endregion

    #region MonoBehaviour Functions

    private void Start()
    {
        _initYPos = transform.position.y;
        transform.position = new Vector3(transform.position.x, _initYPos-_yDisplacement, transform.position.z);
        SetOff();
    }
    #endregion

    #region Other Functions

    public void Show()
    {
        _cg.interactable = true;

        _fireDust.SetActive(User.Firedust > 0);
        _noFireDust.SetActive(User.Firedust <= 0);

        transform.DOMoveY(_initYPos, 0.35f).SetEase(Ease.InOutSine).SetId(GetInstanceID());
        _cg.DOFade(1, 0.35f).SetEase(Ease.InOutSine).SetId(GetInstanceID());

        _fireDustLeftText.text = "(You have "+ User.Firedust+" firedust left)";
    }

    public void Hide()
    {
        Bonfire.StopInteraction();

        transform.DOMoveY(_initYPos - _yDisplacement, 0.35f).SetEase(Ease.InOutSine).SetId(GetInstanceID());
        _cg.DOFade(0, 0.35f).SetEase(Ease.InOutSine).SetId(GetInstanceID()).OnComplete(SetOff);
    }

    private void SetOff()
    {
        _cg.alpha = 0;
        _cg.interactable = false;
    }

    public void ButtonOk()
    {
        Hide();
    }

    public void ButtonNo()
    {
        Hide();
    }

    public void ButonSave()
    {
        Hide();
        Bonfire.Save();
    }

    #endregion
}
