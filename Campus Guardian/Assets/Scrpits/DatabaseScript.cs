using UnityEngine;

public class DatabaseScript : MonoBehaviour
{
	private GameController gameController;

	private void Start()
	{
		gameController = FindObjectOfType<GameController>();
	}
}
