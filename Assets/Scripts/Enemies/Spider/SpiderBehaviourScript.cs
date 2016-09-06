using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class SpiderBehaviourScript : Enemy {

	public bool isGrounded = false;
	public bool canGrab = false;
	public bool isGrabbing = false;
	public Collider2D grabObject = null;
	Pickup grabObjectPickup = null;
	SpriteRenderer grabObjectSR = null;
	int grabObjectSavedOrder = 0;
	public bool isHanging = true;
    Animator animator;
	public Transform spider_bottom_collider;
	public Transform spider_bite_collider;
	public Transform spider_grab_collider;
	HingeJoint2D spiderRopeJoint;
	// Transform spider_rope_1;
	// public HingeJoint2D spider_rope_1_joint;
	// Transform spider_rope_2;
	// public HingeJoint2D spider_rope_2_joint;
	// Transform spider_rope_3;
	// public HingeJoint2D spider_rope_3_joint;
	// Transform spider_rope_4;
	// public HingeJoint2D spider_rope_4_joint;
    Rigidbody2D myrigidbody;
	BoxCollider2D mycollider;
	BoxCollider2D grounded_running_collider;
	BoxCollider2D dead_collider;
	BoxCollider2D spider_bottom_collider_c;
	public bool directionIsRight = true;
	Vector2 moveTarget;
	public bool canClimb = false;
	public bool isClimbing = false;

    GameObject player;
    PlayerBehaviourScript playerBS;

	public bool isJumping = false;

	public bool isDescending = false;
	// public bool isWaiting = false;
	public bool isAscending = false;
	public int lastGrab = 200;

	float turnDir = 0f;

	public bool isWaiting = false;
	public bool isMoving = false;

	public bool isDropping = false;

    List<Transform> spider_ropes = new List<Transform>();
    List<HingeJoint2D> spider_rope_joints = new List<HingeJoint2D>();
    List<SpriteRenderer> spider_rope_sr = new List<SpriteRenderer>();

	// Use this for initialization
	void Start () {

		nutPoints = 1;
		lives = 2;
		enemyType = "spider";

		mycollider = GetComponent<BoxCollider2D>();
		animator = GetComponent<Animator>();
		spiderRopeJoint = GetComponent<HingeJoint2D>();
		myrigidbody = GetComponent<Rigidbody2D>();

		player = GameObject.Find("player");
		playerBS = player.GetComponent<PlayerBehaviourScript>();

        foreach (Transform child in transform){
            if (child.name == "spider_bottom_collider"){
                 spider_bottom_collider = child;
				 spider_bottom_collider_c = child.GetComponent<BoxCollider2D>();
            } else if (child.name == "spider_bite_collider"){
                 spider_bite_collider = child;
            } else if (child.name == "spider_grab_collider"){
                 spider_grab_collider = child;
            } else if (child.name == "grounded_running_collider"){
				grounded_running_collider = child.GetComponent<BoxCollider2D>();
			} else if (child.name == "dead_collider"){
				dead_collider = child.GetComponent<BoxCollider2D>();
			}
		}

		foreach (Transform child in transform.parent){
			if (child.name == "web"){

		        foreach (Transform webchild in child.transform){
		            if (webchild.name == "spider_rope_1"){
						spider_ropes.Add(webchild);
						spider_rope_joints.Add(webchild.GetComponent<HingeJoint2D>());
						spider_rope_sr.Add(webchild.GetComponent<SpriteRenderer>());
		            } else if (webchild.name == "spider_rope_2"){
						spider_ropes.Add(webchild);
						spider_rope_joints.Add(webchild.GetComponent<HingeJoint2D>());
						spider_rope_sr.Add(webchild.GetComponent<SpriteRenderer>());
		            } else if (webchild.name == "spider_rope_3"){
						spider_ropes.Add(webchild);
						spider_rope_joints.Add(webchild.GetComponent<HingeJoint2D>());
						spider_rope_sr.Add(webchild.GetComponent<SpriteRenderer>());
		            } else if (webchild.name == "spider_rope_4"){
						spider_ropes.Add(webchild);
						spider_rope_joints.Add(webchild.GetComponent<HingeJoint2D>());
						spider_rope_sr.Add(webchild.GetComponent<SpriteRenderer>());
					} else if (webchild.name == "spider_rope_5"){
						spider_ropes.Add(webchild);
						spider_rope_joints.Add(webchild.GetComponent<HingeJoint2D>());
						spider_rope_sr.Add(webchild.GetComponent<SpriteRenderer>());
		            } else if (webchild.name == "spider_rope_6"){
						spider_ropes.Add(webchild);
						spider_rope_joints.Add(webchild.GetComponent<HingeJoint2D>());
						spider_rope_sr.Add(webchild.GetComponent<SpriteRenderer>());
		            } else if (webchild.name == "spider_rope_7"){
						spider_ropes.Add(webchild);
						spider_rope_joints.Add(webchild.GetComponent<HingeJoint2D>());
						spider_rope_sr.Add(webchild.GetComponent<SpriteRenderer>());
		            } else if (webchild.name == "spider_rope_8"){
						spider_ropes.Add(webchild);
						spider_rope_joints.Add(webchild.GetComponent<HingeJoint2D>());
						spider_rope_sr.Add(webchild.GetComponent<SpriteRenderer>());
					}
				}

			}
		}

		startHanging();

		StartCoroutine(checkOut());
		// upTarget = new Vector2(spider_rope_4_joint.connectedAnchor.x, spider_rope_4_joint.connectedAnchor.y+2f);
	}

	public void startHanging(){
		Debug.Log("startHanging");

		// moveTarget = new Vector2(transform.position.x, transform.position.y-1.5f);

		// for(int x = spider_rope_joints.Count-1; x >= 0; x--){
		// 	spider_rope_joints[x].connectedAnchor = new Vector2(0f, 0f);
		// 	spider_rope_joints[x].anchor = new Vector2(0f, 0f);
		// }
		isClimbing = false;
		transform.localEulerAngles = new Vector3(0f, 0f, 0f);

		spider_rope_joints[spider_rope_joints.Count-1].connectedAnchor = new Vector2(transform.position.x, transform.position.y);

		for(int x = spider_ropes.Count-1; x >= 0; x--){
			spider_ropes[x].gameObject.SetActive(true);
			spider_ropes[x].localScale = new Vector2(1f, 0f);
		}

		myrigidbody.isKinematic = false;
		spiderRopeJoint.enabled = true;

		isHanging = true;

		isDescending = true;
		isAscending = false;

		StartCoroutine(waitDelay());

	}

	public void stopHanging(){
		Debug.Log("stopHanging");

		for(int x = spider_ropes.Count-1; x >= 0; x--){
			spider_ropes[x].gameObject.SetActive(false);
		}

		// transform.position = spider_rope_joints[spider_rope_joints.Count-1].connectedAnchor;

		myrigidbody.isKinematic = true;
		spiderRopeJoint.enabled = false;

		isHanging = false;
		isWaiting = false;
		isMoving = false;

		isDescending = false;
		isAscending = false;

		StartCoroutine(moveToBranch());
		// if (canClimb == true){
		// 	// isClimbing = true;
		// 	// StartCoroutine(hangDelay());
		// } else {
		// 	StartCoroutine(moveToBranch());
		// }
	}

    public IEnumerator moveToBranch(){

		while(transform.position.x != spider_rope_joints[spider_rope_joints.Count-1].connectedAnchor.x || transform.position.y != spider_rope_joints[spider_rope_joints.Count-1].connectedAnchor.y){

			float newX = transform.position.x;
			float newY = transform.position.y;

			if (transform.position.x > spider_rope_joints[spider_rope_joints.Count-1].connectedAnchor.x){
				newX -= 0.05f;
			} else if (transform.position.x < spider_rope_joints[spider_rope_joints.Count-1].connectedAnchor.x){
				newX += 0.05f;
			}

			if (transform.position.y > spider_rope_joints[spider_rope_joints.Count-1].connectedAnchor.y){
				newY -= 0.05f;
			} else if (transform.position.y < spider_rope_joints[spider_rope_joints.Count-1].connectedAnchor.y){
					newY += 0.05f;
			}

			transform.position = new Vector3(newX, newY, 0f);

			if (transform.position.y-0.1f < spider_rope_joints[spider_rope_joints.Count-1].connectedAnchor.y && transform.position.x-0.1 < spider_rope_joints[spider_rope_joints.Count-1].connectedAnchor.x &&
				transform.position.y+0.1f > spider_rope_joints[spider_rope_joints.Count-1].connectedAnchor.y && transform.position.x+0.1 > spider_rope_joints[spider_rope_joints.Count-1].connectedAnchor.x){
				transform.position = new Vector3(spider_rope_joints[spider_rope_joints.Count-1].connectedAnchor.x, spider_rope_joints[spider_rope_joints.Count-1].connectedAnchor.y, 0f);
			}


			yield return new WaitForSeconds(0.025f);
		}

		isClimbing = true;
		StartCoroutine(hangDelay());
	}


    public IEnumerator hangDelay(){

	    yield return new WaitForSeconds(8f);

		startHanging();
	}

	// Update is called once per frame
	void FixedUpdate () {

		float myAngle2 = transform.eulerAngles.z;
		myAngle2 = myAngle2 / 90f;
		// Debug.Log("myAngle2");
		// Debug.Log(myAngle2);

		if (isGrabbing == false){
			lastGrab++;
		}

		if (isDead == false && (myAngle2 > 3.5f || myAngle2 < 0.5f)){

			if (isGrounded == true){

				if (isJumping == false){
					StartCoroutine(waitAndJump());
				}

			} else if (isHanging == true) {

				if (isGrabbing == true && grabObject == null){
					isGrabbing = false;
				}
				if (isGrabbing == true){
					grabObject.gameObject.transform.position = new Vector2(spider_grab_collider.position.x, spider_grab_collider.position.y-0.5f);
				}

				RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, new Vector2(0.6f, 0.6f), 0f, Vector3.down);

				if (isGrabbing == false){
					for (int i = 0; i < hits.Length; i++) {
			            RaycastHit2D hit = hits[i];
						if (hit.collider.name != transform.name && (hit.collider.name == "player" || hit.collider.tag.Contains("enemy") || hit.collider.tag.Contains("pickup"))){
							if (hit.collider.tag.Contains("phys")){
								isMoving = true;
								isWaiting = false;
								isDropping = true;
								StartCoroutine(dropDelay());
								isDescending = true;
								isAscending = false;
							}
						}
					}
				}

				if (isWaiting == false && isMoving == true){

					if (isDescending == true){
						Debug.Log("descending");

						if (spider_ropes[0].transform.localScale.y > 10f) {
							Debug.Log("1");

							isDescending = false;
							isAscending = true;
							isDropping = false;
						} else {
							Debug.Log("2");

							for (int i = 0; i < hits.Length; i++) {
					            RaycastHit2D hit = hits[i];
								if (hit.collider.name != transform.name){
									if (hit.collider.tag.Contains("env") && !hit.collider.tag.Contains("climbable")){

										if ((transform.position.y - hit.point.y) < 1f){
											isDescending = false;
											isAscending = true;
											isDropping = false;
										}
									}
								}
							}
						}
						Debug.Log("3");

						if (isDescending == true){
							Debug.Log("4");

							if (isDropping == true){
								for(int x = 0; x < spider_ropes.Count; x++){
									spider_ropes[x].localScale = new Vector3(1f, spider_ropes[x].transform.localScale.y+0.06f, 1f);
								}
							} else {
								for(int x = 0; x < spider_ropes.Count; x++){
									spider_ropes[x].localScale = new Vector3(1f, spider_ropes[x].transform.localScale.y+0.015f, 1f);
								}
							}
						}

					} else if (isAscending == true){

						Debug.Log("ascending");

						for(int x = 0; x < spider_ropes.Count; x++){
							spider_ropes[x].localScale = new Vector3(1f, spider_ropes[x].transform.localScale.y-0.01f, 1f);
						}

						// double subx = Math.Round((double)spider_rope_joints[spider_rope_joints.Count-1].connectedAnchor.x, 3) - Math.Round((double)transform.position.x, 3);
						// double suby = Math.Round((double)spider_rope_joints[spider_rope_joints.Count-1].connectedAnchor.y, 3) - Math.Round((double)transform.position.y, 3);

						// if (//isClimbing == true &&
						// 	subx < 0.4f && 
						// 	subx > -0.4f &&
						// 	suby < 0.4f && 
						// 	suby > -0.4f){
						if (spider_ropes[0].transform.localScale.y <= 0f) {

							Debug.Log("SPAWN DAT SACK");

							if (isGrabbing == true){
								isGrabbing = false;
								lastGrab = 0;
								if (grabObject.gameObject.name != "player"){
									Destroy(grabObject.gameObject);
								}
								addSpiderSack(gameObject.transform.position);
							}

							stopHanging();
						}

					}
				}

			} else if (isClimbing == true){

				float myAngle = transform.eulerAngles.z;
				myAngle = myAngle / 90f;
				Vector2 checkDirection = new Vector2(99f, 99f);
				float x = 99f;
				float y = 99f;
				if (myAngle < 2f){
					if (myAngle < 1f){
						x = myAngle;
					} else {
						x = 1f - (myAngle - 1f);
					}
				} else {
					if (myAngle < 3f){
						x = 2f - myAngle;
					} else {
						x = -1f + (myAngle-3f);
					}
				}
				if (myAngle < 1f || myAngle > 3f){
					if (myAngle < 1f){
						y = -1f + myAngle;
					} else {
						y = 1f - (myAngle - 2f);
					}
				} else {
					if (myAngle < 2f){
						y = myAngle - 1f;
					} else {
						y = 0f - (myAngle - 3f);
					}
				}
				checkDirection = new Vector2(x, y);

				RaycastHit2D[] hits = null;
				hits = Physics2D.RaycastAll(transform.position, checkDirection, 0.05f);

				bool found = false;
				for (int i = 0; i < hits.Length; i++) {
		            RaycastHit2D hit = hits[i];
					if (hit.transform.tag.Contains("climbable")){
						turnDir = 0f;
						transform.position = new Vector2(transform.position.x+(checkDirection.x/100f), transform.position.y+(checkDirection.y/100f));
						found = true;
					}
				}
				if (found == false){
					if (turnDir == 0){
						turnDir = UnityEngine.Random.Range(-1, 2);
					}
					transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + turnDir);
				}
			}
		}

		if (directionIsRight){ //is right
			transform.localScale = new Vector3(10, 10, 1);
		} else { //is left
			transform.localScale = new Vector3(-10, 10, 1);
		}

		animator.SetBool("isMoving", isMoving);
        animator.SetBool("isWaiting", isWaiting);
		animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("isDead", isDead);
		// animator.SetBool("canGrab", canGrab);
		animator.SetBool("isGrabbing", isGrabbing);
		animator.SetBool("isHanging", isHanging);
	}

	IEnumerator dropDelay(){

		yield return new WaitForSeconds(1f);
		if (isDropping == true){
			isDropping = false;
		}
	}



	IEnumerator waitDelay(){

		if (isDropping == false){
			isWaiting = true;
		}
		yield return new WaitForSeconds(1f);
		if (isHanging == true){
			isWaiting = false;
			StartCoroutine(moveDelay());
		}
	}

	IEnumerator moveDelay(){

		isMoving = true;
		yield return new WaitForSeconds(1f);
		if (isHanging == true){
			if (isDropping == false){
				isMoving = false;
			}
			StartCoroutine(waitDelay());
		}
	}

	public override void tryKill(){
		Debug.Log("tryKill");

		isMoving = false;
		isWaiting = false;
		isAscending = false;
		isDescending = false;
		isHanging = false;

		lives--;
		if (isDead == false && isProtected == false && lives <= 0){
	        isDead = true;

			mycollider.size = dead_collider.size;
			mycollider.offset = dead_collider.offset;
			spider_bite_collider.gameObject.SetActive(false);
			isJumping = false;

	        StartCoroutine(dieDelay());
		} else {
			StartCoroutine(protect());

			if (isGrabbing == true){
				isGrabbing = false;
				grabObjectPickup.isProtected = false;
				grabObjectSR.sortingOrder = grabObjectSavedOrder;
				lastGrab = 0;
			}

			if (lives == 1){

				// StartCoroutine(hideWebStrand());
				spiderRopeJoint.enabled = false;
				isGrabbing = false;

				transform.localEulerAngles = new Vector3(0f, 0f, 0f);
				myrigidbody.freezeRotation = true;

				mycollider.size = grounded_running_collider.size;
				mycollider.offset = grounded_running_collider.offset;

				spider_grab_collider.gameObject.SetActive(false);
				spider_bottom_collider.gameObject.SetActive(true);
				spider_bite_collider.gameObject.SetActive(true);

	        	StartCoroutine(dieDelayWeb());

				// spider_bottom_collider_c.size = new Vector2(mycollider.size.x, mycollider.size.y/2+spider_bottom_collider_c.size)

			}

		}
    }

    public IEnumerator dieDelayWeb(){

		for (int i = 0; i < 3; i++) {

			for(int x = 0; x < spider_rope_sr.Count; x++){
				spider_rope_sr[x].enabled = false;
			}
	        yield return new WaitForSeconds(0.5f);
			for(int x = 0; x < spider_rope_sr.Count; x++){
				spider_rope_sr[x].enabled = true;
			}
	        yield return new WaitForSeconds(0.5f);
		}

		for(int x = spider_ropes.Count-1; x >= 0; x--){
			spider_ropes[x].gameObject.SetActive(false);
		}
    }

	public void setGrabObject(Collider2D other){
		if (isHanging == true && lastGrab > 200){
			// Debug.Log(other.gameObject.name);
			isGrabbing = true;
			grabObject = other;
			grabObjectSR = other.gameObject.GetComponent<SpriteRenderer>();
			grabObjectSavedOrder = grabObjectSR.sortingOrder;
			grabObjectSR.sortingOrder = -82;

			if (other.gameObject.name == "player"){
				if (playerBS.isProtected == false){
					StartCoroutine(HangingBitePlayer());
				}
			}
			if (other.gameObject.tag.Contains("pickup")){
				grabObjectPickup = other.gameObject.GetComponent<Pickup>();
				grabObjectPickup.isProtected = true;
			}
			isDescending = false;
			Physics2D.IgnoreCollision(mycollider, grabObject);
			isAscending = true;
			isDropping = false;
			// moveTarget = new Vector2(spider_rope_4_joint.connectedAnchor.x, spider_rope_4_joint.connectedAnchor.y+1.5f);

		}
	}

	IEnumerator HangingBitePlayer(){

		for (int x = 0; x < UnityEngine.Random.Range(3, 4); x++) {
			// Debug.Log("bla");
			if (isGrabbing == true && grabObject.gameObject.name == "player"){
				bitePlayer();
			} else {
				break;
			}
			yield return new WaitForSeconds(1.1f);
		}

		isGrabbing = false;
		grabObjectSR.sortingOrder = grabObjectSavedOrder;
		lastGrab = 0;

	}

	public void bitePlayer(){

		if (playerBS.isProtected == false){
			playerBS.getHit(1, directionIsRight);
		}
	}

	public void setClimbing(bool state){
		canClimb = state;
		// isClimbing = state;

		// if (isHanging == false){
		// 	myrigidbody.isKinematic = state;
		// }
	}

	IEnumerator hideWebStrand(){

		isDescending = false;
		isAscending = false;
		// Vector2 retractTarget = new Vector2(spider_rope_4_joint.connectedAnchor.x, spider_rope_4_joint.connectedAnchor.y+10f);
		for (int x = 0; x < 500; x++) {
			// spider_rope_4_joint.connectedAnchor = Vector2.Lerp(spider_rope_4_joint.connectedAnchor, retractTarget, Time.deltaTime*1f);
			yield return new WaitForSeconds(0.05f);
		}

	}

	IEnumerator waitAndJump(){

		isJumping = true;
		yield return new WaitForSeconds(UnityEngine.Random.Range(0.2f, 0.5f));
		if (isJumping == true){
			Vector2 velocity = new Vector2(0f,0f);
			float dirMultiplier = -1f;
			directionIsRight = false;
			if (UnityEngine.Random.Range(0, 2) == 1){
				dirMultiplier = 1f;
				directionIsRight = true;
			}
			velocity.x = (UnityEngine.Random.Range(20f, 60f) * dirMultiplier);
			velocity.y = UnityEngine.Random.Range(40f, 60f);
			myrigidbody.AddForce(velocity, ForceMode2D.Impulse);
			isJumping = false;
		}
	}


    void addSpiderSack(Vector3 pos){
        pos = new Vector2(pos.x, pos.y);
      	GameObject myspidersack = Instantiate(Resources.Load("Prefabs/spider_sack")) as GameObject;
        myspidersack.transform.parent = GameObject.Find("enemies").transform;
      	myspidersack.transform.position = pos;
        // SpiderBehaviourScript spiderBS = myspidersack.GetComponent<SpiderBehaviourScript>();

        foreach (Transform child in myspidersack.transform){
            if (child.name == "spider_rope_8"){
				HingeJoint2D spider_rope_8_joint = child.GetComponent<HingeJoint2D>();
                spider_rope_8_joint.connectedAnchor = new Vector2(pos.x, pos.y);
			}
		}

        // frogBS.directionIsRight = directionIsRight;
    }


}
