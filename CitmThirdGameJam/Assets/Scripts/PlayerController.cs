using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 0.0f;

    public GameObject shadow1 = null;
    private GameObject shadow_child = null;
    // Start is called before the first frame update
    void Start()
    {
        InstanciateShadow();
    }

    void Update()
    {
        Vector3 pos = transform.position;

        if (Input.GetKey("w"))
        {
            pos.y += speed * Time.deltaTime;
            shadow_child.GetComponent<ShadowBehaviour>().SetPlayerInput(ShadowBehaviour.PlayerInput.UP);
        }
        else if (Input.GetKey("s"))
        {
            pos.y -= speed * Time.deltaTime;
            shadow_child.GetComponent<ShadowBehaviour>().SetPlayerInput(ShadowBehaviour.PlayerInput.DOWN);
        }
        else if (Input.GetKey("d"))
        {
            pos.x += speed * Time.deltaTime;
            shadow_child.GetComponent<ShadowBehaviour>().SetPlayerInput(ShadowBehaviour.PlayerInput.RIGHT);
        }
        else if (Input.GetKey("a"))
        {
            pos.x -= speed * Time.deltaTime;
            shadow_child.GetComponent<ShadowBehaviour>().SetPlayerInput(ShadowBehaviour.PlayerInput.LEFT);
        }
        else
        {
            shadow_child.GetComponent<ShadowBehaviour>().SetPlayerInput(ShadowBehaviour.PlayerInput.WAIT);
        }


        transform.position = pos;
    }

    private void InstanciateShadow()
    {
        if (shadow1 != null)
            shadow_child = Instantiate(shadow1);

        shadow_child.GetComponent<ShadowBehaviour>().setVel(speed);
    }
}
