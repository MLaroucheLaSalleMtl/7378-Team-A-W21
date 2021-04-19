using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenChest : MonoBehaviour
{
    [Header("Text & Sprite Renderer")]
    [SerializeField] private Text textForChest;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite openSprite;

    [Header("GameObjects")]
    [SerializeField] private GameObject chestRewardHealthPotion;
    [SerializeField] private GameObject chestRewardStaminaPotion;
    [SerializeField] private GameObject chestRewardDamagePotion;
    [SerializeField] private GameObject chestRewardSpeedPotion;
    [SerializeField] private GameObject chestRewardAddProjectile;

    [Header("Score")]
    private ScoreAdded score;
    private int chestScore = 100;

    [Header("Random")]
    [SerializeField] private int random;

    [Header("Booleans")]
    private bool isOpen = false;
    private bool canOpen = false;

    [Header("Pause Settings")]
    private PauseSettings pause;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip openChestClip;

    private void Start()
    {
        GameObject scoreObject = GameObject.FindGameObjectWithTag("Score");
        score = scoreObject.GetComponent<ScoreAdded>();

        GameObject pauseObject = GameObject.FindGameObjectWithTag("Pause");
        pause = pauseObject.GetComponent<PauseSettings>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Interact") && isOpen == false && canOpen == true && pause.isPaused == false)
        {
            random = Random.Range(1, 101);

            if (random >= 1 && random <= 20)
            {
                Instantiate(chestRewardHealthPotion, transform.position + new Vector3(0, -0.5f, 0), Quaternion.identity);
            }
            else if (random >= 21 && random <= 40)
            {
                Instantiate(chestRewardStaminaPotion, transform.position + new Vector3(0, -0.5f, 0), Quaternion.identity);
            }
            else if (random >= 41 && random <= 50)
            {
                Instantiate(chestRewardDamagePotion, transform.position + new Vector3(0, -0.5f, 0), Quaternion.identity);
            }
            else if (random >= 51 && random <= 60)
            {
                Instantiate(chestRewardSpeedPotion, transform.position + new Vector3(0, -0.5f, 0), Quaternion.identity);
            }
            else if (random > 60)
            {
                Instantiate(chestRewardAddProjectile, transform.position + new Vector3(0, -0.5f, 0), Quaternion.identity);
            }

            isOpen = true;
            spriteRenderer.sprite = openSprite;
            AudioClipManager.instance.PlayHitSound(openChestClip);
            textForChest.gameObject.SetActive(false);
            score.GainScore(chestScore);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && isOpen == false)
        {
            textForChest.gameObject.SetActive(true);
            canOpen = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            textForChest.gameObject.SetActive(false);
            canOpen = false;
        }
    }
}
