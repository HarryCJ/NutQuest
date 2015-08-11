using UnityEngine;
using System.Collections;

public class BirdBehaviourScript : MonoBehaviour {

	BoxCollider2D mycollider;
	Rigidbody2D myrigidbody;
    Animator animator;

	int flapDelay = 0;
	float dirMultiplier = 1f;
	public bool directionIsRight = true;
	public bool isCarrying = false;
	public bool isDead = false;
	public bool isGrounded = false;

	// Use this for initialization
	void Start () {

		mycollider = GetComponent<BoxCollider2D>();
		myrigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

	}

	// Update is called once per frame
	void FixedUpdate () {

		if (directionIsRight){ //is right
			dirMultiplier = 1f;
			transform.localScale = new Vector3(10, 10, 1);
		} else { //is left
			dirMultiplier = -1f;
			transform.localScale = new Vector3(-10, 10, 1);
		}

		if (isDead == false){
			if (flapDelay <= 0){

				float forceY = 0f;
				float forceX = 0f;

				// if (isCarrying == true){
				//
				// 	flapDelay = UnityEngine.Random.Range(3, 6);
				// 	forceY = UnityEngine.Random.Range(40f, 45f);
				// 	forceX = UnityEngine.Random.Range(3f, 4f);
				//
				// 	myrigidbody.AddForce(new Vector2(forceX * dirMultiplier, forceY), ForceMode2D.Impulse);
				//
				// } else

					if (transform.position.y < 4f){

					forceX = UnityEngine.Random.Range(1f, 3f);

					if (transform.position.y < -2f){

						flapDelay = 15;
						forceY = UnityEngine.Random.Range(35f, 40f);

					} else if (transform.position.y < 0f){

						flapDelay = 20;
						forceY = UnityEngine.Random.Range(30f, 35f);

					} else if (transform.position.y < 2f){

						flapDelay = 25;
						forceY = UnityEngine.Random.Range(25f, 30f);

					}

					if (isCarrying == true){
						flapDelay = flapDelay / 2;
						forceY = forceY * 10f;
						forceX = forceX * 10f;
					}
					// Debug.Log(forceY);

					myrigidbody.AddForce(new Vector2(forceX * dirMultiplier, forceY), ForceMode2D.Impulse);
					//  else if (transform.position.y < 2f){
					//
					// 	flapDelay = UnityEngine.Random.Range(50, 60);
					//
					// } else {
					//
					// 	flapDelay = UnityEngine.Random.Range(50, 60);
					// }
				}
			} else {

				flapDelay--;

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
		}

        animator.SetBool("isDead", isDead);
        animator.SetBool("isCarrying", isCarrying);
        animator.SetBool("isGrounded", isGrounded);
        animator.SetInteger("flapDelay", flapDelay);

	}

	public void die(){
		isDead = true;
	}
}
