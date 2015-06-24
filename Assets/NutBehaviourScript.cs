using UnityEngine;
using System.Collections;
using System;

public class NutBehaviourScript : MonoBehaviour {
    
    Sprite[] mySprites;
    string[] spriteNames;

    // Use this for initialization
    void Start()
    {
        mySprites = Resources.LoadAll<Sprite>("sprites");
        spriteNames = new string[mySprites.Length];
        for (int ii = 0; ii < spriteNames.Length; ii++)
        {
            spriteNames[ii] = mySprites[ii].name;
        }
        GetComponent<SpriteRenderer>().sprite = mySprites[Array.IndexOf(spriteNames, "nut")];

    }
	
	// Update is called once per frame
	void Update () {

        GetComponent<SpriteRenderer>().sortingOrder = 20000 - ((int)(transform.position.y*10));
	}
}
