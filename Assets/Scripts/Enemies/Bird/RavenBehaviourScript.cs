using UnityEngine;
using System.Collections;

public class RavenBehaviourScript : Enemy {

	public bool isPecking = false;
	public bool isGrounded = true;
	public bool isBirthing = false;
	public bool isFlapping = false;
	public bool isFlying = false;

	Animator animator;
	Rigidbody2D myrigidbody;
	Collider2D mycollider;

	public bool directionIsRight = true;
	float dirMultiplier = 1f;

	public string currentAction = "waitAction";

    GameObject player;
    PlayerBehaviourScript playerBS;
    Vector3 nestSearchLoc = new Vector3();
    bool hasNestSearchLoc = false;
    public bool branchColliding = false;
	GameObject environment;
	public WorldScript environment_ws;
	GameObject pickups;

	bool hasNest = false;
	GameObject nest = null;
	BirdNestBehaviourScript nestBS;

	public Transform pickup = null;
	public bool hasPickup = false;

	public bool ravenDroppingPickup = false;
	public int birthPoints = 1;

	public bool inNest = false;

	Transform sprite;
	Transform raven_grab_collider;


    //Looks for closest nut, player, or enemy
    //Swoops to the ground and gets it
    //Returns to nest, or builds a nest
    //Then either lays an egg, or feeds a hatchling (odds depend on nest state?)

	// Use this for initialization
	void Start () {

		lives = 3;
		player = GameObject.Find("player");
		playerBS = player.GetComponent<PlayerBehaviourScript>();
		myrigidbody = GetComponent<Rigidbody2D>();
        // mycollider = GetComponent<Collider2D>();

		environment = GameObject.Find("environment");
		environment_ws = environment.GetComponent<WorldScript>();
		pickups = GameObject.Find("pickups");

        foreach (Transform child in transform) {

            if (child.name == "sprite"){

            	sprite = child;
        		animator = child.GetComponent<Animator>();
        		mycollider = child.GetComponent<Collider2D>();
        		// myrigidbody = child.GetComponent<Rigidbody2D>();

        		foreach (Transform child2 in sprite) {

           			if (child2.name == "raven_grab_collider"){
           				raven_grab_collider = child2;
           			}
           		}
            }
        }

		StartCoroutine(waitAction());
	}
	
	// Update is called once per frame
	void Update () {
	
        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("isPecking", isPecking);
        // animator.SetBool("isFlapping", isFlapping);

        animator.SetBool("isDead", isDead);
        // animator.SetBool("isLicking", isLicking);
        // animator.SetBool("isLickingUp", isLickingUp);
        animator.SetBool("isBirthing", isBirthing);
        animator.SetBool("isFlying", isFlying);
	
	}
	void FixedUpdate () {

		if (isFlapping == true){

			if (myrigidbody.velocity.y > 0f) {

				sprite.transform.Rotate(0, 0, (myrigidbody.velocity.y/5f));

			} else {

				sprite.transform.Rotate(0, 0, -1f);
			}

		// // if (myrigidbody.velocity.y > (0.75f*10f)) {
		// 	// sprite.transform.eulerAngles = new Vector3(0, 0, 90f);
		// // } else 
		// if (myrigidbody.velocity.y > (0.25f*20f)) {
		// 	// sprite.transform.eulerAngles = new Vector3(0, 0, 45f);

		// 	sprite.transform.Rotate(0, 0, 1f);
		// } else if (myrigidbody.velocity.y < (-0.25f*20f)) {
		// 	// sprite.transform.eulerAngles = new Vector3(0, 0, 0f);
		// // } else if (myrigidbody.velocity.y > (-0.75f*10f)) {
		// // 	sprite.transform.eulerAngles = new Vector3(0, 0, 325f);
		// // } else {
		// 	sprite.transform.Rotate(0, 0, -1f);
		// 	// sprite.transform.eulerAngles = new Vector3(0, 0, 270f);
		// }

			if (sprite.transform.eulerAngles.z > 30f && sprite.transform.eulerAngles.z < 350f) {
				if (sprite.transform.eulerAngles.z > 180f){
					sprite.transform.eulerAngles = new Vector3(0, 0, 350f);
				} else {
					sprite.transform.eulerAngles = new Vector3(0, 0, 30f);
				}
			}
		}

	}

	string getPlayerAdjacent(){

		RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, new Vector2(0.5f, 0.5f), 0f, Vector3.right, 1.5f);

		for (int i = 0; i < hits.Length; i++) {

            RaycastHit2D hit = hits[i];
			if (hit.collider.name != transform.name && (hit.collider.name == "player")){

				return "right";
			}
		}

		hits = Physics2D.BoxCastAll(transform.position, new Vector2(0.5f, 0.5f), 0f, Vector3.left, 1.5f);

		for (int i = 0; i < hits.Length; i++) {

            RaycastHit2D hit = hits[i];
			if (hit.collider.name != transform.name && (hit.collider.name == "player")){

				return "left";
			}
		}

		return null;

	}

	bool getObstacleDir(Vector3 dir){

		RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, new Vector2(0.5f, 0.5f), 0f, dir, 10f);
		for (int i = 0; i < hits.Length; i++) {

            RaycastHit2D hit = hits[i];
			if (hit.collider.tag.Contains("frozen")){

				return true;
			}
		}
		return false;

	}

   IEnumerator peckAction(){
   		currentAction = "peckAction";
   		isPecking = true;

   		yield return new WaitForSeconds(0.25f);

   		RaycastHit2D[] hits = null;

   		if (directionIsRight == true){
			hits = Physics2D.BoxCastAll(transform.position, new Vector2(0.5f, 0.5f), 0f, Vector3.right, 1.5f);
   		} else {
			hits = Physics2D.BoxCastAll(transform.position, new Vector2(0.5f, 0.5f), 0f, Vector3.left, 1.5f);
   		}

		for (int i = 0; i < hits.Length; i++) {

            RaycastHit2D hit = hits[i];
			if (hit.collider.name != transform.name && (hit.collider.name == "player")){

				playerBS.getHit(5, directionIsRight);
			}
		}
		
   		isPecking = false;
		StartCoroutine(getNextAction());
   }

   void changeDirection(){
		if (directionIsRight == true){
			directionIsRight = false;
			dirMultiplier = -1f;
			transform.localScale = new Vector3(-10, 10, 1);
		} else {
			directionIsRight = true;
			dirMultiplier = 1f;
			transform.localScale = new Vector3(10, 10, 1);
		}
   }

	IEnumerator getNextAction(){
		// Debug.Log("getNextAction");
		yield return new WaitForSeconds(0.05f);

		// while (isGrounded == false && isDead == false){
		// 	yield return new WaitForSeconds(0.1f);
		// }
		if (hasPickup == true && pickup == null){
			hasPickup = false;
		}

		if (isDead == false){

			//If player adjecent, peck
			string pDir = null;
			if (isGrounded == true){
				 pDir = getPlayerAdjacent();
			}

			if (pDir != null){

				if (directionIsRight == true && pDir == "left"){

					directionIsRight = false;
					dirMultiplier = -1f;
					transform.localScale = new Vector3(-10, 10, 1);

				} else if (directionIsRight == false && pDir == "right"){

					directionIsRight = true;
					dirMultiplier = 1f;
					transform.localScale = new Vector3(10, 10, 1);
				}

				StartCoroutine(peckAction());

			} else if (nest == null){

				//Make nest
				StartCoroutine(goToNewNestAction());

			} else {

				if (hasPickup == false){

					if (birthPoints > 0){

						StartCoroutine(layEggAction());

					} else {

						//eat nut or collect nut
						if (UnityEngine.Random.Range(0, 2) == 0){

							StartCoroutine(collectFoodAction());

						} else {

							StartCoroutine(eatFoodAction());
						}

					}

				} else {

					//Return food to nest
					StartCoroutine(dropFoodOffAction());

				}
			}
		}
	}

   IEnumerator waitAction(){
   		currentAction = "waitAction";

   		bool foundTarget = false;

   		for (int x = 0; x < UnityEngine.Random.Range(10, 20); x++){

   			yield return new WaitForSeconds(0.1f);

			// if (foundTarget == false){
			// 	if (lickValid() == true){
			// 		foundTarget = true;
			// 		StartCoroutine(lickAction());
			// 		break;
			// 	}
			// }

			// if (foundTarget == false){

			// 	if (lickUpValid() == true){
			// 		foundTarget = true;
			// 		StartCoroutine(lickUpAction());
			// 		break;
			// 	}
			// }
   		}
		
		if (foundTarget == false){
			StartCoroutine(getNextAction());
		}
		// StartCoroutine(jumpAction());
   }

   public float maxed(float x, float max){
   		if (x > max){
   			return max;
   		} else {
   			return x;
   		}
   }

   IEnumerator goToNewNestAction(){
   		currentAction = "goToNewNestAction";
   		yield return new WaitForSeconds(0.25f);

   		if (hasNestSearchLoc == false){

   			//Get Nest location
   			// nestSearchLoc = new Vector3(-4.95f, 4.18f);

            // int foundTenacity = 0;

            bool foundV = false;
            // Vector3 nestSearchLoc = new Vector3();

            while (foundV != true){
                nestSearchLoc = new Vector3(UnityEngine.Random.Range(-16f, 16f), UnityEngine.Random.Range(0f, 22f), 0);

                foreach (Collider2D child in environment_ws.branches_foreground_colliders){
                    if (child.OverlapPoint(nestSearchLoc)){
                        foundV = true;
                    }
                    // foundTenacity++;
                }
            }

   			hasNestSearchLoc = true;
   		}

   		Vector3 targetLoc = nestSearchLoc;//new Vector3(nest.transform.position.x, nest.transform.position.y+3f, 1f);

   		if (isNearTarget(targetLoc) == true){

			branchColliding = true;
			StartCoroutine(makeNestAction());

   		} else {

			// if (isGrounded == false){
				// while (myrigidbody.velocity.y > -0.3f && targetLoc.y > transform.position.y-0.5f){
	   // 				yield return new WaitForSeconds(0.25f);
				// }
			// }

	   		// isFlapping = true;
	   		// animator.SetBool("isFlapping", isFlapping);

	   		// yield return new WaitForSeconds(flyToLocation(targetLoc));

	   		// isFlapping = false;
	   		// animator.SetBool("isFlapping", isFlapping);

   			// flyToLocation(new Vector3(nest.transform.position.x, nest.transform.position.y+3f, 1f));
			StartCoroutine(flyToLocationAction(targetLoc));
   		}
		
		// StartCoroutine(getNextAction());

		// bool aboveNest = (transform.position.y > nestSearchLoc.y);

		// //Check near nest
		// if (transform.position.y-1f < nestSearchLoc.y && transform.position.x-1f < nestSearchLoc.x &&
		// 	transform.position.y+1f > nestSearchLoc.y && transform.position.x+1f > nestSearchLoc.x
		// 	//){
		// 	//if (
		// 		&& aboveNest == true && branchColliding == false){

		// 		// environment_ws.setBranchCollider(mycollider, false);
		// 		branchColliding = true;
		// 		StartCoroutine(makeNestAction());

		// 	// }

		// } else {

		// 	if (isGrounded == false){
		// 		while (myrigidbody.velocity.y > -0.3f && nestSearchLoc.y > transform.position.y-0.5f){
	 //   				yield return new WaitForSeconds(0.25f);
		// 		}
		// 	}

	 //   		isFlapping = true;
	 //   		animator.SetBool("isFlapping", isFlapping);

		// 	float forceY = myrigidbody.velocity.y;//UnityEngine.Random.Range(8f, 10f);//17.5f;
		// 	float forceX = 0f;
		// 	float lowerFlapRange = 0.2f;
		// 	float upperFlapRange = 0.6f;

		// 	bool insideColumn = (nestSearchLoc.x > transform.position.x-2f && nestSearchLoc.x < transform.position.x+2f);

	 //   		if (aboveNest == false || insideColumn == false){
	 //   			forceY += UnityEngine.Random.Range(15f, 20f);

	 //   			upperFlapRange = 0.4f;
	 //   		} else {

	 //   			forceY += 5f;
		// 		lowerFlapRange = 0.5f;

	 //   		}

	 //   		float dif = nestSearchLoc.x - transform.position.x;

	 //   		forceX = dif*2f;// / 10f

	 //   		//cap it
	 //   		if (forceX > 0f){
	 //   			if (forceX < 7.5f){
	 //   				forceX = 7.5f;
	 //   			} else if (forceX > 15f){
	 //   				forceX = 15f;
	 //   			}
	 //   		} else {
	 //   			if (forceX > -7.5f){
	 //   				forceX = -7.5f;
	 //   			} else if (forceX < -15f){
	 //   				forceX = -15f;
	 //   			}
	 //   		}

	 //   		if ((forceX > 0.1f && directionIsRight == false) || (forceX < -0.1f && directionIsRight == true)){
	 //   			changeDirection();
	 //   		}

	 //   		myrigidbody.velocity = new Vector3(myrigidbody.velocity.x, 0f, 0f);
		// 	myrigidbody.AddForce(new Vector2(forceX, forceY), ForceMode2D.Impulse);
	 //   		yield return new WaitForSeconds(UnityEngine.Random.Range(lowerFlapRange, upperFlapRange));

	 //   		isFlapping = false;
	 //   		animator.SetBool("isFlapping", isFlapping);
			
		// 	StartCoroutine(getNextAction());
		// }
   }

   IEnumerator moveIntoNest(){
   	Debug.Log("moveIntoNest");
   	isFlying = false;
   	// myrigidbody.mass = 1f;

   		if (nest != null){

        	myrigidbody.isKinematic = true;
   			while((transform.position.x != nest.transform.position.x) || (transform.position.y != nest.transform.position.y+3.5f)){

   				float newX = transform.position.x;
   				float newY = transform.position.y;

   				if (transform.position.x > nest.transform.position.x){
   					newX -= 0.05f;
				} else if (transform.position.x < nest.transform.position.x){
   					newX += 0.05f;
				}

   				if (transform.position.y > nest.transform.position.y+3.5f){
   					newY -= 0.05f;
				} else if (transform.position.y < nest.transform.position.y+3.5f){
   					newY += 0.05f;
				}

				transform.position = new Vector3(newX, newY, 0f);

				if (transform.position.y-0.1f < nest.transform.position.y+3.5f && transform.position.x-0.1f < nest.transform.position.x &&
					transform.position.y+0.1f > nest.transform.position.y+3.5f && transform.position.x+0.1f > nest.transform.position.x){
					transform.position = new Vector3(nest.transform.position.x, nest.transform.position.y+3.5f, 0f);
				}


   				yield return new WaitForSeconds(0.01f);
   			}
   			inNest = true;
   		}
   		Debug.Log("moveIntoNest complete");
		StartCoroutine(getNextAction());
   }

   public void setGrounded(bool state){
   		isGrounded = state;
   		if (isGrounded == true){
   			sprite.transform.eulerAngles = new Vector3(0, 0, 0f);
   		}
   }

   IEnumerator collectFoodAction(){
   		Debug.Log("collectFoodAction");
   		currentAction = "collectFoodAction";

        if (inNest == true){
        	// myrigidbody.isKinematic = false;
        	inNest = false;
        }

   		// yield return new WaitForSeconds(0.1f);

   		// if (hasPickup == true && pickup == null){
   		// 	hasPickup = false;
   		// }

   		//find closest nut
   		if (pickup == null){

   			while (pickups.transform.childCount == 0){
   				yield return new WaitForSeconds(0.1f);
   			}

   			float BestDistance = 99999f;

            foreach (Transform child in pickups.transform){
                
   				float distance = Vector3.Distance (transform.position, child.transform.position);
   				if (child.tag.Contains("pickup") && BestDistance > distance && child.transform.position.y <= -0.5f){
   					BestDistance = distance;
   					pickup = child;
   				}
            }
   		}

   		if (pickup == null){
   			yield return new WaitForSeconds(0.1f);
			StartCoroutine(getNextAction());

		} else {

	   		Vector3 targetLoc = pickup.position;//new Vector3(nest.transform.position.x, nest.transform.position.y+3f, 1f);

	   		if (isNearTarget(targetLoc) == true){

				hasPickup = true;
				StartCoroutine(getNextAction());

	   		} else {

				// if (isGrounded == false){
				// 	while (myrigidbody.velocity.y > -0.3f && targetLoc.y > transform.position.y-0.5f){
		  //  				yield return new WaitForSeconds(0.25f);
				// 	}
				// }

		  //  		isFlapping = true;
		  //  		animator.SetBool("isFlapping", isFlapping);

		  //  		yield return new WaitForSeconds(flyToLocation(targetLoc));

		   		// isFlapping = false;
		   		// animator.SetBool("isFlapping", isFlapping);

	   			// flyToLocation(new Vector3(nest.transform.position.x, nest.transform.position.y+3f, 1f));
				StartCoroutine(flyToLocationAction(targetLoc));
	   		}
		}
		
		// StartCoroutine(getNextAction());


		//Check near nut
		// bool abovePickup = (transform.position.y > pickup.position.y);
		// if (transform.position.y-1f < pickup.position.y && transform.position.x-1f < pickup.position.x &&
		// 	transform.position.y+1f > pickup.position.y && transform.position.x+1f > pickup.position.x){

		// 	hasPickup = true;

		// } else {

		// 	if (isGrounded == false){
		// 		while (myrigidbody.velocity.y > -0.3f && pickup.position.y > transform.position.y-0.5f){
	 //   				yield return new WaitForSeconds(0.25f);
		// 		}
		// 	}

	 //   		isFlapping = true;
	 //   		animator.SetBool("isFlapping", isFlapping);

		// 	float forceY = 0f;//myrigidbody.velocity.y;
		// 	float forceX = 0f;
		// 	float lowerFlapRange = 0.2f;
		// 	float upperFlapRange = 0.6f;

		// 	bool insideColumn = (pickup.position.x > transform.position.x-2f && pickup.position.x < transform.position.x+2f);

	 //   		if (abovePickup == false){
	 //   			forceY += UnityEngine.Random.Range(15f, 20f);

	 //   			upperFlapRange = 0.4f;
	 //   		}// else {

	 //   // 			forceY += 5f;
		// 		// lowerFlapRange = 0.5f;

	 //   // 		}

	 //   		float dif = pickup.position.x - transform.position.x;

	 //   		forceX = dif*2f;// / 10f

	 //   		//cap it
	 //   		if (forceX > 0f){
	 //   			if (forceX < 7.5f){
	 //   				forceX = 7.5f;
	 //   			} else if (forceX > 15f){
	 //   				forceX = 15f;
	 //   			}
	 //   		} else {
	 //   			if (forceX > -7.5f){
	 //   				forceX = -7.5f;
	 //   			} else if (forceX < -15f){
	 //   				forceX = -15f;
	 //   			}
	 //   		}

	 //   		if ((forceX > 0.1f && directionIsRight == false) || (forceX < -0.1f && directionIsRight == true)){
	 //   			changeDirection();
	 //   		}

	 //   		myrigidbody.velocity = new Vector3(myrigidbody.velocity.x, 0f, 0f);
		// 	myrigidbody.AddForce(new Vector2(forceX, forceY), ForceMode2D.Impulse);
	 //   		yield return new WaitForSeconds(UnityEngine.Random.Range(lowerFlapRange, upperFlapRange));

	 //   		isFlapping = false;
	 //   		animator.SetBool("isFlapping", isFlapping);
			
		// }
		
		// StartCoroutine(getNextAction());
   }

   public void setPickup(Collider2D other){
   		if (other.gameObject != nest && ravenDroppingPickup == false){
	   		pickup = other.transform;
   			hasPickup = true;
			StartCoroutine(holdPickup());
   		}
   }


   // IEnumerator dropPickupDelay(){

   //      ravenDroppingPickup = true;
   // 		yield return new WaitForSeconds(2f);
   //      ravenDroppingPickup = false;
   // }

   IEnumerator makeNestAction(){
   		currentAction = "makeNestAction";

        hasNest = true;
		nest = Instantiate(Resources.Load("Prefabs/bird_nest")) as GameObject;
        nest.transform.parent = GameObject.Find("enemies").transform;
		nest.transform.position = new Vector2(transform.position.x, transform.position.y);
		nestBS = nest.GetComponent<BirdNestBehaviourScript>();

		StartCoroutine(moveIntoNest());
   		yield return new WaitForSeconds(0.1f);

		
		// StartCoroutine(getNextAction());
   }

   IEnumerator holdPickup(){

   		Rigidbody2D pickup_rb = pickup.GetComponent<Rigidbody2D>();
   		Collider2D pickup_c = pickup.GetComponent<Collider2D>();

	   	pickup_rb.isKinematic = true;
	   	pickup_c.enabled = false;

   		while (hasPickup == true && pickup != null){

   			pickup.position = raven_grab_collider.position;//new Vector3(transform.position.x, transform.position.y-1f, 0f);
   			yield return new WaitForSeconds(0.005f);
   		}

   		if (pickup != null){
		   	pickup_rb.isKinematic = false;
		   	pickup_c.enabled = true;
   		}
   }

	IEnumerator eatFoodAction(){
		currentAction = "eatFoodAction";
		yield return new WaitForSeconds(0.1f);

		StartCoroutine(getNextAction());

	}

	IEnumerator layEggAction(){
		currentAction = "layEggAction";
		isFlying = false;
   		isBirthing = true;

   		for (int x = 0; x < birthPoints; x++){
   			Debug.Log("layEggAction loop");
   			Debug.Log(birthPoints);
			yield return new WaitForSeconds(UnityEngine.Random.Range(0.5f, 1.5f));

			// if (isBirthing == true){
				
			// }

			if (isDead == false){
				GameObject bird_egg = null;
				bird_egg = Instantiate(Resources.Load("Prefabs/bird_egg")) as GameObject;
				Collider2D bird_egg_c = bird_egg.transform.GetComponent<Collider2D>();
        		bird_egg.transform.parent = GameObject.Find("enemies").transform;
				bird_egg.transform.position = new Vector2(transform.position.x, transform.position.y);
				bird_egg.transform.GetComponent<BirdEggBehaviourScript>().nest = nest;
            	Physics2D.IgnoreCollision(bird_egg_c, mycollider, true);
            	environment_ws.setBranchCollider(bird_egg_c, true);
				// bird_egg.transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(UnityEngine.Random.Range(-80f, -150f) * dirMultiplier, UnityEngine.Random.Range(60f, 100f)), ForceMode2D.Impulse);


				// myrigidbody.AddForce(new Vector2(50f * dirMultiplier, 20f), ForceMode2D.Impulse);
			}
   		}

		birthPoints = 0;
   		isBirthing = false;

		StartCoroutine(getNextAction());

	}

	bool isNearTarget(Vector3 targetLoc){

		if (transform.position.y-1f < (targetLoc.y) && transform.position.x-1f < targetLoc.x &&
			transform.position.y+1f > (targetLoc.y) && transform.position.x+1f > targetLoc.x){

			return true;

		} else {
			return false;
		}
	}

	float speed = 0.05f;

	IEnumerator flyToLocationAction(Vector3 targetLoc){
		Debug.Log("flyToLocation");
		isFlying = true;
		// myrigidbody.mass=0f;

		// if (isGrounded == false){
		// 	while (myrigidbody.velocity.y > -0.3f && targetLoc.y > transform.position.y-0.5f){
  //  				yield return new WaitForSeconds(0.25f);
		// 	}
		// }

		myrigidbody.isKinematic = true;

		// float newX = 0f;
		// float newY = 0f;

		// if (targetLoc.x > transform.position.x){
		// 	newX += 1f;
		// } else {
		// 	newX -= 1f;
		// }

		// if (targetLoc.y > transform.position.y){
		// 	newY += 1f;
		// } else {
		// 	newY -= 1f;
		// }
		// transform.position = Vector3.Lerp(transform.position, targetLoc, Time.deltaTime/(Vector3.Distance(transform.position, targetLoc)/5f));
		while (isNearTarget(targetLoc) == false){

			Vector3 newPos = Vector3.MoveTowards(transform.position, targetLoc, speed+UnityEngine.Random.Range(-0.01f, 0.01f));

	   		if ((newPos.x > transform.position.x && directionIsRight == false) || (newPos.x < transform.position.x && directionIsRight == true)){
	   			changeDirection();
	   		}

			transform.position = newPos;
	   		yield return new WaitForSeconds(0.01f);
	   		if (speed < 0.1f) {
	   			speed = speed * 1.01f;
	   		}

   		}
		StartCoroutine(getNextAction());

		// transform.position = new Vector3(newX, newY, 0f);
   		// myrigidbody.velocity = new Vector3(newX, newY, 0f);
		// myrigidbody.AddForce(new Vector2(newX, newY), ForceMode2D.Impulse);

		// float forceY = myrigidbody.velocity.y;
		// float forceX = 0f;
		// float waitTime = 0.4f;
		// bool aboveTarget = (transform.position.y > (targetLoc.y));
  //  		if (aboveTarget == false){
  //  			forceY += UnityEngine.Random.Range(15f, 20f);

		// 	waitTime = 0.3f;
  //  		} else {

  //  			forceY += 5f;
		// 	waitTime = 0.5f;
  //  		}
  //  		if (getObstacleDir(Vector3.up) == true || getObstacleDir(Vector3.down) == true){
  //  			if (directionIsRight){
  //  				forceX = 10f;
  //  			} else {
  //  				forceX = -10f;
  //  			}
	 //   	} else {

	 //   		float dif = targetLoc.x - transform.position.x;

	 //   		forceX = dif*2f;// / 10f

	 //   		//cap it
	 //   		if (forceX > 0f){
	 //   			if (forceX < 7.5f){
	 //   				forceX = 7.5f;
	 //   			} else if (forceX > 15f){
	 //   				forceX = 15f;
	 //   			}
	 //   		} else {
	 //   			if (forceX > -7.5f){
	 //   				forceX = -7.5f;
	 //   			} else if (forceX < -15f){
	 //   				forceX = -15f;
	 //   			}
	 //   		}

	 //   		if ((forceX > 0.1f && directionIsRight == false) || (forceX < -0.1f && directionIsRight == true)){
	 //   			changeDirection();
	 //   		}
	 //   	}
 
   		// myrigidbody.velocity = new Vector3(0f, 0f, 0f);
		// myrigidbody.AddForce(new Vector2(forceX, forceY), ForceMode2D.Impulse);
		// return waitTime;
		// return 0.005f;

		// return UnityEngine.Random.Range(lowerFlapRange, upperFlapRange);
   		// yield return new WaitForSeconds(UnityEngine.Random.Range(lowerFlapRange, upperFlapRange));
	}

   IEnumerator dropFoodOffAction(){
   		currentAction = "dropFoodOffAction";

   		Vector3 targetLoc = new Vector3(nest.transform.position.x, nest.transform.position.y+8f, 1f);

   		if (isNearTarget(targetLoc) == true){
   			Debug.Log("isNearTarget true");

			// StartCoroutine(dropPickupDelay());

	        ravenDroppingPickup = true;
	   		yield return new WaitForSeconds(2f);
	        ravenDroppingPickup = false;
			hasPickup = false;

			StartCoroutine(getNextAction());

   		} else {
   			Debug.Log("isNearTarget false");

			// if (isGrounded == false){
			// 	while (myrigidbody.velocity.y > -0.3f && targetLoc.y > transform.position.y-0.5f){
	  //  				yield return new WaitForSeconds(0.25f);
			// 	}
			// }

	   		// isFlapping = true;
	   		// animator.SetBool("isFlapping", isFlapping);

	   		// yield return new WaitForSeconds(flyToLocation(targetLoc));

	   		// isFlapping = false;
	   		// animator.SetBool("isFlapping", isFlapping);

   			// flyToLocation(new Vector3(nest.transform.position.x, nest.transform.position.y+3f, 1f));
		
			StartCoroutine(flyToLocationAction(targetLoc));
   		}
		
		// StartCoroutine(getNextAction());

		// bool aboveNest = (transform.position.y > (nest.transform.position.y+3f));

		// //Check near nest
		// if (transform.position.y-1f < (nest.transform.position.y+3f) && transform.position.x-1f < nest.transform.position.x &&
		// 	transform.position.y+1f > (nest.transform.position.y+3f) && transform.position.x+1f > nest.transform.position.x
		// 		&& aboveNest == true){

		// 		StartCoroutine(dropPickupDelay());
		// 		hasPickup = false;

		//    		yield return new WaitForSeconds(2f);

		// } else {

		// 	if (isGrounded == false){
		// 		while (myrigidbody.velocity.y > -0.3f && (nest.transform.position.y+3f) > transform.position.y-0.5f){
	 //   				yield return new WaitForSeconds(0.25f);
		// 		}
		// 	}

	 //   		isFlapping = true;
	 //   		animator.SetBool("isFlapping", isFlapping);

		// 	float forceY = myrigidbody.velocity.y;//UnityEngine.Random.Range(8f, 10f);//17.5f;
		// 	float forceX = 0f;
		// 	float lowerFlapRange = 0.2f;
		// 	float upperFlapRange = 0.6f;

		// 	bool insideColumn = (nest.transform.position.x > transform.position.x-2f && nest.transform.position.x < transform.position.x+2f);

	 //   		if (aboveNest == false || insideColumn == false){
	 //   			forceY += UnityEngine.Random.Range(15f, 20f);

	 //   			upperFlapRange = 0.4f;
	 //   		} else {

	 //   			forceY += 5f;
		// 		lowerFlapRange = 0.5f;

	 //   		}
		   		
	 //   		//if (aboveNest == false && insideColumn == true){
	 //   		if (getObstacleDir(Vector3.up) == true || getObstacleDir(Vector3.down) == true){
	 //   			if (directionIsRight){
	 //   				forceX = 10f;
	 //   			} else {
	 //   				forceX = -10f;
	 //   			}
		//    	} else {

		//    		float dif = nest.transform.position.x - transform.position.x;

		//    		forceX = dif*2f;// / 10f

		//    		//cap it
		//    		if (forceX > 0f){
		//    			if (forceX < 7.5f){
		//    				forceX = 7.5f;
		//    			} else if (forceX > 15f){
		//    				forceX = 15f;
		//    			}
		//    		} else {
		//    			if (forceX > -7.5f){
		//    				forceX = -7.5f;
		//    			} else if (forceX < -15f){
		//    				forceX = -15f;
		//    			}
		//    		}

		//    		if ((forceX > 0.1f && directionIsRight == false) || (forceX < -0.1f && directionIsRight == true)){
		//    			changeDirection();
		//    		}
		//    	}

	 //   		myrigidbody.velocity = new Vector3(myrigidbody.velocity.x, 0f, 0f);
		// 	myrigidbody.AddForce(new Vector2(forceX, forceY), ForceMode2D.Impulse);
	 //   		yield return new WaitForSeconds(UnityEngine.Random.Range(lowerFlapRange, upperFlapRange));

	 //   		isFlapping = false;
	 //   		animator.SetBool("isFlapping", isFlapping);
		// }
   }

}
