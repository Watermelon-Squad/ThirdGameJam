using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class SecondPlayerController : MonoBehaviour
{
    public enum AnimStates
    {
        UP,
        DOWN,
        RIGHT,
        LEFT,
        IDLE_NONE
    }

    public KeyCode up_input;
    public KeyCode down_input;
    public KeyCode left_input;
    public KeyCode right_input;
    public KeyCode shoot_input;

    public float speed = 0.0f;

    public GameObject shadow1 = null;
    private GameObject shadow_child = null;

    public GameObject Bullet = null;
    Vector2 bulletPos;

    private Rigidbody2D rigidbody2D;

    public float cooldown_shoot = 3.0f;
    private float actual_cooldown = 0.0f;

    public float shoot_time = 2.0f;
    private float actual_shoot_time = 0.0f;

    private SpriteRenderer sprite;
    private Animator animator;

    bool playerIndexSet = false;
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;

    private GameSceneManage sceneManaging = null;
    private AnimStates lastState = AnimStates.IDLE_NONE;
    // Start is called before the first frame update
    void Start()
    {
        actual_cooldown = cooldown_shoot;
        actual_shoot_time = shoot_time;
        InstanciateShadow(transform.position);

        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();

        GameObject go;
        go = GameObject.Find("Scene Manager");
        sceneManaging = go.GetComponent<GameSceneManage>();
    }

    void Update()
    {
        bool second_controller = false;

        if (!playerIndexSet || !prevState.IsConnected)
        {
            for (int i = 0; i < 4; ++i)
            {
                PlayerIndex testPlayerIndex = (PlayerIndex)i;
                GamePadState testState = GamePad.GetState(testPlayerIndex);
                if (testState.IsConnected)
                {
              
                    Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
                    playerIndex = testPlayerIndex;
                    playerIndexSet = true;

                    if (second_controller)
                    {
                        break;
                    }

                    second_controller = true;
                }
            }
        }


        prevState = state;
        state = GamePad.GetState(playerIndex);

        if (sceneManaging.do_actions)
        {
            if (actual_shoot_time >= shoot_time)
            {
                if (state.ThumbSticks.Left.Y > 0.1f || Input.GetKey(up_input))
                {
                    rigidbody2D.AddForce(Vector2.up * speed * Time.deltaTime);
                    // pos.y += speed * Time.deltaTime;
                    shadow_child.GetComponent<ShadowBehaviour>().SetPlayerInput(ShadowBehaviour.PlayerInput.UP);
                    animator.SetInteger("State", 0);
                    lastState = AnimStates.UP;

                    if (animator.GetBool("Idle"))
                    {
                        animator.SetBool("Idle", false);
                    }
                }
                else if (state.ThumbSticks.Left.Y < -0.1f || Input.GetKey(down_input))
                {
                    rigidbody2D.AddForce(Vector2.down * speed * Time.deltaTime);
                    //pos.y -= speed * Time.deltaTime;
                    shadow_child.GetComponent<ShadowBehaviour>().SetPlayerInput(ShadowBehaviour.PlayerInput.DOWN);
                    animator.SetInteger("State", 1);
                    lastState = AnimStates.DOWN;

                    if (animator.GetBool("Idle"))
                    {
                        animator.SetBool("Idle", false);
                    }
                }
                else if (state.ThumbSticks.Left.X > 0.1f || Input.GetKey(right_input))
                {
                    rigidbody2D.AddForce(Vector2.right * speed * Time.deltaTime);
                    //pos.x += speed * Time.deltaTime;
                    shadow_child.GetComponent<ShadowBehaviour>().SetPlayerInput(ShadowBehaviour.PlayerInput.RIGHT);
                    animator.SetInteger("State", 2);
                    lastState = AnimStates.RIGHT;

                    if (animator.GetBool("Idle"))
                    {
                        animator.SetBool("Idle", false);
                    }
                    //sprite.flipX = false;
                }
                else if (state.ThumbSticks.Left.X < -0.1f || Input.GetKey(left_input))
                {
                    rigidbody2D.AddForce(Vector2.left * speed * Time.deltaTime);
                    //pos.x -= speed * Time.deltaTime;
                    shadow_child.GetComponent<ShadowBehaviour>().SetPlayerInput(ShadowBehaviour.PlayerInput.LEFT);
                    animator.SetInteger("State", 3);
                    lastState = AnimStates.LEFT;

                    if (animator.GetBool("Idle"))
                    {
                        animator.SetBool("Idle", false);
                    }
                    //sprite.flipX = true;
                }
                else
                {
                    if (!animator.GetBool("Idle"))
                    {
                        animator.SetBool("Idle", true);
                    }
                    animator.SetInteger("State", -1);
                    shadow_child.GetComponent<ShadowBehaviour>().SetPlayerInput(ShadowBehaviour.PlayerInput.WAIT);
                }

            }
            else
            {
                if (!animator.GetBool("Idle"))
                {
                    animator.SetBool("Idle", true);
                }
                animator.SetInteger("State", -1);
                shadow_child.GetComponent<ShadowBehaviour>().SetPlayerInput(ShadowBehaviour.PlayerInput.WAIT);
                actual_shoot_time += Time.deltaTime;
            }


            if (actual_cooldown >= cooldown_shoot)
            {
                if (state.Buttons.LeftShoulder == ButtonState.Pressed || Input.GetKey(shoot_input))
                {
                    actual_cooldown = 0.0f;
                    actual_shoot_time = 0.0f;
                    shadow_child.GetComponent<ShadowBehaviour>().SetPlayerInput(ShadowBehaviour.PlayerInput.SHOOT);
                    Fire();
                }
            }
            else
            {
                actual_cooldown += Time.deltaTime;
            }
        }
    }

    private void Fire()
    {
        animator.SetBool("Shoot", true);

        float shootOffset = 0.3f;
        bulletPos = transform.position;

        GameObject newBullet = Instantiate(Bullet, bulletPos, Quaternion.identity);

        newBullet.GetComponent<BulletMovement>().parentTag = transform.tag;

        if (animator.GetInteger("State") == 0 || lastState == AnimStates.UP)
        {
            newBullet.GetComponent<BulletMovement>().pd = BulletMovement.PlayerDirection.UP;
            newBullet.transform.position = new Vector2(bulletPos.x, bulletPos.y + shootOffset);
        }
        else if (animator.GetInteger("State") == 1 || lastState == AnimStates.DOWN)
        {
            newBullet.GetComponent<BulletMovement>().pd = BulletMovement.PlayerDirection.DOWN;
            newBullet.transform.position = new Vector2(bulletPos.x, bulletPos.y - shootOffset);
        }
        else if (animator.GetInteger("State") == 2 || lastState == AnimStates.RIGHT)
        {
            newBullet.GetComponent<BulletMovement>().pd = BulletMovement.PlayerDirection.RIGHT;
            newBullet.transform.position = new Vector2(bulletPos.x + shootOffset, bulletPos.y);
        }
        else if (animator.GetInteger("State") == 3 || lastState == AnimStates.LEFT)
        {
            newBullet.GetComponent<BulletMovement>().pd = BulletMovement.PlayerDirection.LEFT;
            newBullet.transform.position = new Vector2(bulletPos.x - shootOffset, bulletPos.y);
        }
    }

    private void InstanciateShadow(Vector2 position)
    {
        if (shadow1 != null)
            if(position == Vector2.down)
                shadow_child = Instantiate(shadow1);
            else
                shadow_child = Instantiate(shadow1,position, Quaternion.identity);

        shadow_child.GetComponent<ShadowBehaviour>().setVel(speed);
    }

    public void DieInPresent()
    {
        //Player Die
        /*animator.setAnimation("PlayerDie")*/
        //Swap position with shadow
        transform.position = shadow_child.transform.position;
        //Delete old shadow
        Destroy(shadow_child);
        //Create new shadow in last player position
        InstanciateShadow(transform.position);

        animator.SetInteger("State", 6);
    }

    public void DieInPast()
    {
        animator.SetInteger("State", 7);
    }

    public void stopShoot()
    {
        animator.SetBool("Shoot", false);
    }

}


