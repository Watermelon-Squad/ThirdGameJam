using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowBehaviour : MonoBehaviour
{
    public enum PlayerInput { WAIT =0, UP, RIGHT, LEFT, DOWN, SHOOT, NONE };
    public PlayerInput input = PlayerInput.NONE;

    private Queue<PlayerInput> playerInput;

    private bool do_action = false;

    [HideInInspector]
    public GameObject player_parent = null;

    [SerializeField]
    private float delay = 0.5f;
    private float actual_time = 0.0f;

    private float player_vel = 0.0f;

    private Rigidbody2D rigidbody2D = null;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = new Queue<PlayerInput>();
        do_action = true;
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(do_action)
        {
            if(actual_time >= delay)
            {

                PlayerInput actual_action;

                if (playerInput.Count == 0)
                {
                    actual_action = PlayerInput.NONE;
                }
                else
                    actual_action = playerInput.Dequeue();

                if (actual_action == PlayerInput.UP)
                {
                    rigidbody2D.AddForce(Vector2.up * player_vel * Time.deltaTime);

                }
                else if (actual_action == PlayerInput.DOWN)
                {
                    rigidbody2D.AddForce(Vector2.down * player_vel * Time.deltaTime);

                }
                else if (actual_action == PlayerInput.RIGHT)
                {
                    rigidbody2D.AddForce(Vector2.right * player_vel * Time.deltaTime);

                }
                else if (actual_action == PlayerInput.LEFT)
                {
                    rigidbody2D.AddForce(Vector2.left * player_vel * Time.deltaTime);

                }
                else if (actual_action == PlayerInput.SHOOT)
                {

                }

            }
            else
            {
                actual_time += Time.deltaTime;
            }
        }
    }

    public void SetPlayerInput(PlayerInput input)
    {
        if (playerInput == null)
            playerInput = new Queue<PlayerInput>();

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
