using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetAxis("R_Joystick_V") > 0.1)
        {
            int i = 0;
        }

        if (Input.GetAxis("R_Joystick_H") > 0.1)
        {
            int i = 0;
        }

        if (Input.GetAxis("L_Joystick_V") > 0.1)
        {
            int i = 0;
        }

        if (Input.GetAxis("L_Joystick_H") > 0.1)
        {
            int i = 0;
        }



        if (Input.GetButtonDown("A"))
        {
            int i = 0;
        }

        if (Input.GetButtonDown("B"))
        {
            int i = 0;
        }

        if (Input.GetButtonDown("X"))
        {
            int i = 0;
        }

        if (Input.GetButtonDown("Y"))
        {
            int i = 0;
        }



        if (Input.GetButtonDown("R1"))
        {
            int i = 0;
        }

        if (Input.GetAxis("R2") > 0.1)
        {
            int i = 0;
        }

        if (Input.GetButtonDown("L1"))
        {
            int i = 0;
        }

        if (Input.GetAxis("L2") > 0.1)
        {
            int i = 0;
        }



    }
}
