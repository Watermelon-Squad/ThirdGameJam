using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowBehaviour : MonoBehaviour
{
    public enum PlayerInput { NONE };
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
        
    }

    // Update is called once per frame
    void Update()
    {
        if(do_action)
        {
            if(actual_time >= delay)
            {

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

}
