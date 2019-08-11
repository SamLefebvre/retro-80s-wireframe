using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMusic : MonoBehaviour
{
    private AudioClip[] audioClips;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioClips = Resources.LoadAll<AudioClip>("music");
        audioSource = gameObject.GetComponent<AudioSource>();

        //var a = audioClips[Random.Range(0, audioClips.Length)];

        //sound.clip = a;
        //sound.Play();

        
    }

    

    // Update is called once per frame
    void LateUpdate()
    {
        if (!audioSource.isPlaying)
        {
            PlayRandomMusic();
        }
    }

    void PlayRandomMusic()
    {
        audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
        audioSource.Play();
    }
}
