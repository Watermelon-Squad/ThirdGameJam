using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondPlayerController : MonoBehaviour
{
    public float speed = 0.0f;

    public GameObject shadow1 = null;
    private GameObject shadow_child = null;

    public GameObject Bullet = null;
    Vector2 bulletPos;

    // Start is called before the first frame update
    void Start()
    {
        InstanciateShadow(Vector2.down);
    }

    void Update()
    {
        Vector3 pos = transform.position;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            pos.y += speed * Time.deltaTime;
            shadow_child.GetComponent<ShadowBehaviour>().SetPlayerInput(ShadowBehaviour.PlayerInput.UP);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            pos.y -= speed * Time.deltaTime;
            shadow_child.GetComponent<ShadowBehaviour>().SetPlayerInput(ShadowBehaviour.PlayerInput.DOWN);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            pos.x += speed * Time.deltaTime;
            shadow_child.GetComponent<ShadowBehaviour>().SetPlayerInput(ShadowBehaviour.PlayerInput.RIGHT);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            pos.x -= speed * Time.deltaTime;
            shadow_child.GetComponent<ShadowBehaviour>().SetPlayerInput(ShadowBehaviour.PlayerInput.LEFT);
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
        bulletPos = transform.position;

        //if facing up then bulletPos.y += something
        //if facing down then bulletPos.y -= something
        //if facing right then bulletPos.x += something
        //if facing left then bulletPos.x -= something
        GameObject go = null;
        go = Instantiate(Bullet, bulletPos, Quaternion.identity);
        go.GetComponent<BulletMovement>().parentTag = transform.tag;
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
        Vector2 player_die_position = transform.position;
        transform.position = shadow_child.transform.position;
        //Delete old shadow
        Destroy(shadow_child);
        //Create new shadow in last player position
        InstanciateShadow(player_die_position);
    }

}


