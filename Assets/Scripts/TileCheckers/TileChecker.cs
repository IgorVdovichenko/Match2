using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TileChecker
{
	protected List<Tile> tilesToRemove;
	protected List<Tile> tilesToCheck;

	public abstract void Check(Tile tile);

	protected bool IsChecked(Tile tile)
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
}
