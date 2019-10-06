using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AnimationEventsHandler : MonoBehaviour
{
    #region _player events

    [SerializeField] public GameObject _player;
    [SerializeField] public GameObject[] _boneAreas;
    [SerializeField] public GameObject []_porraAreas;

    private Vector2[] _bonesScales, _porrasScales;


    private void Start()
    {
        _bonesScales = new Vector2[_boneAreas.Length];
        _porrasScales = new Vector2[_porraAreas.Length];

        for (int i = 0; i < _boneAreas.Length; i++) _bonesScales[i] = _boneAreas[i].transform.localScale;
        for (int i = 0; i < _porraAreas.Length; i++) _porrasScales[i] = _porraAreas[i].transform.localScale;
    }

    public void onAttackFinish()
    {
        PlayerController.I._attacking = false;
        for(int i=0;i<4;i++)
        {
            _boneAreas[(int)PlayerController.I._aimDirection].SetActive(false);
            _porraAreas[(int)PlayerController.I._aimDirection].SetActive(false);
        }
    }

    public void boneAttack()
    {
        var dir = (int) PlayerController.I._aimDirection;
        var area = _boneAreas[dir];
        DOTween.Kill("Area Scale" + area.GetInstanceID());

        area.SetActive(true);
        area.transform.localScale = Vector3.zero;

        area.transform.DOScale(_bonesScales[dir], 0.2f).OnComplete(() => area.SetActive(false)).SetId("Area Scale" + area.GetInstanceID());

        Debug.Log("Attack " + dir);
    }

    public void porraAttack()
    {
        var dir = (int)PlayerController.I._aimDirection;
        var area = _porraAreas[dir];
        DOTween.Kill("Area Scale" + area.GetInstanceID());

        area.SetActive(true);
        area.transform.localScale = Vector3.zero;

        area.transform.DOScale(_porrasScales[dir], 0.2f).OnComplete(() => area.SetActive(false)).SetId("Area Scale" + area.GetInstanceID());

        Debug.Log("Attack " + dir);
    }

    #endregion

    #region _enemy events

    [SerializeField] public GameObject _enemy,_attackArea;

    public void onAttackDone()
    {
        _enemy.GetComponent<Enemy>()._attacking = false;
        _attackArea.SetActive(false);
    }
    public void _enemyAttack()
    {
        _attackArea.SetActive(true);
    }


    #endregion
}
