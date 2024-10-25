using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class DatabaseScript : MonoBehaviour
{
	private GameController gameController;
	[SerializeField] private Transform inputSpawnpoint;
	[SerializeField] GameObject inputPrefab;
	private List<GameObject> inputs = new List<GameObject>();
	private List<TMP_Text> textBoxes = new List<TMP_Text>();

	public enum order
	{
		first = 1,
		second, 
		third, 
		fourth,
	}

	private Animator animator;

	private void Start()
	{
		gameController = FindObjectOfType<GameController>();
		animator = GetComponent<Animator>();
	}

	public void setDatabase(int course)
	{
		int index;

		switch ((order)course) 
		{
			case order.first:
				index = gameController.Students.FindIndex(s => s.course == (int)order.first);
				setInputs(index, gameController.Students, order.first);
				break;
			case order.second:
				index = gameController.Students.FindIndex(s => s.course == (int)order.second);
				setInputs(index, gameController.Students, order.second);
				break;
			case order.third:
				index = gameController.Students.FindIndex(s => s.course == (int)order.third);
				setInputs(index, gameController.Students, order.third);
				break;
			case order.fourth:
				index = gameController.Students.FindIndex(s => s.course == (int)order.fourth);
				setInputs(index, gameController.Students, order.fourth);
				break;
		}
	}
	private void setInputs(int index, List<StudentClass> students, order order)
	{
		if (index == -1) return;
		while (index < students.Count - 1 && students[index].course == (int)order)
		{
			inputs.Add(Instantiate(inputPrefab, inputSpawnpoint));
			textBoxes = inputs[inputs.Count - 1].GetComponentsInChildren<TMP_Text>().ToList();
			(textBoxes[0].text, textBoxes[1].text, textBoxes[2].text) =
				(students[index].studName,
				students[index].studSurname,
				students[index].course.ToString());
			index++;
		}
	}

	public void deleteInputs()
	{
		inputs.ForEach(t => Destroy(t));
		inputs.Clear();
	}

	public void fadeIn()
	{
		animator.SetBool("fadeIn", true);
		animator.SetBool("fadeOut", false);
	}
	public void fadeOut()
	{
		animator.SetBool("fadeIn", false);
		animator.SetBool("fadeOut", true);
	}
}
