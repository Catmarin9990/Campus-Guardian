using UnityEngine;
using UnityEngine.UI;

public class StudentScript : MonoBehaviour
{
	// Parametars Settings
	private string studName;
	private string studSurname;
	private byte course;
	public bool isGirl { get; set; } = true;
	private Image sprite;
	[SerializeField] private Sprite girlSprite;

	[Header("Movement Settings")]
	[SerializeField] private Transform start;
	[SerializeField] private Transform end;
	private float speed = 5f;
	private bool fadeIn = false;
	private bool fadeOut = false;

	[SerializeField] private GameObject docPrefab;
	private GameController gameController;

	private void Awake()
	{
		gameController = FindAnyObjectByType<GameController>();
	}
	private void Start()
	{

		fadeIn = true;
	}

	private void Update()
	{
		if (fadeIn)
		{
			transform.position = Vector2.MoveTowards(transform.position, start.position, speed * Time.deltaTime);
			if(Vector2.Distance(transform.position, start.position) == 0)
			{
				fadeIn = false;
				gameController.docSpawn();
			}
		}
		if (fadeOut)
		{
			transform.position = Vector2.MoveTowards(transform.position, end.position, speed * Time.deltaTime);
			if(Vector2.Distance(transform.position, end.position) == 0)
				fadeOut = false;
		}
	}

	public void setSprite()
	{
		sprite = GetComponentInChildren<Image>();
		if (isGirl)
			sprite.sprite = girlSprite;
	}
	//fadeout mechanic
}
