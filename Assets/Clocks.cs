using UnityEngine;
using System.Collections;

public class Clocks : MonoBehaviour {
	
	public bool Stitched;
	public bool Dragging;
	public Clocks PartnerNode;
	public float LineWidthMatched;
	public float LineWidthDragging;
	public float LineWidthSmoothingFactor;
	public Color LineColorDefault;
	public Color LineColorBreak;
	public Color LineColorWin;
	public float LineColorSmoothingFactor;
	public float LineZDepth;
	
	InputManager _inputManager;
	NodeManager _nodeManager;
	LineManager _lineManager;
	LineRenderer _lineRenderer;
	RaycastHit2D[] _hitInfo;
	Transform _lineEnd;
	float _lineWidth;
	float _targetLineWidth;
	Color _lineColor;
	
	void Awake ()
	{
		_inputManager = InputManager.Instance;
		_lineManager = LineManager.Instance;
		_nodeManager = NodeManager.Instance;
		
		_nodeManager.AddNode (this);
		GetComponent<Clickable>().DownAction += OnDownAction;
		_lineRenderer = transform.FindChild("line_renderer").GetComponent<LineRenderer>();
		_lineEnd = transform.FindChild("line_end");
	}
	
	void OnDownAction (Vector3 position)
	{
		if (!this.Stitched) {
			Dragging = true;
		}
	}
	
	void UpdateLine ()
	{
		_lineWidth = Mathf.Lerp(_lineWidth, _targetLineWidth, Time.deltaTime * LineWidthSmoothingFactor);
		_lineRenderer.SetWidth(_lineWidth, _lineWidth);
		
		_lineColor = Color.Lerp(_lineColor, LineColorDefault, Time.deltaTime * LineColorSmoothingFactor);
		_lineRenderer.SetColors(_lineColor, _lineColor);
		
		
		if (this.Stitched)
		{

			_lineEnd.position = PartnerNode.transform.position;
			_targetLineWidth = LineWidthMatched;
		}
		_lineRenderer.SetPosition(0, new Vector3(transform.position.x, transform.position.y, transform.position.z + LineZDepth));
		_lineRenderer.SetPosition(1, new Vector3(_lineEnd.position.x, _lineEnd.position.y, _lineEnd.position.z + LineZDepth));
		_hitInfo = Physics2D.LinecastAll(transform.position, _lineEnd.position);

	}
//	
	void UpdateDragging ()
	{
		if (Dragging)
		{
			Clocks clock = _nodeManager.DifferentClockHit(this);
			if(clock != null && clock.Stitched == false)
			{
				this.PartnerNode = clock;
				this.Stitched = true;
				this.PartnerNode.Dragging = true;
				Dragging = false;
				_lineManager.AddPair(this);

				
			}else{
			//check if you hit a node make that your partner and add line to line manager
				_lineEnd.position = _inputManager.MouseWorldPosition;
				_targetLineWidth = LineWidthDragging;
			}
		}

	}

	void CheckLineIntact ()
	{
		if (!Dragging && !Stitched)
			return;

		if(_hitInfo.Length >2){

			_nodeManager.BreakAll(false);
		
		}
		if (Dragging) {
			Vector2 lineStart = new Vector2(transform.position.x, transform.position.y);
			Vector2 lineEnd = new Vector2(_lineEnd.position.x, _lineEnd.position.y);
			Line drag = new Line(lineStart, lineEnd);
			_lineManager.ShouldLineBreak(drag);
		}
	}

	public void BreakLine(bool win)
	{
		if (!win)
			_lineColor = LineColorBreak;
		else
			_lineColor = LineColorWin;

		_targetLineWidth = 0;
		PartnerNode = null;
		Stitched = false;
		Dragging = false;
		_hitInfo = null;
		_lineManager.RemovePair (this);
	}

	void Update ()
	{
		UpdateDragging();
		UpdateLine();
		CheckLineIntact ();
	}
}





