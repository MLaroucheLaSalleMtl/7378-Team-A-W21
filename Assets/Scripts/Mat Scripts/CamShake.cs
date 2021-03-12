using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamShake : MonoBehaviour
{
    private CinemachineVirtualCamera camera;
    private float time;

    public static CamShake instance;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<CinemachineVirtualCamera>();
    }


    public void ShakeCam(float amplitude, float timer)
    {
        CinemachineBasicMultiChannelPerlin camShake = camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        camShake.m_AmplitudeGain = amplitude;
        time = timer;
    }

    // Update is called once per frame
    void Update()
    {
       if (time > 0)
        {
            time -= Time.deltaTime;
            if (time <= 0f)
            {
                CinemachineBasicMultiChannelPerlin camShake = camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                camShake.m_AmplitudeGain = 0;
            }
        }
    }
}
