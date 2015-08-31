using UnityEngine;
using System.Collections;

public class CameraBehaviourScript : MonoBehaviour {

	GameObject player;
	PlayerBehaviourScript playerBS;
    Vector3 position;

    // Use this for initialization
    void Start () {
		player = GameObject.Find("player");
		playerBS = player.GetComponent<PlayerBehaviourScript>();
        position = transform.position;
    }

	// Update is called once per frame
	void FixedUpdate () {

        position.x = player.transform.position.x;
		if (position.x > 15){
            position.x = 15;
        } else if (position.x < -15){
            position.x = -15;
        }

		transform.position = position;

    }

}
