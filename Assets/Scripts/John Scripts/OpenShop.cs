using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenShop : MonoBehaviour
{
    [SerializeField] private Text textForShopKeeper;
    [SerializeField] private Text shopPoints;
    private bool canOpen;

    [SerializeField] private GameObject panelLevelUp;

    private ScoreAdded score;
    private PauseSettings pause;

    private void Start()
    {
        GameObject scoreObject = GameObject.FindGameObjectWithTag("Score");
        score = scoreObject.GetComponent<ScoreAdded>();

        GameObject pauseObject = GameObject.FindGameObjectWithTag("Pause");
        pause = pauseObject.GetComponent<PauseSettings>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interact") && canOpen == true && pause.isPaused == false)
        {
            panelLevelUp.SetActive(true);
            shopPoints.text = "Score Points: " + score.currentScore.ToString();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            canOpen = true;
            textForShopKeeper.gameObject.SetActive(true);
        } 
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canOpen = false;
            textForShopKeeper.gameObject.SetActive(false);
        }
    }
}
