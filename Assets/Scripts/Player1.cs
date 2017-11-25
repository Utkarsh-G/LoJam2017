using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour {

	public TileGrid oneGrid;

	public GameObject MyBomb;

	public Tile Location;

	public bool IsMoving;
	public float speed;
	ColourOfPaint MyColour;
	int Score;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		ProcessInput ();
		if (IsMoving) {
			Vector3 target = new Vector3 ((float)Location.PosX + 0.5f, 1f, (float)Location.PosZ + 0.5f);
			//transform.Translate(Vector3.forward*Time.deltaTime);
			float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position, target, step);
		}
	}

	void ProcessInput()
	{
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			oneGrid.MovePlayer(gameObject,Direction.Up);
			Debug.Log ("Hit Up. PosX: "+ Location.PosX + " PosZ: " + Location.PosZ);
		}

		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			oneGrid.MovePlayer(gameObject,Direction.Down);
			Debug.Log ("Hit Up. PosX: "+ Location.PosX + " PosZ: " + Location.PosZ);
		}

		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			oneGrid.MovePlayer(gameObject,Direction.Left);
			Debug.Log ("Hit Up. PosX: "+ Location.PosX + " PosZ: " + Location.PosZ);
		}

		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			oneGrid.MovePlayer(gameObject,Direction.Right);
			Debug.Log ("Hit Up. PosX: "+ Location.PosX + " PosZ: " + Location.PosZ);
		}

	}

	public void PositionPlayer(Tile location)
	{
		Location = location;
		transform.position = new Vector3 ((float)location.PosX + 0.5f, 1f, (float)location.PosZ+0.5f);
	}
}
