using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpperTileChecker : TileChecker
{
	public UpperTileChecker(List<Tile> tilesToRemove)
	{
		this.tilesToRemove = tilesToRemove;
	}

	public override void Check(Tile tile)
	{
		//if (tile.Y < height - 1)
		//{
		//	Tile upperTile = tiles[tilesToCheck.X, tilesToCheck[i].Y + 1];
		//	if (tilesToCheck[i].Color == upperTile.Color && IsChecked(upperTile) == false)
		//		tilesToCheck.Add(upperTile);
		//}
	}
}
