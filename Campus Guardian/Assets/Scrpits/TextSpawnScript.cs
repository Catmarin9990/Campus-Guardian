using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextSpawnScript : MonoBehaviour
{
	private TMP_Text textBox;
	private List<IEnumerator> sentenses = new List<IEnumerator>();

	private void Start()
	{
		textBox = GetComponentInChildren<TMP_Text>();
	}

	public void addQuation()
	{

	}

	public IEnumerator TypeSentånce(string sentence)
	{
		foreach (char letter in sentence.ToCharArray())
		{
			textBox.text += letter;
			yield return new WaitForSeconds(0.05f);
		}
	}
}
