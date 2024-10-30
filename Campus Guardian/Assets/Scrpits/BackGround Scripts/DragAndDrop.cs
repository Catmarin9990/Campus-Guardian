using System;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
	[NonSerialized] public bool isDragging = false;

	private DragController dragController;

	[Header("Position Settings")]
	[NonSerialized] public Vector3 lastPosition;
	private Vector3? mouvementDestination;
	[SerializeField] private float movementTime = 15f;

	private void FixedUpdate()
	{
		if (mouvementDestination.HasValue)
		{
			if (isDragging)
			{
				mouvementDestination = null;
				return;
			}
			if (mouvementDestination == transform.position)
			{
				mouvementDestination = null;
				return;
			}
			else
			{
				transform.position = Vector3.Lerp(transform.position, mouvementDestination.Value, movementTime * Time.deltaTime);
			}
		}
	}

	public void setDestination()
	{
		mouvementDestination = lastPosition;
	}
}
