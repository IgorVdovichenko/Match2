﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Menu : MonoBehaviour
{
	[SerializeField] private string mainMenuScene;

	public void LoadMainMenu()
	{
		SceneManager.LoadScene(mainMenuScene);
	}
}
