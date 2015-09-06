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
	public int boostCombo = 1;


    List<GameObject> nutCounterUIText  = new List<GameObject>();
    List<Text> nutCounterUITextText  = new List<Text>();

    List<GameObject> boostComboUIText  = new List<GameObject>();
    List<Text> boostComboUITextText  = new List<Text>();

	Transform player_surrounding_collider;
	player_surrounding_collider_script player_surrounding_collider_s;

    // Use this for initialization
    void Start(){

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

		        tryBoost();

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

	public void resetBoostCombo(){

		foreach (Text t in boostComboUITextText){
			t.text = "";
		}
		boostCombo = 1;
	}

	public void addNutPoints(int num){
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
		} else if (nutString.Length == 3){
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

	public void tryBoost(){

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
				player_bottom_collider.targets.Remove(child);
				if (myEnemy.isDead == false){
					isJumping = true;
					myEnemy.die();
					if (myEnemy.isDead == true){
						if (boostCombo > 1){
							addPointsAndSparkle(myEnemy.nutPoints, child);
						}
						boost();
					}
				}
			}
		}
    }

	public void addPointsAndSparkle(int num, GameObject child){

		addNutPoints(num);

		GameObject mynutmarker = Instantiate(Resources.Load("Prefabs/nut_point_marker")) as GameObject;
		TextMesh nutText = mynutmarker.GetComponent<TextMesh>();
		nutText.text = "+"+(num * boostCombo).ToString();
		mynutmarker.transform.position = new Vector2(child.transform.position.x+0.5f, child.transform.position.y+0.5f);

		GameObject mysparkle = Instantiate(Resources.Load("Prefabs/sparkle")) as GameObject;
		mysparkle.transform.position = new Vector2(child.transform.position.x, child.transform.position.y-0.45f);
		mysparkle.GetComponent<Rigidbody2D>().velocity = child.GetComponent<Rigidbody2D>().velocity;
	}

	public void boost(){

		boostCombo += 1;
		jumpingTimer = 16;
		isBoosting = true;
        StartCoroutine(boostComboTransition());

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
