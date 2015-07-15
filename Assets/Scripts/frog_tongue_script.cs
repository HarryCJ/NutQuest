using UnityEngine;
using System.Collections;

public class frog_tongue_script : MonoBehaviour {

	BoxCollider2D mycollider;
	FrogBehaviourScript parent;
	Rigidbody2D myrigidbody;
	Collider2D parentcollider;

	public int thrustTimer = 0;
	public bool withdrawing = false;

	Collider2D pickup;

	float dirMultiplier = 1f;

	// Use this for initialization
	void Start () {

		parent = transform.parent.gameObject.transform.parent.gameObject.GetComponent<FrogBehaviourScript>();
		mycollider = GetComponent<BoxCollider2D>();
		myrigidbody = GetComponent<Rigidbody2D>();
		parentcollider = parent.GetComponent<Collider2D>();
		Physics2D.IgnoreCollision(mycollider, parentcollider);
		// thrust();
		if (parent.directionIsRight == false){
			dirMultiplier = -1f;
		}
	}

	// Update is called once per frame
	void Update () {

		float posX = transform.position.x;
		float parentPosX = parent.gameObject.transform.position.x;

		//thrusting tongue
		if (thrustTimer > 0){
			myrigidbody.AddForce(new Vector2(100f * dirMultiplier, 0f), ForceMode2D.Impulse);
			thrustTimer--;
		}
		//not thrusting tongue
		else if (dirMultiplier == 1 && (withdrawing == true || posX >= parentPosX)){
			withdrawing = true;
			myrigidbody.AddForce(new Vector2(-60f, 0f), ForceMode2D.Impulse);
			if (pickup != null){
				pickup.gameObject.transform.position = new Vector2(posX, transform.position.y);
			}
		} else if (dirMultiplier == -1 && (withdrawing == true || posX <= parentPosX)){
			withdrawing = true;
			myrigidbody.AddForce(new Vector2(60f, 0f), ForceMode2D.Impulse);
			if (pickup != null){
				pickup.gameObject.transform.position = new Vector2(posX, transform.position.y);
			}
		}

		//detect end of lick
		if (dirMultiplier == 1 && posX < parentPosX - 0.1f){
			if (pickup != null){
				Destroy(pickup.gameObject);
			}
			if (withdrawing == true){
				withdrawing = false;
				parent.canLick = false;
				parent.isLicking = false;
			}
		} else if (dirMultiplier == -1 && posX > parentPosX + 0.1f){
			if (pickup != null){
				Destroy(pickup.gameObject);
			}
			if (withdrawing == true){
				withdrawing = false;
				parent.canLick = false;
				parent.isLicking = false;
			}
		}

		// if (thrustTimer == 0 && withdrawing == false){
		// 	withdrawing = true;
		// }

		//max lick distance
		if (dirMultiplier == 1 && posX > parentPosX + 3f){
			posX = parentPosX + 3f;
			thrustTimer = 0;
			withdrawing = true;
		} else if (dirMultiplier == -1 && posX < parentPosX - 3f){
			posX = parentPosX - 3f;
			thrustTimer = 0;
			withdrawing = true;
		}

		//Get width of tongue
		float relPosX = (posX-parentPosX) / 10;
		if (dirMultiplier == 1 && relPosX < 0){
			relPosX = 0f;
		} else if (dirMultiplier == -1 && relPosX > 0){
			relPosX = 0f;
		}

		//The issue is, the size of the hitbox depends on the position and relative position of the tongue.
		//the calculations assume it is extending from its starting place, relative 0

		mycollider.size = new Vector2(relPosX, 0.03f);
		transform.position = new Vector2(posX, parent.transform.position.y);
		if (dirMultiplier == 1){
			mycollider.offset = new Vector2(-0.155f + (relPosX/2), 0f);
		} else {
			mycollider.offset = new Vector2(0.155f - (relPosX/2), 0f);
		}
	}

	public void thrust(){
		thrustTimer = 15;
		withdrawing = false;
	}

	public void withdraw(Collider2D other){
		Debug.Log("withdraw");
		pickup = other;
		Physics2D.IgnoreCollision(other, parentcollider);
		thrustTimer = 0;
		withdrawing = true;
	}
}
