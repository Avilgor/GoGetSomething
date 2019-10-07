/**
 * DamageText.cs
 * Created by Akeru on 07/10/2019
 */

using DG.Tweening;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    #region Fields

    [SerializeField] private TextMeshProUGUI _damageText;
    [SerializeField] private float _effectTime = 0.5f;
    [SerializeField] private Ease _effectEase = Ease.OutBack;
    [SerializeField] private Vector2 _xPosRange;
    [SerializeField] private float _yPos;

    #endregion

    #region MonoBehaviour Functions

    #endregion

    #region Other Functions

    public void SpawnText(int damage)
    {
        _damageText.text = damage.ToString();
        _damageText.rectTransform.anchoredPosition = Vector2.zero;
        _damageText.DOFade(0, 0);

        _damageText.DOFade(1, _effectTime * 0.3f).SetEase(Ease.InOutSine);
        _damageText.DOFade(0, _effectTime * 0.25f).SetDelay(_effectTime * 0.75f).SetEase(Ease.InOutSine);
        _damageText.rectTransform.DOScale(1, _effectTime * 0.3f).SetEase(Ease.InOutSine);
        _damageText.rectTransform.DOAnchorPos(new Vector2(Random.Range(_xPosRange.x, _xPosRange.y), _yPos), _effectTime).SetEase(_effectEase).OnComplete(()=> SimplePool.Despawn(gameObject));
    }
    #endregion
}
