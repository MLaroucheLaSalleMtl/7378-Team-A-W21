using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Saving : MonoBehaviour
{
    //public bool wasLoaded = false;

    // Start is called before the first frame update
    /*void Start()
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
    }*/

    /*private void OnTriggerEnter2D(Collider2D collision)
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
    }*/

    public void OnSave()
    {
        //PlayerPrefs.SetInt("PosX", (int)GameObject.FindGameObjectWithTag("Player").transform.position.x);
        //PlayerPrefs.SetInt("PosY", (int)GameObject.FindGameObjectWithTag("Player").transform.position.y);
        //PlayerPrefs.SetInt("Keys", GameObject.FindGameObjectWithTag("Player").GetComponent<Keys>().numberOfKeys);
        PlayerPrefs.SetInt("Score", GameObject.FindGameObjectWithTag("Score").GetComponent<ScoreAdded>().currentScore);
        PlayerPrefs.SetInt("HP", (int)GameObject.FindGameObjectWithTag("Player").GetComponent<HealthBar>().CurrentHealth);
        PlayerPrefs.SetInt("MaxHP", (int)GameObject.FindGameObjectWithTag("Player").GetComponent<HealthBar>().MaxHealth);
        PlayerPrefs.SetInt("Stamina", GameObject.FindGameObjectWithTag("Player").GetComponent<StaminaBar>().CurrentStamina);
        PlayerPrefs.SetInt("MaxStamina", GameObject.FindGameObjectWithTag("Player").GetComponent<StaminaBar>().MaxStamina);
        PlayerPrefs.SetInt("Attack", GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterAttack>().dmg);
        PlayerPrefs.SetFloat("UltCool", GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterAttack>().UltCd);
        PlayerPrefs.SetInt("ProjCount", GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterAttack>().projCount);
        PlayerPrefs.SetInt("#HpPot", GameObject.FindGameObjectWithTag("Player").GetComponent<PotionHandler>().numHealthPotion);
        PlayerPrefs.SetInt("#StamPot", GameObject.FindGameObjectWithTag("Player").GetComponent<PotionHandler>().numStaminaPotion);
        PlayerPrefs.SetInt("#SpeedPot", GameObject.FindGameObjectWithTag("Player").GetComponent<PotionHandler>().numSpeedPotion);
        PlayerPrefs.SetInt("#DmgPot", GameObject.FindGameObjectWithTag("Player").GetComponent<PotionHandler>().numDamagePotion);
        PlayerPrefs.SetInt("HpPotBool", (GameObject.FindGameObjectWithTag("Player").GetComponent<PotionHandler>().hasHealPotion ? 1 : 0));
        PlayerPrefs.SetInt("StamPotBool", (GameObject.FindGameObjectWithTag("Player").GetComponent<PotionHandler>().hasStaminaPotion ? 1 : 0));
        PlayerPrefs.SetInt("SpeedPotBool", (GameObject.FindGameObjectWithTag("Player").GetComponent<PotionHandler>().hasSpeedPotion ? 1 : 0));
        PlayerPrefs.SetInt("DmgPotBool", (GameObject.FindGameObjectWithTag("Player").GetComponent<PotionHandler>().hasDamagePotion ? 1 : 0));
        PlayerPrefs.SetInt("Scene", SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.Save();
    }

    public void OnLoad()
    {
        //Vector3 position = new Vector3(PlayerPrefs.GetInt("PosX"), PlayerPrefs.GetInt("PosY"));
        //GameObject.FindGameObjectWithTag("Player").transform.position = position;
        //GameObject.FindGameObjectWithTag("Player").GetComponent<Keys>().numberOfKeys = PlayerPrefs.GetInt("Keys");
        GameObject.FindGameObjectWithTag("Score").GetComponent<ScoreAdded>().currentScore = PlayerPrefs.GetInt("Score");
        GameObject.FindGameObjectWithTag("Player").GetComponent<HealthBar>().CurrentHealth = PlayerPrefs.GetInt("HP");
        GameObject.FindGameObjectWithTag("Player").GetComponent<HealthBar>().MaxHealth = PlayerPrefs.GetInt("MaxHP");
        GameObject.FindGameObjectWithTag("Player").GetComponent<StaminaBar>().CurrentStamina = PlayerPrefs.GetInt("Stamina");
        GameObject.FindGameObjectWithTag("Player").GetComponent<StaminaBar>().MaxStamina = PlayerPrefs.GetInt("MaxStamina");
        GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterAttack>().dmg = PlayerPrefs.GetInt("Attack");
        GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterAttack>().UltCd = PlayerPrefs.GetFloat("UltCool");
        GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterAttack>().projCount = PlayerPrefs.GetInt("ProjCount");
        GameObject.FindGameObjectWithTag("Player").GetComponent<PotionHandler>().numHealthPotion = PlayerPrefs.GetInt("#HpPot");
        GameObject.FindGameObjectWithTag("Player").GetComponent<PotionHandler>().numStaminaPotion = PlayerPrefs.GetInt("#StamPot");
        GameObject.FindGameObjectWithTag("Player").GetComponent<PotionHandler>().numSpeedPotion = PlayerPrefs.GetInt("#SpeedPot");
        GameObject.FindGameObjectWithTag("Player").GetComponent<PotionHandler>().numDamagePotion = PlayerPrefs.GetInt("#DmgPot");
        GameObject.FindGameObjectWithTag("Player").GetComponent<PotionHandler>().hasHealPotion = (PlayerPrefs.GetInt("HpPotBool") != 0);
        GameObject.FindGameObjectWithTag("Player").GetComponent<PotionHandler>().hasStaminaPotion = (PlayerPrefs.GetInt("StamPotBool") != 0);
        GameObject.FindGameObjectWithTag("Player").GetComponent<PotionHandler>().hasSpeedPotion = (PlayerPrefs.GetInt("SpeedPotBool") != 0);
        GameObject.FindGameObjectWithTag("Player").GetComponent<PotionHandler>().hasDamagePotion = (PlayerPrefs.GetInt("DmgPotBool") != 0);
    }

}
