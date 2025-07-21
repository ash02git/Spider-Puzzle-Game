using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Playbutton : MonoBehaviour
{
    public string scenename = "Gameplay";
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SceneSwitcher()
    {
        {
            if (SceneManager.GetActiveScene().name == "Homescreen")
                SceneManager.LoadScene(scenename);
            else if (SceneManager.GetActiveScene().name == "EndScreen")
                SceneManager.LoadScene("Gameplay");
        }
    }

    public void ApplicationQuitter()
    {

        Debug.Log("quit application");
        Application.Quit();
    }
}
