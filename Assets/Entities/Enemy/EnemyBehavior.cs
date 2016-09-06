using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {

	public float health = 150f;
	public GameObject enemyLaser;
	public float velocity = 5f;
	public float shotsPerSecond = 0.5f;
	public int scoreValue = 150;
	public AudioClip fireSource;
	public AudioClip deathSound;
	private ScoreKeeper scoreKeeper;

	void Start(){
		scoreKeeper = GameObject.Find ("Scoreboard").GetComponent<ScoreKeeper> ();
	}
	
	void Update(){
		float probability = shotsPerSecond * Time.deltaTime;

		if (Random.value < probability) {
			Fire ();
		}

	}

	void OnTriggerEnter2D(Collider2D col){

		Projectile missile = col.gameObject.GetComponent<Projectile> ();

		if (missile) {
			health -= missile.GetDamage();

			missile.Hit();

			if (health <= 0){
				Destroy (gameObject);
				AudioSource.PlayClipAtPoint(deathSound, transform.position);
				scoreKeeper.ScorePoints(scoreValue);
			} 

		}
	}

	#region Private Functions

	private void Fire(){
		Vector3 position = transform.position + new Vector3 (0, -1, 0);
		GameObject laser = Instantiate (enemyLaser, position, Quaternion.identity) as GameObject;
		laser.GetComponent<Rigidbody2D>().velocity = new Vector3 (0, -velocity, 0);
		AudioSource.PlayClipAtPoint (fireSource, transform.position);
	}

	#endregion


}
