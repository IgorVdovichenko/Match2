using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesRemover
{
	private Tile[,] tiles;
	private List<Tile> tilesToRemove = new List<Tile>();
	private bool canClick = true;

	public TilesRemover(Tile[,] tiles)
	{
		this.tiles = tiles;
	}

	public void RemoveTiles(Tile clickedTile)
	{
		canClick = false;
		tilesToRemove.Clear();
		FindTiles(clickedTile);
		if (tilesToRemove.Count > 1)
			Remove();
		else
			canClick = true;
	}

	private void FindTiles(Tile clickedTile)
	{
		List<Tile> tilesToCheck = new List<Tile>();
		tilesToCheck.Add(clickedTile);
		int counter = tilesToCheck.Count;
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
	}

	private void CheckRightTile(List<Tile> tilesToCheck, int i)
	{
		if (tilesToCheck[i].X < tiles.GetLength(0) - 1)
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
		if (tilesToCheck[i].Y < tiles.GetLength(1) - 1)
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

	private void Remove()
	{
		for (int i = 0; i < tilesToRemove.Count; i++)
		{
			tiles[tilesToRemove[i].X, tilesToRemove[i].Y] = null;
			MonoBehaviour.Destroy(tilesToRemove[i].gameObject);
		}
	}
}
