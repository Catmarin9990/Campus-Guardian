using UnityEngine;
using UnityEngine.UI;

public class StudentScript : MonoBehaviour
{
	// Parametars Settings
	public string studName { get; set; }
	public string studSurname { get; set; }
	public byte course { get; set; }
	public bool isGirl { get; set; } = true;
	private Image sprite;
	[SerializeField] private Sprite girlSprite;

	[Header("Movement Settings")]
	[SerializeField] private Transform start;
	[SerializeField] private Transform inPoint;
	[SerializeField] private Transform backPoint;
	private float speed = 5f;
	private bool fadeIn = false;
	public bool fadeOut { get; set; }
	public bool goBack { get; set; }
	public bool isGetDoc { get; set; } = false;
 
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
		if (fadeOut && isGetDoc)
		{
			transform.position = Vector2.MoveTowards(transform.position, inPoint.position, speed * Time.deltaTime);
			if(Vector2.Distance(transform.position, inPoint.position) == 0)
			{
				fadeOut = false;
				gameController.StudentLeft();
			}
		}
		else if (goBack && isGetDoc)
        {
			transform.position = Vector2.MoveTowards(transform.position, backPoint.position, speed * Time.deltaTime);
			if (Vector2.Distance(transform.position, backPoint.position) == 0)
			{
				goBack = false;
				gameController.StudentLeft();
			}
		}
    }

	public void setSprite()
	{
		sprite = GetComponentInChildren<Image>();
		if (isGirl)
			sprite.sprite = girlSprite;
	}
	
}
