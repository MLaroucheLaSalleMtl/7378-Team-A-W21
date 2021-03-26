using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelTrigger : MonoBehaviour
{
    public GameObject LevelUpPanel;
    private bool onlyOnce = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && onlyOnce == true)
        {
            LevelUpPanel.SetActive(true);
        }
    }

    

    
}
