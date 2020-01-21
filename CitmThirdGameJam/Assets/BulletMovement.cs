using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BulletMovement : MonoBehaviour
{
    enum PlayerDirection
    {
        UP,
        DOWN,
        RIGHT,
        LEFT
    }

    public float bulletSpeed = 0.0f;
    Rigidbody2D rb;
    PlayerDirection pd;
    // Start is called before the first frame update
    void Start()
    {
        pd = PlayerDirection.UP;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        if (pd == PlayerDirection.UP)
        {
            pos.y += bulletSpeed * Time.deltaTime;
        }
        else if (pd == PlayerDirection.DOWN)
        {
            pos.y -= bulletSpeed * Time.deltaTime;
        }
        else if (pd == PlayerDirection.RIGHT)
        {
            pos.x += bulletSpeed * Time.deltaTime;
        }
        else if (pd == PlayerDirection.LEFT)
        {
            pos.x -= bulletSpeed * Time.deltaTime;
        }


        transform.position = pos;
    }
}
