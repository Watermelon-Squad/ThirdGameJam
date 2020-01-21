using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSceneManage : MonoBehaviour
{

    public GameObject p1WinLabelGO;
    public GameObject p2WinLabelGO;

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
        if (p1WinLabelGO)
            p1WinLabelGO.SetActive(true);
    }

    public void ShowPlayer2WinLabel()
    {
        if (p2WinLabelGO)
            p2WinLabelGO.SetActive(true);
    }
}
