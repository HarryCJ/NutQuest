using UnityEngine;
using System.Collections;

public class CameraBehaviourScript : MonoBehaviour {

	GameObject player;
	PlayerBehaviourScript playerBS;
    Vector3 position;
    float baselineY = 0f;

    // Use this for initialization
    void Start () {
		player = GameObject.Find("player");
		playerBS = player.GetComponent<PlayerBehaviourScript>();
        position = transform.position;
        baselineY = player.transform.position.y+3.8f;
    }

	// Update is called once per frame
	void FixedUpdate () {

        position.x = player.transform.position.x;
		if (position.x > 13){
            position.x = 13;
        } else if (position.x < -13){
            position.x = -13;
        }

        if (playerBS.isGrounded == true){
            position.y = baselineY;
        } else {
            if (playerBS.myrigidbody.velocity.y > 0.25f){
                position.y = player.transform.position.y+1.5f;
            } else if (playerBS.myrigidbody.velocity.y < -0.25f){
                position.y = player.transform.position.y-1.5f;
            } else {
                position.y = player.transform.position.y;
            }
        }
        if (position.y < 0.5f){
            position.y = 0.5f;
        }
		transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime*5);//position;

    }

    public void setCameraBaseline(){
        if (player.transform.position.y+3.8f > 0.75f){
            baselineY = player.transform.position.y+1.5f;
        } else {
            baselineY = 0.5f;
        }
        // baselineY = player.transform.position.y+2.8f;
    }

}
