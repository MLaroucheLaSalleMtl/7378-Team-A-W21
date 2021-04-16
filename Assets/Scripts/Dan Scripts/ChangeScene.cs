using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] public int sceneNumber;

    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 3)
        {
            GameObject.FindGameObjectWithTag("Save").GetComponent<Saving>().OnLoad();
        }
        else if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            GameObject.FindGameObjectWithTag("Save").GetComponent<Saving>().OnLoad();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            if(SceneManager.GetActiveScene().buildIndex == 2)
            {
                GameObject.FindGameObjectWithTag("Save").GetComponent<Saving>().OnSave();
            }
            else if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                GameObject.FindGameObjectWithTag("Save").GetComponent<Saving>().OnSave();
            }
            else if (SceneManager.GetActiveScene().buildIndex == 4)
            {
                GameObject.FindGameObjectWithTag("Save").GetComponent<Saving>().OnSave();
            }

            SceneManager.LoadScene(sceneBuildIndex: sceneNumber);
        }
    }
}
