using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour {

	public TileGrid oneGrid;

	public Bomb MyBomb;

	public Tile Location;

	public bool IsMoving;
	public float speed;
	public float StunTime;

	float stunTimer;
	public bool IsStunned;
	public bool IsPlacingBomb;
	public float TimeToReachTarget;
	ColourOfPaint MyColour;
	int Score;


	// Use this for initialization
	void Start () {
		IsStunned = false;
		IsPlacingBomb = false;
		TimeToReachTarget = 0f;
	}
	
	// Update is called once per frame
	void Update () {

		if (!IsStunned && !IsPlacingBomb) {
			ProcessInput ();
		}

		if (IsStunned){
			stunTimer -= Time.deltaTime;
			if (stunTimer < 0) {
				IsStunned = false;
				Debug.Log ("Stun timer expired");
			}
		}
		if (IsMoving) {
			Vector3 target = new Vector3 ((float)Location.PosX + 0.5f, 1f, (float)Location.PosZ + 0.5f);
			//transform.Translate(Vector3.forward*Time.deltaTime);
			float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position, target, step);

			if (TimeToReachTarget > 0f) {
				TimeToReachTarget -= Time.deltaTime;
			} else {
				IsMoving = false;
				TimeToReachTarget = 0f;
			}

		}

		if (IsPlacingBomb && !IsMoving) {
			PlaceBomb ();
		}

	}

	void ProcessInput()
	{
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			oneGrid.MovePlayer(gameObject,this,Direction.Up);
			//Debug.Log ("Hit Up. PosX: "+ Location.PosX + " PosZ: " + Location.PosZ);
		}

		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			oneGrid.MovePlayer(gameObject,this,Direction.Down);
			//Debug.Log ("Hit Up. PosX: "+ Location.PosX + " PosZ: " + Location.PosZ);
		}

		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			oneGrid.MovePlayer(gameObject,this,Direction.Left);
			//Debug.Log ("Hit Up. PosX: "+ Location.PosX + " PosZ: " + Location.PosZ);
		}

		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			oneGrid.MovePlayer(gameObject,this,Direction.Right);
			//Debug.Log ("Hit Up. PosX: "+ Location.PosX + " PosZ: " + Location.PosZ);
		}

		if (Input.GetKeyDown (KeyCode.Space)) {
			PlacingBombIfValid ();
		}

	}

	public void PositionPlayer(Tile location)
	{
		Location = location;
		transform.position = new Vector3 ((float)location.PosX + 0.5f, 1f, (float)location.PosZ+0.5f);
	}

	void PlacingBombIfValid()
	{
		if (!MyBomb.isTicking && !IsStunned) {
			IsPlacingBomb = true;
		}
	}

	void PlaceBomb()
	{		
		MyBomb.BringBombInPlayArea (Location);
		IsPlacingBomb = false;
	}

	public void Stun()
	{
		stunTimer = StunTime;
		IsStunned = true;
	}
}
