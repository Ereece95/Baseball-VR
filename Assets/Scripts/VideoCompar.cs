using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]

public class VideoCompar : MonoBehaviour
{

    public RawImage video1;
    public MovieTexture movie;
    private AudioSource audioS;
    private GameController gc;

    void Start()
    {
        gc = GameObject.Find("GameController").GetComponent("GameController") as GameController;
        GetComponent<RawImage>().texture = movie as MovieTexture;
        audioS = GetComponent<AudioSource>();
        audioS.clip = movie.audioClip;
        video1.enabled = false;
    }

    private void Update()
    {
        if (movie.isPlaying == false && gc.GetState() != States.VideoPlay)
        {
            video1.enabled = false;
        }
    }

    public void playVideo()
    {
        video1.enabled = true;
        movie.Play();
        audioS.Play();
    }
}
