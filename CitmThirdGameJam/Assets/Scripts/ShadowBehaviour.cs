using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowBehaviour : MonoBehaviour
{
    public enum PlayerInput { WAIT =0, UP, RIGHT, LEFT, DOWN, SHOOT, NONE };
    public PlayerInput input = PlayerInput.NONE;

    private Queue<PlayerInput> playerInput;

    private bool do_action = false;

    [SerializeField]
    private float delay = 0.5f;
    private float actual_time = 0.0f;

    private float player_vel = 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        playerInput = new Queue<PlayerInput>();
        do_action = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(do_action)
        {
            if(actual_time >= delay)
            {
                Vector3 pos = transform.position;

                if(playerInput.Count == 0)
                {
                    int x = 0;
                }


                PlayerInput actual_action = playerInput.Dequeue();

                if (actual_action == PlayerInput.UP)
                {
                    pos.y += player_vel * Time.deltaTime;

                }
                else if (actual_action == PlayerInput.DOWN)
                {
                    pos.y -= player_vel * Time.deltaTime;
 
                }
                else if (actual_action == PlayerInput.RIGHT)
                {
                    pos.x += player_vel * Time.deltaTime;

                }
                else if (actual_action == PlayerInput.LEFT)
                {
                    pos.x -= player_vel * Time.deltaTime;

                }

                transform.position = pos;

            }
            else
            {
                actual_time += Time.deltaTime;
            }
        }
    }

    public void SetPlayerInput(PlayerInput input)
    {
        playerInput.Enqueue(input);
    }

    public void StartAction()
    {
        do_action = true;
    }

    public void setVel(float pvel)
    {
        player_vel = pvel;
    }
}
