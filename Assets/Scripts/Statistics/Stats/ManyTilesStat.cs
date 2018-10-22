using System.Collections.Generic;

public class ManyTilesStat : StatisticsUpdater
{
	public void Update(List<Tile> tiles, ref StatisticsData data)
	{
		if (tiles.Count >= 7)
			data.sevenAndMoreCount++;
	}
}