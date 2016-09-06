﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed = 15.0f;
	public float padding = 0.2f;
	public GameObject projectile;
	public float projectileSpeed;
	public float firingRate = 0.2f;
	public float health = 250f;

	public AudioClip fireSource;
	private Thruster thruster;


	float xmin = -5;
	float xmax = 5;

	void Start(){
		
		thruster = GameObject.FindObjectOfType<Thruster> ();

	   
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftMost = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distance));
		Vector3 rightMost = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, distance));

		xmin = leftMost.x + padding;
		xmax = rightMost.x - padding;
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.Space)){
			InvokeRepeating("Fire", 0.000001f, firingRate);
		}

		if (Input.GetKeyUp (KeyCode.Space)){
			CancelInvoke("Fire");
		}

		if (Input.GetKey(KeyCode.LeftArrow)) {
			transform.position += Vector3.left * speed*Time.deltaTime;
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			transform.position += Vector3.right * speed*Time.deltaTime;
		}

		//Restricts the player to the gamespace
		float newX = Mathf.Clamp (transform.position.x, xmin, xmax);
		transform.position = new Vector3 (newX, transform.position.y, transform.position.z);

	}

	void OnTriggerEnter2D(Collider2D col){

		Projectile missile = col.gameObject.GetComponent<Projectile> ();

		if (missile) {

			health -= missile.GetDamage();
			missile.Hit();

			if (health <= 0){
				Die ();
			}
		}
	}

	void Die(){
		Destroy (gameObject);
		thruster.Destroy();
		LevelManager lvlMgr = GameObject.Find ("LevelManager").GetComponent<LevelManager> ();
		lvlMgr.LoadScene ("End");
	}

	#region Private Function

	private void Fire(){
		Vector3 offset = new Vector3 (0, 1, 0);
		GameObject beam = Instantiate(projectile, transform.position + offset, Quaternion.identity) as GameObject;
		beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed, 0);

		AudioSource.PlayClipAtPoint (fireSource, transform.position);
	}
	#endregion
}
