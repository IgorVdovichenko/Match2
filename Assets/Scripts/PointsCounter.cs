using UnityEngine;
using UnityEngine.UI;

public class PointsCounter : MonoBehaviour
{
	[SerializeField] private Text pointsText;
	[SerializeField] private string textFormat = "0000000";
	[SerializeField] private int ptsPerSlot = 50;
	[SerializeField] private int bonusDelta = 25;

	private int currPoints;

	public MyIntUnityEvent onPointsAdd;

	private void Start()
	{
		pointsText.text = currPoints.ToString(textFormat);
	}

	public void AddPoints(int slotsCount)
	{
		currPoints += slotsCount * (ptsPerSlot + bonusDelta * (slotsCount - 2));
		pointsText.text = currPoints.ToString(textFormat);
		onPointsAdd.Invoke(currPoints);
	}
}
