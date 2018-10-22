using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour, IPointerClickHandler
{
	[SerializeField]
	private Palette palette;

	private SpriteRenderer image;

	public int X { get; private set; }
	public int Y { get; private set; }
	public Color Color { get; private set; }

	private Board board;

	private void Start()
	{
		image = GetComponent<SpriteRenderer>();
		board = FindObjectOfType<Board>();
		Set();
		SetCoords((int)transform.position.x, (int)transform.position.y);
	}

	private void Update()
	{
		Move();
	}

	public void SetCoords(int x, int y)
	{
		X = x;
		Y = y;
		gameObject.name = string.Format("{0}-{1}", X, Y);
	}

	private void Set()
	{
		int index = Random.Range(0, palette.colors.Length);
		image.sprite = palette.sprites[index];
		Color = palette.colors[index];
		image.color = Color;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if(board.canClick)
			board.FindTilesToRemove(this);
	}

	private void Move()
	{
		int speed = 10;
		Vector2 tempPos = new Vector2(transform.position.x, Y);
		transform.position = Vector2.MoveTowards(transform.position, tempPos, speed * Time.deltaTime);
	}
}
