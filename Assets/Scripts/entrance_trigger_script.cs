using UnityEngine;
using System.Collections;

public class entrance_trigger_script : MonoBehaviour {

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
		if (other.name == "player"){
			// ws.playerEnterEntrance();
			ws.inTreeEntrance = true;
			ws.calculateInTree();
		}
    }
    void OnTriggerStay2D(Collider2D other)
    {
    }
    void OnTriggerExit2D(Collider2D other)
    {
		if (other.name == "player"){
			// ws.playerLeaveEntrance();
			ws.inTreeEntrance = false;
			ws.calculateInTree();
		}
    }
}
