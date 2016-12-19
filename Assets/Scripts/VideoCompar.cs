using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]

public class VideoCompar : MonoBehaviour {

    public RawImage video1;
    public MovieTexture movie;
    private AudioSource audioS;

    void Start()
    {
        GetComponent<RawImage>().texture = movie as MovieTexture;
        audioS = GetComponent<AudioSource>();
        audioS.clip = movie.audioClip;
        video1.enabled = false;
    }

    public void playVideo()
    {
        video1.enabled = true;
        movie.Play();
        audioS.Play();
    }
}
