using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BulletMovement : MonoBehaviour
{
    public enum PlayerDirection
    {
        UP,
        DOWN,
        RIGHT,
        LEFT
    }

    public string parentTag = "No parent sad";

    public float bulletSpeed = 0.0f;
    Rigidbody2D rb;
    public PlayerDirection pd;
    // Start is called before the first frame update
    void Start()
    {

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (parentTag != collision.transform.tag)
        {
            if (collision.transform.tag == "ShadowPlayer1")
            {
                //Win condition for player 1 and lose for player 2
            }
            else if (collision.transform.tag == "ShadowPlayer2")
            {
                //Win condition for player 2 and lose for player 1
            }
            else
            {
                if (collision.transform.tag == "Player1")
                {
                    collision.transform.GetComponent<PlayerController>().DieInPresent();

                }
                else if (collision.transform.tag == "Player2")
                {
                    collision.transform.GetComponent<SecondPlayerController>().DieInPresent();

                }
            }

        }
    }
}

