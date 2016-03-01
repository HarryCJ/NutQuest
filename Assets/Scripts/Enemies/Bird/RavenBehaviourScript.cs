using UnityEngine;
using System.Collections;

public class RavenBehaviourScript : Enemy {

	public bool isPecking = false;
	public bool isGrounded = true;
	// public bool isBirthing = false;
	public bool isFlapping = false;

	Animator animator;
	Rigidbody2D myrigidbody;
	Collider2D mycollider;

	public bool directionIsRight = true;
	float dirMultiplier = 1f;

	public string currentAction = "waitAction";

	public Transform pickup = null;
    GameObject player;
    PlayerBehaviourScript playerBS;
    public Transform nest = null;
    Vector3 nestSearchLoc = new Vector3();
    bool hasNestSearchLoc = false;

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
        animator = GetComponent<Animator>();
        mycollider = GetComponent<Collider2D>();

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
        // animator.SetBool("isBirthing", isBirthing);
	
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
		Debug.Log("getNextAction");
		yield return new WaitForSeconds(0.05f);

		// while (isGrounded == false && isDead == false){
		// 	yield return new WaitForSeconds(0.1f);
		// }

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
				StartCoroutine(makeNestAction());

			} else {

				StartCoroutine(getNextAction());

				if (pickup == null){

					//Search for food

				} else {

					//Return food to nest

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

   IEnumerator makeNestAction(){
   		currentAction = "makeNestAction";

   		if (hasNestSearchLoc == false){
   			//Get Nest location
   			nestSearchLoc = new Vector3(-4.95f, 4.18f);
   			hasNestSearchLoc = true;
   		} else {

   			//Check near nest


   		}


		if (isGrounded == false){
			while (myrigidbody.velocity.y > -0.3f && nestSearchLoc.y > transform.position.y){
   				yield return new WaitForSeconds(0.25f);
			}
		}

   		isFlapping = true;
   		animator.SetBool("isFlapping", isFlapping);

		float forceY = UnityEngine.Random.Range(15f, 16f);//17.5f;
		float forceX = 0f;

   		if (nestSearchLoc.y > transform.position.y+0.5f){
   			forceY += 5f;
   		}
   		// else if (nestSearchLoc.y > transform.position.y){
   		// 	forceY += 2.5f;
   		// } 

   		float dif = 0f;
   		if (nestSearchLoc.x > transform.position.x){
   			dif = transform.position.x - nestSearchLoc.x;
   		} else {
   			dif = nestSearchLoc.x - transform.position.x;
   		}
   		Debug.Log(dif);
   		forceX = dif;// / 10f;

   		if ((forceX > 0.0f && directionIsRight == false) || (forceX < -0.0f && directionIsRight == true)){
   			changeDirection();
   		}

   		myrigidbody.velocity = new Vector3(myrigidbody.velocity.x, 0f, 0f);
		myrigidbody.AddForce(new Vector2(forceX, forceY), ForceMode2D.Impulse);
   		yield return new WaitForSeconds(UnityEngine.Random.Range(0.5f, 0.8f));

   		isFlapping = false;
   		animator.SetBool("isFlapping", isFlapping);
		
		StartCoroutine(getNextAction());
   }

   IEnumerator findFoodAction(){
   		currentAction = "findFoodAction";

   		yield return new WaitForSeconds(0.1f);
		
		StartCoroutine(getNextAction());
   }

   IEnumerator goToNestAction(){
   		currentAction = "goToNestAction";

   		yield return new WaitForSeconds(0.1f);
		
		StartCoroutine(getNextAction());
   }

}
