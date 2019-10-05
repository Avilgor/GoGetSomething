/**
 * SFXCommon.cs
 * Created by Akeru on 13/03/2019
 * Copyright © iBoo Mobile. All rights reserved.
 */

using System;
using System.Collections.Generic;
using MEC;
using UnityEngine;

public enum CommonSFX
{
    Button, MisteryResolved, TimeUp,
    SuspenseShort, SuspenseMedium, SuspenseLong,
    Tap, ItemClose, ShowHeritage, Tap2, Clue, DarkImpact, Bag
}

public class SFXCommon : Singleton<SFXCommon>
{
    #region Fields
    [SerializeField] private List<CommonSFXStruct> _sfxCommonList;

    private Transform _audioListenerTransform;

    [Serializable]
    public struct CommonSFXStruct
    {
        public CommonSFX ID;
        public AudioClip SFX;
    }
    #endregion

    #region Unity Functions

    #endregion

    #region Other Functions

    private Vector3 AudioListenerPosition()
    {
        if (_audioListenerTransform != null) return _audioListenerTransform.position;

        var listener = GameObject.FindGameObjectWithTag("MainAudioListener");

        if (listener != null)
        {
            _audioListenerTransform = listener.transform;
            return _audioListenerTransform.position;
        }

        return Camera.main.transform.position;
    }

    public void PlaySFX(CommonSFX id, float delay = 0)
    {
        if (delay <= 0) AudioSource.PlayClipAtPoint(GetClip(id), AudioListenerPosition());
        else Timing.RunCoroutine(_PlaySFX(id, delay));
    }

    private AudioClip GetClip(CommonSFX id)
    {
        return _sfxCommonList.Find(commonSfx => commonSfx.ID == id).SFX;
    }

    private IEnumerator<float> _PlaySFX(CommonSFX id, float delay)
    {
        yield return Timing.WaitForSeconds(delay);

        AudioSource.PlayClipAtPoint(GetClip(id), AudioListenerPosition());
    }
    #endregion
}
