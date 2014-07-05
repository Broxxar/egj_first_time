using UnityEngine;
using System.Collections;

public class Node : MonoBehaviour
{
	public bool Matched;
	public Node PartnerNode; 
	public float LineWidthMatched;
	public float LineWidthDragging;
	public float LineWidthSmoothingFactor;
	public Color LineColorDefault;
	public Color LineColorBreak;
	public float LineColorSmoothingFactor;
	public float LineZDepth;

	InputManager _inputManager;
	bool _dragging;
	LineRenderer _lineRenderer;
	RaycastHit2D[] _hitInfo;
	Transform _lineEnd;
	float _lineWidth;
	float _targetLineWidth;
	Color _lineColor;
		
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
				LineManager.Instance.AddPair(this);
				PartnerNode.Matched = true;
			}
			_dragging = false;
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
		_lineWidth = Mathf.Lerp(_lineWidth, _targetLineWidth, Time.deltaTime * LineWidthSmoothingFactor);
		_lineRenderer.SetWidth(_lineWidth, _lineWidth);

		_lineColor = Color.Lerp(_lineColor, LineColorDefault, Time.deltaTime * LineColorSmoothingFactor);
		_lineRenderer.SetColors(_lineColor, _lineColor);

		if (this.Matched)
		{
			_lineEnd.position = PartnerNode.transform.position;
			_targetLineWidth = LineWidthMatched;
		}
		_lineRenderer.SetPosition(0, new Vector3(transform.position.x, transform.position.y, transform.position.z + LineZDepth));
		_lineRenderer.SetPosition(1, new Vector3(_lineEnd.position.x, _lineEnd.position.y, _lineEnd.position.z + LineZDepth));
		_hitInfo = Physics2D.LinecastAll(transform.position, _lineEnd.position);

	}
	
	void UpdateDragging ()
	{
		if (_dragging)
		{
			_lineEnd.position = _inputManager.MouseWorldPosition;
			_targetLineWidth = LineWidthDragging;
		}
		else if(!Matched)
		{
			_targetLineWidth = 0;
		}
	}

	void CheckLineIntact ()
	{
		if (!_dragging && !Matched)
			return;

		//if (Matched) 
		{
			foreach (RaycastHit2D cldObject in _hitInfo) 
			{
					if (cldObject.collider != this.transform.collider2D &&
							cldObject.collider != PartnerNode.transform.collider2D) {
							Matched = false;
							_hitInfo = null;
							_dragging = false;
							_lineColor = LineColorBreak;
					}
			}
		} 
		if (_dragging) {
			Vector2 lineStart = new Vector2(transform.position.x, transform.position.y);
			Vector2 lineEnd = new Vector2(_lineEnd.position.x, _lineEnd.position.y);
			Line drag = new Line(lineStart, lineEnd);
			LineManager.Instance.ShouldLineBreak(drag);
		}
	}

	public void BreakLine()
	{
		
		_lineColor = LineColorBreak;
		PartnerNode._lineColor = LineColorBreak;
		Matched = false;
		PartnerNode.Matched = false;
		_dragging = false;
	}

	void Update ()
	{
		UpdateDragging();
		UpdateLine();
		CheckLineIntact ();
	}
}





