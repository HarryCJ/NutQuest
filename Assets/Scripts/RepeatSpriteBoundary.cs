using UnityEngine;
 using System.Collections;

 [RequireComponent (typeof (SpriteRenderer))]
 public class RepeatSpriteBoundary : MonoBehaviour {
     SpriteRenderer sprite;
     void Awake () {

         sprite = GetComponent<SpriteRenderer>();
        //  if(!SpritePivotAlignment.GetSpriteAlignment(gameObject).Equals(SpriteAlignment.TopRight)){
            //  Debug.LogError("You forgot change the sprite pivot to Top Right.");
        //  }
         Vector2 spriteSize = new Vector2(sprite.bounds.size.x / transform.localScale.x, sprite.bounds.size.y / transform.localScale.y);

         GameObject childPrefab = new GameObject();

         SpriteRenderer childSprite = childPrefab.AddComponent<SpriteRenderer>();
         childPrefab.transform.position = transform.position;
         childSprite.sprite = sprite.sprite;
		 childSprite.sortingLayerID = sprite.sortingLayerID;
		 childSprite.sortingOrder = sprite.sortingOrder;
         childSprite.color = sprite.color;

         GameObject child;

		 int freq = 3;
		 int curFreq = 0;
         for (int i = 0, h = (int)Mathf.Round(sprite.bounds.size.y); i*spriteSize.y < h; i++) {
             for (int j = 0, w = (int)Mathf.Round(sprite.bounds.size.x); j*spriteSize.x < w; j++) {
			 	curFreq++;
				 if (curFreq == freq){
	                 child = Instantiate(childPrefab) as GameObject;
	                 child.transform.position = transform.position - (new Vector3(spriteSize.x*j, spriteSize.y*i, 0));
					 child.transform.localScale = transform.parent.localScale;
	                 child.transform.parent = transform;
			 	} else if (curFreq > freq) {
					curFreq = 0;
				}
             }
			 if (curFreq == freq){
				 curFreq++;
			 }
         }

         Destroy(childPrefab);
         sprite.enabled = false;

     }
 }
