using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {


	public GameObject enemyPrefab;
	public float speed;
	public float width;
	public float height;
	public float spawnDelay = 0.5f;

	private enum Direction{Left, Right};
	private Direction direction;

	private float xmin;
	private float xmax;
	private float formationXMin;
	private float formationXMax;

	
	// Use this for initialization
	void Start () {
		//Sets up the inital direction
		direction = Direction.Right;

		//Initialize the max and the min of the width.
		float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftBoundary = Camera.main.ViewportToWorldPoint (new Vector3(0, 0, distanceToCamera));
		Vector3 rightBoundary = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, distanceToCamera));

		xmin = leftBoundary.x;
		xmax = rightBoundary.x;

		//Sets the enemy formation max and min
		SetFormationWidth ();

		SpawnUntilFull ();

	}

	void OnDrawGizmos(){
		Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0));
	}

	// Update is called once per frame
	void Update () {

		if (AllMembersDead ()) {
			SpawnUntilFull();
		}

		//Continues to toggle the direction that the piece is moving
		if (direction == Direction.Left) {
			transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
			//transform.position += Vector3.left * (speed * Time.deltaTime);

			SetFormationWidth();

			if (formationXMin < xmin){
				direction = Direction.Right;
			}

		} else {
			transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
			//transform.position += Vector3.right * (speed * Time.deltaTime);

			SetFormationWidth();

			if (formationXMax > xmax){
				direction = Direction.Left;
			}

		}

		//Create Restriction for the player. 
		float newX = Mathf.Clamp (transform.position.x, -width / 2, width / 2);
		transform.position = new Vector3 (newX, transform.position.y, transform.position.z);

	}

	#region Private Functions

	private Transform NextFreePosition(){
		foreach (Transform childPositionGameObject in transform) {
			if (childPositionGameObject.childCount == 0){
				return childPositionGameObject;
			}
		}
		return null;
	}

	private void SpawnEnemies(){
		foreach (Transform child in transform) {
			GameObject enemy = Instantiate (enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = child;
		}
	}

	private void SpawnUntilFull(){
		Transform freePosition = NextFreePosition ();

		if (freePosition) {
			GameObject enemy = Instantiate (enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = freePosition;
		}

		if (NextFreePosition()){
			Invoke ("SpawnUntilFull", spawnDelay);
		}

	}

	private bool AllMembersDead (){
	
		foreach (Transform childPositionGameObject in transform) {
			if (childPositionGameObject.childCount > 0){
				return false;
			}
		}
		return true;
	}

	private void SetFormationWidth(){ //Sets the enemy formation max and min
		formationXMin = this.transform.position.x - width / 2;
		formationXMax = this.transform.position.x + width / 2;
	}

	#endregion
}
