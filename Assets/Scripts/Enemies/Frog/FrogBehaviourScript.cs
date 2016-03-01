using UnityEngine;
using System.Collections;
using System;

public class FrogBehaviourScript : Enemy
{

	public Vector3 velocity = new Vector3();
    public bool isGrounded = false;

    public bool directionIsRight = true;

    public bool isJumping = false;
    public int jumpingTimer = -1;
    int lastJump = 0;
	System.Random random;
    Animator animator;

	int jumpDelay = 0;

	public bool isLicking = false;
	public bool canLick = false;
	float dirMultiplier = 1f;

    Collider2D mycollider;
    Rigidbody2D myrigidbody;

	public Transform tongue_mask;

	frog_tongue_script tongue;
	frog_tongue_end_script tongue_end;
	frog_tongue_area_collider_script tongue_area;
	frog_top_collider_script frog_top_collider_s;
	frog_bottom_collider_script frog_bottom_collider_s;

	Collider2D tongue_end_collider;
	Collider2D tongue_area_collider;
	Collider2D frog_top_collider;
	Collider2D frog_bottom_collider;

	public string frogType = "frog";
	public bool isEvolving = false;

    GameObject player;
    PlayerBehaviourScript playerBS;
	Rigidbody2D playerRB;
	Collider2D playercollider;
    public GameObject enemies;

	public virtual void frogStart(){
	}

    void Start(){
		nutPoints = 1;
		enemyType = "frog";

		player = GameObject.Find("player");
		playerBS = player.GetComponent<PlayerBehaviourScript>();
		playerRB = player.GetComponent<Rigidbody2D>();
		playercollider = player.GetComponent<Collider2D>();

        enemies = GameObject.Find("enemies");
		myrigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
		random = new System.Random();
        mycollider = GetComponent<Collider2D>();
        foreach (Transform child in transform){
            if (child.name == "frog_tongue_mask"){
                 tongue_mask = child;
		         foreach (Transform child2 in tongue_mask){
					 tongue = child2.GetComponent<frog_tongue_script>();
			         foreach (Transform child3 in child2){
					 	tongue_end = child3.GetComponent<frog_tongue_end_script>();
						tongue_end_collider = child3.GetComponent<Collider2D>();
					}
				 }
            } else if (child.name == "frog_tongue_area_collider"){
				tongue_area = child.GetComponent<frog_tongue_area_collider_script>();
				tongue_area_collider = child.GetComponent<Collider2D>();

			} else if (child.name == "frog_top_collider"){
				frog_top_collider_s = child.GetComponent<frog_top_collider_script>();
				frog_top_collider = child.GetComponent<Collider2D>();

			} else if (child.name == "frog_bottom_collider"){
				frog_bottom_collider_s = child.GetComponent<frog_bottom_collider_script>();
				frog_bottom_collider = child.GetComponent<Collider2D>();

			}
        }
		Physics2D.IgnoreCollision(tongue_end_collider, tongue_area_collider);
		Physics2D.IgnoreCollision(tongue_end_collider, frog_top_collider);
		Physics2D.IgnoreCollision(tongue_end_collider, frog_bottom_collider);
		Physics2D.IgnoreCollision(tongue_end_collider, mycollider);

		frogStart();
    }

	public virtual void frogFixedUpdate(){
	}

    void FixedUpdate()
    {

		if (isEvolving == false){

			if (jumpingTimer > 0)
			{
				velocity.x = (UnityEngine.Random.Range(400f, 600f) * dirMultiplier);
				if (jumpingTimer < 10)
				{
					velocity.y = 20f;
				}
				else
				{
					velocity.y = 100f;
				}
				myrigidbody.AddForce(velocity, ForceMode2D.Impulse);
				jumpingTimer--;
			}
			else if (isGrounded == true || jumpingTimer == 0)
			{
				velocity.y = 0;
				isJumping = false;
			}

			if (directionIsRight){ //is right
				dirMultiplier = 1f;
				transform.localScale = new Vector3(10, 10, 1);
			} else { //is left
				dirMultiplier = -1f;
				transform.localScale = new Vector3(-10, 10, 1);
			}

			if (isGrounded == true && myrigidbody.velocity.y <= 0f){
				isJumping = false;
			}

	        if (isGrounded == true && isDead == false && isLicking == false) {
				if (((directionIsRight == false && transform.position.x < -10f) || (directionIsRight == true && transform.position.x > 10f)) &&
						UnityEngine.Random.Range(0, 800) == 0){
					if (directionIsRight == true){
						directionIsRight = false;
					} else {
						directionIsRight = true;
					}
				} else if (canLick == true && isLicking == false){

					tongue_mask.gameObject.SetActive(true);
					isLicking = true;
					tongue.thrust();

				}
	        }

			if (isDead == false && jumpDelay <= 0 && isGrounded == true && isLicking == false){

				if (transform.position.y < 4f){


					jumpDelay = UnityEngine.Random.Range(100, 250);

					jumpingTimer = UnityEngine.Random.Range(15, 20);
					isJumping = true;
				}
			} else {

				jumpDelay--;

			}

			if (myrigidbody.velocity.x > 4f)
	        {
	            myrigidbody.velocity = new Vector2(4f, myrigidbody.velocity.y);
	        } else if (myrigidbody.velocity.x < -4f)
	        {
	            myrigidbody.velocity = new Vector2(-4f, myrigidbody.velocity.y);
	        }
	        if (myrigidbody.velocity.y > 10f)
	        {
	            myrigidbody.velocity = new Vector2(myrigidbody.velocity.x, 10f);
	        }
	        else if (myrigidbody.velocity.y < -10f)
	        {
	            myrigidbody.velocity = new Vector2(myrigidbody.velocity.x, -10f);
	        }
		}



		frogFixedUpdate();

        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("isJumping", isJumping);
        animator.SetBool("isDead", isDead);
        animator.SetBool("isLicking", isLicking);

    }

	public override void tryKill(){
		Debug.Log("tryKill");

		lives--;
		if (isDead == false && isProtected == false && lives <= 0){
	        isDead = true;
			if (frogType == "frog"){
				BoxCollider2D myFrogCollider = GetComponent<BoxCollider2D>();
		        myFrogCollider.offset = new Vector2(0f, -0.035f);
		        myFrogCollider.size = new Vector2(0.12f, 0.01f);
			}

	        StartCoroutine(dieDelay());
		} else {
			StartCoroutine(protect());
		}
    }


	public void consume(Collider2D pickup){
		frogConsume(pickup);
		Destroy(pickup.gameObject);
		isLicking = false;
		tongue_mask.gameObject.SetActive(false);
		canLick = false;
	}

	public virtual void frogConsume(Collider2D pickup) {
		Pickup p = pickup.GetComponent<Pickup>();
		if (p.level > 1){
			StartCoroutine(evolveIntoToad());
		}
   }

   IEnumerator evolveIntoToad(){

		// isProtected = true;
		// isEvolving = true;

		// SpriteRenderer sr = GetComponent<SpriteRenderer>();
		// Animator animator = GetComponent<Animator>();
		// animator.enabled = false;

		// Sprite[] sprites = Resources.LoadAll<Sprite>(@"sprites");
		// Sprite toadSprite = null;
		// Sprite frogSprite = null;
		// for (int x = 0; x < sprites.Length; x++) {
		// 	if (sprites[x].name == "toad_idle_offset"){
		// 		toadSprite = sprites[x];
		// 	}
		// 	if (sprites[x].name == "frog_idle"){
		// 		frogSprite = sprites[x];
		// 	}
		// }

		// for (float x = 0f; x < 0.2f; x+=0.01f) {
		// 	yield return new WaitForSeconds(x);
		// 	sr.sprite = frogSprite;
		// 	yield return new WaitForSeconds(0.2f-x);
		// 	sr.sprite = toadSprite;
		// }
		yield return new WaitForSeconds(2f);

		// GameObject mytoad = Instantiate(Resources.Load("Prefabs/toad")) as GameObject;
  //       mytoad.transform.parent = enemies.transform;
		// mytoad.transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
		// ToadBehaviourScript toadBS = mytoad.GetComponent<ToadBehaviourScript>();
		// toadBS.directionIsRight = directionIsRight;
		// Destroy(gameObject);
   }

	public virtual int getThrustTimer() {
      return 23;
   }

   public virtual float getEatPickupCutoff() {
	 return 0.1f;
  }

}
