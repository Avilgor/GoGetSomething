/**
 * AudioClipsSingleton.cs
 * Created by Akeru on 22/03/2019
 * Copyright © iBoo Mobile. All rights reserved.
 */

using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public enum SongsEnum
{
    Empty,
    Main,
    Butler,
    Bobal,
    Merlot,
    Gardener,
    ScanB
}

public enum AmbientSoundEnum
{
    AmbientCrowd
}

public abstract  class AudioClipsSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    #region Serializabled/Public Variables

    [Title("Setup")]
    [SerializeField] protected float VolumeStandard = 0.6f;
    [SerializeField] protected float VolumeLow = 0.2f;
    [SerializeField] protected int AudioSourcesToUse = 2;

    [SerializeField] protected List<AudioSource> AudioSources;

    #endregion

    #region Private Variables
    private int CurrentAudioSourceId;

    [SerializeField] protected AudioSource CurrentAudioSource;
    #endregion

    #region Singleton

    public static T I;

    protected virtual void Awake()
    {
        if (I == null)
        {
            I = (T)FindObjectOfType(typeof(T));
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
    #endregion

    #region Mono Functions
    private void Start()
    {
        CurrentAudioSource = GetAvailableAudioSource();
    }

    #endregion

    #region My Functions

    public void VolumeDown()
    {
        DOTween.Kill("ClipTween" + GetInstanceID());
        DOTween.To(() => CurrentAudioSource.volume, value => CurrentAudioSource.volume = value, VolumeLow, 0.5f).SetEase(Ease.InOutSine).SetId("ClipTween" + GetInstanceID());
    }

    public void VolumeUp()
    {
        DOTween.Kill("ClipTween" + GetInstanceID());
        DOTween.To(() => CurrentAudioSource.volume, value => CurrentAudioSource.volume = value, VolumeStandard, 0.5f).SetEase(Ease.InOutSine).SetId("ClipTween" + GetInstanceID());
    }

    public void Pause()
    {
        DOTween.Kill("ClipTween" + GetInstanceID());
        DOTween.To(() => CurrentAudioSource.volume, value => CurrentAudioSource.volume = value, 0, 1).SetEase(Ease.InOutSine).OnComplete(CurrentAudioSource.Pause).SetId("ClipTween" + GetInstanceID());
    }

    public void Resume()
    {
        DOTween.Kill("ClipTween" + GetInstanceID());
        CurrentAudioSource.Play();
        VolumeUp();
    }

    protected void ChangeAudioSource()
    {
        CurrentAudioSourceId++;
        if (CurrentAudioSourceId >= AudioSourcesToUse) CurrentAudioSourceId = 0;

        CurrentAudioSource = GetAvailableAudioSource();
    }

    protected void StopAudioSource(AudioSource a)
    {
        a.clip = null;
        a.loop = false;
        a.Stop();
    }

    protected AudioSource GetAvailableAudioSource()
    {
        if (AudioSources.Count <= 0) AudioSources = new List<AudioSource>();
        if (AudioSources.Count < CurrentAudioSourceId+1)
        {
            var newAs = gameObject.AddComponent<AudioSource>();
            newAs.volume = 0;
            AudioSources.Add(newAs);

        }

        return AudioSources[CurrentAudioSourceId];
    }

    #endregion
}
