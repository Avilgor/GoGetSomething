/**
 * MusicController.cs
 * Created by Akeru on 13/03/2019
 * Copyright © iBoo Mobile. All rights reserved.
 */

using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MEC;
using UnityEngine;

public class LevitateEffect : MonoBehaviour
{
    public float Amount = 30;
    public float Duration = 3;
    public Ease Easetype = Ease.InOutSine;
    public float AfterTime = 0;
    public bool LocalPosition = false;

    private float _initY;

    private void Start()
    {
        Timing.RunCoroutine(_StartEffect());
    }

    private IEnumerator<float> _StartEffect()
    {
        yield return Timing.WaitForSeconds(AfterTime);

        if (!LocalPosition)
        {
            _initY = transform.position.y;

            DOTween.Sequence()
                .Append(transform.DOMoveY(_initY + Amount, Duration).SetEase(Easetype))
                .SetLoops(-1, LoopType.Yoyo);
        }
        else
        {
            _initY = transform.localPosition.y;

            DOTween.Sequence()
                .Append(transform.DOLocalMoveY(_initY + Amount, Duration).SetEase(Easetype))
                .SetLoops(-1, LoopType.Yoyo);
        }
    }
}