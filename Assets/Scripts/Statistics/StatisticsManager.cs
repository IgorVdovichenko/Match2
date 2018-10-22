using System.Collections.Generic;
using UnityEngine;

public class StatisticsManager: MonoBehaviour
{
	private StatisticsData data;
	private Saver<StatisticsData> saver;

	private StatisticsUpdater[] updaters = {
		new MaxTilesStat(),
		new ThreeTilesStat(),
		new FourTilesStat(),
		new FiveTilesStat(),
		new SixTilesStat(),
		new ManyTilesStat()
	};

	private void Start()
	{
		saver = new Saver<StatisticsData>();
		data = saver.GetData();
	}

	private void OnApplicationQuit()
	{
		saver.SaveData(data);
	}

	private void OnApplicationPause(bool pause)
	{
		if(pause)
			saver.SaveData(data);
	}

	private void OnDestroy()
	{
		saver.SaveData(data);
	}

	public void UpdateData(List<Tile> tiles)
	{
		foreach (var item in updaters)
		{
			item.Update(tiles, ref data);
		}
	}

	public void UpdatePointsData(int amount)
	{
		if(data.maxPoints < amount)
			data.maxPoints = amount;
	}
}
