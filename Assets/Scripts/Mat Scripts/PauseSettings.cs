using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseSettings : MonoBehaviour
{
    private GameObject pauseCanvas;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject controlPanel;
    [SerializeField] private GameObject SoundPanel;
    public bool isPaused = false;
    private Slider[] sliders;

    public static PauseSettings instance;

    private void Awake()
    {
        instance = this;
    }


    void Start()
    {
        sliders = SoundPanel.GetComponentsInChildren<Slider>();
        pauseCanvas = GameObject.FindGameObjectWithTag("Pause");
        sliders[0].value = PlayerPrefs.GetFloat("MasterVolume");
        sliders[1].value = PlayerPrefs.GetFloat("SoundVolume");
        sliders[2].value = PlayerPrefs.GetFloat("SFXVolume");
        //HideCursor();
    }

    void Update()
    {
        if(Input.GetButtonDown("Cancel") && isPaused == false)
        {
            PauseGame();
        }
        else if (Input.GetButtonDown("Cancel") && isPaused == true && pausePanel.activeInHierarchy == true)
        {
            Resume();

        }
        else if (Input.GetButtonDown("Cancel") && isPaused == true && pausePanel.activeInHierarchy == false)
        {
            back();
        }
        else if (Input.GetButtonDown("Cancel") && isPaused == true && pausePanel.activeInHierarchy == false && SoundPanel.activeInHierarchy == true)
        {
            
            PlayerPrefs.SetFloat("MasterVolume", sliders[0].value);
            PlayerPrefs.SetFloat("SoundVolume", sliders[1].value);
            PlayerPrefs.SetFloat("SFXVolume", sliders[2].value);
            PlayerPrefs.Save();
            back();

        }
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        isPaused = false;
        //HideCursor();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        controlPanel.SetActive(false);
        isPaused = true;
        //ShowCursor();
        
    }

    public void GoToSubMenu(GameObject panelToActivate)
    {
        pausePanel.SetActive(false);
        panelToActivate.SetActive(true);
    }


    public void back()
    {
        controlPanel.SetActive(false);
        SoundPanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    private void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    private void ShowCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
    }

}
