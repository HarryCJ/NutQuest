using UnityEngine;
using System.Collections;

public class AppleBehaviourScript : NutBehaviourScript {

	// // Use this for initialization
	// void Start () {
	//
	// }
	//
	// // Update is called once per frame
	// void FixedUpdate () {
	//
	// }

    public override float getNutPoints(){
        return 5f;
    }

	public override void nutStart(){
        level = 2;
        colliderType = "polygon";
    }
}
