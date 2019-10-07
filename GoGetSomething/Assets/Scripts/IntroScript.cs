using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class IntroScript : MonoBehaviour
{
    VideoPlayer player;
    [SerializeField]AudioClip sound;
    private bool started,finished;
    void Start()
    {
        player = GetComponent<VideoPlayer>();
        started = false;
        finished = false;
        GetComponent<AudioSource>().PlayOneShot(sound);
    }

    void Update()
    {
        if (player.isPlaying)       
            started = true;

        if (!player.isPlaying && started)
            finished = true;

        if (finished)
            SceneManager.LoadScene(1);
    }
}