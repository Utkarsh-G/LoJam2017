﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {


	public int PosX;
	public int PosZ;

	public ColourOfPaint ColourOnTile;

	public bool HasCrate;

	public GameObject CrateGO;
	public Crate myCrate;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ChangeColour(ColourOfPaint inColour)
	{
		this.ColourOnTile = inColour;
	}
}
