using UnityEngine;
using UnityEngine.UI;

public class StudentScript : MonoBehaviour
{
	// Parametars Settings
	public string studName { get; set; }
	public string studSurname { get; set; }
	public byte course { get; set; }
	public bool isGirl { get; set; } = true;

	public bool isGetDoc { get; set; } = false;
 
	[SerializeField] private GameObject docPrefab;
	private GameController gameController;
	[SerializeField] private Animator animator;

	private void Awake()
	{
		gameController = FindAnyObjectByType<GameController>();
	}
	private void Start()
	{
		animator.SetBool("fadeIn", true);
	}

	public void leave(bool isIn)
	{
		if (isGetDoc) 
		{
			if (isIn)
				animator.SetBool("fadeOut", true);
			else
				animator.SetBool("goBack", true);
			isGetDoc = false;
		}
		
	}
	
	public void deleteStudent()
	{
		gameController.StudentLeft();
		animator.SetBool("goBack", false);
		animator.SetBool("fadeOut", false);
		animator.SetBool("fadeIn", true);
	}

	public void spawnDocument()
	{
		gameController.docSpawn();
	}
}
