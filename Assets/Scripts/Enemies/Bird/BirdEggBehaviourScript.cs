using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BirdEggBehaviourScript : Enemy {

	public bool isWiggling = false;
	public bool isBiting = false;
	Rigidbody2D myrigidbody;
	Animator animator;
	// Collider2D mycollider;
	int wiggleCount = 0;
	// WorldScript environment_ws;
	// GameObject environment;
	public bool canBite = false;


	public GameObject nest = null;
	bool hasHatched = false;
	public bool isGrounded = true;

	// public Transform biteObject = null;
    PlayerBehaviourScript playerBS;
    GameObject player;

    Transform bird_egg_top_collider;
	bird_egg_top_collider_script bird_egg_top_collider_s;
	// public System.Collections.Generic.List<GameObject> bird_egg_top_collider_s.biteTargets = new System.Collections.Generic.List<GameObject>();

	// Use this for initialization
	void Start () {
		player = GameObject.Find("player");
		playerBS = player.GetComponent<PlayerBehaviourScript>();
		myrigidbody = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		// mycollider = GetComponent<Collider2D>();
		StartCoroutine(hatchAction());
		// environment = GameObject.Find("environment");
		// environment_ws = environment.GetComponent<WorldScript>();

        foreach (Transform child in transform){
            if (child.name == "bird_egg_top_collider"){
                 bird_egg_top_collider = child;
                 bird_egg_top_collider_s = child.GetComponent<bird_egg_top_collider_script>();
            }
        }
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
	}

  //  IEnumerator enableCollider(){

		// yield return new WaitForSeconds(0.25f);
		// mycollider.enabled = true;
		// StartCoroutine(wiggleAction());

  //  }

   IEnumerator hatchAction(){
	
		yield return new WaitForSeconds(UnityEngine.Random.Range(5f, 10f));
		hasHatched = true;
   		transform.eulerAngles = new Vector3(0, 0, 0f);
   		myrigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
		animator.SetBool("hasHatched", hasHatched);
		StartCoroutine(getNextAction());

   }

   IEnumerator getNextAction(){

		yield return new WaitForSeconds(0.1f);

		if (isDead == false){

			if (canBite == true){

				StartCoroutine(biteAction());

			} else {

				StartCoroutine(wiggleAction());

			}
		}

   }

   IEnumerator wiggleAction(){
   		isWiggling = true;
		animator.SetBool("isWiggling", isWiggling);

   		wiggleCount += 1;
   		float velX = UnityEngine.Random.Range(0f, 15f);

   		// if (vel.x > 0f){
   		// 	transform.localScale = new Vector3(10f, 10f, 1f);
   		// } else {
   		// 	transform.localScale = new Vector3(-10f, 10f, 1f);
   		// }
   		if (nest != null){
   			if (nest.transform.position.x < transform.position.x){
   				velX = velX * -1f;
   			}
   		}

   		Vector3 vel = new Vector3(velX, UnityEngine.Random.Range(10f, 30f), 0f);

		myrigidbody.AddForce(vel, ForceMode2D.Impulse);

		yield return new WaitForSeconds(UnityEngine.Random.Range(0.1f, 0.5f));
   		isWiggling = false;
		animator.SetBool("isWiggling", isWiggling);
		StartCoroutine(getNextAction());

		// if (isDead == false){

		// 	if (canBite == true){

		// 		StartCoroutine(biteAction());

		// 	} else {

		// 		yield return new WaitForSeconds(UnityEngine.Random.Range(0.1f, 0.5f));

		// 		// if (wiggleCount < 40){
		// 		StartCoroutine(wiggleAction());
		// 		// } else {
		// 		// 	StartCoroutine(transformIntoFrog());
		// 		// }
		// 	}
		// }
   }

   IEnumerator biteAction(){
   		isBiting = true;
		animator.SetBool("isBiting", isBiting);

		myrigidbody.AddForce(new Vector3(0f, UnityEngine.Random.Range(60f, 90f), 0f), ForceMode2D.Impulse);

		// if (biteObject != null){
		System.Collections.Generic.List<GameObject> biteTargets = bird_egg_top_collider_s.getBiteTargets();

		if (biteTargets.Count > 0){

			foreach (GameObject bt in biteTargets) // Loop through List with foreach.
			{
				if (bt == null){
					// biteTargets.Remove(bt);
				} else {

					if (bt.tag.Contains("pickup")){

						Destroy(bt.gameObject);
						// biteTargets.Remove(bt);
						// bt = null;

					} else if (bt.name == "player"){

						bool hitDirRight = true;
						if (playerBS.directionIsRight == true){
							hitDirRight = false;
						}
						playerBS.getHit(1, hitDirRight);
					}
				}
			}

		}

		//add grounded check
		yield return new WaitForSeconds(0.5f);
		while(isGrounded == false){
			yield return new WaitForSeconds(0.025f);
		}
   		isBiting = false;
		animator.SetBool("isBiting", isBiting);
		StartCoroutine(getNextAction());
   }

  //  IEnumerator transformIntoFrog(){

		// animator.SetBool("isTransforming", true);
		// yield return new WaitForSeconds(UnityEngine.Random.Range(1f, 2f));

		// if (isDead == false){

	 //   		if (transform.localScale.x > 0f){
		// 		environment_ws.addFrog(transform.position, true);
		// 	} else {
		// 		environment_ws.addFrog(transform.position, true);
		// 	}
		// 	Destroy(gameObject);
		// }
  //  }
	public override void tryKill(){
		Debug.Log("tryKill");

		lives--;
		if (isDead == false && isProtected == false && lives <= 0){
	        isDead = true;
	        // mycollider.size = new Vector2(0.12f, 0.04f);
	        // mycollider.offset = new Vector2(0f, -0.02f);

			animator.SetBool("isDead", isDead);
	        StartCoroutine(dieDelay());
			StartCoroutine(blink());

	        // Destroy(gameObject);
		} else {
			StartCoroutine(protect());
		}
    }
}
