using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScreen : MonoBehaviour
{
    public GameObject[] wins;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(StaticData.Win == 0)
        {
            wins[0].active = true;
        }
        if (StaticData.Win == 1)
        {
            wins[1].active = true;
        }
    }
}
