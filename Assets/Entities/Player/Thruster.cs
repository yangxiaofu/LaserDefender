using UnityEngine;
using System.Collections;

public class Thruster : MonoBehaviour {

	private PlayerController player;
	private Projectile missile;
	private Vector3 thrusterToPlayerVector;

	// Use this for initialization
	void Start () {
		player = GameObject.FindObjectOfType<PlayerController> ();
		thrusterToPlayerVector = this.transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = player.transform.position + thrusterToPlayerVector;

	}

	public void Destroy(){
		Destroy (gameObject);
	}
}
