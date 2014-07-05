using UnityEngine;
using System.Collections;

public class Node : MonoBehaviour
{
	InputManager _inputManager;
	bool _dragging;
	LineRenderer _lineRenderer;
	RaycastHit2D[] _hitInfo;
	Transform _lineEnd;
	bool _matched;
	public bool Matched
	{
		get{ return _matched;}
		set{_matched = value;}
	}
		
	public Node PartnerNode; 

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
		if (_dragging )
		{
			if (Physics2D.OverlapPoint (position) == (PartnerNode.collider2D))
			{

				_lineEnd.position = PartnerNode.transform.position;
				this.Matched = true;
				print (_matched);
				PartnerNode.Matched = true;
			}
			_dragging = false;
			//TODO: Check to see if we released on the correct node
		}
	}

	void OnDownAction (Vector3 position)
	{
		if (!this.Matched) {
			_dragging = true;
		}
	}


	
	void UpdateLine ()
	{
		//TODO: Fix z depths, so that these lines don't appear over top of stuff


		if (this.Matched)
		{
			_lineEnd.position = PartnerNode.transform.position;
		}
		_lineRenderer.SetPosition(0, transform.position);
		_lineRenderer.SetPosition(1, _lineEnd.position);
		_hitInfo = Physics2D.LinecastAll(transform.position, _lineEnd.position);

	}
	
	void UpdateDragging ()
	{
		if (_dragging)
		{
			//TODO: 2D Linecast to make sure we haven't hit anything with the line
			_lineEnd.position = _inputManager.MouseWorldPosition;
		}
		else if(!_matched)
		{
			//TODO: Replace the 10 here with a smoothingfactor variable, or just make lines disappear
			// ooh ooh or make them thin out into nothingness. yeah let's do that maybe!
			_lineEnd.position = Vector3.Lerp(_lineEnd.position, transform.position, Time.deltaTime * 10);
		}
	}

	void CheckLineIntact ()
	{
		foreach( RaycastHit2D cldObject in _hitInfo)
		{
			if(cldObject.collider != this.transform.collider2D &&
			   cldObject.collider != PartnerNode.transform.collider2D)
			{
				_lineEnd.position = this.transform.position;
				this.Matched = false;
				_hitInfo = null;
			}
		}
	}

	void Update ()
	{
		UpdateDragging();
		UpdateLine();
		CheckLineIntact ();
	}
}





