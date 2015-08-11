using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerBehaviourScript : MonoBehaviour {

	public Vector3 velocity = new Vector3();
    public bool isGrounded;
    public bool isDucking;
    public bool isJumping;
    public bool isRunning;

    public int jumpingTimer = -1;
    Animator animator;
    Animator tailAnimator;
    Rigidbody2D myrigidbody;
	player_bottom_collider_script player_bottom_collider;
    int lastBoost = 0;

    // Use this for initialization
    void Start(){

		myrigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        foreach (Transform child in transform){
            if (child.name == "tail"){
                 tailAnimator = child.GetComponent<Animator>();
            } else if (child.name == "player_bottom_collider"){
				player_bottom_collider = child.GetComponent<player_bottom_collider_script>();
			}
        }

	}

	// Update is called once per frame
	void FixedUpdate () {

		if (lastBoost > 0){
            lastBoost--;
        }

		velocity.x = Input.GetAxisRaw("Horizontal") * 4f;
        isDucking = false; //has to be held down
		myrigidbody.mass = 5f;

        if (velocity.x > 0){ //is right
            transform.localScale = new Vector3(10, 10, 1);
            isRunning = true;
		} else if (velocity.x < 0) { //is left
            transform.localScale = new Vector3(-10, 10, 1);
            isRunning = true;
        } else {
            isRunning = false;
        }

        if (isGrounded == true) {
            // velocity.y = 0;
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)){

                isJumping = true;
                jumpingTimer = 10;

		        foreach (GameObject child in player_bottom_collider.targets){
		            if (child != null && child.transform.tag.Contains("enemy")){
						Enemy myEnemy = child.GetComponent<Enemy>();
						myEnemy.die();
					}
				}

            }
            if (isRunning == false && (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))) {
                isDucking = true;
				myrigidbody.mass = 100f;
            }
        }

        if (isJumping == true){
            if (jumpingTimer > 0)
            {
                if (jumpingTimer < 10)
                {
                	velocity.y = 20f;
                }
                else
                {
                	velocity.y = 100f;
                }
                jumpingTimer--;
            }
            else if (isGrounded == true || jumpingTimer == 0)
            {
                velocity.y = 0;
                isJumping = false;
            }
        }

		// if (isGrounded == false && isJumping == false){
        //     velocity.y = -0.2f;
        // }

		// Vector3 rVel = rigidbody.velocity;
		// rVel.x += velocity.x;
		// rVel.y += velocity.y;
	    // transform.Translate(velocity);
		// myrigidbody.AddTorque(velocity);
		myrigidbody.AddForce(new Vector2(velocity.x, velocity.y), ForceMode2D.Impulse);

        if (myrigidbody.velocity.x > 8f)
        {
            myrigidbody.velocity = new Vector2(8f, myrigidbody.velocity.y);
        } else if (myrigidbody.velocity.x < -8f)
        {
            myrigidbody.velocity = new Vector2(-8f, myrigidbody.velocity.y);
        }
        if (myrigidbody.velocity.y > 15f)
        {
            myrigidbody.velocity = new Vector2(myrigidbody.velocity.x, 15f);
        }
        else if (myrigidbody.velocity.y < -15f)
        {
            myrigidbody.velocity = new Vector2(myrigidbody.velocity.x, -15f);
        }

        // animator.SetFloat("velocity.x", velocity.x);
        // animator.SetFloat("velocity.y", velocity.y);
        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("isDucking", isDucking);
        animator.SetBool("isJumping", isJumping);
        animator.SetBool("isRunning", isRunning);

        // tailAnimator.SetFloat("velocity.x", velocity.x);
        // tailAnimator.SetFloat("velocity.y", velocity.y);
        tailAnimator.SetBool("isGrounded", isGrounded);
        tailAnimator.SetBool("isDucking", isDucking);
        tailAnimator.SetBool("isJumping", isJumping);
        tailAnimator.SetBool("isRunning", isRunning);
    }

	public void boost(){

		if (lastBoost == 0){
			Debug.Log("boost");
	        isJumping = true;
	        jumpingTimer = 15;
	        lastBoost = 15;
			velocity.y = 0.2f;
		}
    }
}
