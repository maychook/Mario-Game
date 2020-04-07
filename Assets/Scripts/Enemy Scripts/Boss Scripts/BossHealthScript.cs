using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthScript : MonoBehaviour
{
    private Animator anim;
    private int health = 10;

    private bool canDamage;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        canDamage = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator WaitForDamage()
    {
        yield return new WaitForSeconds(2f);
        canDamage = true;
    }

    IEnumerator DeactivateBoss()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (canDamage)
        {
            if (target.gameObject.tag == MyTags.BULLET_TAG)
            {
                health--;
                canDamage = false;

                if (health <= 0)
                {
                    GetComponent<BossScript>().DeactivateBossScript();
                    anim.Play("BossDead");

                    StartCoroutine(DeactivateBoss());
                }

                StartCoroutine(WaitForDamage());
            }
        }
    }

} // class
