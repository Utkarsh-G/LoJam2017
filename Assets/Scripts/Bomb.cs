using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

	public float TimeToExplode = 2f;
	public ColourOfPaint BombColour;
	public Tile Location;
	public TileGrid oneGrid;
	public GameObject ColourSplat;
	public bool isTicking;


	float timer;
	bool isInPlay;

	/*void Awake(){
		Location = new Tile ();
	}*/

	// Use this for initialization
	void Start () {
		timer = TimeToExplode;
		isTicking = false;
		//BringBombInPlayArea (Location);
	}
	
	// Update is called once per frame
	void Update () {
		if (isTicking) {
			timer -= Time.deltaTime;

			if (timer < 0) {	

				Explode ();
			}
		}
	}

	public void BringBombInPlayArea(Tile location)
	{
		Location.PosX = location.PosX;
		Location.PosZ = location.PosZ;
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
		SplashColour();
		ResetBomb ();
	}

	void SplashColour()
	{
		GameObject splat = Instantiate(ColourSplat);
		float yValue = 0.5f + ((float)oneGrid.NumberOfSplats * 0.001f);
		splat.transform.position = new Vector3((float)Location.PosX + 0.5f, yValue, (float)Location.PosZ+0.5f);
		oneGrid.SplashColour(Location, BombColour);
		oneGrid.NumberOfSplats++;
	}

	void ResetBomb()
	{
		isTicking = false;
		TakeBombOutOfPlayArea ();
		timer = TimeToExplode;
	}

}
