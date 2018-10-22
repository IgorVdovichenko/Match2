using System.Collections.Generic;

public interface StatisticsUpdater
{
	void Update(List<Tile> tiles, ref StatisticsData data);
}
