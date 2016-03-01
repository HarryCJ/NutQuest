using UnityEngine;
using System.Collections;

public class TadpoleBehaviourScript : Enemy {

	// public bool isWriggling = true;
	Rigidbody2D myrigidbody;
	Animator animator;
	Collider2D mycollider;
	int wiggleCount = 0;
	WorldScript environment_ws;
	GameObject environment;

	// Use this for initialization
	void Start () {
		myrigidbody = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		mycollider = GetComponent<Collider2D>();
		StartCoroutine(enableCollider());
		environment = GameObject.Find("environment");
		environment_ws = environment.GetComponent<WorldScript>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
	}

   IEnumerator enableCollider(){

		yield return new WaitForSeconds(0.25f);
		mycollider.enabled = true;
		StartCoroutine(wiggle());

   }

   IEnumerator wiggle(){

   		wiggleCount += 1;
   		Vector3 vel = new Vector3(UnityEngine.Random.Range(-40f, 40f), UnityEngine.Random.Range(10f, 50f), 0f);

   		if (vel.x > 0f){
   			transform.localScale = new Vector3(10f, 10f, 1f);
   		} else {
   			transform.localScale = new Vector3(-10f, 10f, 1f);
   		}

		myrigidbody.AddForce(vel, ForceMode2D.Impulse);

		if (isDead == false){
			yield return new WaitForSeconds(UnityEngine.Random.Range(0.1f, 0.5f));

			if (wiggleCount < 40){
				StartCoroutine(wiggle());
			} else {
				StartCoroutine(transformIntoFrog());
			}
		}
   }

   IEnumerator transformIntoFrog(){

		animator.SetBool("isTransforming", true);
		yield return new WaitForSeconds(UnityEngine.Random.Range(1f, 2f));

		if (isDead == false){

	   		if (transform.localScale.x > 0f){
				environment_ws.addFrog(transform.position, true);
			} else {
				environment_ws.addFrog(transform.position, true);
			}
			Destroy(gameObject);
		}
   }
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
