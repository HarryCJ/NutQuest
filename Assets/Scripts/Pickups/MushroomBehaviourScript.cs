using UnityEngine;
using System.Collections;
using System;

public class MushroomBehaviourScript : Pickup {

    Rigidbody2D myrigidbody;
    public bool isGrowing = true;
    public int growStage = 1;
    SpriteRenderer sr;
    Sprite[] sprites;
    int growTimer = 300;
    int clockPoints = 1;
        

	// public virtual void nutStart(){
	// 	// Debug.Log("ENEMY DIES!!!");
	// }

    // Use this for initialization
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        myrigidbody = GetComponent<Rigidbody2D>();
        sprites = Resources.LoadAll<Sprite>(@"sprites");

        // if (isGrowing == true){
            transform.localScale = new Vector2(5f, 5f);
            StartCoroutine(grow());
    		// nutStart();
        // } 
        // else {
        //     myrigidbody.isKinematic = false;
        // }
        type = "time";
    }

	// Update is called once per frame
	void FixedUpdate () {
        // if (isGrowing){
        //     if (growStage != 4){
        //         transform.position = new Vector3(transform.position.x, transform.position.y+0.000625f, transform.position.z);
        //         transform.localScale = new Vector2(transform.localScale.x+0.025f,transform.localScale.x+0.025f);
        //         if (growStage == 1 && transform.localScale.x > 12f){
        //             growSprite();
        //         } else if (growStage == 2 && transform.localScale.x > 10f){
        //             growSprite();
        //         } else if (growStage == 3 && transform.localScale.x > 10f){
        //             growSprite();
        //         }
                
        //     } else {
        //         transform.localScale = new Vector2(10f, 10f);
        //         isGrowing = false;
        //     }
        // }
	}

    void growSprite(){
        growStage++;
        for (int x = 0; x < sprites.Length; x++) {
            if (sprites[x].name == "mushroom_"+growStage){
                sr.sprite = sprites[x];
            }
        }
        growTimer = 80;
        transform.localScale = new Vector2(10f, 10f);
        transform.position = new Vector3(transform.position.x, transform.position.y+0.1f, transform.position.z);
        // switch(growStage){

        //     case 1:
        //         transform.localScale = new Vector2(7f, 7f);
        //         break;

        //     case 2:
        //         transform.localScale = new Vector2(5f, 5f);
        //         break;

        //     case 3:
        //         transform.localScale = new Vector2(5f, 5f);
        //         break;
        // }
    }

    IEnumerator grow(){

        while (isGrowing == true){

            if (growTimer > 0){
                if (growStage == 1){
                    // transform.position = new Vector3(transform.position.x, transform.position.y+0.0015f, transform.position.z);
                    if (transform.localScale.x < 10f){
                        transform.localScale = new Vector2(transform.localScale.x+0.03f,transform.localScale.x+0.03f);
                    }
                } else if (growStage == 2){
                    clockPoints = 2;
                    // transform.position = new Vector3(transform.position.x, transform.position.y+0.0015f, transform.position.z);
                    // transform.localScale = new Vector2(transform.localScale.x+0.025f,transform.localScale.x+0.025f);
                } else if (growStage == 3){
                    clockPoints = 3;
                    // transform.position = new Vector3(transform.position.x, transform.position.y+0.0015f, transform.position.z);
                    // transform.localScale = new Vector2(transform.localScale.x+0.025f,transform.localScale.x+0.025f);
                } else {
                    clockPoints = 4;
                    isGrowing = false;
                }
                growTimer--;
            } else {
                growSprite();
            }
            
            yield return new WaitForSeconds(0.01f);
        }

        // float delay = UnityEngine.Random.Range(8f, 20f);
        // yield return new WaitForSeconds(delay);
        isGrowing = false;
        // myrigidbody.isKinematic = false;
        transform.localScale = new Vector2(10f, 10f);
        
        yield return new WaitForSeconds(3f);

        for (int i = 0; i < 12; i++) {
            sr.enabled = false;
            yield return new WaitForSeconds(0.2f);
            sr.enabled = true;
            yield return new WaitForSeconds(0.2f);
        }
        Destroy(gameObject);
    }

    public override float getNutPoints(){
        return clockPoints;
    }

}
