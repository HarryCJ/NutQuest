using UnityEngine;
using System.Collections;

public class bird_nest_inside_trigger_script : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Set up a list to keep track of targets
	public System.Collections.Generic.List<GameObject> targets = new System.Collections.Generic.List<GameObject>();

	// If a new enemy enters the trigger, add it to the list of targets
	void OnTriggerEnter2D(Collider2D other){

		if (other.tag.Contains("phys")){

			GameObject go = other.gameObject;
			if(!targets.Contains(go)){

				targets.Add(go);
			}
	    }

	}

	// When an enemy exits the trigger, remove it from the list
	void OnTriggerExit2D(Collider2D other){
		if (other.tag.Contains("phys")){
			GameObject go = other.gameObject;
			if(targets.Contains(go)){
				targets.Remove(go);

				targets.RemoveAll(item => item == null);
			}
	    }
	}
}
