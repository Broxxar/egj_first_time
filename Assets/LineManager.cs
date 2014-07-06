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
	List<Clocks> _pairs = new List<Clocks>();



	void Awake()
	{
		_inputManager = InputManager.Instance;
		_inputManager.GlobalDownAction += OnGlobalDownAction;
	}

	void OnGlobalDownAction (Vector3 position)
	{

		foreach(Clocks node in _pairs)
		{
			Line testLine = getNodesLine(node);

		}
	}

	
	public void AddPair(Clocks node)
	{
		if (!(_pairs.Contains (node) || _pairs.Contains (node.PartnerNode)))
		{
			_pairs.Add (node);
		}
	}
	
	
	public void RemovePair(Clocks node)
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

	public Line getNodesLine(Clocks node)
	{
		if (_pairs.Contains (node)) {
						Vector2 startPos;
						startPos.x = node.transform.position.x;
						startPos.y = node.transform.position.y;

						Vector2 endPos;
						endPos.x = node.PartnerNode.transform.position.x;
						endPos.y = node.PartnerNode.transform.position.y;

						return new Line (startPos, endPos);
				}
		return null;
	}

	public bool ShouldLineBreak(Line mainLine)
	{
		Clocks clock = NodeManager.Instance.MouseIntersectedAt (mainLine);
		List<Clocks> remove = new List<Clocks> ();
		foreach(Clocks node in _pairs){
			Line testLine = getNodesLine(node);

			if(linesCrossed(mainLine, testLine)){

				remove.Add(node);
			}
		}
		if(remove.Count >1 ){
			NodeManager.Instance.BreakAll(false);
			return true;
		}
		return false;
	}

}
