using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Saving : MonoBehaviour
{
    public bool wasLoaded = false;

    // Start is called before the first frame update
    void Start()
    {
        wasLoaded = (PlayerPrefs.GetInt("LoadBool") != 0);

        if (wasLoaded == true)
        {
            Debug.Log("Loaded");
            OnLoad();
            wasLoaded = false;
            PlayerPrefs.SetInt("LoadBool", (wasLoaded ? 1 : 0));
            PlayerPrefs.Save();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && SceneManager.GetActiveScene().buildIndex != 1)
        {
            OnSave();
            Debug.Log("Saved");
        }
        else if (collision.gameObject.CompareTag("Player") && SceneManager.GetActiveScene().buildIndex == 1)
        {
            wasLoaded = true;
            PlayerPrefs.SetInt("LoadBool", (wasLoaded ? 1 : 0));
            PlayerPrefs.Save();
            SceneManager.LoadScene(sceneBuildIndex: PlayerPrefs.GetInt("Scene"));
        }
    }

    private void OnSave()
    {
        PlayerPrefs.SetInt("PosX", (int)GameObject.FindGameObjectWithTag("Player").transform.position.x);
        PlayerPrefs.SetInt("PosY", (int)GameObject.FindGameObjectWithTag("Player").transform.position.y);
        PlayerPrefs.SetInt("Keys", GameObject.FindGameObjectWithTag("Player").GetComponent<Keys>().numberOfKeys);
        PlayerPrefs.SetInt("HP", (int)GameObject.FindGameObjectWithTag("Player").GetComponent<HealthBar>().CurrentHealth);
        PlayerPrefs.SetInt("Stamina", GameObject.FindGameObjectWithTag("Player").GetComponent<StaminaBar>().CurrentStamina);
        PlayerPrefs.SetInt("Scene", SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.Save();
    }

    private void OnLoad()
    {
        Vector3 position = new Vector3(PlayerPrefs.GetInt("PosX"), PlayerPrefs.GetInt("PosY"));
        GameObject.FindGameObjectWithTag("Player").transform.position = position;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Keys>().numberOfKeys = PlayerPrefs.GetInt("Keys");
        GameObject.FindGameObjectWithTag("Player").GetComponent<HealthBar>().CurrentHealth = PlayerPrefs.GetInt("HP");
        GameObject.FindGameObjectWithTag("Player").GetComponent<StaminaBar>().CurrentStamina = PlayerPrefs.GetInt("Stamina");
    }

}
