
using UnityEngine;
using System.Collections;

public class tree_trigger_script : MonoBehaviour {

	GameObject environment;
	WorldScript ws;

	// Use this for initialization
	void Start () {
        environment = GameObject.Find("environment");
		ws = environment.GetComponent<WorldScript>();
	}

	// Update is called once per frame
	void FixedUpdate () {

	}

    void OnTriggerEnter2D(Collider2D other)
    {
    	// Debug.Log("OnTriggerEnter2D");
		if (other.name == "player"){
			// ws.playerEnterEntrance();
			ws.inTree = true;
			ws.calculateInTree();
		}
    }
    void OnTriggerStay2D(Collider2D other)
    {
    }
    void OnTriggerExit2D(Collider2D other)
    {
    	// Debug.Log("OnTriggerStay2D");
		if (other.name == "player"){
			// ws.playerLeaveEntrance();
			ws.inTree = false;
			ws.calculateInTree();
		}
    }
}
