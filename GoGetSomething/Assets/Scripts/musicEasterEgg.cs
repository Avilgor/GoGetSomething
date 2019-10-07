using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicEasterEgg : MonoBehaviour
{
    [SerializeField] AudioClip original;
    [SerializeField] AudioClip party;
    private bool _original;
    private AudioSource audio;

    void Start()
    {
        _original = true;
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F10))
        {
            audio.Stop();
            if (_original)
            {
                audio.clip = party;
                audio.Play();
                _original = false;
            }
            else
            {
                audio.clip = original;
                audio.Play();
                _original = true;
            }
        }
    }
}
