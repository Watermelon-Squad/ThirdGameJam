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

    // Start is called before the first frame update
    void Start()
    {
        InstanciateShadow(Vector2.down);
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Vector3 pos = transform.position;

        if (Input.GetKey("w"))
        {
            pos.y += speed * Time.deltaTime;
            shadow_child.GetComponent<ShadowBehaviour>().SetPlayerInput(ShadowBehaviour.PlayerInput.UP);
            animator.SetInteger("State", 0);
        }
        else if (Input.GetKey("s"))
        {
            pos.y -= speed * Time.deltaTime;
            shadow_child.GetComponent<ShadowBehaviour>().SetPlayerInput(ShadowBehaviour.PlayerInput.DOWN);
            animator.SetInteger("State", 1);
        }
        else if (Input.GetKey("d"))
        {
            pos.x += speed * Time.deltaTime;
            shadow_child.GetComponent<ShadowBehaviour>().SetPlayerInput(ShadowBehaviour.PlayerInput.RIGHT);
            animator.SetInteger("State", 2);
            //sprite.flipX = false;
        }
        else if (Input.GetKey("a"))
        {
            pos.x -= speed * Time.deltaTime;
            shadow_child.GetComponent<ShadowBehaviour>().SetPlayerInput(ShadowBehaviour.PlayerInput.LEFT);
            animator.SetInteger("State", 3);
            //sprite.flipX = true;
        }
        else
        {
            shadow_child.GetComponent<ShadowBehaviour>().SetPlayerInput(ShadowBehaviour.PlayerInput.WAIT);
        }

        if (Input.GetKeyDown("v"))
        {
            Fire();
        }

        transform.position = pos;
    }



    private void Fire()
    {
        float shootOffset = 0.75f;
        bulletPos = transform.position;

        GameObject newBullet = Instantiate(Bullet, bulletPos, Quaternion.identity);
        //if facing up then bulletPos.y += something
        //if facing down then bulletPos.y -= something
        //if facing right then bulletPos.x += something
        //if facing left then bulletPos.x -= something
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
        Vector2 player_die_position = transform.position;
        transform.position = shadow_child.transform.position;
        //Delete old shadow
        Destroy(shadow_child);
        //Create new shadow in last player position
        InstanciateShadow(player_die_position);
    }

}
