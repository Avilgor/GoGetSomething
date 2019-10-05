using System;
using System.Collections.Generic;
using MEC;
using UnityEngine;

public class SFXButler : MonoBehaviour
{   
    #region Fields

    public static SFXButler I;

    [SerializeField] private Transform _audioListenerTransform;
    [SerializeField] private AudioClip[] _agonyScreams;
    [SerializeField] private List<SFXData> _sfxCommonList;

    public enum ID
    {
        Null = -1,
        DoorOpen, DoorLocked,
        KeyFound,
        AgonyScream,
        Footstep1, Footstep2,
        Drink,

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

    public void PlayFootsteps(float time, float timeBetween, float delay)
    {
        Timing.RunCoroutine(_PlayFootsteps(time, timeBetween, delay));
    }

    private AudioClip GetClip(ID id)
    {
        AudioClip clip;
        if (id == ID.AgonyScream) clip = _agonyScreams[UnityEngine.Random.Range(0, _agonyScreams.Length - 1)];
        else clip = _sfxCommonList.Find(commonSfx => commonSfx.ID == id).SFX;

        return clip;
    }

    private IEnumerator<float> _PlaySFX(ID id, float delay)
    {
        yield return Timing.WaitForSeconds(delay);

        AudioSource.PlayClipAtPoint(GetClip(id), _audioListenerTransform.position);
    }

    private IEnumerator<float> _PlayFootsteps(float time, float timeBetween, float delay)
    {
        yield return Timing.WaitForSeconds(delay);

        var counter = 0f;
        var left = false;

        while (counter < time)
        {
            counter += timeBetween;
            AudioSource.PlayClipAtPoint(GetClip(left ? ID.Footstep1 : ID.Footstep2), _audioListenerTransform.position);
            left = !left;
            yield return Timing.WaitForSeconds(timeBetween);
        }
    }

    #endregion
}