﻿using UnityEngine;
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
		_lineRenderer.SetPosition(0, transform.position);
		_lineRenderer.SetPosition(1, _lineEnd.position);
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

		foreach( RaycastHit2D cldObject in _hitInfo)
		{
			if(cldObject.collider != this.transform.collider2D &&
			   cldObject.collider != PartnerNode.transform.collider2D)
			{
				Matched = false;
				_hitInfo = null;
				_dragging = false;
				_lineColor = LineColorBreak;
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





