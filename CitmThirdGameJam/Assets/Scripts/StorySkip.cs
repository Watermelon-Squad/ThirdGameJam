using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StorySkip : MonoBehaviour
{
    public GameObject[] story;
    public GameSceneManage sceneManage;

    int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown)
        {
            story[i].active = false;
            i++;
        }

        if(i == 3)
        {
            if (sceneManage)
                sceneManage.LoadMainScene();
        }
    }
}
