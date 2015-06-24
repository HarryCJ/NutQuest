using UnityEngine;
using System.Collections;

public class PlayerBehaviourScript : MonoBehaviour {

    public enum States
    {
        Idle = 0,
        Jumping = 1,
    }

	public Vector3 velocity = new Vector3();
    public bool isGrounded;
    public States state = States.Idle;

    int jumpingTimer = -1;
    Animator animator;
    Animator tailAnimator;

	// Use this for initialization
    void Start(){
    
        animator = GetComponent<Animator>();
        foreach (Transform child in transform){
            if (child.name == "tail"){
    
                 tailAnimator = child.GetComponent<Animator>();
            }
        }
	
	}
	
	// Update is called once per frame
	void Update () {

		velocity.x = Input.GetAxisRaw("Horizontal") * 0.1f;

		if (velocity.x > 0){ //is right
            transform.localScale = new Vector3(10, 10, 1);
		} else if (velocity.x < 0) { //is left
            transform.localScale = new Vector3(-10, 10, 1);
        }

        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            state = States.Jumping;
            jumpingTimer = 15;
        }

        switch (state)
        {
            case States.Idle:

                break;
            case States.Jumping:
                if (jumpingTimer > 0)
                {
                    if (jumpingTimer < 5)
                    {
                        velocity.y = 0.05f;
                    }
                    else
                    {
                        velocity.y = 0.2f;
                    }
                    jumpingTimer--;
                }
                else if (isGrounded == true || jumpingTimer == 0)
                {
                    velocity.y = 0;
                    state = States.Idle;
                }

                break;
        }
		
		transform.Translate(velocity);

        animator.SetFloat("velocity.x", velocity.x);
        animator.SetFloat("velocity.y", velocity.y);
        animator.SetInteger("state", (int) state);
        animator.SetBool("isGrounded", isGrounded);

        tailAnimator.SetFloat("velocity.x", velocity.x);
        tailAnimator.SetFloat("velocity.y", velocity.y);
        tailAnimator.SetInteger("state", (int)state);
        tailAnimator.SetBool("isGrounded", isGrounded);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        isGrounded = true;
    }
    void OnTriggerStay2D(Collider2D other)
    {
        isGrounded = true;
    }
    void OnTriggerExit2D(Collider2D other)
    {
        isGrounded = false;
    }
}
