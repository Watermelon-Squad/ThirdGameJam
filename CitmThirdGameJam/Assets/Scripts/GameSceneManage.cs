using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSceneManage : MonoBehaviour
{

    public GameObject p1WinLabelGO;
    public GameObject p2WinLabelGO;
    public float secondsToFinishScreen = 0.0f;
    public bool do_actions = true;

    int player_win = 0;

    public Animator transition;
    public float transitionTime = 1.0f;

    void Start()
    {

    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    public void LoadMainScene()
    {
        StartCoroutine(LoadSceneFade("Story"));
    }


    IEnumerator WaitForSceneChange()
    {
        yield return new WaitForSeconds(secondsToFinishScreen);
        StartCoroutine(LoadSceneFade("FinishScene"));
    }

    IEnumerator LoadSceneFade(string scene)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(scene);

    }

    public void LoadEndSceneWait()
    {
        StartCoroutine("WaitForSceneChange");
    }

    public void ShowPlayer1WinLabel()
    {
        if (p1WinLabelGO)
            p1WinLabelGO.SetActive(true);

        StaticData.Win = 0;

        do_actions = false;
    }

    public void ShowPlayer2WinLabel()
    {
        if (p2WinLabelGO)
            p2WinLabelGO.SetActive(true);

        StaticData.Win = 1;

        do_actions = false;
    }
}
