using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StorySkip : MonoBehaviour
{
    public GameObject[] story;
    public GameSceneManage sceneManage;

    int i = 0;
    bool transitioning_scene = false;

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
            if (sceneManage && !transitioning_scene)
            {
                sceneManage.LoadMainScene();
                transitioning_scene = true;
            }
        }
    }
}
