using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardRefactored : MonoBehaviour
{
	public int width;
	public int height;
	public GameObject tile;

	private GameObject[,] objects;

	private BoardCreator creator;

	private void Awake()
	{
		creator = new BoardCreator(width, height, tile);
		objects = creator.Create();
		ReorganizeHierarcy();
	}

	private void ReorganizeHierarcy()
	{
		foreach (var item in objects)
		{
			item.transform.parent = transform;
		}
	}
}
