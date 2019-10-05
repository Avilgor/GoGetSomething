/**
 * MusicController.cs
 * Created by Akeru on 13/03/2019
 * Copyright © iBoo Mobile. All rights reserved.
 */

using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

[Serializable]
public struct SongsClass
{
    public SongsEnum Id;
    public AudioClip Song;
}

public class MusicController : AudioClipsSingleton<MusicController>
{
    #region Serializabled/Public Variables

    [Title("Setup")]
    [SerializeField] private float _standardVolume = 0.6f;
    [SerializeField] private int _audioSourcesToUse = 2;

    [Title("Songs")]
    [SerializeField] private List<SongsClass> _songs;

    #endregion

    #region Private Variables

    public SongsEnum CurrentSong { get; private set; }

    #endregion

    #region Mono Functions
    private void Start()
    {
        CurrentAudioSource = GetAvailableAudioSource();
//        InitAudioSource(SongsEnum.Main, VolumeStandard);
    }
    #endregion

    #region My Functions

    private void InitAudioSource(SongsEnum songId, float volume = -1)
    {
        if (volume == -1) volume = _standardVolume;

        CurrentSong = songId;

        var clip = _songs.Find(s => s.Id == songId).Song;

        CurrentAudioSource.volume = 0;
        CurrentAudioSource.clip = clip;
        CurrentAudioSource.loop = true;
        CurrentAudioSource.Play();

        DOTween
            .To(() => CurrentAudioSource.volume, value => CurrentAudioSource.volume = value, volume, 1f)
            .SetEase(Ease.InOutSine)
            .SetId("MusicTween");
    }

    public void ChangeMusic(SongsEnum newId)
    {
        Debug.Log("<color=green> Changing Music | New ID: " + newId + " </color>");

        var tempAs = CurrentAudioSource;
        DOTween
            .To(() => tempAs.volume, value => tempAs.volume = value, 0, 0.5f)
            .SetEase(Ease.InOutSine)
            .SetId("MusicTween")
            .OnComplete(() => StopAudioSource(tempAs));

        ChangeAudioSource();
        InitAudioSource(newId);
    }

    #endregion
}
