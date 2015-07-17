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

	public Vector2 MouseWorldPosition {get; private set;}

	void Update ()
	{
#if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR
		MouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		if (Input.GetMouseButtonDown(0))
		{
			Collider2D collider = Physics2D.OverlapPoint(MouseWorldPosition);
			GlobalDownAction(MouseWorldPosition);

			if (collider != null)
			{
				Clickable clickable = collider.GetComponent<Clickable>();
				if (clickable != null)
				{
					clickable.FireEvent(MouseEventType.Down, MouseWorldPosition);
				}
			}
		}

		else if (Input.GetMouseButtonUp(0))
		{
			Collider2D collider = Physics2D.OverlapPoint(MouseWorldPosition);
			GlobalUpAction(MouseWorldPosition);
			
			if (collider != null)
			{
				Clickable clickable = collider.GetComponent<Clickable>();	
				if (clickable != null)
				{
					clickable.FireEvent(MouseEventType.Up, MouseWorldPosition);
				}
			}
		}
#else
		if(Input.touchCount > 0){
			Touch myTouch = Input.touches[0];
			MouseWorldPosition = Camera.main.ScreenToWorldPoint(myTouch.position);
			if(myTouch.phase == TouchPhase.Began){
				Collider2D collider = Physics2D.OverlapPoint(MouseWorldPosition);
				GlobalDownAction(MouseWorldPosition);
				
				if (collider != null)
				{
					Clickable clickable = collider.GetComponent<Clickable>();
					if (clickable != null)
					{
						clickable.FireEvent(MouseEventType.Down, MouseWorldPosition);
					}
				}

			}
			else if(myTouch.phase == TouchPhase.Ended){
				Collider2D collider = Physics2D.OverlapPoint(myTouch.position);
				GlobalUpAction(MouseWorldPosition);
				
				if (collider != null)
				{
					Clickable clickable = collider.GetComponent<Clickable>();	
					if (clickable != null)
					{
						clickable.FireEvent(MouseEventType.Up, MouseWorldPosition);
					}
				}
			}
		}else{
			MouseWorldPosition = -Vector2.one;
		}
#endif
	}
}
