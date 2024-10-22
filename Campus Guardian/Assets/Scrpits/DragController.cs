using UnityEngine;

public class DragController : MonoBehaviour
{
	public DragAndDrop LastDragged => lastDragged;

	// Postition Settings
	private Vector3 screenPosition;
	private Vector3 worldPosition;
	private Vector3 mousPosOffset;

	private DragAndDrop lastDragged;
	private GameController gameController;
	private bool isDragActive = false;

	private void Start()
	{
		gameController = FindObjectOfType<GameController>();
	}

	private void Awake()
	{
		DragController[] controllers = FindObjectsOfType<DragController>();
		if (controllers.Length > 1)
		{
			Destroy(gameObject);
		}
	}

	private void Update()
	{
		if (isDragActive && (Input.GetMouseButtonUp(0) || (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)))
		{
			Drop();
			return;
		}

		if (Input.GetMouseButton(0))
		{
			Vector3 mousePos = Input.mousePosition;
			screenPosition = new Vector2(mousePos.x, mousePos.y);
		}
		else if (Input.touchCount > 0)
		{
			screenPosition = Input.GetTouch(0).position;
		}
		else return;


		worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

		if (isDragActive)
		{
			Drag();
		}
		else
		{
			RaycastHit2D[] hits = Physics2D.RaycastAll(worldPosition, Vector2.zero);
			foreach (RaycastHit2D hit in hits)
			{
				if (hit.collider != null)
				{
					DragAndDrop drag = hit.transform.gameObject.GetComponent<DragAndDrop>();
					if (drag != null)
					{
						lastDragged = drag;
						InItDrag();
						break;
					}
				}
			}
		}
	}
	private void InItDrag()
	{
		mousPosOffset = lastDragged.transform.position - worldPosition;
		UpdateDragStatus(true);
	}
	private void Drag()
	{
		lastDragged.transform.position = new Vector3(worldPosition.x, worldPosition.y) + mousPosOffset;
	}
	private void Drop()
	{
		UpdateDragStatus(false);
	}
	private void UpdateDragStatus(bool isDragging)
	{
		isDragActive = lastDragged.isDragging = isDragging;
	}
}
