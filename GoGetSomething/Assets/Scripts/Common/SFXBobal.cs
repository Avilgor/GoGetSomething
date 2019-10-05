using System;
using System.Collections.Generic;
using MEC;
using UnityEngine;

public class SFXBobal : MonoBehaviour
{   
    #region Fields

    public static SFXBobal I;

    [SerializeField] private Transform _audioListenerTransform;
    [SerializeField] private List<SFXData> _sfxList;

    public enum ID
    {
        Null = -1,
        Knife,
        BearStripped,
        SecretBox
    }

    [Serializable]
    public struct SFXData
    {
        public ID ID;
        public AudioClip SFX;
    }
    #endregion

    #region Unity Functions

    private void Start()
    {
        I = this;
    }

    #endregion

    #region Other Functions

    public void PlaySFX(ID id, float delay = 0)
    {
        if (delay <= 0) AudioSource.PlayClipAtPoint(GetClip(id), _audioListenerTransform.position);
        else Timing.RunCoroutine(_PlaySFX(id, delay));
    }

    private AudioClip GetClip(ID id)
    {
        var clip = _sfxList.Find(commonSfx => commonSfx.ID == id).SFX;

        return clip;
    }

    private IEnumerator<float> _PlaySFX(ID id, float delay)
    {
        yield return Timing.WaitForSeconds(delay);

        AudioSource.PlayClipAtPoint(GetClip(id), _audioListenerTransform.position);
    }

    #endregion
}