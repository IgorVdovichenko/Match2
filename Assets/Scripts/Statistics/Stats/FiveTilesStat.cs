using System.Collections.Generic;

public class FiveTilesStat : StatisticsUpdater
{
	public void Update(List<Tile> tiles, ref StatisticsData data)
	{
		if (tiles.Count == 5)
			data.fiveCount++;
	}
}