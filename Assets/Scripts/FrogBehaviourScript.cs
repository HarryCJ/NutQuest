using UnityEngine;
using System.Collections;
using System;

public class FrogBehaviourScript : MonoBehaviour
{

	public Vector3 velocity = new Vector3();
    public bool isGrounded = false;

    public bool directionIsRight = true;

    public bool isJumping = false;
	public bool isDead = false;
    int jumpingTimer = -1;
    int lastJump = 0;
	System.Random random;
    Animator animator;

	public bool isLicking = false;
	public bool canLick = false;

    BoxCollider2D mycollider;

	Transform tongue_mask;

	frog_tongue_script tongue;
	frog_tongue_end_script tongue_end;
	frog_tongue_area_collider_script tongue_area;
	frog_top_collider_script frog_top_collider_s;
	frog_bottom_collider_script frog_bottom_collider_s;

	Collider2D tongue_end_collider;
	Collider2D tongue_area_collider;
	Collider2D frog_top_collider;
	Collider2D frog_bottom_collider;

    // Use this for initialization
    void Start(){

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
    }

    // Update is called once per frame
    void Update()
    {
        if (lastJump > 0){
            lastJump--;
        }

        if (directionIsRight){ //is right
            transform.localScale = new Vector3(10, 10, 1);
		} else { //is left
            transform.localScale = new Vector3(-10, 10, 1);
        }

        if (isJumping == true) {
            if (jumpingTimer > 0) {
                if (jumpingTimer < 5) {
                    velocity.y = 0.05f;
                } else {
                    velocity.y = 0.2f;
					if (directionIsRight == true) {
			          velocity.x = 0.1f;
			        } else {
			          velocity.x = -0.1f;
			        }
                }
                jumpingTimer--;
            } else if (isGrounded == true || jumpingTimer == 0) {
                velocity.y = 0;
                isJumping = false;
            }
        }

        if (isGrounded == true && isJumping == false && isDead == false && isLicking == false) {
			if (canLick == true){

				isLicking = true;
				tongue.thrust();

			} else if (lastJump == 0){
	            isJumping = true;
	            jumpingTimer = random.Next(8, 13);
	            lastJump = random.Next(55, 80);
			}
        }

		//add friction? needed?
		if (isJumping == false){
			if (velocity.x >= 0.02f || velocity.x <= -0.02f){
				if (directionIsRight == true) {
					velocity.x -= 0.002f;
				} else {
					velocity.x += 0.002f;
				}
			} else if (velocity.x > -0.02f && velocity.x < 0.02f){
				velocity.x = 0;
			}
		}

        transform.Translate(velocity);

        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("isJumping", isJumping);
        animator.SetBool("isDead", isDead);
        animator.SetBool("isLicking", isLicking);

    }


	public void die(){
        isDead = true;
        mycollider.size = new Vector2(0.12f, 0.04f);
        mycollider.offset = new Vector2(0f, -0.02f);
        StartCoroutine(dieDelay());
        // Destroy(gameObject);
    }

	IEnumerator dieDelay(){
        yield return new WaitForSeconds(5);
		Destroy(gameObject);
    }

}
