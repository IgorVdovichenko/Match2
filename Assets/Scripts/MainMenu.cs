using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	[SerializeField]
	private string gameScene;

	[SerializeField]
	private string statisticsScene;

	[SerializeField]
	private string settingsScene;

	public void LoadGameScene()
	{
		SceneManager.LoadScene(gameScene);
	}

	public void LoadStatisticsScene()
	{
		SceneManager.LoadScene(statisticsScene);
	}

	public void LoadSettingsScene()
	{
		SceneManager.LoadScene(settingsScene);
	}
}
