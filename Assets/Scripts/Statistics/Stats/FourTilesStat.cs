using System.Collections.Generic;

public class FourTilesStat : StatisticsUpdater
{
	public void Update(List<Tile> tiles, ref StatisticsData data)
	{
		if (tiles.Count == 4)
			data.fourCount++;
	}
}