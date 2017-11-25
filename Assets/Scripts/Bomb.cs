using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

	public int TimeToExplode = 2;
	public ColourOfPaint BombColour;
	public Tile Location;
	public TileGrid oneGrid;
	public GameObject ColourSplat;


	float timer;
	bool isTicking;
	bool isInPlay;

	/*void Awake(){
		Location = new Tile ();
	}*/

	// Use this for initialization
	void Start () {
		timer = (float)TimeToExplode;
		isTicking = false;
		//BringBombInPlayArea (Location);
	}
	
	// Update is called once per frame
	void Update () {
		if (isTicking) {
			timer -= Time.deltaTime;

			if (timer < 0) {
				
				isTicking = false;
				TakeBombOutOfPlayArea ();
				Explode ();
			}
		}
	}

	public void BringBombInPlayArea(Tile location)
	{
		Location = location;
		isInPlay = true;
		isTicking = true;
		transform.position = new Vector3 ((float)location.PosX + 0.5f, 1f, (float)location.PosZ+0.5f);

	}

	void TakeBombOutOfPlayArea()
	{
		transform.position += new Vector3 (0f, 100f, 0f);
		isInPlay = false;

	}

	void Explode()
	{
		//add logical colours to tiles
		//Make sure to not exceed the bounds
		//Deal damage to players
		//SplashColour();
		GameObject splat = Instantiate(ColourSplat);
		splat.transform.position = new Vector3((float)Location.PosX + 0.5f, 0.5001f, (float)Location.PosZ+0.5f);
		oneGrid.SplashColour(Location, BombColour);
	}



}
