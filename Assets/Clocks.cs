using UnityEngine;
using System.Collections;

public class Clocks : MonoBehaviour {
	
	public bool Stiched;
	public bool Dragging;
	public Clocks PartnerNode;
	public float LineWidthMatched;
	public float LineWidthDragging;
	public float LineWidthSmoothingFactor;
	public Color LineColorDefault;
	public Color LineColorBreak;
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
		_inputManager.GlobalUpAction += OnGlobalUpAction;
		_lineRenderer = transform.FindChild("line_renderer").GetComponent<LineRenderer>();
		_lineEnd = transform.FindChild("line_end");
	}
	
	void OnGlobalUpAction (Vector3 position)
	{
			Dragging = false;
			if (!_nodeManager.AllStiched ()) {

				_nodeManager.BreakAll ();
			}
	}

	
	void OnDownAction (Vector3 position)
	{
		if (!this.Stiched) {
			Dragging = true;
		}
	}
	
	void UpdateLine ()
	{
		_lineWidth = Mathf.Lerp(_lineWidth, _targetLineWidth, Time.deltaTime * LineWidthSmoothingFactor);
		_lineRenderer.SetWidth(_lineWidth, _lineWidth);
		
		_lineColor = Color.Lerp(_lineColor, LineColorDefault, Time.deltaTime * LineColorSmoothingFactor);
		_lineRenderer.SetColors(_lineColor, _lineColor);
		
		
		if (this.Stiched)
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
			if(clock != null && clock.Stiched == false)
			{
				this.PartnerNode = clock;
				this.Stiched = true;
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
		if (!Dragging && !Stiched)
			return;

		if(_hitInfo.Length >2){

			_nodeManager.BreakAll();
		
		}
		if (Dragging) {
			Vector2 lineStart = new Vector2(transform.position.x, transform.position.y);
			Vector2 lineEnd = new Vector2(_lineEnd.position.x, _lineEnd.position.y);
			Line drag = new Line(lineStart, lineEnd);
			_lineManager.ShouldLineBreak(drag);
		}
	}

	public void BreakLine()
	{

		_lineColor = LineColorBreak;
		_targetLineWidth = 0;
		PartnerNode = null;
		Stiched = false;
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





