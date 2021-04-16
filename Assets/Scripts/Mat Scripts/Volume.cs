using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Audio;

public class Volume : MonoBehaviour
{

    [SerializeField] private AudioMixer myAudioMixer;
    [SerializeField] private string parameterName;
    private float volValue;

    public void SetVolume (float vol)
    {
        myAudioMixer.SetFloat(parameterName, vol);
        volValue = vol;
    }

    public float GetVolume()
    {
        return volValue;
    }

}
