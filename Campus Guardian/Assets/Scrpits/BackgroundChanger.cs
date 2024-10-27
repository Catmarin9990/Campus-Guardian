using UnityEngine;
using UnityEngine.UI;

public class BackgroundChanger : MonoBehaviour
{
	private Image spirte;
	private bool isChacked = true;
	private void Start()
	{
		spirte = GetComponent<Image>();
	}
	public void changeBackground()
	{
		if (isChacked)
		{
			spirte.color = Color.gray;
			isChacked = !isChacked;
		}
		else
		{
			spirte.color = Color.white;
			isChacked = !isChacked;
		}
	}
}
