using UnityEngine;
using System.Collections;

public class Node : MonoBehaviour
{
	InputManager _inputManager;
	bool _dragging;
	LineRenderer _lineRenderer;
	Transform _lineEnd;

	void Awake ()
	{
		_inputManager = InputManager.Instance;

		GetComponent<Clickable>().DownAction += OnDownAction;
		_inputManager.GlobalUpAction += OnGlobalUpAction;
		_lineRenderer = transform.FindChild("line_renderer").GetComponent<LineRenderer>();
		_lineEnd = transform.FindChild("line_end");
	}

	void OnGlobalUpAction (Vector3 position)
	{
		if (_dragging)
		{
			_dragging = false;
			//TODO: Check to see if we released on the correct node
		}
	}

	void OnDownAction (Vector3 position)
	{
		_dragging = true;
	}
	
	void UpdateLineRenderer ()
	{
		//TODO: Fix z depths, so that these lines don't appear over top of stuff
		_lineRenderer.SetPosition(0, transform.position);
		_lineRenderer.SetPosition(1, _lineEnd.position);
	}
	
	void UpdateDragging ()
	{
		if (_dragging)
		{
			//TODO: 2D Linecast to make sure we haven't hit anything with the line
			_lineEnd.position = _inputManager.MouseWorldPosition;
		}
		else
		{
			//TODO: Replace the 10 here with a smoothingfactor variable, or just make lines disappear
			// ooh ooh or make them thin out into nothingness. yeah let's do that maybe!
			_lineEnd.position = Vector3.Lerp(_lineEnd.position, transform.position, Time.deltaTime * 10);
		}
	}

	void Update ()
	{
		UpdateDragging();
		UpdateLineRenderer();
	}
}





