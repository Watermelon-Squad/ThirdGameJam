using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 0.0f;

    public GameObject shadow1 = null;
    private GameObject shadow_child = null;

    public GameObject Bullet = null;
    private Vector2 bulletPos;

    private SpriteRenderer sprite;
    private Animator animator;

    private Rigidbody2D rigidbody2D;

    public float cooldown_shoot = 3.0f;
    private float actual_cooldown = 0.0f;

    public float shoot_time = 2.0f;
    private float actual_shoot_time = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        actual_cooldown = cooldown_shoot;
        actual_shoot_time = shoot_time;
        InstanciateShadow(transform.position);

        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        if (actual_shoot_time >= shoot_time)
        {
            if (Input.GetKey("w"))
            {
                rigidbody2D.AddForce(Vector2.up * speed * Time.deltaTime);
                // pos.y += speed * Time.deltaTime;
                shadow_child.GetComponent<ShadowBehaviour>().SetPlayerInput(ShadowBehaviour.PlayerInput.UP);
                animator.SetInteger("State", 0);
            }
            else if (Input.GetKey("s"))
            {
                rigidbody2D.AddForce(Vector2.down * speed * Time.deltaTime);
                //pos.y -= speed * Time.deltaTime;
                shadow_child.GetComponent<ShadowBehaviour>().SetPlayerInput(ShadowBehaviour.PlayerInput.DOWN);
                animator.SetInteger("State", 1);
            }
            else if (Input.GetKey("d"))
            {
                rigidbody2D.AddForce(Vector2.right * speed * Time.deltaTime);
                //pos.x += speed * Time.deltaTime;
                shadow_child.GetComponent<ShadowBehaviour>().SetPlayerInput(ShadowBehaviour.PlayerInput.RIGHT);
                animator.SetInteger("State", 2);
                //sprite.flipX = false;
            }
            else if (Input.GetKey("a"))
            {
                rigidbody2D.AddForce(Vector2.left * speed * Time.deltaTime);
                //pos.x -= speed * Time.deltaTime;
                shadow_child.GetComponent<ShadowBehaviour>().SetPlayerInput(ShadowBehaviour.PlayerInput.LEFT);
                animator.SetInteger("State", 3);
                //sprite.flipX = true;
            }
            else
            {
                shadow_child.GetComponent<ShadowBehaviour>().SetPlayerInput(ShadowBehaviour.PlayerInput.WAIT);
            }

        }
        else
        {
            shadow_child.GetComponent<ShadowBehaviour>().SetPlayerInput(ShadowBehaviour.PlayerInput.WAIT);
            actual_shoot_time += Time.deltaTime;
        }
           

        if (actual_cooldown >= cooldown_shoot)
        {
            if (Input.GetKeyDown("v"))
            {
                actual_cooldown = 0.0f;
                actual_shoot_time = 0.0f;
                Fire();
            }
        }
        else
        {
            actual_cooldown += Time.deltaTime;
        }

    }



    private void Fire()
    {
        float shootOffset = 0.75f;
        bulletPos = transform.position;

        GameObject newBullet = Instantiate(Bullet, bulletPos, Quaternion.identity);

        newBullet.GetComponent<BulletMovement>().parentTag = transform.tag;

        if (animator.GetInteger("State") == 0)
        {
            newBullet.GetComponent<BulletMovement>().pd = BulletMovement.PlayerDirection.UP;
            newBullet.transform.position = new Vector2(bulletPos.x, bulletPos.y + shootOffset);
        }
        else if (animator.GetInteger("State") == 1)
        {
            newBullet.GetComponent<BulletMovement>().pd = BulletMovement.PlayerDirection.DOWN;
            newBullet.transform.position = new Vector2(bulletPos.x, bulletPos.y - shootOffset);
        }
        else if (animator.GetInteger("State") == 2)
        {
            newBullet.GetComponent<BulletMovement>().pd = BulletMovement.PlayerDirection.RIGHT;
            newBullet.transform.position = new Vector2(bulletPos.x + shootOffset, bulletPos.y);
        }
        else if (animator.GetInteger("State") == 3)
        {
            newBullet.GetComponent<BulletMovement>().pd = BulletMovement.PlayerDirection.LEFT;
            newBullet.transform.position = new Vector2(bulletPos.x - shootOffset, bulletPos.y);
        }
    }

    private void InstanciateShadow(Vector2 position)
    {
        if (shadow1 != null)
            if (position == Vector2.down)
                shadow_child = Instantiate(shadow1);
            else
                shadow_child = Instantiate(shadow1, position, Quaternion.identity);

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
    }

}
