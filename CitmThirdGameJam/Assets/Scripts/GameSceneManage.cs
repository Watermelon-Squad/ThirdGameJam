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
        SceneManager.LoadScene("MainScene");
    }


    public void LoadEndScene()
    {
        SceneManager.LoadScene("FinishScene");
    }

    public void ShowPlayer1WinLabel()
    {
        Debug.Log("Player 1 WINS");
        //SceneManager.LoadScene("FinishScene");
    }

    public void ShowPlayer2WinLabel()
    {
        Debug.Log("Player 2 WINS");
        //SceneManager.LoadScene("FinishScene");
    }
}
