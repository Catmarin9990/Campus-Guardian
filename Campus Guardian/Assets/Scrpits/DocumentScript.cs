using TMPro;
using UnityEngine;

public class DocumentScript : MonoBehaviour
{
	public string studName { get; set; }
	public string studSurname { get; set; }
	public byte course { get; set; }
	private SpriteRenderer sprite;
	private BoxCollider2D box;

	[SerializeField] private Sprite smallVersion;
	[SerializeField] private Sprite bigVersion;
	private bool isBig = false;

	private GameObject input;
	private TMP_Text[] texts;

	private Animator anim;
	private void Start()
	{
		sprite = GetComponent<SpriteRenderer>();
		box = GetComponent<BoxCollider2D>();
		input = GetComponentInChildren<Canvas>().gameObject;
		texts = input.GetComponentsInChildren<TMP_Text>();
		(texts[0].text, texts[1].text, texts[2].text) =
			(studName, studSurname, course.ToString());
		anim = GetComponent<Animator>();
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
	}

	private void OnTriggerStay2D(Collider2D coll)
	{
		if (coll.gameObject.name == "Table")
			isBig = true;
		else
			isBig = false;
	}
}
