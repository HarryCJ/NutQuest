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

    public override int getNutPoints(){
        return 5;
    }

	public override void nutStart(){
        level = 2;
        colliderType = "polygon";
    }
}
