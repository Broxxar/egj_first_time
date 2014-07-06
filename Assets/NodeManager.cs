using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NodeManager : MonoBehaviour {

	private static NodeManager _instance;
	
	public static NodeManager Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = GameObject.FindObjectOfType<NodeManager>();
			}
			return _instance;
		}
		
		private set { }
	}

	InputManager _inputManager;
	List<Clocks> _clocks = new List<Clocks>();
	public Vector2 MouseWorldPosition {get; private set;}

	void Awake ()
	{
		_inputManager = InputManager.Instance;

		_inputManager.GlobalUpAction += OnGlobalUpAction;
	}

	public void AddNode(Clocks node)
	{
		if (!(_clocks.Contains (node) || _clocks.Contains (node.PartnerNode)))
		{
			_clocks.Add (node);
		}
	}
	
	void OnGlobalUpAction (Vector3 position)
	{

		if (AllStiched ())
		{
			BreakAll ();
			print ("winner");
		}
	}
	
	public void RemoveNode(Clocks node)
	{
		if(_clocks.Contains(node))
		{
			_clocks.Remove(node);
		}else if(_clocks.Contains(node.PartnerNode))
		{
			_clocks.Remove (node.PartnerNode);
		}	
	}

	public Clocks DifferentClockHit(Clocks current)
	{
		MouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Collider2D collider = Physics2D.OverlapPoint(MouseWorldPosition);
		if (collider !=null)
		if(Input.GetMouseButton(0))
		{
			foreach (Clocks node in _clocks) 
			{	
				if(node != current && collider.Equals(node.collider2D))
				{
					return node;
				}
			}
		}
		return null;
	}

	public Clocks MouseIntersectedAt( Line line)
	{	
		foreach (Clocks clock in _clocks)
		{
			Vector2 circlePos = clock.transform.position;
			Vector2 point1 = line.StartPos;
			Vector2 point2 = line.EndPos;
			float dx, dy, A, B, C, det, t;
			float radius = .3f;

			dx = point2.x - point1.x;
			dy = point2.y - point1.y;

			A = dx * dx + dy * dy;
			B = 2 * (dx * (point1.x - circlePos.x) + dy * (point1.y - circlePos.y));
			C = (point1.x - circlePos.x) * (point1.x - circlePos.x) + (point1.y - circlePos.y) * (point1.y - circlePos.y) - radius * radius;

			det = B * B - 4 * A * C;
			if ((A <= 0.0000001) || (det < 0)) {
					return clock;
			} else if (det == 0) {
					return clock;
			} else {
					return clock;
			
			}
		}
		return null;
	}

	public bool AllStiched()
	{
		int count = 0;
		foreach (Clocks node in _clocks) 
		{	
			if(node.Stiched == false)
			{
				count++;
			}
		}

		if(count >1)
		{
			return false;
		}
		return true;
	}

	public void BreakAll()
	{
		foreach (Clocks node in _clocks) 
		{	
			node.BreakLine();
		}
	}
}