using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	[SerializeField] private string scene = "MainMenu";

	private void Update()
	{
		if (Input.GetKey(KeyCode.Escape))
		{
			ExitToMainMenu();
		}
	}

	private void ExitToMainMenu()
	{
		SceneManager.LoadScene(scene);
	}
}
