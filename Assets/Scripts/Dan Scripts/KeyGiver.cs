using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyGiver : MonoBehaviour
{
    [SerializeField] public Text keyCounter;
    // Start is called before the first frame update
    void Start()
    {
        keyCounter.text = GameObject.FindGameObjectWithTag("Player").GetComponent<Keys>().numberOfKeys.ToString("0");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GiveKey()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Keys>().numberOfKeys++;
        keyCounter.text = GameObject.FindGameObjectWithTag("Player").GetComponent<Keys>().numberOfKeys.ToString("0");
    }

}
