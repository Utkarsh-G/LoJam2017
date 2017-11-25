using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGrid : MonoBehaviour {

	public Tile[,] Tiles;
	public GameObject TilePrefab; 
	public GameObject CratePrefab;
	public LevelLayout Level1;
	public Bomb myBomb;
	public Player1 playerOne;
	public int SIZEOFARENA = 11;

	public int NumberOfSplats = 2;
	// Use this for initialization
	void Awake () {
		Tiles = new Tile[SIZEOFARENA, SIZEOFARENA];
		CreateGrid ();
		//PositionBomb (Tiles[5,5]);
		playerOne.PositionPlayer(Tiles[5,5]);
	}


	// Update is called once per frame
	void Update () {

	}


	void CreateGrid()
	{
		for (int i = 0; i < SIZEOFARENA; i++) {
			for (int j = 0; j < SIZEOFARENA; j++) {
				//Tiles [i, j] = new Tile ();
				GameObject TempTileObject = Instantiate (TilePrefab, this.transform);
				Tile TempTile = TempTileObject.GetComponent<Tile> ();
				TempTile.PosX = i;
				TempTile.PosZ = j;
				TempTileObject.transform.position = new Vector3 ((float)i+0.5f, 0f, (float)j+0.5f);
				Tiles [i, j] = TempTile;

				if (Level1.CrateArray [i*SIZEOFARENA + j]) {
					Tiles [i, j].HasCrate = true;
					Tiles [i, j].CrateGO = Instantiate (CratePrefab, this.transform);
					Tiles [i, j].CrateGO.transform.position = new Vector3 ((float)i + 0.5f, 1f, (float)j + 0.5f);
					Tiles [i, j].myCrate = Tiles [i, j].CrateGO.GetComponent<Crate> ();
					Tiles [i, j].myCrate.Location = Tiles [i, j];
				}
			}
		}
	}

	void PositionBomb(Tile location)
	{
		myBomb.BringBombInPlayArea (location);

	}

	public void SplashColour(Tile location, ColourOfPaint inColour)
	{
		Tile[] splashedTiles = new Tile[9];
		splashedTiles[0] = Tiles [location.PosX, location.PosZ];
		splashedTiles[1] = Tiles [(Mathf.Clamp (location.PosX - 1, 0, SIZEOFARENA - 1)), location.PosZ];
		splashedTiles[2] = Tiles [(Mathf.Clamp (location.PosX - 2, 0, SIZEOFARENA - 1)), location.PosZ];
		splashedTiles[3] = Tiles [(Mathf.Clamp (location.PosX + 1, 0, SIZEOFARENA - 1)), location.PosZ];
		splashedTiles[4] = Tiles [(Mathf.Clamp (location.PosX + 2, 0, SIZEOFARENA - 1)), location.PosZ];
		splashedTiles [5] = Tiles [location.PosX, (Mathf.Clamp (location.PosZ - 1, 0, SIZEOFARENA - 1))];
		splashedTiles [6] = Tiles [location.PosX, (Mathf.Clamp (location.PosZ - 2, 0, SIZEOFARENA - 1))];
		splashedTiles [7] = Tiles [location.PosX, (Mathf.Clamp (location.PosZ + 1, 0, SIZEOFARENA - 1))];
		splashedTiles [8] = Tiles [location.PosX, (Mathf.Clamp (location.PosZ + 2, 0, SIZEOFARENA - 1))];
		Debug.Log ("Checking for stuns");
		for (int i = 0; i < 9; i++) {
			splashedTiles [i].ChangeColour (inColour);
			AffectPlayers (splashedTiles [i]);
		}
	}

	void AffectPlayers(Tile tile)
	{
		if (playerOne.Location.PosX == tile.PosX && playerOne.Location.PosZ == tile.PosZ) {
			playerOne.Stun ();
			Debug.Log ("Stunning Player");
		}
	}

	public void MovePlayer(GameObject Player, Player1 player, Direction direction)
	{
		//check if player is in arena after move
		player = Player.GetComponent<Player1>();
		if (IsMoveValid (player.Location, direction)) {
			//change logical block
			MoveLogicalPlayer(player,direction);
			//physically move player + animation
			//MoveVisualPlayer(Player, direction);

		}
	}



	bool IsMoveValid(Tile tile, Direction direction)
	{
		bool isValid = true;

		if ((tile.PosX == 0) && (direction == Direction.Left)) {
			isValid = false;
		}
		if ((tile.PosZ == 0) && (direction == Direction.Down)) {
			isValid = false;
		}
		if ((tile.PosX == SIZEOFARENA-1) && (direction == Direction.Right)) {
			isValid = false;
		}
		if ((tile.PosZ == SIZEOFARENA-1) && (direction == Direction.Up)) {
			isValid = false;
		}

		if (!((tile.PosZ == SIZEOFARENA - 1) && (direction == Direction.Up))) {
			if (direction == Direction.Up && Tiles [tile.PosX, tile.PosZ + 1].HasCrate) {
				isValid = false;
			}
		}

		if (!((tile.PosZ == 0) && (direction == Direction.Down))) {
			if (direction == Direction.Down && Tiles [tile.PosX, tile.PosZ - 1].HasCrate) {
				isValid = false;
			}
		}

		if (!((tile.PosX == 0) && (direction == Direction.Left))) {
			if (direction == Direction.Left && Tiles [tile.PosX - 1 , tile.PosZ].HasCrate) {
				isValid = false;
			}
		}

		if (!((tile.PosX == SIZEOFARENA-1) && (direction == Direction.Right))) {
			if (direction == Direction.Right && Tiles [tile.PosX + 1, tile.PosZ].HasCrate) {
				isValid = false;
			}
		}



		return isValid;
	}

	/*void MoveVisualPlayer(GameObject player, Direction direction)
	{
		
	}*/

	void MoveLogicalPlayer(Player1 player, Direction direction)
	{
		switch (direction) {
		case Direction.Up:
			player.Location.PosZ++;
			break;
		case Direction.Down:
			player.Location.PosZ--;
			break;
		case Direction.Right:
			player.Location.PosX++;
			break;
		case Direction.Left:
			
			player.Location.PosX--;
			break;
		default:
			break;
		}

		player.IsMoving = true;
		player.TimeToReachTarget += 1f / player.speed;
	}
}
