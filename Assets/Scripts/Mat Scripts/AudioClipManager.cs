using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClipManager : MonoBehaviour
{

    public static AudioClipManager instance;

    private AudioSource playerAudio;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayHitSound(AudioClip soundToPlay)
    {
        playerAudio.clip = soundToPlay;
        playerAudio.Play();
    }
}
