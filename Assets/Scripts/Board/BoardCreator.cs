using UnityEngine;

public class BoardCreator
{
	private int width;
	private int height;
	private GameObject tile;

	public BoardCreator(int width, int height, GameObject tilePrefab)
	{
		this.width = width;
		this.height = height;
		tile = tilePrefab;
	}

	public GameObject[,] Create()
	{
		GameObject[,] objects = new GameObject[width, height];
		for (int i = 0; i < width; i++)
		{
			for (int j = 0; j < height; j++)
			{
				objects[i, j] = InstantiateTile(i, j);
			}
		}
		return objects;
	}

	public GameObject InstantiateTile(int x, int y)
	{
		Vector3 position = new Vector3(x, y, 0);
		GameObject slot = MonoBehaviour.Instantiate(tile, position, Quaternion.identity);
		return slot;
	}
}
