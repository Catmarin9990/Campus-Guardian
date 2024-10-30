using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextSpawnScript : MonoBehaviour
{
	private TMP_Text textBox;
	public List<IEnumerator> sentenses { get; set; } = new List<IEnumerator>();

	// Quations
	[SerializeField] private TMP_Text[] quations; 
	private string forgotCard;
	private string introduce;
	private bool isForgotActive = false;
	private bool isIntroduceActive = false;
	public bool canWrite { get; set; } = true;

	// Answers
	private string forgotToGive = "My bad, here it is\n";
	private string forgotInHome = "I’m sorry, I left it at home today. Is there any way I can still get in?\n";
	private string introduceAnswer;

	// Errors
	private string wrongOneIn = "You let the wrong one in\n";
	private string wasAlright = "Everything was alright\n";
	public string WrongOneIn => wrongOneIn;
	public string WasAlright => wasAlright;

	private void Start()
	{
		textBox = GetComponentInChildren<TMP_Text>();
		forgotCard = quations[0].text + '\n';
		introduce = quations[1].text + '\n';
	}
	
	public void forgotStudCard()
	{
		isForgotActive = !isForgotActive;
	}

	public void studIntroduce()
	{
		isIntroduceActive = !isIntroduceActive;
	}

	public void startType()
	{
		if (isForgotActive)
		{
			sentenses.Add(TypeSentånce(forgotCard));
			sentenses.Add(TypeSentånce(forgotInHome));
		}
		if (isIntroduceActive)
		{
			sentenses.Add(TypeSentånce(introduce));
			sentenses.Add(TypeSentånce(introduceAnswer));
		}
		if (canWrite)
			StartCoroutine(MainCoroutine());
	}

	public IEnumerator TypeSentånce(string sentence)
	{
		foreach (char letter in sentence.ToCharArray())
		{
			textBox.text += letter;
			yield return new WaitForSeconds(0.05f);
		}
	}

	public void setIntroductionAnswer(string studName, string studSurname, byte course)
	{
		introduceAnswer = $"{studName} {studSurname}, {course}";
	}

	public IEnumerator MainCoroutine()
	{
		canWrite = false;
		textBox.text = "";
		foreach (IEnumerator sentence in sentenses)
		{
			if (sentence != null)
				yield return StartCoroutine(sentence);
		}
		sentenses.Clear();
		canWrite = true;
	}
}
