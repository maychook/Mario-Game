using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogScript : MonoBehaviour
{
    private Animator anim;

    private bool animation_Started;
    private bool animation_Finished;

    private int jupmedTimes;
    private bool jumpLeft = true;

    private string coroutine_Name = "FrogJump";

    public LayerMask playerLayer;

    private GameObject player;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(coroutine_Name);
        player = GameObject.FindGameObjectWithTag(MyTags.PLAYER_TAG);
    }

    private void Update()
    {
        if (Physics2D.OverlapCircle(transform.position, 0.5f, playerLayer))
        {
            player.GetComponent<PlayerDamage>().DealDamage();
        }
    }

    // Update is called once per frame - late Update is called at the end of a frame
    void LateUpdate() // when the upadate is finished the late update will be called
    {
        if (animation_Finished && animation_Started)
        {
            animation_Started = false;

            // will make the animation independed
            transform.parent.position = transform.position;
            transform.localPosition = Vector3.zero;
        }
    }

    IEnumerator FrogJump()
    {
        yield return new WaitForSeconds(Random.Range(1f, 4f));

        animation_Started = true;
        animation_Finished = false;

        jupmedTimes++;

        if (jumpLeft)
        {
            anim.Play("FrogJumpLeft");
        }
        else
        {
            anim.Play("FrogJumpRight");
        }

        StartCoroutine(coroutine_Name);
    }

    void AnimationFinish() // will be called from the Animation Event on the last frame of the jump animation
    {
        animation_Finished = true;

        if (jumpLeft)
        {
            anim.Play("FrogIdleLeft");
        }
        else
        {
            anim.Play("FrogIdleRight");
        }

        if (jupmedTimes == 3) // jump 3 times to the left and 3 to the right
        {
            jupmedTimes = 0;
            Vector3 tempScale = transform.localScale;
            tempScale.x *= -1;
            transform.localScale = tempScale;

            jumpLeft = !jumpLeft; // change direction
        }
    }
}
