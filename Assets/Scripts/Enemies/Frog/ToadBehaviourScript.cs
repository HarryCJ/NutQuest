using UnityEngine;
using System.Collections;

public class ToadBehaviourScript : Enemy {

	public bool isLicking = false;
	public bool isLickingUp = false;
	public bool isJumping = false;
	public bool isGrounded = true;
	public bool isBirthing = false;

	Animator animator;
	Rigidbody2D myrigidbody;
	Collider2D mycollider;

	Transform toad_tongue_end;
	Transform toad_tongue_middle;

	public bool directionIsRight = true;
	float dirMultiplier = 1f;

	public string currentAction = "waitAction";

	public Transform pickup = null;
    GameObject player;
    PlayerBehaviourScript playerBS;

   	int birthPoints = 0;

	// Use this for initialization
	void Start () {
		lives = 3;
        // enemies = GameObject.Find("enemies");
		player = GameObject.Find("player");
		playerBS = player.GetComponent<PlayerBehaviourScript>();
		myrigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        mycollider = GetComponent<Collider2D>();

        foreach (Transform child in transform){
            if (child.name == "toad_tongue_end"){
                 toad_tongue_end = child;
            } else if (child.name == "toad_tongue_middle"){
                 toad_tongue_middle = child;
            }
        }
		StartCoroutine(waitAction());
	}
	
	// Update is called once per frame
	void Update () {
	
        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("isJumping", isJumping);
        animator.SetBool("isDead", isDead);
        animator.SetBool("isLicking", isLicking);
        animator.SetBool("isLickingUp", isLickingUp);
        animator.SetBool("isBirthing", isBirthing);
	}

	bool lickValid(){

		RaycastHit2D[] hits = null;

		if (dirMultiplier > 0f){
			hits = Physics2D.BoxCastAll(transform.position, new Vector2(0.25f, 0.25f), 0f, Vector3.right, 4f);
		} else {
			hits = Physics2D.BoxCastAll(transform.position, new Vector2(0.25f, 0.25f), 0f, Vector3.left, 4f);
		}

		for (int i = 0; i < hits.Length; i++) {
            RaycastHit2D hit = hits[i];

			if (hit.collider.name != transform.name && (hit.collider.name == "player" || hit.collider.tag.Contains("pickup"))){

				return true;
				// foundTarget = true;
				// StartCoroutine(lickAction());
				// break;
			}
		}

		return false;

	}

	bool lickUpValid(){

		RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, new Vector2(0.5f, 0.25f), 0f, Vector3.up, 4f);

		for (int i = 0; i < hits.Length; i++) {
            RaycastHit2D hit = hits[i];

			if (hit.collider.name != transform.name && (hit.collider.name == "player" || hit.collider.tag.Contains("pickup"))){ // || hit.collider.tag.Contains("enemy")
				return true;
			}
		}

		return false;
	}

	IEnumerator getNextAction(){
		Debug.Log("getNextAction");

		while (isGrounded == false && isDead == false){
			yield return new WaitForSeconds(0.1f);
		}

		if (isDead == false){

			isJumping = false;

			bool foundTarget = false;

			if (((directionIsRight == false && transform.position.x < -10f) || (directionIsRight == true && transform.position.x > 10f)) &&	UnityEngine.Random.Range(0, 2) == 0){

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


			// foundTarget = true;
			// StartCoroutine(lickUpAction());

			if (birthPoints > 0){
				foundTarget = true;
				StartCoroutine(birthAction());
			}

			if (foundTarget == false){
				if (lickValid() == true){
					foundTarget = true;
					StartCoroutine(lickAction());
				}
			}

			if (foundTarget == false){

				if (lickUpValid() == true){
					foundTarget = true;
					StartCoroutine(lickUpAction());
				}
			}

			if (foundTarget == false){

				switch (currentAction) {
					case "waitAction":
						StartCoroutine(jumpAction());
						break;

					case "jumpAction":
						StartCoroutine(waitAction());
						break;

					case "lickAction":
						StartCoroutine(waitAction());
						break;

					case "birthAction":
						StartCoroutine(waitAction());
						break;

					default:
						StartCoroutine(waitAction());
						break;

				}
			}
		}
	}

   IEnumerator waitAction(){
   		currentAction = "waitAction";

   		bool foundTarget = false;

   		for (int x = 0; x < UnityEngine.Random.Range(10, 20); x++){

   			yield return new WaitForSeconds(0.1f);

			if (foundTarget == false){
				if (lickValid() == true){
					foundTarget = true;
					StartCoroutine(lickAction());
					break;
				}
			}

			if (foundTarget == false){

				if (lickUpValid() == true){
					foundTarget = true;
					StartCoroutine(lickUpAction());
					break;
				}
			}
   		}
		
		if (foundTarget == false){
			StartCoroutine(getNextAction());
		}
		// StartCoroutine(jumpAction());
   }

   IEnumerator birthAction(){
   		currentAction = "birthAction";
   		isBirthing = true;

   		for (int x = 0; x < birthPoints; x++){	
			yield return new WaitForSeconds(UnityEngine.Random.Range(0.5f, 1.5f));

			// if (isBirthing == true){
				
			// }

			if (isDead == false){
				GameObject tadpole = null;
				tadpole = Instantiate(Resources.Load("Prefabs/tadpole")) as GameObject;
				tadpole.transform.position = new Vector2(transform.position.x, transform.position.y);
				tadpole.transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(UnityEngine.Random.Range(-80f, -150f) * dirMultiplier, UnityEngine.Random.Range(60f, 100f)), ForceMode2D.Impulse);


				myrigidbody.AddForce(new Vector2(50f * dirMultiplier, 20f), ForceMode2D.Impulse);
			}
   		}

		birthPoints = 0;
   		isBirthing = false;
		StartCoroutine(getNextAction());
   }

   IEnumerator jumpAction(){

   		currentAction = "jumpAction";
   		isJumping = true;
   		for (int jumpingTimer = 0; jumpingTimer < 10; jumpingTimer++){
			if (isDead == false){
	   			Vector3 velocity = new Vector3();
				velocity.x = (UnityEngine.Random.Range(10f, 15f) * dirMultiplier);
				if (jumpingTimer > 8)
				{
					velocity.y = 20f;
				}
				else
				{
					velocity.y = 30f;
				}
				myrigidbody.AddForce(velocity, ForceMode2D.Impulse);
			}
			yield return new WaitForSeconds(0.01f);
   		}

		StartCoroutine(getNextAction());
   }

   IEnumerator lickAction(){
   		currentAction = "lickAction";
   		isLicking = true;

	   	toad_tongue_end.transform.localPosition = new Vector3(0f, 0f, 0f);

   		toad_tongue_end.transform.localPosition = new Vector3(0f, 0f, 0f);
   		toad_tongue_end.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
   		toad_tongue_middle.transform.localPosition = new Vector3(0f, -0.005f, 0f);

   		toad_tongue_end.gameObject.SetActive(true);
   		toad_tongue_middle.gameObject.SetActive(true);

   		Vector3 tongueTarget = new Vector3(toad_tongue_end.transform.localPosition.x+0.5f, toad_tongue_end.transform.localPosition.y, 0f);

   		while (tongueTarget.x-0.05f > toad_tongue_end.transform.localPosition.x){

			if (isDead == false){
	   			toad_tongue_end.transform.localPosition = Vector3.Lerp(toad_tongue_end.transform.localPosition, tongueTarget, Time.deltaTime*5f);
	   			toad_tongue_middle.localScale = new Vector3(toad_tongue_end.transform.localPosition.x*100f, 1f, 0f);
				yield return new WaitForSeconds(0.01f);
			}

			if (pickup != null){
				break;
			}
   		}

   		while (0.05f < toad_tongue_end.transform.localPosition.x){

			if (isDead == false){
	   			toad_tongue_end.transform.localPosition = Vector3.Lerp(toad_tongue_end.transform.localPosition, new Vector3(0f, 0f, 0f), Time.deltaTime*5f);
	   			toad_tongue_middle.localScale = new Vector3(toad_tongue_end.transform.localPosition.x*100f, 1f, 0f);

				if (pickup != null){
					pickup.position = toad_tongue_end.transform.position;
				}
				yield return new WaitForSeconds(0.01f);
			}
   		}
			
		if (isDead == false){

			if (pickup != null){
				birthPoints += pickup.gameObject.GetComponent<Pickup>().getNutPoints();
				Destroy(pickup.gameObject);
				pickup = null;
			}

	   		toad_tongue_end.transform.localPosition = new Vector3(0f, 0f, 0f);
	   		isLicking = false;

	   		toad_tongue_end.gameObject.SetActive(false);
	   		toad_tongue_middle.gameObject.SetActive(false);
		}

		StartCoroutine(getNextAction());
		// StartCoroutine(waitAction());
   }

   IEnumerator lickUpAction(){
   		currentAction = "lickUpAction";
   		isLickingUp = true;

   		yield return new WaitForSeconds(UnityEngine.Random.Range(0.5f, 1f));

   		if (isDead == false){

		   	toad_tongue_end.transform.localPosition = new Vector3(0f, -0.05f, 0f);

	   		toad_tongue_end.transform.localPosition = new Vector3(0.035f, 0f, 0f);
	   		toad_tongue_end.transform.localEulerAngles = new Vector3(0f, 0f, 90f);
	   		toad_tongue_middle.transform.localPosition = new Vector3(0.03f, 0f, 0f);
	   		
	   		toad_tongue_end.gameObject.SetActive(true);
	   		toad_tongue_middle.gameObject.SetActive(true);

	   		Vector3 tongueTarget = new Vector3(toad_tongue_end.transform.localPosition.x, toad_tongue_end.transform.localPosition.y+0.5f, 0f);


	   		while (tongueTarget.y-0.05f > toad_tongue_end.transform.localPosition.y){

				if (isDead == false){
		   			toad_tongue_end.transform.localPosition = Vector3.Lerp(toad_tongue_end.transform.localPosition, tongueTarget, Time.deltaTime*5f);
		   			toad_tongue_middle.localScale = new Vector3(1f, toad_tongue_end.transform.localPosition.y*100f, 0f);
					yield return new WaitForSeconds(0.01f);
				}

				if (pickup != null){
					break;
				}
	   		}

	   		while (0f < toad_tongue_end.transform.localPosition.y){

				if (isDead == false){
		   			toad_tongue_end.transform.localPosition = Vector3.Lerp(toad_tongue_end.transform.localPosition, new Vector3(0.035f, -0.05f, 0f), Time.deltaTime*5f);
		   			toad_tongue_middle.localScale = new Vector3(1f, toad_tongue_end.transform.localPosition.y*100f, 0f);

					if (pickup != null){
						pickup.position = toad_tongue_end.transform.position;
					}
					yield return new WaitForSeconds(0.01f);
				}
	   		}
   		}
			
		if (isDead == false){

			if (pickup != null){
				birthPoints += pickup.gameObject.GetComponent<Pickup>().getNutPoints();
				Destroy(pickup.gameObject);
				pickup = null;
			}

	   		toad_tongue_end.transform.localPosition = new Vector3(0f, 0f, 0f);
	   		isLickingUp = false;

	   		toad_tongue_end.gameObject.SetActive(false);
	   		toad_tongue_middle.gameObject.SetActive(false);
		}

		StartCoroutine(getNextAction());
		// StartCoroutine(waitAction());
   }

   public void setLickItem(Transform p){
   		Debug.Log("setLickItem");
   		if (pickup == null){
   			pickup = p;
   			Physics2D.IgnoreCollision(mycollider, p.gameObject.GetComponent<Collider2D>());
   		}
   }

	public override void tryKill(){
		Debug.Log("tryKill");

		if (isDead == false && isProtected == false){

			lives--;
			if (lives <= 0){
		        isDead = true;
		        isBirthing = false;
		        isJumping = false;
		        isLicking = false;
		        // mycollider.size = new Vector2(0.12f, 0.04f);
		        // mycollider.offset = new Vector2(0f, -0.02f);

				animator.SetBool("isDead", isDead);
		        StartCoroutine(dieDelay());
				StartCoroutine(customBlink());

		        // Destroy(gameObject);
			} else {
				StartCoroutine(protect());
			}
		}
    }

	public void hitPlayer(){

		if (playerBS.isProtected == false){
			GameObject mynut = null;

			playerBS.getHit(5, directionIsRight);
			// mynut = Instantiate(Resources.Load("Prefabs/apple")) as GameObject;
			mynut = Instantiate(Resources.Load("Prefabs/nut")) as GameObject;

			mynut.transform.position = new Vector2(transform.position.x, transform.position.y);
			NutBehaviourScript nutBS = mynut.GetComponent<NutBehaviourScript>();
			nutBS.isGrowing = false;

			pickup = mynut.transform;
		}

		// playerRB.AddForce(new Vector2(200f, 100f), ForceMode2D.Impulse);
	}

	public IEnumerator customBlink(){

		SpriteRenderer sr = GetComponent<SpriteRenderer>();
		SpriteRenderer toad_tongue_end_sr = toad_tongue_end.GetComponent<SpriteRenderer>();
		SpriteRenderer toad_tongue_middle_sr = toad_tongue_middle.GetComponent<SpriteRenderer>();

		for (int i = 0; i < 12; i++) {

			sr.enabled = false;
			toad_tongue_end_sr.enabled = false;
			toad_tongue_middle_sr.enabled = false;
	        yield return new WaitForSeconds(0.2f);
			sr.enabled = true;
			toad_tongue_end_sr.enabled = true;
			toad_tongue_middle_sr.enabled = true;
	        yield return new WaitForSeconds(0.2f);
		}

    }


}
