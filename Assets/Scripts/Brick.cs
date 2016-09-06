using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour {

	public AudioClip crack;
	public Sprite[] hitSprites;
	public static int breakableCount = 0;
	public GameObject smoke;

	private int timesHit;
	private LevelManager lvlMgr;
	private bool isBreakable;

	void Start () {
		isBreakable = (this.tag == "Breakable");

		//Keep track of breakable bricks
		if (isBreakable) {
			breakableCount++;
		}
		print ("Breakable Bricks : " + breakableCount);

		timesHit = 0;

		lvlMgr = GameObject.FindObjectOfType<LevelManager> ();
	}

	void OnCollisionEnter2D(Collision2D collision){

		AudioSource.PlayClipAtPoint (crack, transform.position);
		if (isBreakable) {
			HandleHits ();
		}
	}

	void HandleHits(){
		timesHit += 1;

		int maxHits = hitSprites.Length + 1;
		
		if (timesHit >= maxHits) {
			breakableCount--;
			print ("Breakable Bricks Left : " + breakableCount);


			GameObject smokePuff = Instantiate(smoke, transform.position, Quaternion.identity) as GameObject;
			smokePuff.GetComponent<ParticleSystem>().startColor = gameObject.GetComponent<SpriteRenderer>().color;

			Destroy (gameObject);
		} else {
			LoadSprites();
		}
	}


	void LoadSprites(){
		int spriteIndex = timesHit - 1;

		if (hitSprites[spriteIndex]){
			this.GetComponent<SpriteRenderer> ().sprite = hitSprites[spriteIndex];
		}

		
	}

	//TODO - remove this method once we can actually win.
	void SimulateWin(){
		lvlMgr.LoadNextLevel ();
	}
}
