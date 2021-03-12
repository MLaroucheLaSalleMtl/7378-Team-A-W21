using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Saving : MonoBehaviour
{
    private string[] Save1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && SceneManager.GetActiveScene().buildIndex == 2)
        {
            OnSave();
        }
        else if(collision.CompareTag("Player") && SceneManager.GetActiveScene().buildIndex == 1)
        {
            SceneManager.LoadScene(sceneBuildIndex: PlayerPrefs.GetInt(Save1[5]));
            OnLoad();
            
        }
    }

    private void OnSave()
    {
        PlayerPrefs.SetInt(Save1[0] , (int)GameObject.FindGameObjectWithTag("Player").transform.position.x);
        PlayerPrefs.SetInt(Save1[1], (int)GameObject.FindGameObjectWithTag("Player").transform.position.y);
        PlayerPrefs.SetInt(Save1[2], GameObject.FindGameObjectWithTag("Player").GetComponent<Keys>().numberOfKeys);
        PlayerPrefs.SetInt(Save1[3], (int)GameObject.FindGameObjectWithTag("Player").GetComponent<HealthBar>().CurrentHealth);
        PlayerPrefs.SetInt(Save1[4], GameObject.FindGameObjectWithTag("Player").GetComponent<StaminaBar>().CurrentStamina);
        PlayerPrefs.SetInt(Save1[5], SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Saved");
    }

    private void OnLoad()
    {
        Vector3 position = new Vector3(PlayerPrefs.GetInt(Save1[0]), PlayerPrefs.GetInt(Save1[1]));
        GameObject.FindGameObjectWithTag("Player").transform.position = position;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Keys>().numberOfKeys = PlayerPrefs.GetInt(Save1[2]);
        GameObject.FindGameObjectWithTag("Player").GetComponent<HealthBar>().CurrentHealth = PlayerPrefs.GetInt(Save1[3]);
        GameObject.FindGameObjectWithTag("Player").GetComponent<StaminaBar>().CurrentStamina = PlayerPrefs.GetInt(Save1[4]);
        //SceneManager.LoadScene(sceneBuildIndex: PlayerPrefs.GetInt(Save1[5]));
    }

}
