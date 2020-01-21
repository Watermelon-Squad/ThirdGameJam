using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManage : MonoBehaviour
{

    void Start()
    {
        
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
