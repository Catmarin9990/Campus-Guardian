using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class DatabaseScript : MonoBehaviour
{
	private GameController gameController;

	private void Start()
	{
		gameController = FindObjectOfType<GameController>();
	}

	public void setDatabase(int course)
	{
		int index;
		switch (course) 
		{
			case 1:
				index = gameController.Students.FindIndex(s => s.course == 1);
				while (gameController.Students[index].course == 1)
				{
					index++;
				}
				break;
			case 2:
				index = gameController.Students.FindIndex(s => s.course == 2);
				break;
			case 3:
				index = gameController.Students.FindIndex(s => s.course == 3);
				break;
			case 4:
				index = gameController.Students.FindIndex(s => s.course == 4);
				break;
		}
	}
}
