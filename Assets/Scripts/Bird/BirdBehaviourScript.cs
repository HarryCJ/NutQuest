using UnityEngine;
using System.Collections;

public class BirdBehaviourScript : Enemy {

	BoxCollider2D mycollider;
	Rigidbody2D myrigidbody;
    Animator animator;

	int flapDelay = 0;
	float dirMultiplier = 1f;
	public bool directionIsRight = true;
	public bool isCarrying = false;
	public bool isGrounded = false;

	public float maxFlightHeight = 4f;

	bird_bottom_collider_script bird_bottom_collider_s;

	BoxCollider2D bird_bottom_collider;

	// Use this for initialization
	void Start () {

		nutPoints = 1;

		maxFlightHeight = UnityEngine.Random.Range(0f, 15f);

		mycollider = GetComponent<BoxCollider2D>();
		myrigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        foreach (Transform child in transform){
            // if (child.name == "frog_tongue_mask"){
            //      tongue_mask = child;
		    //      foreach (Transform child2 in tongue_mask){
			// 		 tongue = child2.GetComponent<frog_tongue_script>();
			//          foreach (Transform child3 in child2){
			// 		 	tongue_end = child3.GetComponent<frog_tongue_end_script>();
			// 			tongue_end_collider = child3.GetComponent<Collider2D>();
			// 		}
			// 	 }
            // } else if (child.name == "frog_tongue_area_collider"){
			// 	tongue_area = child.GetComponent<frog_tongue_area_collider_script>();
			// 	tongue_area_collider = child.GetComponent<Collider2D>();
			//
			// } else if (child.name == "frog_top_collider"){
			// 	frog_top_collider_s = child.GetComponent<frog_top_collider_script>();
			// 	frog_top_collider = child.GetComponent<Collider2D>();

			// } else
			if (child.name == "bird_bottom_collider"){
				bird_bottom_collider_s = child.GetComponent<bird_bottom_collider_script>();
				bird_bottom_collider = child.GetComponent<BoxCollider2D>();

			}
        }

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

			RaycastHit2D[] hits = null;
			if (directionIsRight == true){
				hits = Physics2D.RaycastAll(transform.position, Vector3.right);
			} else {
				hits = Physics2D.RaycastAll(transform.position, Vector3.left);
			}
			for (int i = 0; i < hits.Length; i++) {
	            RaycastHit2D hit = hits[i];
				if (hit.collider.name != transform.name && hit.collider.tag.Contains("phys")){
					if (hit.transform.position.y > transform.position.y && transform.position.y > -2f){
						maxFlightHeight-=0.05f;
					} else if (transform.position.y < 20f) {
						maxFlightHeight+=0.1f;
					}
					// float dif = transform.parent.position.y - hit.point.y;
					// transform.position = new Vector2(transform.parent.position.x, hit.point.y);
					// transform.rotation = Quaternion.Euler(transform.parent.rotation.x, transform.parent.rotation.x, -transform.parent.rotation.z);
					// Color myC = sr.color;
		    		// myC.a = 0.3f-(dif/30);
		    		// sr.color = myC;
				}
			}

			if (flapDelay <= 0 && myrigidbody.velocity.y <= 0f){

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

				if (transform.position.y < maxFlightHeight ){

					forceX = UnityEngine.Random.Range(10f, 10f);

					if (transform.position.y < maxFlightHeight - 6f){

						flapDelay = 15;
						forceY = UnityEngine.Random.Range(800f, 800f);

					} else if (transform.position.y < maxFlightHeight - 4f){

						flapDelay = 17;
						forceY = UnityEngine.Random.Range(750f, 750f);

					} else if (transform.position.y < maxFlightHeight - 2f){

						flapDelay = 20;
						forceY = UnityEngine.Random.Range(700f, 700f);

					}

					if (isCarrying == true){
						flapDelay = flapDelay / 4;
						forceY = forceY * 30f;
						forceX = forceX * 30f;
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

	// public void die(){
	// 	isDead = true;
	// }

	public override void die(){
		if (isDead == false){
	        isDead = true;
	        // mycollider.size = new Vector2(0.09f, 0.1f);
	        // mycollider.offset = new Vector2(0f, 0.0f);
	        mycollider.offset = new Vector2(0f, -0.045f);
	        mycollider.size = new Vector2(0.07f, 0.01f);
	        bird_bottom_collider.size = new Vector2(0.09f, 0.01f);
	        bird_bottom_collider.offset = new Vector2(0.0f, -0.055f);
	        StartCoroutine(dieDelay());
	        // Destroy(gameObject);
		}
    }
}
