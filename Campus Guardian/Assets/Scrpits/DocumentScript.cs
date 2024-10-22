using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DocumentScript : MonoBehaviour
{
	// Document Settings
	public string studName { get; set; }
	public string studSurname { get; set; }
	public byte course { get; set; }
	private SpriteRenderer sprite;
	public Transform endPoint { get; set; }

	[Header("Sprite Settings")]
	[SerializeField] private Sprite smallVersion;
	[SerializeField] private Sprite bigVersion;
	private bool isBig = false;

	// Text Box Settings
	private GameObject input;
	private TMP_Text[] texts;

	private bool arrived = false;
	private float speed = 4f;
	private DragAndDrop drag;

	private void Start()
	{
		sprite = GetComponent<SpriteRenderer>();
		input = GetComponentInChildren<Canvas>().gameObject;
		texts = input.GetComponentsInChildren<TMP_Text>();
		drag = GetComponent<DragAndDrop>();

		(texts[0].text, texts[1].text, texts[2].text) =
			(studName, studSurname, course.ToString());
	}

	private void switchDoc()
	{
		if (isBig)
		{
			sprite.sprite = bigVersion;
			input.SetActive(true);
		}
		else if (!isBig)
		{
			sprite.sprite = smallVersion;
			input.SetActive(false);
		}
	}

	private void Update()
	{
		switchDoc();
		if (!arrived)
		{
			transform.position = Vector2.MoveTowards(transform.position, endPoint.position, speed * Time.deltaTime);
			if (Vector2.Distance(transform.position, endPoint.position) == 0)
				arrived = true;
			if (drag.isDragging)
			{
				arrived = true;
			}
		}
	}

	private void OnTriggerStay2D(Collider2D coll)
	{
		if (coll.gameObject.name == "Table")
			isBig = true;
		else
			isBig = false;
	}
}
