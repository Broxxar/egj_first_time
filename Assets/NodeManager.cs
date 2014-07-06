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

	List<Clocks> _clocks = new List<Clocks>();
	public Vector2 MouseWorldPosition {get; private set;}
	
	public void AddNode(Clocks node)
	{
		if (!(_clocks.Contains (node) || _clocks.Contains (node.PartnerNode)))
		{
			_clocks.Add (node);
		}
	}

	Vector3 mouseIntersectedAt(Vector3 mousePos, Line line)
	{	
		Vector2 point1 = line.StartPos;
		Vector2 point2 = line.EndPos;
		float dx, dy, A, B, C, det, t;
		float radius = .3f;
		
		dx = point2.x - point1.x;
		dy = point2.y - point1.y;
		
		A = dx * dx + dy * dy;
		B = 2 * (dx * (point1.x - mousePos.x) + dy * (point1.y - mousePos.y));
		C = (point1.x - mousePos.x) * (point1.x - mousePos.x) + (point1.y - mousePos.y) * (point1.y - mousePos.y) - radius * radius;
		
		det = B * B - 4 * A * C;
		if ((A <= 0.0000001) || (det < 0))
		{
			return Vector3.zero;
		}
		else if (det == 0)
		{
			// One solution.
			t = -B / (2 * A);
			return new Vector3(point1.x + t * dx, point1.y + t * dy, 0);
			
		}
		else
		{
			// Two solutions.
			t = (float)((-B + System.Math.Sqrt(det)) / (2 * A));
			return new Vector3(point1.x + t * dx, point1.y + t * dy,0);
			
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
		foreach (Clocks clock in _clocks) {
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

	public bool AllStiched(){
		foreach (Clocks node in _clocks) 
		{	
			
			if(node.Stiched == false){
				return false;
			}
		}
		return true;
	}
	public void BreakAll(){
		foreach (Clocks node in _clocks) 
		{	

				node.BreakLine();

		}
	}


}
