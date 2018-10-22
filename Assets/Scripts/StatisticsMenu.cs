using UnityEngine;
using UnityEngine.UI;

public class StatisticsMenu : Menu
{
	[Header("Texts")]
	[SerializeField] private Text maxPointsText;
	[SerializeField] private Text maxDestroyedCountText;
	[SerializeField] private Text threesText;
	[SerializeField] private Text foursText;
	[SerializeField] private Text fivesText;
	[SerializeField] private Text sixsTexts;
	[SerializeField] private Text sevensText;

	private StatisticsData data;

	private void Start()
	{
		data = new Saver<StatisticsData>().GetData();
		Show();
	}

	private void Show()
	{
		maxPointsText.text = data.maxPoints.ToString();
		maxDestroyedCountText.text = data.maxDestroyed.ToString();
		threesText.text = data.threeCount.ToString();
		foursText.text = data.fourCount.ToString();
		fivesText.text = data.fiveCount.ToString();
		sixsTexts.text = data.sixCount.ToString();
		sevensText.text = data.sevenAndMoreCount.ToString();
	}
}
