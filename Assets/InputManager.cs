using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
	private static InputManager _instance;

	public event MouseEventHandler GlobalDownAction = delegate {};
	public event MouseEventHandler GlobalUpAction = delegate {};
	
	public static InputManager Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = GameObject.FindObjectOfType<InputManager>();
			}
			return _instance;
		}

		private set { }
	}

	void Update ()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Vector2 worldPosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Collider2D collider = Physics2D.OverlapPoint(worldPosition);
			GlobalDownAction(worldPosition);

			if (collider != null)
			{
				Clickable clickable = collider.GetComponent<Clickable>();
				if (clickable != null)
				{
					clickable.FireEvent(MouseEventType.Down, worldPosition);
				}
			}
		}

		else if (Input.GetMouseButtonUp(0))
		{
			Vector2 worldPosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Collider2D collider = Physics2D.OverlapPoint(worldPosition);
			GlobalUpAction(worldPosition);
			
			if (collider != null)
			{
				Clickable clickable = collider.GetComponent<Clickable>();	
				if (clickable != null)
				{
					clickable.FireEvent(MouseEventType.Up, worldPosition);
				}
			}
		}
	}
}
