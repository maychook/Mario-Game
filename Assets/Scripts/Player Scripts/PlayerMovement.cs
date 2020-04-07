using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpPower = 5f;
    public Transform groundCheckPosition;
    public LayerMask groundLayer;

    private Rigidbody2D myBody;
    private Animator anim;

    private bool isGrounded;
    private bool jumped;
    

    // the first function that is called when we run the game
    private void Awake() 
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckIfGrounded();
        PlayerJump();
    }

    private void FixedUpdate()
    {
        PlayerWalk();
    }

    void PlayerWalk()
    {
        float h = Input.GetAxisRaw("Horizontal");

        if (h > 0) // goes right
        {
            myBody.velocity = new Vector2(speed, myBody.velocity.y);

            ChangeDirection(1); // keeps the player direction to look to the right
        }
        else if(h < 0) // goes left
        {
            myBody.velocity = new Vector2(-speed, myBody.velocity.y);

            ChangeDirection(-1); // keeps the player direction to look to the left
        }
        else // stands or jump
        {
            myBody.velocity = new Vector2(0f, myBody.velocity.y);
        }

        // it will set the speed animation transition variable to be set by the direction of the walk
        anim.SetInteger("Speed", Mathf.Abs((int)myBody.velocity.x));
    }

    void ChangeDirection(int direction)
    {
        Vector3 tempscale = transform.localScale;
        tempscale.x = direction;
        transform.localScale = tempscale;
    }

    void CheckIfGrounded()
    {
        isGrounded = Physics2D.Raycast(groundCheckPosition.position, Vector2.down, 0.1f, groundLayer);

        if(isGrounded)
        {
            if(jumped)
            {
                jumped = false;
                anim.SetBool("Jump", false);
            }
        }
    }

    void PlayerJump()
    {
        if (isGrounded)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                myBody.velocity = new Vector2(myBody.velocity.x, jumpPower);
                jumped = true;

                anim.SetBool("Jump", true); // change the animation variable to make transition
            }
        }
    }
}
