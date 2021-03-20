using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpSystem : MonoBehaviour
{
    [SerializeField] private int scoreDivider = 2;
    private ScoreAdded score;


    void Start()
    {
        GameObject scoreObject = GameObject.FindGameObjectWithTag("Score");
        score = scoreObject.GetComponent<ScoreAdded>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
