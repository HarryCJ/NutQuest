using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerBehaviourScript : MonoBehaviour {

	public Vector3 velocity = new Vector3();
    public bool isGrounded;
    public bool isDucking;
    public bool isJumping;
    public bool isRunning;
	public bool isProtected = false;

    public int jumpingTimer = -1;
    Animator animator;
    Animator tailAnimator;
    Rigidbody2D myrigidbody;
	player_bottom_collider_script player_bottom_collider;
    int lastBoost = 0;
	public bool isBoosting;

	public int nuts = 0;
	GameObject nutCounterUIText;
	Text nutCounterUITextText;

	Transform player_surrounding_collider;
	player_surrounding_collider_script player_surrounding_collider_s;

    // Use this for initialization
    void Start(){

		nutCounterUIText = GameObject.Find("nutCounterUIText");
		nutCounterUITextText = nutCounterUIText.GetComponent<Text>();

		myrigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        foreach (Transform child in transform){
            if (child.name == "tail"){
                 tailAnimator = child.GetComponent<Animator>();
            } else if (child.name == "player_bottom_collider"){
				player_bottom_collider = child.GetComponent<player_bottom_collider_script>();
			} else if (child.name == "player_surrounding_collider"){
				player_surrounding_collider = child;
				player_surrounding_collider_s = child.GetComponent<player_surrounding_collider_script>();
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
						if (myEnemy.isDead == false){
							myEnemy.die();
							if (myEnemy.isDead == true){
				                jumpingTimer = 16;
								isBoosting = true;
							}
						}
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
					if (isBoosting == true){
	                	velocity.y = 50f;
					} else {
	                	velocity.y = 20f;
					}
                }
                else
                {
					if (isBoosting == true){
                		velocity.y = 250f;
					} else {
                		velocity.y = 40f;
					}
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
        if (myrigidbody.velocity.y > 20f)
        {
            myrigidbody.velocity = new Vector2(myrigidbody.velocity.x, 20f);
        }
        else if (myrigidbody.velocity.y < -15f)
        {
            myrigidbody.velocity = new Vector2(myrigidbody.velocity.x, -15f);
        }

        // animator.SetFloat("velocity.x", velocity.x);
        // animator.SetFloat("velocity.y", velocity.y);
		animator.SetInteger("jumpingTimer", jumpingTimer);
        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("isDucking", isDucking);
        animator.SetBool("isJumping", isJumping);
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isBoosting", isBoosting);
        animator.SetBool("isProtected", isProtected);


        // tailAnimator.SetFloat("velocity.x", velocity.x);
        // tailAnimator.SetFloat("velocity.y", velocity.y);
        tailAnimator.SetBool("isGrounded", isGrounded);
        tailAnimator.SetBool("isDucking", isDucking);
        tailAnimator.SetBool("isJumping", isJumping);
        tailAnimator.SetBool("isRunning", isRunning);
    }

	public void addNutPoints(int num){
		nuts += num;
		nutCounterUITextText.text = nuts.ToString();
	}

	public void subtractNutPoints(int num){
		nuts -= num;
		nutCounterUITextText.text = nuts.ToString();
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

	public void getHit(bool forceIsRight){
        StartCoroutine(protect());
		subtractNutPoints(1);
		float forceMultiplier = 1f;
		if (forceIsRight == false){
			forceMultiplier = -1f;
		}
		myrigidbody.AddForce(new Vector2(((500f * forceMultiplier)), 100f), ForceMode2D.Impulse);
	}

	IEnumerator protect(){

		isProtected = true;
		player_surrounding_collider.gameObject.SetActive(false);

		SpriteRenderer sr = GetComponent<SpriteRenderer>();

		sr.enabled = false;
        yield return new WaitForSeconds(0.2f);
		sr.enabled = true;
        yield return new WaitForSeconds(0.2f);
		sr.enabled = false;
        yield return new WaitForSeconds(0.2f);
		sr.enabled = true;
        yield return new WaitForSeconds(0.2f);
		sr.enabled = false;
	    yield return new WaitForSeconds(0.2f);
		sr.enabled = true;

        // yield return new WaitForSeconds(1);
		isProtected = false;
		player_surrounding_collider.gameObject.SetActive(true);
    }
}
