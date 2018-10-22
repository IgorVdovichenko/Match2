using System.Collections.Generic;

public class ThreeTilesStat : StatisticsUpdater
{
	public void Update(List<Tile> tiles, ref StatisticsData data)
	{
		if (tiles.Count == 3)
			data.threeCount++;
	}
}
