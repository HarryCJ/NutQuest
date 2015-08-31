﻿using UnityEngine;
using System.Collections;
using System;

public class FrogBehaviourScript : Enemy
{

	public Vector3 velocity = new Vector3();
    public bool isGrounded = false;

    public bool directionIsRight = true;

    public bool isJumping = false;
	// public bool isDead = false;
    int jumpingTimer = -1;
    int lastJump = 0;
	System.Random random;
    Animator animator;

	int jumpDelay = 0;

	public bool isLicking = false;
	public bool canLick = false;
	float dirMultiplier = 1f;

    BoxCollider2D mycollider;
    Rigidbody2D myrigidbody;

	public Transform tongue_mask;

	frog_tongue_script tongue;
	frog_tongue_end_script tongue_end;
	frog_tongue_area_collider_script tongue_area;
	frog_top_collider_script frog_top_collider_s;
	frog_bottom_collider_script frog_bottom_collider_s;

	Collider2D tongue_end_collider;
	Collider2D tongue_area_collider;
	Collider2D frog_top_collider;
	Collider2D frog_bottom_collider;

	public virtual void frogStart(){
		// Debug.Log("ENEMY DIES!!!");
	}

    // Use this for initialization
    void Start(){
		Debug.Log("frog hello");

		myrigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
		random = new System.Random();
        mycollider = GetComponent<BoxCollider2D>();
        foreach (Transform child in transform){
            if (child.name == "frog_tongue_mask"){
                 tongue_mask = child;
		         foreach (Transform child2 in tongue_mask){
					 tongue = child2.GetComponent<frog_tongue_script>();
			         foreach (Transform child3 in child2){
					 	tongue_end = child3.GetComponent<frog_tongue_end_script>();
						tongue_end_collider = child3.GetComponent<Collider2D>();
					}
				 }
            } else if (child.name == "frog_tongue_area_collider"){
				tongue_area = child.GetComponent<frog_tongue_area_collider_script>();
				tongue_area_collider = child.GetComponent<Collider2D>();

			} else if (child.name == "frog_top_collider"){
				frog_top_collider_s = child.GetComponent<frog_top_collider_script>();
				frog_top_collider = child.GetComponent<Collider2D>();

			} else if (child.name == "frog_bottom_collider"){
				frog_bottom_collider_s = child.GetComponent<frog_bottom_collider_script>();
				frog_bottom_collider = child.GetComponent<Collider2D>();

			}
        }
		Physics2D.IgnoreCollision(tongue_end_collider, tongue_area_collider);
		Physics2D.IgnoreCollision(tongue_end_collider, frog_top_collider);
		Physics2D.IgnoreCollision(tongue_end_collider, frog_bottom_collider);
		Physics2D.IgnoreCollision(tongue_end_collider, mycollider);

		frogStart();
    }

	public virtual void frogFixedUpdate(){
		// Debug.Log("ENEMY DIES!!!");
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        // if (lastJump > 0){
        //     lastJump--;
        // }
		//
        // if (directionIsRight){ //is right
        //     transform.localScale = new Vector3(10, 10, 1);
		// } else { //is left
        //     transform.localScale = new Vector3(-10, 10, 1);
        // }
		//

		if (jumpingTimer > 0)
		{
			velocity.x = (UnityEngine.Random.Range(400f, 600f) * dirMultiplier);
			if (jumpingTimer < 10)
			{
				velocity.y = 20f;
			}
			else
			{
				velocity.y = 100f;
			}
			myrigidbody.AddForce(velocity, ForceMode2D.Impulse);
			jumpingTimer--;
		}
		else if (isGrounded == true || jumpingTimer == 0)
		{
			velocity.y = 0;
			isJumping = false;
		}

		if (directionIsRight){ //is right
			dirMultiplier = 1f;
			transform.localScale = new Vector3(10, 10, 1);
		} else { //is left
			dirMultiplier = -1f;
			transform.localScale = new Vector3(-10, 10, 1);
		}

		if (isGrounded == true && myrigidbody.velocity.y <= 0f){
			isJumping = false;
		}

        if (isGrounded == true && isDead == false && isLicking == false) {
			if (((directionIsRight == false && transform.position.x < -10f) || (directionIsRight == true && transform.position.x > 10f)) &&
					UnityEngine.Random.Range(0, 800) == 0){
				if (directionIsRight == true){
					directionIsRight = false;
				} else {
					directionIsRight = true;
				}
			} else if (canLick == true && isLicking == false){

				tongue_mask.gameObject.SetActive(true);
				isLicking = true;
				tongue.thrust();

			}
			// else if (lastJump == 0){
	        //     // isJumping = true;
	        //     // jumpingTimer = random.Next(8, 13);
	        //     lastJump = random.Next(55, 80);
			// }
        }

		if (isDead == false && jumpDelay <= 0 && isGrounded == true && isLicking == false){

			if (transform.position.y < 4f){

				// float forceY = UnityEngine.Random.Range(1000f, 1500f);

				jumpDelay = UnityEngine.Random.Range(100, 250);

				// myrigidbody.AddForce(new Vector2(forceX * dirMultiplier, forceY), ForceMode2D.Impulse);
				jumpingTimer = UnityEngine.Random.Range(15, 20);
				isJumping = true;
				//  else if (transform.position.y < 2f){
				//
				// 	jumpDelay = UnityEngine.Random.Range(50, 60);
				//
				// } else {
				//
				// 	jumpDelay = UnityEngine.Random.Range(50, 60);
				// }
			}
		} else {

			jumpDelay--;

		}

		if (myrigidbody.velocity.x > 4f)
        {
            myrigidbody.velocity = new Vector2(4f, myrigidbody.velocity.y);
        } else if (myrigidbody.velocity.x < -4f)
        {
            myrigidbody.velocity = new Vector2(-4f, myrigidbody.velocity.y);
        }
        if (myrigidbody.velocity.y > 10f)
        {
            myrigidbody.velocity = new Vector2(myrigidbody.velocity.x, 10f);
        }
        else if (myrigidbody.velocity.y < -10f)
        {
            myrigidbody.velocity = new Vector2(myrigidbody.velocity.x, -10f);
        }

		//add friction? needed?
		// if (isJumping == false){
		// 	if (velocity.x >= 0.02f || velocity.x <= -0.02f){
		// 		if (directionIsRight == true) {
		// 			velocity.x -= 0.002f;
		// 		} else {
		// 			velocity.x += 0.002f;
		// 		}
		// 	}
		// 	// else if (velocity.x > -0.02f && velocity.x < 0.02f){
		// 	// 	velocity.x = 0;
		// 	// }
		// }

        // transform.Translate(velocity);
		// myrigidbody.AddForce(new Vector2(velocity.x, velocity.y), ForceMode2D.Impulse);

		frogFixedUpdate();

        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("isJumping", isJumping);
        animator.SetBool("isDead", isDead);
        animator.SetBool("isLicking", isLicking);

    }

	public override void die(){
		Debug.Log("die");

		lives--;
		if (isDead == false && isProtected == false && lives <= 0){
	        isDead = true;
	        // mycollider.size = new Vector2(0.12f, 0.04f);
	        // mycollider.offset = new Vector2(0f, -0.02f);
	        mycollider.offset = new Vector2(0f, -0.035f);
	        mycollider.size = new Vector2(0.12f, 0.01f);
	        StartCoroutine(dieDelay());
	        // Destroy(gameObject);
		} else {
			StartCoroutine(protect());
		}
    }

	IEnumerator dieDelay(){
        yield return new WaitForSeconds(5);
		Destroy(gameObject);
    }

	public virtual int getThrustTimer() {
      return 23;
   }

   public virtual float getEatPickupCutoff() {
	 return 0.1f;
  }

}
