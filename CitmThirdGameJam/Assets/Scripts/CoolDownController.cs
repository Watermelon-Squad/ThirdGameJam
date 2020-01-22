using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolDownController : MonoBehaviour
{
    public Image imageCooldown;
    public float cooldown = 2.0f;
    bool isCooldown;

    public GameObject player = null;
    private Animator anim = null;

    private void Start()
    {
        anim = player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (anim.GetBool("Shoot"))
        {
            isCooldown = true;
            imageCooldown.fillAmount = 1;
        }

        if (isCooldown)
        {
            imageCooldown.fillAmount -= 1 / cooldown * Time.deltaTime;

            if (imageCooldown.fillAmount <= 0)
            {
                imageCooldown.fillAmount = 0;
                isCooldown = false;
            }
        }
        
    }
}
