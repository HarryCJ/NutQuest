using UnityEngine;
using System.Collections;

public class frog_tongue_script : MonoBehaviour {

	BoxCollider2D mycollider;
	FrogBehaviourScript parent;
	// Rigidbody2D myrigidbody;
	Collider2D parentcollider;

	public int thrustTimer = 0;
	public bool withdrawing = false;

	Collider2D pickup;

	float dirMultiplier = 1f;

    GameObject player;
    PlayerBehaviourScript playerBS;
	Rigidbody2D playerRB;

	Vector2 targetThrust;
	float thrustSpeed = 2.5f;

	public bool isThrusting = false;

	SpriteMask parent_mask;
	float rightThrustBoundary;

	// Use this for initialization
	void Start () {

		player = GameObject.Find("player");
		playerBS = player.GetComponent<PlayerBehaviourScript>();
		playerRB = player.GetComponent<Rigidbody2D>();

		parent_mask = transform.parent.GetComponent<SpriteMask>();
		// Debug.Log("parent_mask.size");
		// Debug.Log(parent_mask.size);
		rightThrustBoundary = parent_mask.size.x + 0.2f;

		parent = transform.parent.gameObject.transform.parent.gameObject.GetComponent<FrogBehaviourScript>();
		mycollider = GetComponent<BoxCollider2D>();
		// myrigidbody = GetComponent<Rigidbody2D>();
		parentcollider = parent.GetComponent<Collider2D>();
		Physics2D.IgnoreCollision(mycollider, parentcollider);
		// thrust();
		if (parent.directionIsRight == false){
			dirMultiplier = -1f;
		}
	}

	// Update is called once per frame
	void FixedUpdate () {

		// float posX = transform.position.x;
		// float parentPosX = parent.gameObject.transform.position.x;

		//Get width of tongue
		// float relPosX = (posX-parentPosX) / 10;
		// Debug.Log(relPosX);

		if (isThrusting == true){
			// Debug.Log("thrusting");
			// Debug.Log(transform.position.y);
			// if ( (dirMultiplier == 1f && relPosX < 0.33f) || (dirMultiplier == -1f && relPosX > -0.33f) ){
				// myrigidbody.AddForce(new Vector2(100f * dirMultiplier, 0f), ForceMode2D.Impulse);
			// if (pickup == null){
				transform.localPosition = Vector2.Lerp(transform.localPosition, targetThrust, Time.deltaTime * thrustSpeed); // new Vector2(transform.position.x+(0.15f*dirMultiplier), transform.position.y);
				// Debug.Log("thrust");
				// Debug.Log(transform.position.x);
				// Debug.Log(targetThrust.x);
				if (transform.localPosition.x >= targetThrust.x-0.2f){
					// Debug.Log("reached end");
					withdrawing = true;
					isThrusting = false;
					targetThrust = new Vector2((rightThrustBoundary*-1f)/2f, transform.localPosition.y);
					// targetThrust = new Vector2(targetThrust.x-10f, targetThrust.y);
				}
			// }
			// else {
			// 	// transform.position = new Vector2(transform.position.x+(0.02f*dirMultiplier), transform.position.y);
			// 	withdrawing = true;
			// 	isThrusting = false;
			// 	targetThrust = new Vector2(-0.5f, 0.07f);
			// 	// targetThrust = new Vector2(targetThrust.x-10f, targetThrust.y);
			// }
			// } else {
				// myrigidbody.velocity=Vector3.zero;
				// parent.isLicking = false;
				// withdrawing = true;
				// thrustTimer = 0;
			// }

			// thrustTimer--;
		} else if (withdrawing == true) {
			// Debug.Log("not thrusting");
			// Debug.Log("withdraw");
			// Debug.Log(transform.localPosition.x);

			// if ( (dirMultiplier == 1f && relPosX > 0) || (dirMultiplier == -1f && relPosX < 0) ){
			if (transform.localPosition.x > (rightThrustBoundary*-1f)/3f){
				// Debug.Log("tongue still out");
				// myrigidbody.AddForce(new Vector2(-100f * dirMultiplier, 0f), ForceMode2D.Impulse);
				transform.localPosition = Vector2.Lerp(transform.localPosition, targetThrust, Time.deltaTime * thrustSpeed);
				// transform.position = new Vector2(transform.position.x+(-0.1f*dirMultiplier), transform.position.y);
			} else {
				// Debug.Log("tongue is back");
				// myrigidbody.velocity=Vector3.zero;
				parent.isLicking = false;
				withdrawing = false;
				isThrusting = false;
				parent.tongue_mask.gameObject.SetActive(false);
				parent.canLick = false;
			}
		}

		// transform.position = new Vector2(transform.position.x, parent.transform.position.y);
		if (pickup != null){
			// Debug.Log("move pickup");
			pickup.gameObject.transform.position = new Vector2(transform.position.x, transform.position.y);
			if (transform.localPosition.x <= 0.1f){
				parent.consume(pickup);
				parent.isLicking = false;
				parent.tongue_mask.gameObject.SetActive(false);
				parent.canLick = false;
			}
			// if ( (dirMultiplier == 1f && relPosX < parent.getEatPickupCutoff()) || (dirMultiplier == -1f && relPosX > (parent.getEatPickupCutoff() * -1f)) ){
			// 	parent.consume(pickup);
			// 	// Destroy(pickup.gameObject);
			// 	parent.isLicking = false;
			// 	parent.tongue_mask.gameObject.SetActive(false);
			// 	parent.canLick = false;
			// }
		}

		// if (dirMultiplier == 1 && relPosX < 0){
		// 	relPosX = 0f;
		// } else if (dirMultiplier == -1 && relPosX > 0){
		// 	relPosX = 0f;
		// }
		//
		// //The issue is, the size of the hitbox depends on the position and relative position of the tongue.
		// //the calculations assume it is extending from its starting place, relative 0
		//
		// // mycollider.size = new Vector2(relPosX, 0.03f);
		// transform.position = new Vector2(posX, parent.transform.position.y);
		// if (dirMultiplier == 1){
		// 	mycollider.offset = new Vector2(-0.155f + (relPosX/2), 0f);
		// } else {
		// 	mycollider.offset = new Vector2(0.155f - (relPosX/2), 0f);
		// }
	}

	public void thrust(){
		// Debug.Log("thrust fn");
		// thrustTimer = parent.getThrustTimer();
		if (isThrusting == false){
			isThrusting = true;
			targetThrust = new Vector2(rightThrustBoundary, transform.localPosition.y);
			withdrawing = false;
		}
	}

	public void withdraw(Collider2D other){
		// Debug.Log("withdraw fn");
		if (withdrawing == false){

			pickup = other;
			// Physics2D.IgnoreCollision(other, parentcollider);
			// thrustTimer = 0;
			withdrawing = true;
			// isThrusting = false;
			// targetThrust = new Vector2(-0.5f, 0.07f);
		}
	}

	public void hitPlayer(){

		if (playerBS.isProtected == false){
			GameObject mynut = null;
			if (parent.frogType == "frog"){
				playerBS.getHit(1, parent.directionIsRight);
				mynut = Instantiate(Resources.Load("Prefabs/nut")) as GameObject;
			} if (parent.frogType == "toad"){
				playerBS.getHit(5, parent.directionIsRight);
				mynut = Instantiate(Resources.Load("Prefabs/apple")) as GameObject;
			}
			mynut.transform.position = new Vector2(transform.position.x, transform.position.y);
			Collider2D nutcollider = mynut.GetComponent<Collider2D>();
			NutBehaviourScript nutBS = mynut.GetComponent<NutBehaviourScript>();
			nutBS.isGrowing = false;
			withdraw(nutcollider);
		}

		// playerRB.AddForce(new Vector2(200f, 100f), ForceMode2D.Impulse);
	}
}
