using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class LineManager : MonoBehaviour {

	private static LineManager _instance;

	
	public static LineManager Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = GameObject.FindObjectOfType<LineManager>();
			}
			return _instance;
		}
		
		private set { }
	}
	InputManager _inputManager;
	List<Node> _pairs = new List<Node>();



	void Awake()
	{
		_inputManager = InputManager.Instance;
		_inputManager.GlobalDownAction += OnGlobalDownAction;
	}

	void OnGlobalDownAction (Vector3 position)
	{

		foreach(Node node in _pairs)
		{
			Line testLine = getNodesLine(node);
			Vector3 spot = mouseIntersectedAt (position, testLine);

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
			t = (float)((-B + Math.Sqrt(det)) / (2 * A));
			return new Vector3(point1.x + t * dx, point1.y + t * dy,0);

		}
	}
	
	public void AddPair(Node node)
	{
		if (!(_pairs.Contains (node) || _pairs.Contains (node.PartnerNode)))
		{
			_pairs.Add (node);
		}
	}
	
	
	public void RemovePair(Node node)
	{
		if(_pairs.Contains(node))
		{
			_pairs.Remove(node);
		}else if(_pairs.Contains(node.PartnerNode))
		{
			_pairs.Remove (node.PartnerNode);
		}	
	}

	bool linesCrossed(Line line1, Line line2 )
	{

		float s1_x, s1_y, s2_x, s2_y;
		s1_x = line1.EndPos.x - line1.StartPos.x;  
		s1_y = line1.EndPos.y - line1.StartPos.y;  
		s2_x = line2.EndPos.x - line2.StartPos.x;    
		s2_y = line2.EndPos.y - line2.StartPos.y;  
		float s, t;
		s = (-s1_y * (line1.StartPos.x - line2.StartPos.x) + s1_x * (line1.StartPos.y - line2.StartPos.y)) / (-s2_x * s1_y + s1_x * s2_y);
		t = ( s2_x * (line1.StartPos.y - line2.StartPos.y) - s2_y * (line1.StartPos.x - line2.StartPos.x)) / (-s2_x * s1_y + s1_x * s2_y);
		
		if (s >= 0 && s <= 1 && t >= 0 && t <= 1)
		{

			return true;
		}

		return false; 
	}

	Line getNodesLine(Node node)
	{
		Vector2 startPos;
		startPos.x = node.transform.position.x;
		startPos.y = node.transform.position.y;

		Vector2 endPos;
		endPos.x = node.PartnerNode.transform.position.x;
		endPos.y = node.PartnerNode.transform.position.y;

		return new Line (startPos, endPos);
	}

	public bool ShouldLineBreak(Line mainLine)
	{

		bool shouldBreak = false; 
		List<Node> remove = new List<Node> ();
		foreach(Node node in _pairs){
			Line testLine = getNodesLine(node);
			if(linesCrossed(mainLine, testLine)){
				node.BreakLine();
				shouldBreak = true;
				remove.Add(node);

			}
		}
		foreach(Node node in remove){
			RemovePair(node);

		}
		return shouldBreak;
	}

}
