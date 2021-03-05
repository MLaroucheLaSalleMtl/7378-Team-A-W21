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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GiveKey()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Keys>().numberOfKeys++;
        keyCounter.text = "Keys: " + GameObject.FindGameObjectWithTag("Player").GetComponent<Keys>().numberOfKeys.ToString("0");
    }

}
