using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class bird_egg_top_collider_script : MonoBehaviour {

	BirdEggBehaviourScript parent;

	// Use this for initialization
	void Start () {
		parent = transform.parent.gameObject.GetComponent<BirdEggBehaviourScript>();
	}

	// Update is called once per frame
	void FixedUpdate () {

	}

	// Set up a list to keep track of biteTargets
	public System.Collections.Generic.List<GameObject> biteTargets = new System.Collections.Generic.List<GameObject>();

	public System.Collections.Generic.List<GameObject> getBiteTargets(){
		biteTargets.RemoveAll(item => item == null);
		if (biteTargets.Count == 0){
			parent.canBite = false;
		}
		return biteTargets.GetRange(0, biteTargets.Count);
	}

	// If a new enemy enters the trigger, add it to the list of biteTargets
	void OnTriggerEnter2D(Collider2D other){

		if (other.tag.Contains("pickup") || other.name == "player"){

			GameObject go = other.gameObject;
			if(!biteTargets.Contains(go)){

				biteTargets.Add(go);
		        parent.canBite = true;
		  //       parent.biteObject = other.transform;

		  //       if(other.tag.Contains("enemy")){

				// 	Enemy myEnemy = go.GetComponent<Enemy>();
				// 	if (myEnemy.isBouncy == true){

				// 		parent.tryBoost(myEnemy.jumpLevels[parent.environment_ws.upgrades[myEnemy.enemyType+"_boost"]], myEnemy.addToMultiplier);
				// 	}
		  //       } else if (other.tag.Contains("env")){

				// 	parent.resetBoostCombo();
				// }
			}
	    }

	}

	// When an enemy exits the trigger, remove it from the list
	void OnTriggerExit2D(Collider2D other){
		if (other.tag.Contains("pickup") || other.name == "player"){
			GameObject go = other.gameObject;
			if(biteTargets.Contains(go)){
				biteTargets.Remove(go);

				biteTargets.RemoveAll(item => item == null);
				if (biteTargets.Count == 0){
					parent.canBite = false;
				}
			}
	    }
	}

  //   void OnTriggerEnter2D(Collider2D other)
  //   {
  //   	// Debug.Log(other.name);
		// if (other.tag.Contains("pickup") || other.name == "player"){
	 //        parent.canBite = true;
	 //        parent.biteObject = other.transform;
		// }
  //   }
  //   void OnTriggerStay2D(Collider2D other)
  //   {
		// // if (other.tag.Contains("pickup") || other.name == "player"){
	 // //        parent.canBite = true;
	 // //        parent.biteObject = other.transform;
		// // }
  //   }
  //   void OnTriggerExit2D(Collider2D other)
  //   {
		// if (other.tag.Contains("pickup") || other.name == "player"){
	 //        parent.canBite = false;
		// }
  //   }
}
