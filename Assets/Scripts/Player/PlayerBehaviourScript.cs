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

	public bool directionIsRight = true;

    public int jumpingTimer = -1;
    Animator animator;
    Animator tailAnimator;
    public Rigidbody2D myrigidbody;
	player_bottom_collider_script player_bottom_collider;
    int lastBoost = 0;
	public bool isBoosting;

	Collider2D mycollider;

	GameObject tail = null;
	GameObject tail_1 = null;
	GameObject tail_2 = null;
	GameObject tail_3 = null;
	GameObject tail_4 = null;
	GameObject tail_5 = null;

	SpriteRenderer tail_1_sr = null;
	SpriteRenderer tail_2_sr = null;
	SpriteRenderer tail_3_sr = null;
	SpriteRenderer tail_4_sr = null;
	SpriteRenderer tail_5_sr = null;

	Vector3 tail_pendingPos = new Vector3();
	Vector3 tail_1_pendingPos = new Vector3();
	Vector3 tail_2_pendingPos = new Vector3();
	Vector3 tail_3_pendingPos = new Vector3();
	Vector3 tail_4_pendingPos = new Vector3();
	Vector3 tail_5_pendingPos = new Vector3();

	Vector3 tail_1_accurateRot = new Vector3();
	Vector3 tail_2_accurateRot = new Vector3();
	Vector3 tail_3_accurateRot = new Vector3();
	Vector3 tail_4_accurateRot = new Vector3();
	Vector3 tail_5_accurateRot = new Vector3();

	Vector3 tail_1_pendingRot = new Vector3();
	Vector3 tail_2_pendingRot = new Vector3();
	Vector3 tail_3_pendingRot = new Vector3();
	Vector3 tail_4_pendingRot = new Vector3();
	Vector3 tail_5_pendingRot = new Vector3();

	Vector3 tail_5_pendingScale = new Vector3();

	public float nuts = 0f;
	public int boostCombo = 1;

	SpriteRenderer my_sr = null;
	GameObject environment;
	public WorldScript environment_ws;

    List<GameObject> nutCounterUIText  = new List<GameObject>();
    List<Text> nutCounterUITextText  = new List<Text>();

    List<GameObject> boostComboUIText  = new List<GameObject>();
    List<Text> boostComboUITextText  = new List<Text>();

	Transform player_surrounding_collider;
	player_surrounding_collider_script player_surrounding_collider_s;

	public float frogBoost = 0f;

	public bool isBranchColliding = true;

	GameObject main_camera;
	CameraBehaviourScript main_camera_s;

    // Use this for initialization
    void Start(){

		my_sr = GetComponent<SpriteRenderer>();
		mycollider = GetComponent<Collider2D>();

		environment = GameObject.Find("environment");
		environment_ws = environment.GetComponent<WorldScript>();

		main_camera = GameObject.Find("main_camera");
		main_camera_s = main_camera.GetComponent<CameraBehaviourScript>();

		nutCounterUIText.Add(GameObject.Find("nutCounterUIText1"));
		nutCounterUITextText.Add(nutCounterUIText[0].GetComponent<Text>());
		nutCounterUIText.Add(GameObject.Find("nutCounterUIText1.5"));
		nutCounterUITextText.Add(nutCounterUIText[1].GetComponent<Text>());
		nutCounterUIText.Add(GameObject.Find("nutCounterUIText2"));
		nutCounterUITextText.Add(nutCounterUIText[2].GetComponent<Text>());
		nutCounterUIText.Add(GameObject.Find("nutCounterUIText2.5"));
		nutCounterUITextText.Add(nutCounterUIText[3].GetComponent<Text>());
		nutCounterUIText.Add(GameObject.Find("nutCounterUIText3"));
		nutCounterUITextText.Add(nutCounterUIText[4].GetComponent<Text>());

		boostComboUIText.Add(GameObject.Find("boostComboUITextX"));
		boostComboUITextText.Add(boostComboUIText[0].GetComponent<Text>());
		boostComboUIText.Add(GameObject.Find("boostComboUIText1"));
		boostComboUITextText.Add(boostComboUIText[1].GetComponent<Text>());
		boostComboUIText.Add(GameObject.Find("boostComboUIText2"));
		boostComboUITextText.Add(boostComboUIText[2].GetComponent<Text>());
		boostComboUIText.Add(GameObject.Find("boostComboUIText3"));
		boostComboUITextText.Add(boostComboUIText[3].GetComponent<Text>());


		tail = GameObject.Find("tail");
		tail_1 = GameObject.Find("tail_proto");
		tail_2 = GameObject.Find("tail_part_2");
		tail_3 = GameObject.Find("tail_part_3");
		tail_4 = GameObject.Find("tail_part_4");
		tail_5 = GameObject.Find("tail_part_5");

		tail_1_sr = tail_1.GetComponent<SpriteRenderer>();
		tail_2_sr = tail_2.GetComponent<SpriteRenderer>();
		tail_3_sr = tail_3.GetComponent<SpriteRenderer>();
		tail_4_sr = tail_4.GetComponent<SpriteRenderer>();
		tail_5_sr = tail_5.GetComponent<SpriteRenderer>();

		myrigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        foreach (Transform child in transform){
            if (child.name == "tailBLANK"){
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
			isRunning = true;
			if (directionIsRight == false){
				directionIsRight = true;
				transform.localScale = new Vector3(10, 10, 1);
				// tail.transform.localPosition = new Vector3((tail.transform.localPosition.x*-1), tail.transform.localPosition.y, tail.transform.localPosition.z);
			}
		} else if (velocity.x < 0) { //is left
            isRunning = true;
			if (directionIsRight == true){
				directionIsRight = false;
	            transform.localScale = new Vector3(-10, 10, 1);
				// tail.transform.localPosition = new Vector3((tail.transform.localPosition.x*-1), tail.transform.localPosition.y, tail.transform.localPosition.z);
			}
        } else {
            isRunning = false;
        }

        if (isGrounded == true) {
            // velocity.y = 0;
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)){

                isJumping = true;
                jumpingTimer = 10;

		        // tryBoost(10, true);
		        boost(10, false);

            }
            if (isRunning == false && (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))) {
                isDucking = true;
				myrigidbody.mass = 100f;
            }
        }

        if (isJumping == true){
            if (jumpingTimer > 0)
            {	

				// tail_pendingPos = Vector3.Lerp(tail.transform.localPosition, new Vector3(-0.07f, -0.005f, 0f), Time.deltaTime*30);

				// transform.localEulerAngles = Vector3.Lerp(tail.transform.localPosition, new Vector3(0f, 0f, -40f), Time.deltaTime*30);

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

        if (isBranchColliding == false && myrigidbody.velocity.y <= 0f){
        	isBranchColliding = true;
            environment_ws.setBranchCollider(mycollider, false);
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

		tail_pendingPos = Vector3.Lerp(tail.transform.localPosition, new Vector3(-0.07f, -0.005f, 0f), Time.deltaTime*30);

		tail_1_pendingPos = Vector3.Lerp(tail_1.transform.localPosition, new Vector3(0f, 0.01f, 0f), Time.deltaTime*30);
		tail_2_pendingPos = Vector3.Lerp(tail_2.transform.localPosition, new Vector3(0.045f, -0.025f, 0f), Time.deltaTime*30);
		tail_3_pendingPos = Vector3.Lerp(tail_3.transform.localPosition, new Vector3(0f, 0.03f, 0f), Time.deltaTime*30);
		tail_4_pendingPos = Vector3.Lerp(tail_4.transform.localPosition, new Vector3(-0.01f, 0.02f, 0f), Time.deltaTime*30);
		tail_5_pendingPos = Vector3.Lerp(tail_5.transform.localPosition, new Vector3(-0.01f, 0.01f, 0f), Time.deltaTime*30);

		tail_1_accurateRot = new Vector3(getAccurateRot(tail_1.transform.localEulerAngles.x), getAccurateRot(tail_1.transform.localEulerAngles.y), getAccurateRot(tail_1.transform.localEulerAngles.z));
		tail_2_accurateRot = new Vector3(getAccurateRot(tail_2.transform.localEulerAngles.x), getAccurateRot(tail_2.transform.localEulerAngles.y), getAccurateRot(tail_2.transform.localEulerAngles.z));
		tail_3_accurateRot = new Vector3(getAccurateRot(tail_3.transform.localEulerAngles.x), getAccurateRot(tail_3.transform.localEulerAngles.y), getAccurateRot(tail_3.transform.localEulerAngles.z));
		tail_4_accurateRot = new Vector3(getAccurateRot(tail_4.transform.localEulerAngles.x), getAccurateRot(tail_4.transform.localEulerAngles.y), getAccurateRot(tail_4.transform.localEulerAngles.z));
		tail_5_accurateRot = new Vector3(getAccurateRot(tail_5.transform.localEulerAngles.x), getAccurateRot(tail_5.transform.localEulerAngles.y), getAccurateRot(tail_5.transform.localEulerAngles.z));

		tail_1_pendingRot = Vector3.Lerp(tail_1_accurateRot, new Vector3(0f, 0f, 0f), Time.deltaTime*30);
		tail_2_pendingRot = Vector3.Lerp(tail_2_accurateRot, new Vector3(0f, 0f, 0f), Time.deltaTime*30);
		tail_3_pendingRot = Vector3.Lerp(tail_3_accurateRot, new Vector3(0f, 0f, 0f), Time.deltaTime*30);
		tail_4_pendingRot = Vector3.Lerp(tail_4_accurateRot, new Vector3(0f, 0f, 0f), Time.deltaTime*30);
		tail_5_pendingRot = Vector3.Lerp(tail_5_accurateRot, new Vector3(0f, 0f, 0f), Time.deltaTime*30);

		tail_5_pendingScale = new Vector3(1f, 1f, 1f);

		tail_1_sr.sortingOrder = -1;
		tail_2_sr.sortingOrder = -1;
		tail_3_sr.sortingOrder = -1;
		tail_4_sr.sortingOrder = -1;
		tail_5_sr.sortingOrder = -1;

		if (isRunning == true && isGrounded == true){

			tail_1_pendingPos = Vector3.Lerp(tail_1.transform.localPosition, new Vector3(0f, 0f, 0f), Time.deltaTime*2.5f);
			tail_2_pendingPos = Vector3.Lerp(tail_2.transform.localPosition, new Vector3(0.045f, -0.025f, 0f), Time.deltaTime*2.5f);
			tail_3_pendingPos = Vector3.Lerp(tail_3.transform.localPosition, new Vector3(0f, 0.03f, 0f), Time.deltaTime*2.5f);
			tail_4_pendingPos = Vector3.Lerp(tail_4.transform.localPosition, new Vector3(-0.02f, 0.02f, 0f), Time.deltaTime*2.5f);
			tail_5_pendingPos = Vector3.Lerp(tail_5.transform.localPosition, new Vector3(-0.01f, 0.01f, 0f), Time.deltaTime*2.5f);

			tail_4_pendingRot = Vector3.Lerp(tail_4_accurateRot, new Vector3(0f, 0f, -90f), Time.deltaTime*30);
			// tail_4_pendingRot = new Vector3(0f, 0f, -90f);

			// float realPosZ = (tail_4.transform.localEulerAngles.z > 180) ? tail_4.transform.localEulerAngles.z - 360 : tail_4.transform.localEulerAngles.z;
			// float targetZ = Mathf.Lerp(realPosZ, -90f,  Time.deltaTime*5);
			// tail_4.transform.localEulerAngles = new Vector3(tail_4.transform.localEulerAngles.x, tail_4.transform.localEulerAngles.y, targetZ);
			// tail_4_sr.sortingOrder = -1;
			// tail_5_sr.sortingOrder = -1;

			// tail_1.transform.localEulerAngles = Vector3.Lerp(tail_1.transform.localEulerAngles, new Vector3(0f, 0f, 90f), Time.deltaTime*5);
			tail_1_pendingRot = Vector3.Lerp(tail_1_accurateRot, new Vector3(0f, 0f, 90f), Time.deltaTime*5);
			// realPosZ = (tail_1.transform.localEulerAngles.z > 180) ? tail_1.transform.localEulerAngles.z - 360 : tail_1.transform.localEulerAngles.z;
			// targetZ = Mathf.Lerp(realPosZ, 90f,  Time.deltaTime*5);
			// tail_1.transform.localEulerAngles = new Vector3(tail_1.transform.localEulerAngles.x, tail_1.transform.localEulerAngles.y, 90f);
			if (my_sr.sprite.name == "squirrel_running_1"){
				// tail_pendingPos = Vector3.Lerp(tail.transform.localPosition, new Vector3(-0.08f, -0.005f, 0f), Time.deltaTime*30);
				tail_pendingPos = new Vector3(-0.08f, -0.005f, tail.transform.localPosition.z);
			} else if (my_sr.sprite.name == "squirrel_running_2"){
				// tail_pendingPos = Vector3.Lerp(tail.transform.localPosition, new Vector3(-0.09f, -0.005f, 0f), Time.deltaTime*30);
				tail_pendingPos = new Vector3(-0.09f, -0.005f, tail.transform.localPosition.z);
			} else if (my_sr.sprite.name == "squirrel_running_3"){
				// tail_pendingPos = Vector3.Lerp(tail.transform.localPosition, new Vector3(-0.1f, -0.005f, 0f), Time.deltaTime*30);
				tail_pendingPos = new Vector3(-0.1f, -0.005f, tail.transform.localPosition.z);
			}
		} else if (isDucking == true){

			tail_1_pendingPos = Vector3.Lerp(tail_1.transform.localPosition, new Vector3(0.07f, 0.01f, 0f), Time.deltaTime*30);
			tail_1_pendingRot = Vector3.Lerp(tail_1_accurateRot, new Vector3(0f, 0f, -90f), Time.deltaTime*30);
			// float realPosZ = (tail_1.transform.localEulerAngles.z > 180) ? tail_1.transform.localEulerAngles.z - 360 : tail_1.transform.localEulerAngles.z;
			// float targetZ = Mathf.Lerp(realPosZ, -90f,  Time.deltaTime*30);
			// tail_1.transform.localEulerAngles = new Vector3(tail_1.transform.localEulerAngles.x, tail_1.transform.localEulerAngles.y, targetZ);
			tail_5_pendingPos = Vector3.Lerp(tail_5.transform.localPosition, new Vector3(-0.01f, -0.01f, 0f), Time.deltaTime*30);

			tail_4_pendingRot = Vector3.Lerp(tail_4_accurateRot, new Vector3(0f, 0f, 90f), Time.deltaTime*30);
			tail_4_pendingPos = Vector3.Lerp(tail_4.transform.localPosition, new Vector3(0.0f, 0.02f, 0f), Time.deltaTime*30);
			tail_5_pendingScale = new Vector3(-1f, 1f, 1f);

		} else if (isGrounded == false){

			if (myrigidbody.velocity.y > 0f){

				tail_1_pendingPos = Vector3.Lerp(tail_1.transform.localPosition, new Vector3(0f, 0f, 0f), Time.deltaTime*10f);
				tail_2_pendingPos = Vector3.Lerp(tail_2.transform.localPosition, new Vector3(0.025f, -0.025f, 0f), Time.deltaTime*10f);
				tail_2_pendingRot = Vector3.Lerp(tail_2_accurateRot, new Vector3(0f, 0f, 150f), Time.deltaTime*10f);
				tail_3_pendingPos = Vector3.Lerp(tail_3.transform.localPosition, new Vector3(0f, 0.03f, 0f), Time.deltaTime*10f);
				tail_4_pendingPos = Vector3.Lerp(tail_4.transform.localPosition, new Vector3(-0.02f, 0.02f, 0f), Time.deltaTime*10f);
				tail_5_pendingPos = Vector3.Lerp(tail_5.transform.localPosition, new Vector3(-0.01f, 0.01f, 0f), Time.deltaTime*10f);
				tail_4_pendingRot = Vector3.Lerp(tail_4_accurateRot, new Vector3(0f, 0f, -90f), Time.deltaTime*30);
			} else {
				tail_1_pendingPos = Vector3.Lerp(tail_1.transform.localPosition, new Vector3(0f, 0f, 0f), Time.deltaTime*2.5f);
				tail_2_pendingPos = Vector3.Lerp(tail_2.transform.localPosition, new Vector3(0.045f, -0.025f, 0f), Time.deltaTime*2.5f);
				tail_2_pendingRot = Vector3.Lerp(tail_2_accurateRot, new Vector3(0f, 0f, 0f), Time.deltaTime*2.5f);
				tail_3_pendingPos = Vector3.Lerp(tail_3.transform.localPosition, new Vector3(0f, 0.03f, 0f), Time.deltaTime*2.5f);
				tail_4_pendingPos = Vector3.Lerp(tail_4.transform.localPosition, new Vector3(-0.02f, 0.02f, 0f), Time.deltaTime*2.5f);
				tail_5_pendingPos = Vector3.Lerp(tail_5.transform.localPosition, new Vector3(-0.01f, 0.01f, 0f), Time.deltaTime*2.5f);
				tail_4_pendingRot = Vector3.Lerp(tail_4_accurateRot, new Vector3(0f, 0f, -90f), Time.deltaTime*30);
			}

		} else {

			// tail_1.transform.localPosition = Vector3.Lerp(tail_1.transform.localPosition, new Vector3(0f, 0f, 0f), Time.deltaTime*30);
			// tail_2.transform.localPosition = Vector3.Lerp(tail_2.transform.localPosition, new Vector3(0.045f, -0.025f, 0f), Time.deltaTime*30);
			// tail_3.transform.localPosition = Vector3.Lerp(tail_3.transform.localPosition, new Vector3(0f, 0.03f, 0f), Time.deltaTime*30);
			// tail_4.transform.localPosition = Vector3.Lerp(tail_4.transform.localPosition, new Vector3(-0.01f, 0.02f, 0f), Time.deltaTime*30);
			// tail_5.transform.localPosition = Vector3.Lerp(tail_5.transform.localPosition, new Vector3(-0.01f, 0.01f, 0f), Time.deltaTime*30);
			//
			// tail_5.transform.localScale = new Vector3(1f, 1f, 1f);
			//
			// tail_4.transform.localEulerAngles = Vector3.Lerp(tail_4.transform.localEulerAngles, new Vector3(0f, 0f, 0f), Time.deltaTime*30);
			//
			// // tail_1.transform.localEulerAngles = Vector3.Lerp(tail_1.transform.localEulerAngles, new Vector3(0f, 0f, 0f), Time.deltaTime*30);
			// tail_1.transform.localEulerAngles = Vector3.Lerp(tail_1.transform.localEulerAngles, new Vector3(0f, 0f, tail_1.transform.localEulerAngles.z), Time.deltaTime*30);
			// float realPosZ = (tail_1.transform.localEulerAngles.z > 180) ? tail_1.transform.localEulerAngles.z - 360 : tail_1.transform.localEulerAngles.z;
			// float targetZ = Mathf.Lerp(realPosZ, 0f,  Time.deltaTime*30);
			// tail_1.transform.localEulerAngles = new Vector3(tail_1.transform.localEulerAngles.x, tail_1.transform.localEulerAngles.y, targetZ);
			//
			// tail.transform.localPosition = Vector3.Lerp(tail.transform.localPosition, new Vector3(-0.07f, -0.005f, 0f), Time.deltaTime*30);
			// tail_5_sr.sortingOrder = -1;
			// tail_4_sr.sortingOrder = -1;
		}

		// Debug.Log(myrigidbody.velocity.x);

		tail.transform.localPosition = tail_pendingPos;

		tail_1.transform.localPosition = tail_1_pendingPos;
		tail_2.transform.localPosition = tail_2_pendingPos;
		tail_3.transform.localPosition = tail_3_pendingPos;
		tail_4.transform.localPosition = tail_4_pendingPos;
		tail_5.transform.localPosition = tail_5_pendingPos;

		tail_1.transform.localEulerAngles = tail_1_pendingRot;
		tail_2.transform.localEulerAngles = tail_2_pendingRot;
		tail_3.transform.localEulerAngles = tail_3_pendingRot;
		tail_4.transform.localEulerAngles = tail_4_pendingRot;
		tail_5.transform.localEulerAngles = tail_5_pendingRot;

		tail_5.transform.localScale = tail_5_pendingScale;


        // tailAnimator.SetFloat("velocity.x", velocity.x);
        // tailAnimator.SetFloat("velocity.y", velocity.y);
        // tailAnimator.SetBool("isGrounded", isGrounded);
        // tailAnimator.SetBool("isDucking", isDucking);
        // tailAnimator.SetBool("isJumping", isJumping);
        // tailAnimator.SetBool("isRunning", isRunning);
    }

    public void setGrounded(bool state){
    	if (isGrounded != state){
    		// if (isGrounded == false){
    		main_camera_s.setCameraBaseline();
    		// }
    		isGrounded = state;
    	}
    }

	public float getAccurateRot(float r){
		return (r > 180) ? r - 360 : r;
	}

	public void resetBoostCombo(){

		foreach (Text t in boostComboUITextText){
			t.text = "";
		}
		boostCombo = 1;
	}

	public void addNutPoints(float num){
		nuts += (num * boostCombo);
        StartCoroutine(nutCounterTransition());
	}

	public void subtractNutPoints(int num){
		nuts -= num;
        StartCoroutine(nutCounterTransition());
	}

	IEnumerator nutCounterTransition(){

		foreach (Text t in nutCounterUITextText){
			t.text = "";
		}

		string nutString = nuts.ToString();
		if (nutString.Length == 1){
			nutCounterUITextText[2].text = nutString;
		} else if (nutString.Length == 2){
			nutCounterUITextText[1].text = nutString.Substring(0, 1);
			nutCounterUITextText[3].text = nutString.Substring(1, 1);
		} else if (nutString.Length >= 3){
			nutCounterUITextText[0].text = nutString.Substring(0, 1);
			nutCounterUITextText[2].text = nutString.Substring(1, 1);
			nutCounterUITextText[4].text = nutString.Substring(2, 1);
		}
		

		// nutCounterUITextText[2].fontSize = 100;
		foreach (Text t in nutCounterUITextText){
			t.fontSize = 80;
		}
        yield return new WaitForSeconds(0.005f);
		foreach (Text t in nutCounterUITextText){
			t.fontSize = 84;
		}
        yield return new WaitForSeconds(0.005f);
		foreach (Text t in nutCounterUITextText){
			t.fontSize = 88;
		}
        yield return new WaitForSeconds(0.005f);
		foreach (Text t in nutCounterUITextText){
			t.fontSize = 92;
		}
        yield return new WaitForSeconds(0.005f);
		foreach (Text t in nutCounterUITextText){
			t.fontSize = 96;
		}
        yield return new WaitForSeconds(0.005f);
		foreach (Text t in nutCounterUITextText){
			t.fontSize = 100;
		}
        yield return new WaitForSeconds(0.005f);
		foreach (Text t in nutCounterUITextText){
			t.fontSize = 96;
		}
        yield return new WaitForSeconds(0.005f);
		foreach (Text t in nutCounterUITextText){
			t.fontSize = 92;
		}
        yield return new WaitForSeconds(0.005f);
		foreach (Text t in nutCounterUITextText){
			t.fontSize = 88;
		}
        yield return new WaitForSeconds(0.005f);
		foreach (Text t in nutCounterUITextText){
			t.fontSize = 84;
		}
        yield return new WaitForSeconds(0.005f);
		foreach (Text t in nutCounterUITextText){
			t.fontSize = 80;
		}
	}

	public void tryBoost(int jumpAmount, bool addToMultiplier){

		Debug.Log("tryBoost");

		// if (lastBoost == 0){
		// 	Debug.Log("boost");
	    //     isJumping = true;
	    //     jumpingTimer = 15;
	    //     lastBoost = 15;
		// 	velocity.y = 0.2f;
		// }

		for (int x = 0; x < player_bottom_collider.targets.Count; x++) {
		// foreach (GameObject child in player_bottom_collider.targets){
			GameObject child = player_bottom_collider.targets[x];
			if (child != null && child.transform.tag.Contains("enemy")){
				Enemy myEnemy = child.GetComponent<Enemy>();
				// player_bottom_collider.targets.Remove(child);
				if (myEnemy.isDead == false){
					myEnemy.tryKill();
					if (myEnemy.isDead == true){

						if (boostCombo > 1 && myEnemy.nutPoints > 0){
							addPointsAndSparkle(myEnemy.nutPoints, child, false);
						}
					}
					boost(jumpAmount, addToMultiplier);
					isJumping = true;
				}
			}
		}
    }

	public void addPointsAndSparkle(float num, GameObject child, bool clockEnabled){

		GameObject mynutmarker = Instantiate(Resources.Load("Prefabs/nut_point_marker")) as GameObject;

		// GameObject mysparkle = Instantiate(Resources.Load("Prefabs/sparkle")) as GameObject;
		// mysparkle.transform.position = new Vector3(child.transform.position.x, child.transform.position.y, -1f);

		NutPointMarkerBehaviourScript nutTextBS = mynutmarker.GetComponent<NutPointMarkerBehaviourScript>();

		if (environment_ws.season == "winter"){
			nutTextBS.setBlackColor();
		}

		if (clockEnabled == true){
			nutTextBS.setClock();
			environment_ws.addClockPoints(num);
		} else {
			addNutPoints(num);
			// mysparkle.GetComponent<Rigidbody2D>().velocity = child.GetComponent<Rigidbody2D>().velocity;
		}
		TextMesh nutText = mynutmarker.GetComponent<TextMesh>();
		nutText.text = "+"+(num * boostCombo).ToString();
		mynutmarker.transform.position = new Vector3(child.transform.position.x, child.transform.position.y+1f, -5f);
	}

	public void boost(int jumpAmount, bool addToMultiplier){

		if (addToMultiplier == true){
			boostCombo += 1;
	        StartCoroutine(boostComboTransition());
		}
		jumpingTimer = jumpAmount;
		isBoosting = true;

        isBranchColliding = false;
        environment_ws.setBranchCollider(mycollider, true);
	}

	IEnumerator boostComboTransition(){

		foreach (Text t in boostComboUITextText){
			t.text = "";
		}
		boostComboUITextText[0].text = "x";

		string boostString = boostCombo.ToString();
		if (boostString.Length == 1){
			boostComboUITextText[1].text = boostString;
		} else if (boostString.Length == 2){
			boostComboUITextText[1].text = boostString.Substring(0, 1);
			boostComboUITextText[2].text = boostString.Substring(1, 1);
		} else if (boostString.Length == 3){
			boostComboUITextText[1].text = boostString.Substring(0, 1);
			boostComboUITextText[2].text = boostString.Substring(1, 1);
			boostComboUITextText[3].text = boostString.Substring(2, 1);
		}

		// boostComboUITextText[2].fontSize = 100;
		foreach (Text t in boostComboUITextText){
			t.fontSize = 80;
		}
        yield return new WaitForSeconds(0.005f);
		foreach (Text t in boostComboUITextText){
			t.fontSize = 84;
		}
        yield return new WaitForSeconds(0.005f);
		foreach (Text t in boostComboUITextText){
			t.fontSize = 88;
		}
        yield return new WaitForSeconds(0.005f);
		foreach (Text t in boostComboUITextText){
			t.fontSize = 92;
		}
        yield return new WaitForSeconds(0.005f);
		foreach (Text t in boostComboUITextText){
			t.fontSize = 96;
		}
        yield return new WaitForSeconds(0.005f);
		foreach (Text t in boostComboUITextText){
			t.fontSize = 100;
		}
        yield return new WaitForSeconds(0.005f);
		foreach (Text t in boostComboUITextText){
			t.fontSize = 96;
		}
        yield return new WaitForSeconds(0.005f);
		foreach (Text t in boostComboUITextText){
			t.fontSize = 92;
		}
        yield return new WaitForSeconds(0.005f);
		foreach (Text t in boostComboUITextText){
			t.fontSize = 88;
		}
        yield return new WaitForSeconds(0.005f);
		foreach (Text t in boostComboUITextText){
			t.fontSize = 84;
		}
        yield return new WaitForSeconds(0.005f);
		foreach (Text t in boostComboUITextText){
			t.fontSize = 80;
		}
	}

	public void getHit(int damage, bool forceIsRight){
        StartCoroutine(protect());
		subtractNutPoints(damage);

		GameObject mynutmarker = Instantiate(Resources.Load("Prefabs/nut_point_marker")) as GameObject;
		TextMesh nutText = mynutmarker.GetComponent<TextMesh>();
		Rigidbody2D nutrigidbody = mynutmarker.GetComponent<Rigidbody2D>();
		nutrigidbody.gravityScale = 1f;
		nutText.text = "-"+(damage).ToString();
		mynutmarker.transform.position = new Vector2(transform.position.x+0.5f, transform.position.y+0.5f);

		float forceMultiplier = 1f;
		if (forceIsRight == false){
			forceMultiplier = -1f;
		}
		myrigidbody.AddForce(new Vector2(((500f * forceMultiplier)), 100f), ForceMode2D.Impulse);
	}

	// IEnumerator flashWhite(){
	//
	// 	isProtected = true;
	// 	player_surrounding_collider.gameObject.SetActive(false);
	//
	// 	SpriteRenderer sr = GetComponent<SpriteRenderer>();
	//
	// 	sr.enabled = false;
    //     yield return new WaitForSeconds(0.2f);
	// 	sr.enabled = true;
    //     yield return new WaitForSeconds(0.2f);
	// 	sr.enabled = false;
    //     yield return new WaitForSeconds(0.2f);
	// 	sr.enabled = true;
    //     yield return new WaitForSeconds(0.2f);
	// 	sr.enabled = false;
	//     yield return new WaitForSeconds(0.2f);
	// 	sr.enabled = true;
	//
    //     // yield return new WaitForSeconds(1);
	// 	isProtected = false;
	// 	player_surrounding_collider.gameObject.SetActive(true);
    // }

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
