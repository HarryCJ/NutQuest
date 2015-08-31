using UnityEngine;
using System.Collections;

public class ToadBehaviourScript : FrogBehaviourScript {

	public override int getThrustTimer(){
		return 58;
    }
	public override float getEatPickupCutoff(){
		return 0.2f;
    }

	PolygonCollider2D current_collider;

	PolygonCollider2D dead_collider;


	public override void frogStart(){
		Debug.Log("toad hello");
		lives = 2;

		current_collider = GetComponent<PolygonCollider2D>();

		foreach (Transform child in transform){

			if (child.name == "dead_collider"){
				dead_collider = child.GetComponent<PolygonCollider2D>();
				//  tongue_mask = child;
				//  foreach (Transform child2 in tongue_mask){
				// 	 tongue = child2.GetComponent<frog_tongue_script>();
				// 	 foreach (Transform child3 in child2){
				// 		tongue_end = child3.GetComponent<frog_tongue_end_script>();
				// 		tongue_end_collider = child3.GetComponent<Collider2D>();
				// 	}
				//  }
			}
		}


    }

	public override void frogFixedUpdate(){
		if (isDead == true){
			current_collider.points = dead_collider.points;
			current_collider.offset = dead_collider.offset;
		}
	}

	// void Start(){
	//
	//
	// 	// base.Start();
	// 	Debug.Log("toad hello");
	// }
}
