using UnityEngine;
using UnityEngine.UI;

public class BackgroundChanger : MonoBehaviour
{
	private Image spirte;
	private bool isChacked = false;
	private void Start()
	{
		spirte = GetComponent<Image>();
	}
	public void changeBackground()
	{
		isChacked = !isChacked;
		if (isChacked)
		{
			spirte.color = Color.gray;
		}
		else
		{
			spirte.color = Color.white;
		}
	}
}
