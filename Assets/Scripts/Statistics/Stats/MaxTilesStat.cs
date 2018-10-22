using System.Collections.Generic;

public class MaxTilesStat : StatisticsUpdater
{
	public void Update(List<Tile> tiles, ref StatisticsData data)
	{
		if (tiles.Count > data.maxDestroyed)
			data.maxDestroyed = tiles.Count;
	}
}
