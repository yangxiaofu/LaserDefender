using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	private Paddle paddle;
	private bool hasStarted = false;
	private Vector3 paddleToBallVector;
	private float paddleWidth;

	// Use this for initialization
	void Start () {
		paddle = GameObject.FindObjectOfType<Paddle>();
		paddleToBallVector = this.transform.position - paddle.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (!hasStarted) {
			//Locks the ball to the relative pattle
			this.transform.position = paddle.transform.position + paddleToBallVector;

			//Wait for hte mouse press to luanch. 
			if (Input.GetMouseButtonDown (0)) {
				Vector2 velocity = new Vector2(2f, 10f);
				this.GetComponent<Rigidbody2D>().velocity = velocity;
				hasStarted = true;
			}
		} 
	}

	void OnCollisionEnter2D(Collision2D collision){
		var tweak = new Vector2 (Random.Range(0f, 0.2f), Random.Range(0f, 0.2f));
		this.GetComponent<Rigidbody2D>().velocity += tweak;

		if (hasStarted == true) {
			GetComponent<AudioSource>().Play ();
		}

	}


}
