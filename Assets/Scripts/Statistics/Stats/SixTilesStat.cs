using System.Collections.Generic;

public class SixTilesStat : StatisticsUpdater
{
	public void Update(List<Tile> tiles, ref StatisticsData data)
	{
		if (tiles.Count == 6)
			data.sixCount++;
	}
}