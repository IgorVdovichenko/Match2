using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
	public int width;
	public int height;
	public GameObject tilePrefab;

	public StatisticsManager statisticsManager;

	public ParticleSystem particles;

	private Tile[,] tiles;

	private GameObject[,] objects;

	private List<Tile> tilesToRemove = new List<Tile>();

	private WaitForSeconds collapsingTilesDelay = new WaitForSeconds(0.7f);

	private bool isMoving = false;
	[HideInInspector]
	public bool canClick = true;

	public MyIntUnityEvent onPlay;

	private void Start()
	{
		tiles = new Tile[width, height];
		objects = new GameObject[width, height];
		SetTiles();
	}

	private void SetTiles()
	{
		for (int i = 0; i < width; i++)
		{
			for (int j = 0; j < height; j++)
			{
				objects[i, j] = InstantiateTile(i, j);
				tiles[i, j] = objects[i, j].GetComponent<Tile>();
			}
		}
	}

	private GameObject InstantiateTile(int x, int y)
	{
		Vector3 position = new Vector3(x, y, 0);
		GameObject slot = Instantiate(tilePrefab, position, Quaternion.identity);
		slot.transform.parent = transform;
		return slot;
	}

	public void FindTilesToRemove(Tile clickedTile)
	{
		canClick = false;
		List<Tile> tilesToCheck = new List<Tile>();
		tilesToCheck.Add(clickedTile);
		int counter = tilesToCheck.Count;

		tilesToRemove.Clear();

		while (counter > 0)
		{
			for (int i = 0; i < counter; i++)
			{
				CheckUpperTile(tilesToCheck, i);
				CheckBottomTile(tilesToCheck, i);
				CheckLeftTile(tilesToCheck, i);
				CheckRightTile(tilesToCheck, i);
				tilesToRemove.Add(tilesToCheck[i]);
				tilesToCheck.Remove(tilesToCheck[i]);
				counter = tilesToCheck.Count;
			}
		}
		tilesToCheck.Clear();
		if (tilesToRemove.Count > 1)
		{
			PlayParticles();
			RemoveTiles();
			onPlay.Invoke(tilesToRemove.Count);
			statisticsManager.UpdateData(tilesToRemove);
			StartCoroutine(MoveTiles());
			StartCoroutine(SetNewValuesForEmptyTiles()); 
		}
		else
		{
			canClick = true;
		}
	}

	private void CheckRightTile(List<Tile> tilesToCheck, int i)
	{
		if (tilesToCheck[i].X < width - 1)
		{
			Tile rightTile = tiles[tilesToCheck[i].X + 1, tilesToCheck[i].Y];
			if (tilesToCheck[i].Color == rightTile.Color && IsChecked(rightTile) == false)
				tilesToCheck.Add(rightTile);
		}
	}

	private void CheckLeftTile(List<Tile> tilesToCheck, int i)
	{
		if (tilesToCheck[i].X > 0)
		{
			Tile leftTile = tiles[tilesToCheck[i].X - 1, tilesToCheck[i].Y];
			if (tilesToCheck[i].Color == leftTile.Color && IsChecked(leftTile) == false)
				tilesToCheck.Add(leftTile);
		}
	}

	private void CheckBottomTile(List<Tile> tilesToCheck, int i)
	{
		if (tilesToCheck[i].Y > 0)
		{
			Tile lowerTile = tiles[tilesToCheck[i].X, tilesToCheck[i].Y - 1];
			if (tilesToCheck[i].Color == lowerTile.Color && IsChecked(lowerTile) == false)
				tilesToCheck.Add(lowerTile);
		}
	}

	private void CheckUpperTile(List<Tile> tilesToCheck, int i)
	{
		if (tilesToCheck[i].Y < height - 1)
		{
			Tile upperTile = tiles[tilesToCheck[i].X, tilesToCheck[i].Y + 1];
			if (tilesToCheck[i].Color == upperTile.Color && IsChecked(upperTile) == false)
				tilesToCheck.Add(upperTile);
		}
	}

	private bool IsChecked(Tile tile)
	{
		bool output = false;
		for (int i = 0; i < tilesToRemove.Count; i++)
		{
			if (tile == tilesToRemove[i])
			{
				output = true;
				break;
			}
		}
		return output;
	}

	private void PlayParticles()
	{
		for (int i = 0; i < tilesToRemove.Count; i++)
		{
			var main = particles.main;
			main.startColor = tilesToRemove[i].Color;
			Instantiate(particles, new Vector3(tilesToRemove[i].X, tilesToRemove[i].Y, 0), Quaternion.identity);
		}
	}

	private void RemoveTiles()
	{
		for (int i = 0; i < tilesToRemove.Count; i++)
		{
			tiles[tilesToRemove[i].X, tilesToRemove[i].Y] = null;
			Destroy(tilesToRemove[i].gameObject);
		}
	}

	public IEnumerator MoveTiles()
	{
		isMoving = true;
		for (int i = 0; i < width; i++)
		{
			int emptyCounter = 0;
			for (int j = 0; j < height; j++)
			{
				if (tiles[i, j] == null)
					emptyCounter++;
				else
				{
					Tile tile = tiles[i, j];
					tile.SetCoords(tile.X, tile.Y - emptyCounter);
				}
			}
		}
		yield return collapsingTilesDelay;
		isMoving = false;
	}

	private IEnumerator SetNewValuesForEmptyTiles()
	{
		yield return new WaitUntil(() => { return isMoving == false; });
		ReorganizeBoard();
		ReorganizeTiles();
		canClick = true;
	}

	private void ReorganizeBoard()
	{
		GameObject[,] tempArray = new GameObject[width, height];
		for (int i = 0; i < width; i++)
		{
			for (int j = 0; j < height; j++)
			{
				tempArray[i, j] = FindObjectByPosition(i, j);
			}
		}
		InstantiateTilesForEmptySlots(tempArray);
		objects = tempArray;
	}

	private void InstantiateTilesForEmptySlots(GameObject[,] slots)
	{
		for (int i = 0; i < width; i++)
		{
			for (int j = 0; j < height; j++)
			{
				if (slots[i, j] == null)
				{
					slots[i, j] = InstantiateTile(i, j);
				}
			}
		}
	}

	private GameObject FindObjectByPosition(int x, int y)
	{
		GameObject output = null;
		for (int i = 0; i < width; i++)
		{
			for (int j = 0; j < height; j++)
			{
				if (objects[i, j] != null)
				{
					Tile tile = objects[i, j].GetComponent<Tile>();
					if (tile.X == x && tile.Y == y)
					{
						output = objects[i, j];
						break;
					}  
				}
			}
		}
		return output;
	}

	private void ReorganizeTiles()
	{
		for (int i = 0; i < width; i++)
		{
			for (int j = 0; j < height; j++)
			{
				tiles[i, j] = objects[i, j].GetComponent<Tile>();
			}
		}
	}
}
